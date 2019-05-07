using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using Utility.Error.Application.Exceptions;

namespace Utility.Error.Api.Extensions
{
    /// <summary>
    /// CustomExceptionFilterAttribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        // Public Methods.
        #region PublicMethods

        /// <summary>
        /// OnException.
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case ValidationException _:
                    context.HttpContext.Response.ContentType = "application/json";
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    context.Result = new JsonResult(((ValidationException)context.Exception).Failures);
                    return;
                case NotFoundException _:
                    context.HttpContext.Response.ContentType = "application/json";
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    context.Result = new JsonResult(((NotFoundException)context.Exception).Message);
                    return;
            }

            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Result = new JsonResult(new
            {
                error = new[] { context.Exception.Message },
                stackTrace = context.Exception.StackTrace
            });
        }

        #endregion
    }
}

