using DeliveryHub.Domain.Entities;

namespace DeliveryHub.Application.Abstractions
{
    /// <summary>
    /// Определяет операции сохранения данных для заказов на доставку.
    /// </summary>
    public interface IOrderRepository
    {
        /// <summary>
        /// Добавляет новый заказ на доставку.
        /// </summary>
        Task AddAsync(DeliveryOrder order, CancellationToken cancellationToken = default);

        /// <summary>
        /// Получает все заказы на доставку.
        /// </summary>
        Task<IReadOnlyList<DeliveryOrder>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Получает заказ на доставку по его уникальному идентификатору.
        /// </summary>
        Task<DeliveryOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Получает количество заказов, созданных в указанную дату по UTC.
        /// </summary>
        Task<int> CountCreatedOnDateAsync(DateOnly date, CancellationToken cancellationToken = default);

        /// <summary>
        /// Определяет, существует ли уже заказ с указанным номером.
        /// </summary>
        Task<bool> ExistsByOrderNumberAsync(string orderNumber, CancellationToken cancellationToken = default);
    }
}
