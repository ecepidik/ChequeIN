using System;
using System.Collections.Generic;
using System.Linq;
using ChequeIN.Models;

namespace ChequeIN.Database
{
    public static class Users
    {
        public static IEnumerable<UserProfile> GetAllUsers()
        {
            using (var context = new DatabaseContext())
            {

                context.Database.EnsureCreated();

                var officers = (IEnumerable<Models.UserProfile>)context.FinancialOfficers.ToList().Select(x => (Models.UserProfile)x);
                var admins = (IEnumerable<Models.UserProfile>)context.FinancialAdministrators.ToList().Select(x => (Models.UserProfile)x);

                return officers.Concat(admins);
            }
        }

        public static bool TryGetUserById(string id, out UserProfile user)
        {
            using (var context = new DatabaseContext())
            {

                context.Database.EnsureCreated();
                var officers = from v in context.FinancialOfficers
                               where v.UserProfileID == id
                               select v;

                var admins = from v in context.FinancialAdministrators
                             where v.UserProfileID == id
                             select v;

                if (officers.Any())
                {
                    user = officers.First();
                    return true;
                }
                if (admins.Any())
                {
                    user = admins.First();
                    return true;
                }
                user = null;
                return true;
            }
        }

        public static UserProfile GetCurrentUser(System.Security.Claims.ClaimsPrincipal identity)
        {
            var id = identity.Identities.First().Claims.ElementAt(1).Value;
            var exists = TryGetUserById(id, out UserProfile user);
            if (!exists)
            {
                // TODO: Handle when a user is authenticated, but not in the DB
                return null;
            }
            return user;
        }
    }
}
