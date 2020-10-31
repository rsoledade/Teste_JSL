using System.Threading.Tasks;
using TesteJSL.Domain.Entities;
using System.Collections.Generic;

namespace TesteJSL.Domain.Interfaces
{
    public interface IMotoristaService
    {
        Task<IEnumerable<Motorista>> BuscaListaMotoristas();

        Task<Motorista> BuscaMotoristaPorId(int id);

        Task<Motorista> AdicionaMotorista(Motorista motorista);

        Task AtualizaMotorista(Motorista motorista);

        Task ExcluiMotorista(int motoristaId);
    }
}
