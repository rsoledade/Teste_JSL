using AutoMapper;
using TesteJSL.Domain.Entities;
using TesteJSL.Application.Models;

namespace TesteJSL.Application.AutoMapper
{
    public class DomainToModelMappingProfile : Profile
    {
        public DomainToModelMappingProfile()
        {
            CreateMap<Usuario, UsuarioDto>();
            CreateMap<Veiculo, VeiculoDto>();
            CreateMap<Motorista, MotoristaDto>();
        }
    }
}
