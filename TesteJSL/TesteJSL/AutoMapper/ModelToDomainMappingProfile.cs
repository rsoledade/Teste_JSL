using AutoMapper;
using TesteJSL.Application.Models;
using TesteJSL.Domain.Entities;

namespace TesteJSL.Application.AutoMapper
{
    public class ModelToDomainMappingProfile : Profile
    {
        public ModelToDomainMappingProfile()
        {
            CreateMap<UsuarioDto, Usuario>();
            CreateMap<VeiculoDto, Veiculo>();
            CreateMap<MotoristaDto, Motorista>();
        }
    }
}
