using Academy.Application.Interfaces;
using Academy.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Academy.WebAPI.ErrorHandling
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private IErrorLogAppService _errorLogAppService;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IErrorLogAppService errorLogAppService)
        {
            try
            {
                _errorLogAppService = errorLogAppService;
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;

            _errorLogAppService.Register(new ErrorLog()
            {
                ErrorLogId = Guid.NewGuid(),
                Message = GetErrorMessage(exception),
                ExceptionString = exception.ToString()
            });

            var result = JsonConvert.SerializeObject(new { error = "Some error(s) occoured." });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }

        private static string GetErrorMessage(Exception exception)
        {
            string message = exception?.InnerException?.Message;

            if (string.IsNullOrWhiteSpace(message))
                message = exception?.Message;

            return message;
        }
    }
}
