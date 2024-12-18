// using Microsoft.AspNetCore.Http;
// using System.Threading.Tasks;

// namespace TaskManagementAPI.Middleware
// {
//     public class RoleMiddleware
//     {
//         private readonly RequestDelegate _next;

//         public RoleMiddleware(RequestDelegate next)
//         {
//             _next = next;
//         }

//         public async Task Invoke(HttpContext context)
//         {
//             // Example: Mock role extraction from query string
//             var role = context.Request.Query["role"];
//             context.Items["UserRole"] = role;

//             await _next(context);
//         }
//     }
// }
public class RoleMiddleware
{
    private readonly RequestDelegate _next;

    public RoleMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        // Simulate extracting role and user ID from query string or headers
        var role = context.Request.Query["role"];
        var userId = context.Request.Query["userId"];

        context.Items["UserRole"] = role;
        context.Items["UserId"] = userId;

        await _next(context);
    }
}
