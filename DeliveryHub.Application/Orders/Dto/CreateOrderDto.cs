namespace DeliveryHub.Application.Orders.Dto
{
    /// <summary>
    /// Данные, необходимые для создания нового заказа на доставку.
    /// </summary>
    public sealed class CreateOrderDto
    {
        /// <summary>
        /// Город отправителя.
        /// </summary>
        public string SenderCity { get; set; } = string.Empty;

        /// <summary>
        /// Адрес отправителя.
        /// </summary>
        public string SenderAddress { get; set; } = string.Empty;

        /// <summary>
        /// Город получателя.
        /// </summary>
        public string RecipientCity { get; set; } = string.Empty;

        /// <summary>
        /// Адрес получателя.
        /// </summary>
        public string RecipientAddress { get; set; } = string.Empty;

        /// <summary>
        /// Вес груза в килограммах.
        /// </summary>
        public decimal CargoWeightKg { get; set; }

        /// <summary>
        /// Дата забора груза.
        /// </summary>
        public DateOnly PickupDate { get; set; }
    }
}
