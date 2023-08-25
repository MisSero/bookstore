using Microsoft.EntityFrameworkCore;
using Bookstore.DAL;
using Bookstore.DAL.Interfaces;
using Bookstore.DAL.Repositories;
using Bookstore.Domain.Interfaces;
using Bookstore.Domain.Services;

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
            builder.Services.AddScoped<IBookService, BookService>();
            

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