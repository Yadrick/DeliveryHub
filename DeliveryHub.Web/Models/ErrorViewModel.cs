namespace DeliveryHub.Web.Models
{
    /// <summary>
    /// Модель представления страницы ошибки.
    /// </summary>
    public sealed class ErrorViewModel
    {
        /// <summary>
        /// Идентификатор текущего HTTP-запроса.
        /// </summary>
        public string? RequestId { get; set; }

        /// <summary>
        /// Значение, показывающее, нужно ли отображать идентификатор запроса.
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrWhiteSpace(RequestId);
    }
}
