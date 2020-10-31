using AutoMapper;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace TesteJSL.Application.AutoMapper
{
    public static class AutoMapperConfig
    {
        public static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddSingleton(config =>
                new MapperConfiguration(mc => mc.AddProfiles(new List<Profile> {
                    new DomainToModelMappingProfile(),
                    new ModelToDomainMappingProfile()})).CreateMapper());
        }
    }
}
