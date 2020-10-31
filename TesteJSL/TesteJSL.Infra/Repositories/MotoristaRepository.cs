using TesteJSL.Domain.Entities;
using TesteJSL.Domain.Interfaces;
using TesteJSL.Infra.Data.Context;

namespace TesteJSL.Infra.Data.Repositories
{
    public class MotoristaRepository : RepositoryBase<Motorista>, IMotoristaRepository
    {
        private readonly MotoristaContext _context;

        public MotoristaRepository(MotoristaContext context) : base(context)
        {
            _context = context;
        }
    }
}
