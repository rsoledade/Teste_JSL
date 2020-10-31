using TesteJSL.Domain.Entities;
using TesteJSL.Domain.Interfaces;
using TesteJSL.Infra.Data.Context;

namespace TesteJSL.Infra.Data.Repositories
{
    public class VeiculoRepository : RepositoryBase<Veiculo>, IVeiculoRepository
    {
        private readonly MotoristaContext _context;

        public VeiculoRepository(MotoristaContext context) : base(context)
        {
            _context = context;
        }
    }
}
