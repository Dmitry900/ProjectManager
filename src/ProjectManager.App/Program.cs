using Microsoft.EntityFrameworkCore;
using ProjectManager.Core;
using ProjectManager.Core.Profiles;

namespace ProjectManager.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;
            var configuration = builder.Configuration;

            services.AddContext(options => options.UseSqlite(configuration["ConnectionString"]))
                    .AddApplicationServices();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddControllers();

            services.AddAutoMapper(it => it.AddProfile<MappingProfile>());

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.MapControllers();

            app.Run();
        }
    }
}
