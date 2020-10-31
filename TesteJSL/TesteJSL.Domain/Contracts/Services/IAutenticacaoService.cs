using System.Threading.Tasks;
using TesteJSL.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace TesteJSL.Domain.Interfaces
{
    public interface IAutenticacaoService
    {
        Task<string> GerarToken(Usuario usuario, IConfiguration configuration);
    }
}
