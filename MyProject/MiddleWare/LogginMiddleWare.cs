namespace MyProject.Api.MiddleWare
{
    public class LogginMiddleWare
    {
        private readonly RequestDelegate _next;
        public LogginMiddleWare(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            // לוגיקה לפני העיבוד
            Console.WriteLine("Before processing request");

            // קריאה ל-Middleware הבא בשרשרת
            await _next(context);

            // לוגיקה אחרי העיבוד
            Console.WriteLine("After processing request");
        }

    }
    //הוספתי 
    public static class LogMiddlewareExtensions
    {
        public static IApplicationBuilder UserLogMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LogginMiddleWare>();
        }
    }
}
