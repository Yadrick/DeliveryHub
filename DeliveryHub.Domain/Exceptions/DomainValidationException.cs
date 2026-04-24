namespace DeliveryHub.Domain.Exceptions
{
    /// <summary>
    /// Представляет собой исключение, которое генерируется, когда сущность домена получает недопустимые бизнес-данные.
    /// </summary>
    public sealed class DomainValidationException : Exception
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="DomainValidationException"/>.
        /// </summary>
        public DomainValidationException(string message)
            : base(message)
        {
        }
    }
}
