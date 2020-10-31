using System;
using System.Linq;
using System.Threading.Tasks;
using TesteJSL.Domain.Entities;
using TesteJSL.Domain.Interfaces;
using System.Collections.Generic;
using TesteJSL.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace TesteJSL.Services
{
    public class VeiculoService : IVeiculoService
    {
        private readonly MotoristaContext _context;
        private readonly IVeiculoRepository _veiculoRepository;

        public VeiculoService(IVeiculoRepository veiculoService, MotoristaContext context)
        {
            _context = context;
            _veiculoRepository = veiculoService;
        }

        public async Task<Veiculo> AdicionaVeiculo(Veiculo veiculo)
        {
            return await _veiculoRepository.AddAsync(veiculo);
        }

        public async Task AtualizaVeiculo(Veiculo veiculo)
        {
            var veiculoLocalizado = _context.Veiculo.AsNoTracking().Where(x => x.Id == veiculo.Id).FirstOrDefault();

            if (veiculoLocalizado != null)
            {
                var dataCadastro = veiculoLocalizado.DataCadastro;
                veiculoLocalizado = veiculo;
                veiculoLocalizado.DataCadastro = dataCadastro;

                _context.Entry(veiculoLocalizado).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Veiculo>> BuscaListaVeiculos()
        {
            return await _veiculoRepository.GetAllAsync();
        }

        public async Task<Veiculo> BuscaVeiculoPorId(int veiculoId)
        {
            return await _veiculoRepository.GetByIdAsync(veiculoId);
        }

        public async Task ExcluiVeiculo(int veiculoId)
        {
            var veiculo = await _veiculoRepository.GetByIdAsync(veiculoId);

            await _veiculoRepository.DeleteAsync(veiculo);
        }
    }
}
