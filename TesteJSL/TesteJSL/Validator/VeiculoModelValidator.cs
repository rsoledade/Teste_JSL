using FluentValidation;
using TesteJSL.Application.Models;

namespace TesteJSL.Application.Validator
{
    public class VeiculoModelValidator : AbstractValidator<VeiculoDto>
    {
        public VeiculoModelValidator()
        {
            RuleFor(document => document.Marca)
                .NotNull()
                .NotEmpty()
                .MaximumLength(150)
                .WithMessage("O campo Marca é obrigatório e seu tamanho máximo é 150 caracteres");

            RuleFor(document => document.Modelo)
                .NotNull()
                .NotEmpty()
                .MaximumLength(150)
                .WithMessage("O campo Modelo é obrigatório e seu tamanho máximo é 150 caracteres");

            RuleFor(document => document.Placa)
               .NotNull()
               .NotEmpty()
               .MaximumLength(10)
               .WithMessage("O campo Placa é obrigatório e seu tamanho máximo é 10 caracteres");

            RuleFor(document => document.Eixos)
               .NotNull()
               .NotEmpty()
               .WithMessage("O campo Eixos é obrigatório");
        }
    }
}
