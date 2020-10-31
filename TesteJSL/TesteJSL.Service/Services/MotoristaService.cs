using System;
using RestSharp;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using TesteJSL.Domain.Entities;
using TesteJSL.Domain.Interfaces;
using System.Collections.Generic;
using TesteJSL.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using static TesteJSL.Services.JsonRetornoGoogleGeolocalizacao;

namespace TesteJSL.Services
{
    public class MotoristaService : IMotoristaService
    {
        private readonly MotoristaContext _context;
        private readonly IMotoristaRepository _motoristaRepository;

        public MotoristaService(IMotoristaRepository motoristaRepository, MotoristaContext context)
        {
            _context = context;
            _motoristaRepository = motoristaRepository;
        }

        public async Task<Motorista> AdicionaMotorista(Motorista motorista)
        {
            var retornoGoogle = LatiLongEnderecoMotorista(motorista);

            var objetoRetornoGoogle = JsonConvert.DeserializeObject<Root>(retornoGoogle.Content);

            foreach (var item in objetoRetornoGoogle.results)
            {
                motorista.EnderecoLatitude = item.geometry.location.lat.ToString();
                motorista.EnderecoLongitude = item.geometry.location.lng.ToString();
            }

            return await _motoristaRepository.AddAsync(motorista);
        }

        public async Task AtualizaMotorista(Motorista motorista)
        {
            var motoristaLocalizado = _context.Motorista.AsNoTracking().Where(x => x.Id == motorista.Id).FirstOrDefault();

            if (motoristaLocalizado != null)
            {
                var dataCadastro = motoristaLocalizado.DataCadastro;
                var latitude = motoristaLocalizado.EnderecoLatitude;
                var longitude = motoristaLocalizado.EnderecoLongitude;
                motoristaLocalizado = motorista;
                motoristaLocalizado.DataCadastro = dataCadastro;
                motoristaLocalizado.EnderecoLatitude = latitude;
                motoristaLocalizado.EnderecoLongitude = longitude;

                _context.Entry(motoristaLocalizado).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Motorista>> BuscaListaMotoristas()
        {
            return await _motoristaRepository.GetAllAsync();
        }

        public async Task<Motorista> BuscaMotoristaPorId(int motoristaId)
        {
            return await _motoristaRepository.GetByIdAsync(motoristaId);
        }

        public async Task ExcluiMotorista(int motoristaId)
        {
            var motorista = await _motoristaRepository.GetByIdAsync(motoristaId);
            
            await _motoristaRepository.DeleteAsync(motorista);
        }

        private RestResponse LatiLongEnderecoMotorista(Motorista motorista)
        {
            string urlGoogle = "https://maps.googleapis.com/maps/api/geocode/json?address=" + motorista.Numero +
                motorista.Endereco + ",+" + motorista.Cidade + ",+" + motorista.Estado + "&key=AIzaSyANwLSxjrksADZTcozpGJM0b0VmJpAhuYs";

            //var teste = "https://maps.googleapis.com/maps/api/geocode/json?address=1600+Amphitheatre+Parkway,+Mountain+View,+CA&key=AIzaSyANwLSxjrksADZTcozpGJM0b0VmJpAhuYs";

            var client = new RestClient(urlGoogle);
            var request = new RestRequest(client.BaseUrl, Method.GET);
            var response = client.Execute(request) as RestResponse;

            return response;
        }
    }
}
