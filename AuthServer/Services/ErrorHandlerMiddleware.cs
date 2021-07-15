using AuthServer.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace AuthServer.Services
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlerMiddleware( RequestDelegate next )
        {
            this.next = next;
        }

        public async Task Invoke( HttpContext context )
        {
            try
            {
                await next( context );
            }
            catch ( Exception error )
            {
                var response = context.Response;
                response.ContentType = "application/json";

                response.StatusCode = error switch
                {
                    AppException e => (int)HttpStatusCode.BadRequest,
                    KeyNotFoundException e => (int)HttpStatusCode.NotFound,
                    AuthException e => (int)HttpStatusCode.Unauthorized,
                    _ => (int)HttpStatusCode.InternalServerError,
                };

                var result = JsonSerializer.Serialize( new { message = error?.Message } );
                await response.WriteAsync( result );
            }
        }
    }
}
