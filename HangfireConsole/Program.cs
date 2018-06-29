using Hangfire;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HangfireConsole
{
    class Program
    {
        private static readonly AutoResetEvent autoResetEvent = new AutoResetEvent(false);
        static void Main(string[] args)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            GlobalConfiguration.Configuration.UseSqlServerStorage(connectionString);

            using (var server = new BackgroundJobServer())
            {
                Console.WriteLine("Hangfire Server started.");
                Console.CancelKeyPress += (o, e) =>
                {
                    server.Dispose();
                    Console.WriteLine("Terminating...");
                    autoResetEvent.Set();
                };

                autoResetEvent.WaitOne();
            }
        }
    }
}
