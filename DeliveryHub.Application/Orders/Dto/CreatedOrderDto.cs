namespace DeliveryHub.Application.Orders.Dto
{
    /// <summary>
    /// Отражает результат успешного создания заказа на доставку..
    /// </summary>
    /// <param name="Id">Уникальный идентификатор созданного заказа.</param>
    /// <param name="OrderNumber">Сгенерированный удобочитаемый номер заказа.</param>
    public sealed record CreatedOrderDto(Guid Id, string OrderNumber);
}
