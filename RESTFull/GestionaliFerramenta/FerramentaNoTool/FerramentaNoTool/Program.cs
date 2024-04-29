
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;

namespace FerramentaNoTool
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



                builder.Services.AddDbContext<FeatureContext>(options => options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")));

            //add questo
            //builder.Services.AddSingleton<IApplicationBuilder, ApplicationBuilder>();
            //mettiamo uno scope ovvero un contesto che regola la nostra connessioone
            //ricorda che abbiamo 10 connection pool di default
            //non dobbiam saturali se no morte del software nooooo
            //builder.Services.AddScoped(...);

            //aggiungo lo scope per il prodotto service, in modo che segui lo stesso comportamento della repo
            //builder.Service.AddScope<IService<Prodotto>, ProdottoService()>
            //IService è un interfaccia vuota che viene implementata al fine di disaccopiare del tutto la mia logica di busines
            //con l'implementazione del server
            //le proprietà di scope o di singleton possono essere impostate anche senza interfaccia
            //questa soluzione tuttavia è una good-practice
            //1. obbligo così lo sviluppatore ad avere un comportamento comune
            //2. quando INNIETTO (tramite lo scope) il mio oggetto ho a disposizione solo i metodi che posso accedere secondo la mia logica di business
            //3. la logica di businness è gestita solo e solamente dal mio service(una sorta di servlet in java)
            //gestita infatti come un bean, verrà istanziata solo all'atto della prima chiamata e
            //terminerà la sua vita quando il mio gestore eventi non riceverà più eventi (il tutto automatizzato dal mio framework).
            //CI piace!!!!
            //se dobbiamo chiamre un metodo personalizzato non possiamo con questo approccio, dobbiamo farlo senza IService
            //per tale raggione abbiamo un doppio metodo per settare lo scope 


            /*questa configurazione serve all'observer per tenere i metodi in ascolto e fornirli all http
             * in modo tale che il mio applicativo lato frontend possa ricevere/mandare richieste al mio server
             * o ricevere oggetti in formato json
             * 
             * 
             * app.UseCors(builder =>
                 builder
                 .WithOrigins("*")
                 .AllowAnyMethod()
                 .AllowAnyHeader());
             * 
             */

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
