using ProjectManager.Domain.UnitOfWork;

namespace ProjectManager.Api.Middleware
{
    public class UnitOfWorkMiddleware(RequestDelegate next)
    {
        readonly RequestDelegate next = next;

        public async Task InvokeAsync(HttpContext context, IUnitOfWork unitOfWork)
        {
            // Call the next delegate/middleware in the pipeline.
            await next(context);

            await unitOfWork.SaveAsync(context.RequestAborted);
        }
    }
}