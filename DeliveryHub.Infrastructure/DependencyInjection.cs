using DeliveryHub.Application.Abstractions;
using DeliveryHub.Infrastructure.Persistence;
using DeliveryHub.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DeliveryHub.Infrastructure
{
    /// <summary>
    /// Содержит методы регистрации зависимостей слоя Infrastructure.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Регистрирует инфраструктурные сервисы приложения:
        /// контекст базы данных, репозитории и Unit of Work.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Возникает, если строка подключения DefaultConnection не настроена.
        /// </exception>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");
            }

            services.AddDbContext<DeliveryOrdersDbContext>(options =>
            {
                options.UseSqlite(connectionString);
            });
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<DeliveryOrdersDbContext>());

            return services;
        }
    }
}
