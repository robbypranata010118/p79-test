using P79.Application.Exceptions;
using P79.Base.Wrappers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Builder;

namespace P79.Api.Admin.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next , ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new Response<string>() { Succeeded = false, Message = error?.Message };
                switch (error)
                {
                    //case Application.Exceptions.ApiException e:
                    //    // custom application error
                    //    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    //    break;
                    case ValidationException e:
                        // custom application error
                        responseModel.StatusCode = (int)HttpStatusCode.BadRequest;
                        responseModel.Errors = e.Errors;
                        _logger.LogError(e.InnerException != null ? e.InnerException.Message : e.Message + string.Format("({0})",JsonConvert.SerializeObject(e.Errors)));
                        break;
                    case BadRequestException e:
                        // custom application error
                        responseModel.StatusCode = (int)HttpStatusCode.BadRequest;
                        responseModel.Message = e.Message;
                        _logger.LogError(e.InnerException != null ? e.InnerException.Message : e.Message);
                        break;
                    case KeyNotFoundException e:
                        // not found error
                        responseModel.StatusCode = (int)HttpStatusCode.NotFound;
                        responseModel.Message = e.Message;
                        _logger.LogError(e.InnerException != null ? e.InnerException.Message : e.Message);
                        break;
                    case DuplicateException e:
                        // not found error
                        responseModel.StatusCode = 4001;
                        responseModel.Message = e.Message;
                        _logger.LogError(e.InnerException != null ? e.InnerException.Message : e.Message);
                        break;
                    case ForbiddenException e:
                        // not found error
                        responseModel.StatusCode = (int)HttpStatusCode.Forbidden;
                        responseModel.Message = e.Message;
                        _logger.LogError(e.InnerException != null ? e.InnerException.Message : e.Message);
                        break;
                    case DbUpdateException e:
                        if (e.InnerException != null)
                        {
                            SqlException sqlException = e.InnerException as SqlException;
                            if (sqlException != null)
                            {
                                if (sqlException.Number == 547)
                                {
                                    responseModel.StatusCode = (int)HttpStatusCode.BadRequest * 10 + 1;//conflict with constraint
                                    responseModel.Message = e.InnerException.Message;
                                    _logger.LogError(e.InnerException != null ? e.InnerException.Message : e.Message);
                                }
                            }
                            else
                            {
                                responseModel.StatusCode = (int)HttpStatusCode.BadRequest * 10 + 1;//conflict with constraint
                                responseModel.Message = e.InnerException.Message;
                                _logger.LogError(e.InnerException != null ? e.InnerException.Message : e.Message);
                            }
                        }
                        else {
                            responseModel.StatusCode = (int)HttpStatusCode.BadRequest * 10 + 0;//unhandled exception
                            responseModel.Message = e.Message;
                            _logger.LogError(e.InnerException != null ? e.InnerException.Message : e.Message);
                        }
                        break;
                    default:
                        // unhandled error
                        responseModel.StatusCode = (int)HttpStatusCode.InternalServerError;
                        _logger.LogError("Unhandled Error");
                        break;
                }
                var result = JsonConvert.SerializeObject(responseModel);
                await response.WriteAsync(result);
            }
        }
    }

    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
