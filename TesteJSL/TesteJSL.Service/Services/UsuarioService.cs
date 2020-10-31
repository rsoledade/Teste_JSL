using System.Linq;
using System.Threading.Tasks;
using TesteJSL.Domain.Entities;
using TesteJSL.Domain.Interfaces;
using TesteJSL.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace TesteJSL.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly MotoristaContext _motoristaContext;
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository, MotoristaContext motoristaContext)
        {
            _motoristaContext = motoristaContext;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Usuario> BuscaUsuario(Usuario usuario)
        {
            return await _motoristaContext.Usuario.Where(x => 
                x.Login.ToLower() == usuario.Login.ToLower() && x.Senha.ToLower() == usuario.Senha.ToLower()).FirstOrDefaultAsync();
        }
    }
}
