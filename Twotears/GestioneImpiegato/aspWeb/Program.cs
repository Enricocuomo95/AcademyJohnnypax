using Microsoft.EntityFrameworkCore;

using aspWeb.Models;
using aspWeb.Services;
using aspWeb.Repositoris;

namespace aspWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ImpiegatoContext>(
                options => options.UseSqlServer(
                    builder.Configuration.GetConnectionString("Locale")
                    )
                );
            builder.Services.AddScoped<CittaRepo>();
            builder.Services.AddScoped<IRepository<Impiegato>, ImpiegatoRepo>();
            builder.Services.AddScoped<IRepository<Provincium>, ProvinciaRepo>();
            builder.Services.AddScoped<IRepository<Reparto>, RepartoRepo>();
            builder.Services.AddScoped<ImpiegatoService>();
            builder.Services.AddScoped<CittaService>();
            builder.Services.AddScoped<ProvinciaService>();
            builder.Services.AddScoped<RepartoService>();

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
            
            //sto abbilitando la mia appa a gestire sessioni
            app.UseSession();
            app.UseAuthorization();

            app.UseCors(builder =>
                    builder
                    .WithOrigins("*")
                    .AllowAnyMethod()
                    .AllowAnyHeader());


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Impiegato}/{action=Insert}");

            app.Run();
        }
    }
}
