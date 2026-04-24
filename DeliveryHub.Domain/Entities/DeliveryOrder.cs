using DeliveryHub.Domain.Exceptions;

namespace DeliveryHub.Domain.Entities
{
    /// <summary>
    /// Представляет собой заказ на доставку, созданный клиентом.
    /// </summary>
    public class DeliveryOrder
    {
        /// <summary>
        /// Максимально допустимая длина для полей с названием города.
        /// </summary>
        public const int MaxCityLength = 128;

        /// <summary>
        /// Максимально допустимая длина полей адреса.
        /// </summary>
        public const int MaxAddressLength = 256;

        /// <summary>
        /// Максимально допустимая длина для сгенерированного номера заказа.
        /// </summary>
        public const int MaxOrderNumberLength = 32;

        /// <summary>
        /// Максимально допустимый вес груза в килограммах.
        /// </summary>
        public const decimal MaxCargoWeightKg = 100_000m;

        /// <summary>
        /// Получает или задает уникальный идентификатор заказа в базе данных.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Получает или задает автоматически сгенерированный удобочитаемый номер заказа.
        /// Например: ORD-20260424-000001.
        /// </summary>
        public string OrderNumber { get; private set; } = string.Empty;

        /// <summary>
        /// Получает или задает город отправителя.
        /// </summary>
        public string SenderCity { get; private set; } = string.Empty;

        /// <summary>
        /// Получает или задает адрес отправителя.
        /// </summary>
        public string SenderAddress { get; private set; } = string.Empty;

        /// <summary>
        /// Получает или задает город-получатель..
        /// </summary>
        public string RecipientCity { get; private set; } = string.Empty;

        /// <summary>
        /// Получает или задает адрес получателя.
        /// </summary>
        public string RecipientAddress { get; private set; } = string.Empty;

        /// <summary>
        /// Получает или задает вес груза в килограммах.
        /// </summary>
        public decimal CargoWeightKg { get; private set; }

        /// <summary>
        /// Получает или задает дату отгрузки груза.
        /// </summary>
        public DateOnly PickupDate { get; private set; }

        /// <summary>
        /// Получает или задает дату и время в формате UTC, когда был создан заказ.
        /// </summary>
        public DateTime CreatedAtUtc { get; private set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="DeliveryOrder"/>.
        /// </summary>
        private DeliveryOrder()
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="DeliveryOrder"/>.
        /// </summary>
        public DeliveryOrder(
            string orderNumber,
            string senderCity,
            string senderAddress,
            string recipientCity,
            string recipientAddress,
            decimal cargoWeightKg,
            DateOnly pickupDate,
            DateTime createdAtUtc)
        {
            Validate(
                orderNumber,
                senderCity,
                senderAddress,
                recipientCity,
                recipientAddress,
                cargoWeightKg,
                createdAtUtc);

            Id = Guid.NewGuid();
            OrderNumber = orderNumber.Trim().ToUpperInvariant();
            SenderCity = senderCity.Trim();
            SenderAddress = senderAddress.Trim();
            RecipientCity = recipientCity.Trim();
            RecipientAddress = recipientAddress.Trim();
            CargoWeightKg = decimal.Round(cargoWeightKg, 3, MidpointRounding.AwayFromZero);
            PickupDate = pickupDate;
            CreatedAtUtc = createdAtUtc;
        }

        /// <summary>
        /// Создает новый заказ на доставку.
        /// </summary>
        /// <param name="orderNumber">Сгенерированный номер заказа.</param>
        /// <param name="senderCity">Город отправителя.</param>
        /// <param name="senderAddress">Адрес отправителя.</param>
        /// <param name="recipientCity">Город получателя</param>
        /// <param name="recipientAddress">Адрес получателя</param>
        /// <param name="cargoWeightKg">Вес груза в килограммах</param>
        /// <param name="pickupDate">Дата получения</param>
        /// <param name="createdAtUtc">Дата и время создания заказа в формате UTC</param>
        /// <returns>Новый экземпляр <see cref="DeliveryOrder"/>.</returns>
        public static DeliveryOrder Create(
            string orderNumber,
            string senderCity,
            string senderAddress,
            string recipientCity,
            string recipientAddress,
            decimal cargoWeightKg,
            DateOnly pickupDate,
            DateTime createdAtUtc)
        {
            return new DeliveryOrder(
                orderNumber,
                senderCity,
                senderAddress,
                recipientCity,
                recipientAddress,
                cargoWeightKg,
                pickupDate,
                createdAtUtc);
        }

        /// <summary>
        /// Проверяет данные заказа в соответствии с основными правилами домена.
        /// </summary>
        /// <exception cref="DomainValidationException">
        /// Выбрасывается, если одно или несколько значений нарушают основные правила домена.
        /// </exception>
        private static void Validate(
            string orderNumber,
            string senderCity,
            string senderAddress,
            string recipientCity,
            string recipientAddress,
            decimal cargoWeightKg,
            DateTime createdAtUtc)
        {
            if (string.IsNullOrWhiteSpace(orderNumber))
            {
                throw new DomainValidationException("\r\nТребуется номер заказа.");
            }

            if (orderNumber.Trim().Length > MaxOrderNumberLength)
            {
                throw new DomainValidationException($"Номер заказа не должен превышать {MaxOrderNumberLength} символов.");
            }

            if (string.IsNullOrWhiteSpace(senderCity))
            {
                throw new DomainValidationException("Требуется указать город отправителя.");
            }

            if (senderCity.Trim().Length > MaxCityLength)
            {
                throw new DomainValidationException($"Длина города отправителя не должна превышать {MaxCityLength} символов.");
            }

            if (string.IsNullOrWhiteSpace(senderAddress))
            {
                throw new DomainValidationException("Адрес отправителя обязателен.");
            }

            if (senderAddress.Trim().Length > MaxAddressLength)
            {
                throw new DomainValidationException($"Адрес отправителя не должен превышать {MaxAddressLength} симовлов.");
            }

            if (string.IsNullOrWhiteSpace(recipientCity))
            {
                throw new DomainValidationException("Требуется указать город-получатель..");
            }

            if (recipientCity.Trim().Length > MaxCityLength)
            {
                throw new DomainValidationException($"Город получателя не должен превышать {MaxCityLength} символов.");
            }

            if (string.IsNullOrWhiteSpace(recipientAddress))
            {
                throw new DomainValidationException("Адрес получателя обязателен.");
            }

            if (recipientAddress.Trim().Length > MaxAddressLength)
            {
                throw new DomainValidationException($"Адрес получателя не должен превышать {MaxAddressLength} символов.");
            }

            if (cargoWeightKg <= 0)
            {
                throw new DomainValidationException("Вес груза должен быть больше нуля..");
            }

            if (cargoWeightKg > MaxCargoWeightKg)
            {
                throw new DomainValidationException($"Вес груза не должен превышать {MaxCargoWeightKg} кг.");
            }

            if (createdAtUtc.Kind != DateTimeKind.Utc)
            {
                throw new DomainValidationException("Дата создания должна быть указана в формате UTC.");
            }
        }
    }
}
