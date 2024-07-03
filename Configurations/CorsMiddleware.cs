public class CorsMiddleware
{
    private readonly RequestDelegate _next;

    public CorsMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public Task Invoke(HttpContext httpContext)
    {
        var origin = httpContext.Request.Headers["Origin"].ToString();
        if (!string.IsNullOrEmpty(origin))
        {
            httpContext.Response.Headers.Add("Access-Control-Allow-Origin", origin);
            httpContext.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
            httpContext.Response.Headers.Add("Access-Control-Max-Age", "86400"); // Cache for 1 day
        }

        if (httpContext.Request.Method == HttpMethods.Options)
        {
            httpContext.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
            var requestedHeaders = httpContext.Request.Headers["Access-Control-Request-Headers"].ToString();
            if (!string.IsNullOrEmpty(requestedHeaders))
            {
                httpContext.Response.Headers.Add("Access-Control-Allow-Headers", requestedHeaders);
            }

            httpContext.Response.StatusCode = StatusCodes.Status200OK;
            return Task.CompletedTask;
        }

        return _next(httpContext);
    }
}