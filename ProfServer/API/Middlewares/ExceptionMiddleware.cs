using System;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProfServer.Models;

namespace ProfServer.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;
        private readonly JsonSerializerOptions _jsonOptions = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _env = env ?? throw new ArgumentNullException(nameof(env));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Если AggregateException — используем первый внутренний (или flatten)
            var ex = exception switch
            {
                AggregateException agg => agg.Flatten().InnerExceptions.FirstOrDefault() ?? agg,
                _ => exception
            };

            // DomainException трактуем как ошибка валидации/бизнес-правил — BadRequest
            var statusCode = ex switch
            {
                ConflictException => HttpStatusCode.Conflict,
                ArgumentException => HttpStatusCode.BadRequest,
                NotFoundException => HttpStatusCode.NotFound,
                InvalidOperationException => HttpStatusCode.Conflict,
                DomainException => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError
            };

            // Логирование: бизнес/доменные исключения — warning, прочие — error
            if (ex is DomainException)
                _logger.LogWarning(ex, "Domain exception handled (TraceId: {TraceId})", context.TraceIdentifier);
            else
                _logger.LogError(exception, "Unhandled exception (TraceId: {TraceId})", context.TraceIdentifier);

            var problem = new
            {
                type = "about:blank",
                title = statusCode.ToString(),
                status = (int)statusCode,
                // В development возвращаем детальный стэк, в prod — только сообщение
                detail = _env.IsDevelopment() ? ex.ToString() : ex.Message,
                traceId = context.TraceIdentifier
            };

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = (int)statusCode;

            var payload = JsonSerializer.Serialize(problem, _jsonOptions);
            await context.Response.WriteAsync(payload);
        }
    }

    public static class ExceptionMiddlewareExtensions
    {
        // Для вызова в Program.cs: app.UseExceptionMiddleware();
        public static TUse UseExceptionMiddleware<TUse>(this TUse app) where TUse : IApplicationBuilder
        {
            app.UseMiddleware<ExceptionMiddleware>();
            return app;
        }

        // Удобная перегрузка для WebApplication (минимальный хост)
        public static WebApplication UseExceptionMiddleware(this WebApplication app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            return app;
        }
    }
}
