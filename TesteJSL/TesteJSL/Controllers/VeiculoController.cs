using System;
using AutoMapper;
using System.Threading.Tasks;
using TesteJSL.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TesteJSL.Domain.Interfaces;
using System.Collections.Generic;
using TesteJSL.Application.Models;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace TesteJSL.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeiculoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IVeiculoService _veiculoService;

        public VeiculoController(IVeiculoService veiculoService, IMapper mapper)
        {
            _mapper = mapper;
            _veiculoService = veiculoService;
        }

        /// <summary>
        /// Busca todos os veículos cadastrados
        /// </summary>
        /// <returns>Todos os veículos cadastrados</returns>
        [HttpGet("BuscaTodosOsVeiculos")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var veiculos = await _veiculoService.BuscaListaVeiculos();

                if (veiculos == null)
                {
                    return NotFound("Nenhum veículo foi localizado.");
                }

                var lstveiculos = _mapper.Map<IEnumerable<VeiculoDto>>(veiculos);

                return Ok(lstveiculos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Busca veículo por id
        /// </summary>
        /// <param name="veiculoId"></param>
        /// <returns>Veículo selecionado</returns>
        [HttpGet("BuscaVeiculoPorId/{veiculoId:int}")]
        public async Task<IActionResult> Get(int veiculoId)
        {
            try
            {
                var veiculo = await _veiculoService.BuscaVeiculoPorId(veiculoId);

                if (veiculo == null)
                {
                    return NotFound("Veículo não encontrado.");
                }

                var veiculoSelecionado = _mapper.Map<VeiculoDto>(veiculo);

                return Ok(veiculoSelecionado);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Adiciona um novo veículo
        /// </summary>
        /// <param name="veiculoDto"></param>
        /// <response code="200">Solicitação realizada com sucesso.</response>
        /// <response code="500">Ocorreu um erro na solicitação.</response>
        [HttpPost("AdicionaNovoVeiculo")]
        public async Task<IActionResult> Post([FromBody] VeiculoDto veiculoDto)
        {
            try
            {
                var modelVeiculo = _mapper.Map<Veiculo>(veiculoDto);

                return Ok(await _veiculoService.AdicionaVeiculo(modelVeiculo));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Atualiza os dados do veículo
        /// </summary>
        /// <param name="veiculoDto"></param>
        /// <response code="200">Solicitação realizada com sucesso.</response>
        /// <response code="500">Ocorreu um erro na solicitação.</response>
        [HttpPut("AtualizaDadosDoVeiculo")]
        public async Task<IActionResult> Put([FromBody] VeiculoDto veiculoDto)
        {
            try
            {
                var modelVeiculo = _mapper.Map<Veiculo>(veiculoDto);

                await _veiculoService.AtualizaVeiculo(modelVeiculo);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Exclui veículo selecionado
        /// </summary>
        /// <param name="veiculoId"></param>
        /// <response code="200">Solicitação realizada com sucesso.</response>
        /// <response code="500">Ocorreu um erro na solicitação.</response>
        [HttpDelete("ExcluiVeiculo/{veiculoId:int}")]
        public async Task<IActionResult> Delete(int veiculoId)
        {
            try
            {
                await _veiculoService.ExcluiVeiculo(veiculoId);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
