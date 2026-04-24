using DeliveryHub.Domain.Entities;
using FluentValidation;

namespace DeliveryHub.Application.Orders.Validation
{
    /// <summary>
    /// Проверяет входные данные, необходимые для создания нового заказа на доставку
    /// </summary>
    public sealed class CreateOrderDtoValidator : AbstractValidator<Dto.CreateOrderDto>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CreateOrderDtoValidator"/>.
        /// </summary>
        public CreateOrderDtoValidator()
        {
            RuleFor(x => x.SenderCity)
                .NotEmpty()
                .WithMessage("Город отправителя обязателен.")
                .MaximumLength(DeliveryOrder.MaxCityLength)
                .WithMessage($"Город отправителя не должен превышать {DeliveryOrder.MaxCityLength} символов.");

            RuleFor(x => x.SenderAddress)
                .NotEmpty()
                .WithMessage("Адрес отправителя обязателен.")
                .MaximumLength(DeliveryOrder.MaxAddressLength)
                .WithMessage($"Адрес отправителя не должен превышать {DeliveryOrder.MaxAddressLength} символов.");

            RuleFor(x => x.RecipientCity)
                .NotEmpty()
                .WithMessage("Город получателя обязателен.")
                .MaximumLength(DeliveryOrder.MaxCityLength)
                .WithMessage($"Город получателя не должен превышать {DeliveryOrder.MaxCityLength} символов.");

            RuleFor(x => x.RecipientAddress)
                .NotEmpty()
                .WithMessage("Адрес получателя обязателен.")
                .MaximumLength(DeliveryOrder.MaxAddressLength)
                .WithMessage($"Адрес получателя не должен превышать {DeliveryOrder.MaxAddressLength} символов.");

            RuleFor(x => x.CargoWeightKg)
                .GreaterThan(0)
                .WithMessage("Вес груза должен быть больше нуля.")
                .LessThanOrEqualTo(DeliveryOrder.MaxCargoWeightKg)
                .WithMessage($"Вес груза не должен превышать {DeliveryOrder.MaxCargoWeightKg} кг.");

            RuleFor(x => x.PickupDate)
                .Must(date => date != default)
                .WithMessage("Требуется указать дату забора.");
        }
    }
}
