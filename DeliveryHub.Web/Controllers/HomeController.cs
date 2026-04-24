using DeliveryHub.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DeliveryHub.Web.Controllers
{
    /// <summary>
    /// MVC-контроллер для системных страниц приложения.
    /// Содержит страницы ошибок и служебные страницы.
    /// </summary>
    public sealed class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="HomeController"/>.
        /// </summary>
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Отображает страницу ошибки приложения.
        /// </summary>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var requestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

            _logger.LogWarning(
                "Пользователю отображена страница ошибки. RequestId: {RequestId}",
                requestId);

            return View(new ErrorViewModel
            {
                RequestId = requestId
            });
        }

        /// <summary>
        /// Отображает страницу 404 для несуществующих ресурсов.
        /// </summary>
        /// <returns>HTML-страница 404.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult NotFound()
        {
            Response.StatusCode = StatusCodes.Status404NotFound;

            return View();
        }
    }
}
