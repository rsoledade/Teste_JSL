using System.Threading.Tasks;
using TesteJSL.Domain.Entities;
using System.Collections.Generic;

namespace TesteJSL.Domain.Interfaces
{
    public interface IVeiculoService
    {
        Task<IEnumerable<Veiculo>> BuscaListaVeiculos();

        Task<Veiculo> BuscaVeiculoPorId(int id);

        Task<Veiculo> AdicionaVeiculo(Veiculo motorista);

        Task AtualizaVeiculo(Veiculo motorista);

        Task ExcluiVeiculo(int motoristaId);
    }
}
