using DeliveryHub.Application.Abstractions;
using DeliveryHub.Application.Orders.Dto;
using DeliveryHub.Application.Orders.Services;
using DeliveryHub.Application.Orders.Validation;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DeliveryHub.Application
{
    /// <summary>
    /// Предоставляет методы регистрации внедрения зависимостей для слоя Application.
    /// </summary>
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IValidator<CreateOrderDto>, CreateOrderDtoValidator>();
            return services;
        }
    }
}
