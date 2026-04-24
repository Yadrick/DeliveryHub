using DeliveryHub.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DeliveryHub.Web.Extensions
{
    /// <summary>
    /// Содержит методы расширения для применения миграций базы данных при запуске приложения.
    /// </summary>
    public static class MigrationExtensions
    {
        /// <summary>
        /// Применяет все ожидающие миграции базы данных.
        /// Если база данных ещё не создана, она будет создана автоматически.
        /// </summary>
        public static WebApplication ApplyDatabaseMigrations(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var logger = scope.ServiceProvider
                .GetRequiredService<ILoggerFactory>()
                .CreateLogger("DatabaseMigration");

            try
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<DeliveryOrdersDbContext>();

                logger.LogInformation("Запуск применения миграций базы данных.");

                dbContext.Database.Migrate();

                logger.LogInformation("Миграции базы данных успешно применены.");
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Ошибка при применении миграций базы данных.");
                throw;
            }

            return app;
        }
    }
}
