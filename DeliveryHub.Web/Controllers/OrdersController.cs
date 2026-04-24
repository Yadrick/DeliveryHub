using DeliveryHub.Application.Abstractions;
using DeliveryHub.Application.Common.Exceptions;
using DeliveryHub.Application.Orders.Dto;
using DeliveryHub.Web.Models.Orders;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryHub.Web.Controllers
{
    public sealed class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrdersController> _logger;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="OrdersController"/>.
        /// </summary>
        public OrdersController(
            IOrderService orderService,
            ILogger<OrdersController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        /// <summary>
        /// Отображает список всех созданных заказов.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var orders = await _orderService.GetAllAsync(cancellationToken);

            var viewModel = orders
                .Select(order => new OrderListItemViewModel
                {
                    Id = order.Id,
                    OrderNumber = order.OrderNumber,
                    SenderCity = order.SenderCity,
                    SenderAddress = order.SenderAddress,
                    RecipientCity = order.RecipientCity,
                    RecipientAddress = order.RecipientAddress,
                    CargoWeightKg = order.CargoWeightKg,
                    PickupDate = order.PickupDate,
                    CreatedAtUtc = order.CreatedAtUtc
                })
                .ToList();

            return View(viewModel);
        }

        /// <summary>
        /// Отображает форму создания нового заказа.
        /// </summary>
        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new CreateOrderViewModel
            {
                PickupDate = DateOnly.FromDateTime(DateTime.Today)
            };

            return View(viewModel);
        }

        /// <summary>
        /// Обрабатывает отправку формы создания нового заказа.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateOrderViewModel viewModel, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var dto = new CreateOrderDto
            {
                SenderCity = viewModel.SenderCity,
                SenderAddress = viewModel.SenderAddress,
                RecipientCity = viewModel.RecipientCity,
                RecipientAddress = viewModel.RecipientAddress,
                CargoWeightKg = viewModel.CargoWeightKg!.Value,
                PickupDate = viewModel.PickupDate!.Value
            };

            try
            {
                var createdOrder = await _orderService.CreateAsync(dto, cancellationToken);

                TempData["SuccessMessage"] = $"Заказ {createdOrder.OrderNumber} успешно создан.";

                return RedirectToAction(nameof(Index));
            }
            catch (ValidationException exception)
            {
                foreach (var error in exception.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                return View(viewModel);
            }
        }

        /// <summary>
        /// Отображает созданный заказ в режиме чтения.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Details(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var order = await _orderService.GetByIdAsync(id, cancellationToken);

                var viewModel = new OrderDetailsViewModel
                {
                    Id = order.Id,
                    OrderNumber = order.OrderNumber,
                    SenderCity = order.SenderCity,
                    SenderAddress = order.SenderAddress,
                    RecipientCity = order.RecipientCity,
                    RecipientAddress = order.RecipientAddress,
                    CargoWeightKg = order.CargoWeightKg,
                    PickupDate = order.PickupDate,
                    CreatedAtUtc = order.CreatedAtUtc
                };

                return View(viewModel);
            }
            catch (NotFoundException exception)
            {
                _logger.LogWarning(exception, "Заказ с идентификатором {OrderId} не найден.", id);

                return NotFound();
            }
        }
    }
}
