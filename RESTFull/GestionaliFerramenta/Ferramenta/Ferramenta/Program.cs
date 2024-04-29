namespace Ferramenta
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseCors(builder =>
             builder
             .WithOrigins("*")
             .AllowAnyMethod()
             .AllowAnyHeader());


            app.MapControllers();

            app.Run();
        }
    }
}
