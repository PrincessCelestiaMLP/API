using FluentValidation;
using LW4_API.Data;
using LW4_API.Model.Entity;
namespace LW4_API.FluentValidator
{
    public class RentValidator : AbstractValidator<Rent>
    {
        /*public RentValidator()
        {
            RuleFor(x => x.ClientId)
                .InclusiveBetween(0, ClientData.clients.Count).WithMessage("Такого Id не існує")
                .NotEmpty().WithMessage("Поле 'ClientId' є обов'язковим.");
            RuleFor(x => x.ParkingSpaceId)
                .InclusiveBetween(0, ParkingSpaceData.parckingSpace.Count).WithMessage("Такого Id не існує")
                .NotEmpty().WithMessage("Поле 'ParcingSpaceId' є обов'язковим.");
            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Поле 'Price' є обов'язковим.")
                .GreaterThan(0).WithMessage("Ціна повинна бути >= 0") ;
            RuleFor(x => x.RentStart)
                .NotEmpty().WithMessage("Поле 'PentStart' є обов'язковим.")
                .Must(RentData.rents.)
        }*/
    }
}
