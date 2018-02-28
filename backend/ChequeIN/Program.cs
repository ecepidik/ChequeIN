using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ChequeIN
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            Database.Seed.SeedDatabase();

            using (var context = new DatabaseContext() { IsTest = false }) {
                var test = context.FinancialOfficers.Include(officer => officer.SubmittedChequeReqs).ToList().ElementAt(0).SubmittedChequeReqs.ElementAt(0);
            }

            BuildWebHost(args).Run();

        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
