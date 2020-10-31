using FluentValidation;
using TesteJSL.Application.Models;

namespace TesteJSL.Application.Validator
{
    public class UsuarioModelValidator : AbstractValidator<UsuarioDto>
    {
        public UsuarioModelValidator()
        {
            RuleFor(document => document.Login)
                .NotNull()
                .NotEmpty()
                .WithMessage("O campo Login é obrigatório");

            RuleFor(document => document.Senha)
                .NotNull()
                .NotEmpty()
                .WithMessage("O campo Senha é obrigatório");
        }
    }
}
