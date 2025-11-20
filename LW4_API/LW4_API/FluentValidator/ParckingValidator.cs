using FluentValidation;
using LW4_API.Data;
using LW4_API.Model.Entity;
using System.Text.RegularExpressions;

namespace LW4_API.FluentValidator
{
    public class ParckingValidator : AbstractValidator<ParkingSpace>
    {
        /*public ParckingValidator()
        {
            RuleFor(x => x.UserId)
                .InclusiveBetween(0, ClientData.clients.Count).WithMessage("Такого Id не існує");



            RuleFor(x => x.IsVip)
                   .NotEmpty().WithMessage("Поле 'IsVip' є обов'язковим.");
                   
            RuleFor(x => x.PriceForHour)
                   .NotEmpty().WithMessage("Поле 'PriceForHour' є обов'язковим.")
                   .GreaterThan(0).WithMessage("Ціна за годину повинна бути >= 0");
        }*/
    }
}
