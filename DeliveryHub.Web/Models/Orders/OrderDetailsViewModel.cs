namespace DeliveryHub.Web.Models.Orders
{
    /// <summary>
    /// Модель представления детальной страницы заказа в режиме чтения.
    /// </summary>
    public sealed class OrderDetailsViewModel
    {
        /// <summary>
        /// УИД заказа.
        /// </summary>
        public Guid Id { get; init; }

        /// <summary>
        /// Номер заказа.
        /// </summary>
        public string OrderNumber { get; init; } = string.Empty;

        /// <summary>
        /// Город отправителя.
        /// </summary>
        public string SenderCity { get; init; } = string.Empty;

        /// <summary>
        /// Адрес отправителя.
        /// </summary>
        public string SenderAddress { get; init; } = string.Empty;

        /// <summary>
        /// Город получателя.
        /// </summary>
        public string RecipientCity { get; init; } = string.Empty;

        /// <summary>
        /// Адрес получателя.
        /// </summary>
        public string RecipientAddress { get; init; } = string.Empty;

        /// <summary>
        /// Вес груза в килограммах.
        /// </summary>
        public decimal CargoWeightKg { get; init; }

        /// <summary>
        /// Дата забора груза.
        /// </summary>
        public DateOnly PickupDate { get; init; }

        /// <summary>
        /// Дата и время создания заказа в UTC.
        /// </summary>
        public DateTime CreatedAtUtc { get; init; }
    }
}
