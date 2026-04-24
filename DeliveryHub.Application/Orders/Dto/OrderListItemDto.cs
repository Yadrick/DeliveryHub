namespace DeliveryHub.Application.Orders.Dto
{
    /// <summary>
    /// Представляет собой позицию заказа на доставку, отображаемую в списке заказов.
    /// </summary>
    public sealed record OrderListItemDto(
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
