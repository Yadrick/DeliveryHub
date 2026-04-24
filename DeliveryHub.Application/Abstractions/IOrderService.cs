using DeliveryHub.Application.Orders.Dto;

namespace DeliveryHub.Application.Abstractions
{
    /// <summary>
    /// Определяет операции на уровне приложения для заказов на доставку
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// Создает новый заказ на доставку.
        /// </summary>
        /// <returns>Данные созданного заказа.</returns>
        Task<CreatedOrderDto> CreateAsync(CreateOrderDto dto, CancellationToken cancellationToken = default);

        /// <summary>
        /// Получает все заказы на доставку, упорядоченные по дате создания в порядке убывания.
        /// </summary>
        Task<IReadOnlyList<OrderListItemDto>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Получает заказ на доставку по его уникальному идентификатору.
        /// </summary>
        Task<OrderDetailsDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
