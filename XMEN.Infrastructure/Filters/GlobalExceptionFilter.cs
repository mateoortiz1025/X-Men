using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using XMEN.Core.Exceptions;

namespace XMEN.Infrastructure.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception.GetType() == typeof(BusinessException))
            {

                var exception = (BusinessException)context.Exception;
                var validation = new
                {
                    Status = 400,
                    Tittle = "Bad Request",
                    Detail = exception.Message
                };
                var json = new
                {
                    errors = new[] { validation }
                };
                context.Result = new NotFoundObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.ExceptionHandled = true;
            }
        }
    }
}
