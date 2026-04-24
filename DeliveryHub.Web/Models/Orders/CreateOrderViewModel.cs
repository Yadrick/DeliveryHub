using DeliveryHub.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace DeliveryHub.Web.Models.Orders
{
    /// <summary>
    /// Модель представления формы создания нового заказа.
    /// </summary>
    public sealed class CreateOrderViewModel
    {
        /// <summary>
        /// Город отправителя.
        /// </summary>
        [Required(ErrorMessage = "Город отправителя обязателен для заполнения.")]
        [StringLength(DeliveryOrder.MaxCityLength, ErrorMessage = "Город отправителя не должен превышать {1} символов.")]
        [Display(Name = "Город отправителя")]
        public string SenderCity { get; set; } = string.Empty;

        /// <summary>
        /// Адрес отправителя.
        /// </summary>
        [Required(ErrorMessage = "Адрес отправителя обязателен для заполнения.")]
        [StringLength(DeliveryOrder.MaxAddressLength, ErrorMessage = "Адрес отправителя не должен превышать {1} символов.")]
        [Display(Name = "Адрес отправителя")]
        public string SenderAddress { get; set; } = string.Empty;

        /// <summary>
        /// Город получателя.
        /// </summary>
        [Required(ErrorMessage = "Город получателя обязателен для заполнения.")]
        [StringLength(DeliveryOrder.MaxCityLength, ErrorMessage = "Город получателя не должен превышать {1} символов.")]
        [Display(Name = "Город получателя")]
        public string RecipientCity { get; set; } = string.Empty;

        /// <summary>
        /// Адрес получателя.
        /// </summary>
        [Required(ErrorMessage = "Адрес получателя обязателен для заполнения.")]
        [StringLength(DeliveryOrder.MaxAddressLength, ErrorMessage = "Адрес получателя не должен превышать {1} символов.")]
        [Display(Name = "Адрес получателя")]
        public string RecipientAddress { get; set; } = string.Empty;

        /// <summary>
        /// Вес груза в килограммах.
        /// </summary>
        [Required(ErrorMessage = "Вес груза обязателен для заполнения.")]
        [Range(0.001, 100000, ErrorMessage = "Вес груза должен быть больше 0 и не превышать 100000 кг.")]
        [Display(Name = "Вес груза, кг")]
        public decimal? CargoWeightKg { get; set; }

        /// <summary>
        /// Дата забора груза.
        /// </summary>
        [Required(ErrorMessage = "Дата забора груза обязательна для заполнения.")]
        [DataType(DataType.Date)]
        [Display(Name = "Дата забора груза")]
        public DateOnly? PickupDate { get; set; }
    }
}
