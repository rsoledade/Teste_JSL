using TesteJSL.Domain.Entities;
using TesteJSL.Domain.Interfaces;
using TesteJSL.Infra.Data.Context;

namespace TesteJSL.Infra.Data.Repositories
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        private readonly MotoristaContext _context;

        public UsuarioRepository(MotoristaContext context) : base(context)
        {
            _context = context;
        }
    }
}
