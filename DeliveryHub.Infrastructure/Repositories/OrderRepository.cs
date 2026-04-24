using DeliveryHub.Application.Abstractions;
using DeliveryHub.Domain.Entities;
using DeliveryHub.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DeliveryHub.Infrastructure.Repositories
{
    /// <summary>
    /// Репозиторий заказов на доставку.
    /// </summary>
    public sealed class OrderRepository : IOrderRepository
    {
        private readonly DeliveryOrdersDbContext _dbContext;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="OrderRepository"/>.
        /// </summary>
        public OrderRepository(DeliveryOrdersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public async Task AddAsync(DeliveryOrder order, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(order);
            await _dbContext.DeliveryOrders.AddAsync(order, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<DeliveryOrder>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.DeliveryOrders
                .AsNoTracking()
                .OrderByDescending(order => order.CreatedAtUtc)
                .ToListAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task<DeliveryOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.DeliveryOrders
                .AsNoTracking() // TODO: обдумать этот моментик получше
                .FirstOrDefaultAsync(order => order.Id == id, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<int> CountCreatedOnDateAsync(DateOnly date, CancellationToken cancellationToken = default)
        {
            var startOfDayUtc = date.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc);
            var nextDayUtc = startOfDayUtc.AddDays(1);
            return await _dbContext.DeliveryOrders
                .AsNoTracking()
                .CountAsync(
                    order => order.CreatedAtUtc >= startOfDayUtc && order.CreatedAtUtc < nextDayUtc,
                    cancellationToken);
        }

        /// <inheritdoc />
        public async Task<bool> ExistsByOrderNumberAsync(string orderNumber, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(orderNumber)) return false;

            var normalizedOrderNumber = orderNumber.Trim().ToUpperInvariant();
            return await _dbContext.DeliveryOrders
                .AsNoTracking()
                .AnyAsync(order => order.OrderNumber == normalizedOrderNumber, cancellationToken);
        }
    }
}
