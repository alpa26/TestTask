
using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Services;

namespace TestTask
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            // Add services to the container.
            builder.Services.AddTransient<Repository>();
            builder.Services.AddTransient<GameRepository>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseDefaultFiles(new DefaultFilesOptions
            {
                DefaultFileNames = new
    List<string> { "index.html" }
            });
            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }
    }
}
