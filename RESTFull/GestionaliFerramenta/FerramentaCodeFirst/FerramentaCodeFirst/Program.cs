
using FerramentaCodeFirst.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace FerramentaCodeFirst
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ContattoContext>(options => options.UseSqlServer(
               builder.Configuration.GetConnectionString("DefaultConnection")));

            /*builder.Services.AddScoped<IScoperta<CorpoCeleste>, CorpoCelesteRepo>();
            builder.Services.AddScoped<IRepo<CorpoCeleste>, CorpoCelesteRepo>();
            builder.Services.AddScoped<IRepo<Sistema>, SistemaRepo>();*/

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
