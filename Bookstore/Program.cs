using Microsoft.EntityFrameworkCore;
using Bookstore.DAL;

namespace Bookstore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
       
            var connectingString = builder.Configuration.GetConnectionString("MSSQL");
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectingString);
            });

            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapControllers();
            app.UseRouting();

            app.Run();
        }
    }
}