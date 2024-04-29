using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestitiLibri.Utilities
{
    internal class ConnectionItem
    {
        private string connessioneString;
        private static ConnectionItem istanza;

        private ConnectionItem()
        {
            connessioneString = null;
        }

        public static ConnectionItem getIstanza()
        {
            if (istanza == null)
                return new ConnectionItem();
            return istanza;
        }

        public string GetCredenziali()
        {
            if (connessioneString == null)
            {

                ConfigurationBuilder builder = new ConfigurationBuilder();
                builder.SetBasePath(Directory.GetCurrentDirectory());
                builder.AddJsonFile("C:\\Users\\Utente\\source\\repos\\PrestitiLibri\\PrestitiLibri\\Utilities\\connessione.json", false, false);

                IConfiguration configuration = builder.Build();
                connessioneString = configuration.GetConnectionString("ServerLocale");
            }

            return connessioneString;
        }



    }
}
