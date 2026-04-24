namespace DeliveryHub.Application.Orders.Dto
{
    /// <summary>
    /// Представляет собой подробную информацию о заказе на доставку, доступную только для чтения.
    /// </summary>
    public sealed record OrderDetailsDto(
        Guid Id,
        string OrderNumber,
        string SenderCity,
        string SenderAddress,
        string RecipientCity,
        string RecipientAddress,
        decimal CargoWeightKg,
        DateOnly PickupDate,
        DateTime CreatedAtUtc);
}
