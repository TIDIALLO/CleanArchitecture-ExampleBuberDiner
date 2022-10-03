using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace BuberDinner.Api.Middleware;

public class ErrorHandlingMiddleware{
    private readonly RequestDelegate next;

		public ErrorHandlingMiddleware(RequestDelegate next)
		{
			this.next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await next(context);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(context, ex);
			}
		}

		private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			if (exception == null) { return; }

			const HttpStatusCode code = HttpStatusCode.InternalServerError;
			await WriteExceptionAsync(context, exception, code).ConfigureAwait(false);
		}

		private static async Task<Task> WriteExceptionAsync(HttpContext context, Exception exception, HttpStatusCode code)
		{
			var  response = JsonSerializer.Serialize(new
				{
					error = "An error occurred while processing your request"
				});

			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)code;
            return  context.Response.WriteAsync(response);
		}
}
