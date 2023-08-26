using Microsoft.EntityFrameworkCore;
using Bookstore.DAL;
using Bookstore.DAL.Interfaces;
using Bookstore.DAL.Repositories;
using Bookstore.Domain.Interfaces;
using Bookstore.Domain.Services;
using Bookstore.Middleware;

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

            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();

            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<IOrderService, OrderService>();

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

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.Run();
        }
    }
}