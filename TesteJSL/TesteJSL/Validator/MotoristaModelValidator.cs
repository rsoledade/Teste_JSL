using FluentValidation;
using TesteJSL.Application.Models;

namespace TesteJSL.Application.Validator
{
    public class MotoristaModelValidator : AbstractValidator<MotoristaDto>
    {
        public MotoristaModelValidator()
        {
            RuleFor(document => document.Nome)
                .NotNull()
                .NotEmpty()
                .MaximumLength(200)
                .WithMessage("O campo Nome é obrigatório e seu tamanho máximo é 200 caracteres");

            RuleFor(document => document.Sobrenome)
                .NotNull()
                .NotEmpty()
                .MaximumLength(200)
                .WithMessage("O campo Sobrenome é obrigatório e seu tamanho máximo é 200 caracteres");

            RuleFor(document => document.Endereco)
                .NotNull()
                .NotEmpty()
                .MaximumLength(200)
                .WithMessage("O campo Endereço é obrigatório e seu tamanho máximo é 200 caracteres");

            RuleFor(document => document.Numero)
                .NotNull()
                .NotEmpty()
                .WithMessage("O campo Número é obrigatório");

            RuleFor(document => document.Cep)
                .NotNull()
                .NotEmpty()
                .MaximumLength(12)
                .WithMessage("O campo Cep é obrigatório e seu tamanho máximo é 12 caracteres");

            RuleFor(document => document.Bairro)
                .NotNull()
                .NotEmpty()
                .MaximumLength(200)
                .WithMessage("O campo Bairro é obrigatório e seu tamanho máximo é 200 caracteres");

            RuleFor(document => document.Cidade)
                .NotNull()
                .NotEmpty()
                .MaximumLength(200)
                .WithMessage("O campo Cidade é obrigatório e seu tamanho máximo é 200 caracteres");

            RuleFor(document => document.Estado)
                .NotNull()
                .NotEmpty()
                .MaximumLength(2)
                .WithMessage("O campo Estado é obrigatório e seu tamanho máximo é 2 caracteres");
        }
    }
}
