using DeliveryHub.Application.Abstractions;
using DeliveryHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeliveryHub.Infrastructure.Persistence
{
    /// <summary>
    /// Контекст базы данных приложения доставки заказов.
    /// </summary>
    public sealed class DeliveryOrdersDbContext : DbContext, IUnitOfWork
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="DeliveryOrdersDbContext"/>.
        /// </summary>
        public DeliveryOrdersDbContext(DbContextOptions<DeliveryOrdersDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Получает набор заказов на доставку.
        /// </summary>
        public DbSet<DeliveryOrder> DeliveryOrders => Set<DeliveryOrder>();

        /// <summary>
        /// Настраивает модель данных приложения.
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DeliveryOrdersDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
