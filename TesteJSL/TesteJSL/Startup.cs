using RestSharp;
using System.Text;
using TesteJSL.Services;
using Microsoft.OpenApi.Models;
using TesteJSL.Domain.Interfaces;
using TesteJSL.Application.Models;
using TesteJSL.Infra.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Filters;
using TesteJSL.Application.AutoMapper;
using TesteJSL.Infra.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace TesteJSL
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        readonly string AllowSpecificOrigins = "_AllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSingleton<IConfiguration>(Configuration);

            ConfigureCors(services);

            ConfigureContext(services);

            ConfigureSwagger(services);

            ConfigureTokenJWT(services);

            ConfigureIoC(services);

            services.AddAutoMapper();

            ConfigureNewtonsoftJson(services);
        }

        private void ConfigureCors(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(AllowSpecificOrigins,
                builder =>
                {
                    builder.
                        AllowAnyOrigin().
                        AllowAnyHeader().
                        AllowAnyMethod();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();

            app.UseRouting();            

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseCors(AllowSpecificOrigins);

            //Verifica o ambiente
            if (env.IsDevelopment() || env.IsEnvironment("Local"))
            {
                app.UseDeveloperExceptionPage();

                //Swagger
                app.UseSwagger().UseSwaggerUI(sw =>
                {
                    sw.SwaggerEndpoint("./swagger/v1/swagger.json", "TesteJSL.Application");
                    sw.RoutePrefix = string.Empty;
                });
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigureContext(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("TesteJSL");

            services.AddDbContext<MotoristaContext>(option => option.UseLazyLoadingProxies()
                                                                          .UseSqlServer(connectionString, mig => mig.MigrationsAssembly("TesteJSL.Infra.Data")));
        }

        private void ConfigureSwagger(IServiceCollection services)
        {
            var swaggerInfo = new OpenApiInfo
            {
                Title = "Teste JSL",
                Version = "v1",
                Description = "API Rest destinada ao teste de conhecimento técnico (.net Core) JSL"                
            };

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", swaggerInfo);
                c.AddSecurityDefinition("oauth2", new ApiKeyScheme
                {
                    Description = "Cabeçalho de autorização padrão usando o esquema Bearer. Exemple: \"bearer {token}\"",
                    In = "header",
                    Name = "Authorization",
                    Type = "apiKey"
                });

                c.OperationFilter<SecurityRequirementsOperationFilter>();
            });
        }

        private void ConfigureIoC(IServiceCollection services)
        {
            #region Services

            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IVeiculoService, VeiculoService>();
            services.AddScoped<IMotoristaService, MotoristaService>();
            services.AddScoped<IAutenticacaoService, AutenticacaoService>();

            #endregion

            #region Repositories

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IVeiculoRepository, VeiculoRepository>();
            services.AddScoped<IMotoristaRepository, MotoristaRepository>();

            #endregion
        }

        private void ConfigureNewtonsoftJson(IServiceCollection services)
        {
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
        }

        private void ConfigureTokenJWT(IServiceCollection services)
        {
            var appSettingsSection = Configuration.GetSection("AppSettings");

            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.JwtSecret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }
    }
}
