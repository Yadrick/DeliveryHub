using DeliveryHub.Application.Abstractions;
using DeliveryHub.Application.Common.Exceptions;
using DeliveryHub.Application.Orders.Dto;
using DeliveryHub.Domain.Entities;
using FluentValidation;

namespace DeliveryHub.Application.Orders.Services
{
    public sealed class OrderService : IOrderService
    {
        private const string OrderNumberPrefix = "ORD";
        private const int MaxOrderNumberGenerationAttempts = 5;

        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateOrderDto> _createOrderValidator;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="OrderService"/>.
        /// </summary>
        public OrderService(
            IOrderRepository orderRepository,
            IUnitOfWork unitOfWork,
            IValidator<CreateOrderDto> createOrderValidator)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _createOrderValidator = createOrderValidator;
        }

        /// <inheritdoc/>
        public async Task<CreatedOrderDto> CreateAsync(CreateOrderDto dto, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(dto);

            await _createOrderValidator.ValidateAndThrowAsync(dto, cancellationToken);

            var createdAtUtc = DateTime.UtcNow;
            var orderNumber = await GenerateOrderNumberAsync(createdAtUtc, cancellationToken);

            var order = DeliveryOrder.Create(
                orderNumber,
                dto.SenderCity,
                dto.SenderAddress,
                dto.RecipientCity,
                dto.RecipientAddress,
                dto.CargoWeightKg,
                dto.PickupDate,
                createdAtUtc);

            await _orderRepository.AddAsync(order, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new CreatedOrderDto(order.Id, order.OrderNumber);
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyList<OrderListItemDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var orders = await _orderRepository.GetAllAsync(cancellationToken);

            return orders
                .OrderByDescending(order => order.CreatedAtUtc)
                .Select(order => new OrderListItemDto(
                    order.Id,
                    order.OrderNumber,
                    order.SenderCity,
                    order.SenderAddress,
                    order.RecipientCity,
                    order.RecipientAddress,
                    order.CargoWeightKg,
                    order.PickupDate,
                    order.CreatedAtUtc))
                .ToList();
        }

        /// <inheritdoc/>
        public async Task<OrderDetailsDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty) throw NotFoundException.ForOrder(id);

            var order = await _orderRepository.GetByIdAsync(id, cancellationToken);
            if (order is null) throw NotFoundException.ForOrder(id);

            return new OrderDetailsDto(
                order.Id,
                order.OrderNumber,
                order.SenderCity,
                order.SenderAddress,
                order.RecipientCity,
                order.RecipientAddress,
                order.CargoWeightKg,
                order.PickupDate,
                order.CreatedAtUtc);
        }


        /// <summary>
        /// Генерирует уникальный номер заказа.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Выбрасывается исключение, если после нескольких попыток не удается сгенерировать уникальный номер заказа.
        /// </exception>
        private async Task<string> GenerateOrderNumberAsync(
            DateTime createdAtUtc,
            CancellationToken cancellationToken)
        {
            var date = DateOnly.FromDateTime(createdAtUtc);
            var existingOrdersCount = await _orderRepository.CountCreatedOnDateAsync(date, cancellationToken);

            for (var attempt = 1; attempt <= MaxOrderNumberGenerationAttempts; attempt++)
            {
                var sequence = existingOrdersCount + attempt;
                var orderNumber = FormatOrderNumber(date, sequence);

                var exists = await _orderRepository.ExistsByOrderNumberAsync(orderNumber, cancellationToken);
                if (!exists) return orderNumber;
            }
            throw new InvalidOperationException("Не удалось сгенерировать уникальный номер заказа.");
        }

        /// <summary>
        /// Форматирует номер заказа.
        /// </summary>
        private static string FormatOrderNumber(DateOnly date, int sequence)
        {
            return $"{OrderNumberPrefix}-{date:yyyyMMdd}-{sequence:000000}";
        }
    }
}
