using System;
using AutoMapper;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TesteJSL.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using TesteJSL.Domain.Interfaces;
using TesteJSL.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace TesteJSL.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotoristaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IAutenticacaoService _autenticacao;
        private readonly IMotoristaService _motoristaService;

        public MotoristaController(IMotoristaService motoristaService, IMapper mapper, IAutenticacaoService autenticacao, IConfiguration configuration)
        {
            _mapper = mapper;
            _autenticacao = autenticacao;
            _configuration = configuration;
            _motoristaService = motoristaService;
        }

        /// <summary>
        /// Gera o Token JWT de segurança
        /// </summary>
        /// <returns>Token Bearer</returns>
        [HttpPost("GerarTokenAutenticacao")]
        public async Task<IActionResult> AutenticacaoUsuario([FromBody] UsuarioDto usuarioDto)
        {
            try
            {
                var usuario = _mapper.Map<Usuario>(usuarioDto);
                var token = await _autenticacao.GerarToken(usuario, _configuration);

                return Ok(token);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException.Message);
            }
        }

        /// <summary>
        /// Busca todos os motoristas cadastrados
        /// </summary>
        /// <returns>Todos os Motoristas da base de dados</returns>     
        [HttpGet("BuscaListaMotoristas")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var motoristas = await _motoristaService.BuscaListaMotoristas();

                if (motoristas.Count() == 0)
                {
                    return NotFound("Nenhum motorista foi localizado.");
                }

                var lstMotoristas = _mapper.Map<IEnumerable<MotoristaDto>>(motoristas);

                return Ok(lstMotoristas);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException.Message);
            }
        }

        /// <summary>
        /// Busca motorista por Id
        /// </summary>
        /// <param name="motoristaId"></param>
        /// <returns>Retorna apenas o motorista selecionado</returns>
        [HttpGet("BuscaMotoristaPorId/{motoristaId:int}")]
        public async Task<IActionResult> Get(int motoristaId)
        {
            try
            {
                var motorista = await _motoristaService.BuscaMotoristaPorId(motoristaId);

                if (motorista == null)
                {
                    return NotFound("Motorista não encontrado.");
                }

                var motoristaSelecionado = _mapper.Map<MotoristaDto>(motorista);

                return Ok(motoristaSelecionado);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException.Message);
            }
        }

        /// <summary>
        /// Adiciona um novo motorista
        /// </summary>
        /// <param name="motorista"></param>
        /// <returns>HTTP response code</returns>
        /// <response code="200">Solicitação realizada com sucesso.</response>
        /// <response code="500">Ocorreu um erro na solicitação.</response>
        [HttpPost("AdicionaMotorista")]
        public async Task<IActionResult> Post(MotoristaDto motorista)
        {
            try
            {
                var modelMotorista = _mapper.Map<Motorista>(motorista);

                return Ok(await _motoristaService.AdicionaMotorista(modelMotorista));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException.Message);
            }
        }

        /// <summary>
        /// Atualiza os dados do motorista
        /// </summary>
        /// <param name="motorista"></param>
        /// <response code="200">Solicitação realizada com sucesso.</response>
        /// <response code="500">Ocorreu um erro na solicitação.</response>
        [HttpPut("AtualizaDadosDoMotorista")]
        public async Task<IActionResult> Put([FromBody] MotoristaDto motorista)
        {
            try
            {
                var modelMotorista = _mapper.Map<Motorista>(motorista);

                await _motoristaService.AtualizaMotorista(modelMotorista);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException.Message);
            }
        }

        /// <summary>
        /// Exclui motorista selecionado
        /// </summary>
        /// <param name="motoristaId"></param>
        /// <response code="200">Solicitação realizada com sucesso.</response>
        /// <response code="500">Ocorreu um erro na solicitação.</response>
        [HttpDelete("ExcluiMotorista/{motoristaId:int}")]
        public async Task<IActionResult> Delete(int motoristaId)
        {
            try
            {
                await _motoristaService.ExcluiMotorista(motoristaId);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException.Message);
            }
        }
    }
}
