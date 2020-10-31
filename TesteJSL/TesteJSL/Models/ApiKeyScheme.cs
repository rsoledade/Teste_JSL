using Microsoft.OpenApi.Models;

namespace TesteJSL
{
    internal class ApiKeyScheme : OpenApiSecurityScheme
    {
        public string Description { get; set; }
        public string In { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}