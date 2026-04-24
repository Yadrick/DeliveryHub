using DeliveryHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryHub.Infrastructure.Persistence.Configurations
{
    /// <summary>
    /// Конфигурация EF Core для сущности <see cref="DeliveryOrder"/>.
    /// Определяет таблицу, ключи, ограничения, индексы и типы колонок.
    /// </summary>
    public sealed class DeliveryOrderConfiguration : IEntityTypeConfiguration<DeliveryOrder>
    {
        /// <summary>
        /// Настраивает отображение сущности <see cref="DeliveryOrder"/> на таблицу базы данных.
        /// </summary>
        public void Configure(EntityTypeBuilder<DeliveryOrder> builder)
        {
            builder.ToTable("DeliveryOrders");

            builder.HasKey(order => order.Id);

            builder.Property(order => order.Id)
                .ValueGeneratedNever();

            builder.Property(order => order.OrderNumber)
                .HasMaxLength(DeliveryOrder.MaxOrderNumberLength)
                .IsRequired();

            builder.HasIndex(order => order.OrderNumber)    // TODO: нужно еще подумать о нужности индекса
                .IsUnique();

            builder.Property(order => order.SenderCity)
                .HasMaxLength(DeliveryOrder.MaxCityLength)
                .IsRequired();

            builder.Property(order => order.SenderAddress)
                .HasMaxLength(DeliveryOrder.MaxAddressLength)
                .IsRequired();

            builder.Property(order => order.RecipientCity)
                .HasMaxLength(DeliveryOrder.MaxCityLength)
                .IsRequired();

            builder.Property(order => order.RecipientAddress)
                .HasMaxLength(DeliveryOrder.MaxAddressLength)
                .IsRequired();

            builder.Property(order => order.CargoWeightKg)
                .HasPrecision(10, 3)
                .IsRequired();

            builder.Property(order => order.PickupDate)
                .IsRequired();

            builder.Property(order => order.CreatedAtUtc)
                .IsRequired();

            builder.HasIndex(order => order.PickupDate);    // TODO: нужно еще подумать о нужности индекса

            builder.HasIndex(order => order.CreatedAtUtc);  // TODO: нужно еще подумать о нужности индекса
        }
    }
}
