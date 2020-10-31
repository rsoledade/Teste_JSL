using System.Threading.Tasks;
using TesteJSL.Domain.Entities;

namespace TesteJSL.Domain.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuario> BuscaUsuario(Usuario usuario);
    }
}
