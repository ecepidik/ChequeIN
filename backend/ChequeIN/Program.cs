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

            addFinancialOfficer("alex@hotmail.com", new Random().Next(1000));

            var user = getFinancialOfficerFromId(2001);
            if (user != null)
            {
                Console.WriteLine(user.Email);
            }
            else
            {
                Console.WriteLine("No user found.");
            }
            

            BuildWebHost(args).Run();

        }

        public static void addFinancialOfficer(string email, long input_id)
        {

            using (var context = new DatabaseContext ()) {

                // Create the database if it does not exist
                context.Database.EnsureCreated ();

                var profile = new Model.FinancialOfficer();
                profile.Email = email;
                profile.UserProfileID = input_id;
                context.FinancialOfficers.Add (profile);
                
                // Save changes to the database
                context.SaveChanges ();

            }

        }
        public static Model.FinancialOfficer getFinancialOfficerFromId(long input_id)
        {

            using (var context = new DatabaseContext ()) {

                // Create the database if it does not exist
                context.Database.EnsureCreated ();

                var user = from v in context.FinancialOfficers where v.UserProfileID == input_id
                select v;

                return user.First();

            }

        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
