using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using PIM.Model.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PIM.Filters
{
    public class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, context.Exception.Message);

            int statusCode = (int)HttpStatusCode.InternalServerError;
            context.HttpContext.Response.StatusCode = statusCode;
            context.Result = new JsonResult(new ExceptionResponse
            {
                StatusCode = statusCode,
                Message = context.Exception.Message
            });
        }
    }
}
