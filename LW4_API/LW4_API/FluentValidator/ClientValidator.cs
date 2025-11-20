using System.Text.RegularExpressions;
using FluentValidation;
using LW4_API.Model.DTO;
namespace LW4_API.FluentValidator
{
    public class ClientValidator : AbstractValidator<ClientDTO>
    {
        public ClientValidator() {
            RuleFor(x => x.Name)
                   .NotEmpty().WithMessage("Поле 'Name' є обов'язковим.")
                   .MinimumLength(3).WithMessage("Назва має містити щонайменше 3 символи.")
                   .MaximumLength(100).WithMessage("Назва не може перевищувати 100 символів.");

            RuleFor(x => x.Surname)
                   .NotEmpty().WithMessage("Поле 'Surname' є обов'язковим.")
                   .MinimumLength(3).WithMessage("Прізвище має містити щонайменше 3 символи.")
                   .MaximumLength(100).WithMessage("Прізвище не може перевищувати 100 символів.");

            RuleFor(x => x.Email)
                   .NotEmpty().WithMessage("Поле 'Email' є обов'язковим.")
                   .EmailAddress();

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Номер телефону обов'язковий")
                .Matches(@"\+\d{10,15}$").WithMessage("Невалідний номер телефону");
        }
    }
}
