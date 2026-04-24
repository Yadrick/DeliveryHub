using DeliveryHub.Application.Common.Exceptions;
using FluentValidation;
using System.Net;

namespace DeliveryHub.Web.Middleware
{
    /// <summary>
    /// Middleware для глобальной обработки необработанных исключений приложения.
    /// Логирует ошибку и перенаправляет пользователя на корректную страницу ошибки.
    /// </summary>
    public sealed class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="GlobalExceptionHandlingMiddleware"/>.
        /// </summary>
        public GlobalExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Обрабатывает HTTP-запрос и перехватывает необработанные исключения.
        /// </summary>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (OperationCanceledException) when (context.RequestAborted.IsCancellationRequested)
            {
                _logger.LogInformation(
                    "Запрос был отменён клиентом. Path: {Path}, TraceId: {TraceId}",
                    context.Request.Path,
                    context.TraceIdentifier);
            }
            catch (NotFoundException exception)
            {
                _logger.LogWarning(
                    exception,
                    "Ресурс не найден. Path: {Path}, TraceId: {TraceId}",
                    context.Request.Path,
                    context.TraceIdentifier);

                context.Response.Redirect("/Home/NotFound");
            }
            catch (ValidationException exception)
            {
                _logger.LogWarning(
                    exception,
                    "Ошибка валидации на уровне приложения. Path: {Path}, TraceId: {TraceId}",
                    context.Request.Path,
                    context.TraceIdentifier);

                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.Redirect("/Home/Error");
            }
            catch (Exception exception)
            {
                _logger.LogError(
                    exception,
                    "Необработанная ошибка приложения. Path: {Path}, TraceId: {TraceId}",
                    context.Request.Path,
                    context.TraceIdentifier);

                context.Response.Redirect("/Home/Error");
            }
        }
    }
}
