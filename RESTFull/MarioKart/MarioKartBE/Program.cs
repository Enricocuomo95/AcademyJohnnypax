
using MarioKart.Models;
using MarioKart.Repositoris;
using MarioKart.Services;
using Microsoft.EntityFrameworkCore;

namespace MarioKart
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

            builder.Services.AddDbContext<MarioKartContext>(options => options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IRepository<Giocatore>, GiocatoreRepo>();
            builder.Services.AddScoped<IRepository<Personaggio>, PersonaggioRepo>();
            builder.Services.AddScoped<GiocatoreService>();
            builder.Services.AddScoped<PersonaggioService>();

            var app = builder.Build();

            app.UseCors(builder =>
                builder
                .WithOrigins("*")
                .AllowAnyMethod()
                .AllowAnyHeader());

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
