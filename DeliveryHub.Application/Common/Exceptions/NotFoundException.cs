namespace DeliveryHub.Application.Common.Exceptions
{
    /// <summary>
    /// Представляет собой исключение, которое генерируется, когда запрошенный ресурс не найден.
    /// </summary>
    public sealed class NotFoundException : Exception
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="NotFoundException"/>.
        /// </summary>
        public NotFoundException(string message) : base(message)
        {
        }

        /// <summary>
        /// Создает исключение "Не найдено" для заказа на доставку.
        /// </summary>
        public static NotFoundException ForOrder(Guid id)
        {
            return new NotFoundException($"Заказ на доставку с идентификатором '{id}' не найден.");
        }
    }
}
