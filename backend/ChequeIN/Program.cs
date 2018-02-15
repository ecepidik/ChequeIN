using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ChequeIN
{
    public class Program
    {
        public static void Main(string[] args)
        {

            Console.WriteLine("Hello, world!");

            using (var context = new DatabaseContext ()) {

                // Create the database if it does not exist
                context.Database.EnsureCreated ();

                var profile = new Model.FinancialOfficer();
                profile.Email = "a@a.com";
                profile.UserProfileID = 2001;
                context.FinancialOfficers.Add (profile);
                
                // Save changes to the database
                context.SaveChanges ();

                // Fetch all financial officers
                Console.WriteLine ("Current financial officers");
                foreach (var user in context.FinancialOfficers.ToList ()) {
                    Console.WriteLine ($"{user.Email} - {user.UserProfileID}");
                }

            }




            BuildWebHost(args).Run();

        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
