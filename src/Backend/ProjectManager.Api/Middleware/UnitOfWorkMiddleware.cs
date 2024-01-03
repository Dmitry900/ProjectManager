using ProjectManager.Domain.Context;

namespace ProjectManager.Api.Middleware
{
    public class UnitOfWorkMiddleware(RequestDelegate next)
    {
        readonly RequestDelegate next = next;

        public async Task InvokeAsync(HttpContext context, ApplicationContext appContext)
        {
            // Call the next delegate/middleware in the pipeline.
            await next(context);

            await appContext.SaveChangesAsync(context.RequestAborted);
        }
    }
}