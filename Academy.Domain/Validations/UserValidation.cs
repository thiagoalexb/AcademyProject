using Academy.Domain.Commands;
using FluentValidation;
using System;

namespace Academy.Domain.Validations
{
    public abstract class UserValidation<T> : AbstractValidator<T> where T : UserCommand
    {
        protected void ValidateFirstName()
        {
            RuleFor(c => c.FirstName)
                .NotEmpty().WithMessage("Nome é obrigatório")
                .Length(2, 100).WithMessage("O Nome deve ter entre 3 a 100 caracteres");
        }

        protected void ValidateLastName()
        {
            RuleFor(c => c.LastName)
                .NotEmpty().WithMessage("Sobrenome é obrigatório")
                .Length(2, 100).WithMessage("O Sobrenome deve ter entre 3 a 100 caracteres");
        }

        protected void ValidateBirthDate()
        {
            RuleFor(c => c.DateOfBirth)
                .NotEmpty()
                .WithMessage("A data de nascimento é obrigatória");
        }

        protected void ValidateEmail()
        {
            RuleFor(c => c.Email)
                .NotEmpty()
                .EmailAddress();
        }

        protected void ValidatePassword()
        {
            RuleFor(c => c.Password)
                .NotEmpty().WithMessage("A senha é obrigatória")
                .Length(6, 100).WithMessage("O Sobrenome deve ter entre 6 a 100 caracteres");
        }

        protected void ValidateId()
        {
            RuleFor(c => c.UserId)
                .NotEqual(Guid.Empty);
        }
    }
}
