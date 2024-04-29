using Microsoft.EntityFrameworkCore;
using PalestraMVC.Models;
using PalestraMVC.Repository;
using PalestraMVC.Service;

namespace PalestraMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<PalestraContext>(options => options.UseSqlServer(
                builder.Configuration.GetConnectionString("Locale")));

            builder.Services.AddScoped<IRepository<Corso>, CorsoRepository>();
            builder.Services.AddScoped<IRepository<Utente>, UtenteRepository>();
            builder.Services.AddScoped<IService<Utente>, UtenteService>();
            builder.Services.AddScoped<IService<Corso>, CorsoService>();

            //setta la mia sessione nella memoria del server
            //il server attiva la mia sessione. Se la sessione non viene più sollecitata per 3 min
            // la sessione scade!!!!
            builder.Services.AddSession(options => { options.IdleTimeout = TimeSpan.FromMinutes(3); });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(builder =>
                builder
                .WithOrigins("*")
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Utente}/{action=Login}");

            app.Run();
        }
    }
}
