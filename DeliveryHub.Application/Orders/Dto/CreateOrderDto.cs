namespace DeliveryHub.Application.Orders.Dto
{
    /// <summary>
    /// Представляет собой входные данные, необходимые для создания нового заказа на доставку.
    /// </summary>
    public sealed class CreateOrderDto
    {
        /// <summary>
        /// Получает или задает город отправителя.
        /// </summary>
        public string SenderCity { get; set; } = string.Empty;

        /// <summary>
        /// Получает или задает адрес отправителя.
        /// </summary>
        public string SenderAddress { get; set; } = string.Empty;

        /// <summary>
        /// Получает или задает город-получатель.
        /// </summary>
        public string RecipientCity { get; set; } = string.Empty;

        /// <summary>
        /// Получает или задает адрес получателя.
        /// </summary>
        public string RecipientAddress { get; set; } = string.Empty;

        /// <summary>
        /// Получает или задает вес груза в килограммах.
        /// </summary>
        public decimal CargoWeightKg { get; set; }

        /// <summary>
        /// Получает или устанавливает дату отгрузки груза.
        /// </summary>
        public DateOnly PickupDate { get; set; }
    }
}
