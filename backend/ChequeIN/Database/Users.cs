using System;
using System.Collections.Generic;
using System.Linq;
using ChequeIN.Models;
using Microsoft.EntityFrameworkCore;

namespace ChequeIN.Database
{
    public static class Users
    {
        public static IEnumerable<UserProfile> GetAllUsers(DatabaseContext context)
        {
            var officers = (IEnumerable<Models.UserProfile>)context.FinancialOfficers.Include("AuthorizedAccountGroups").Select(x => (Models.UserProfile)x);
            var admins = (IEnumerable<Models.UserProfile>)context.FinancialAdministrators.Include("AuthorizedAccountGroups").Select(x => (Models.UserProfile)x);

            return officers.Concat(admins);
        }

        public static bool TryGetUserById(DatabaseContext context, string id, out UserProfile user)
        {
            var officers = context.FinancialOfficers
                                  .Where(f => f.AuthenticationIdentifier == id)
                                  .Include("AuthorizedAccountGroups")
                                  .ToList();

            var admins = context.FinancialAdministrators
                                .Where(f => f.AuthenticationIdentifier == id)
                                .Include("AuthorizedAccountGroups")
                                .ToList();

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

        public static UserProfile GetCurrentUser(DatabaseContext context, System.Security.Claims.ClaimsPrincipal identity, bool disableAuth = false, string developmentUserId = "")
        {
            string id;
            // Give the default user id if auth is disabled and no user is authenticated
            if (disableAuth && !identity.Identities.First().Claims.Any()) {
                id = developmentUserId;
            }
            else {
                id = identity.Identities.First().Claims.ElementAt(1).Value;
            }
            var exists = TryGetUserById(context, id, out UserProfile user);
            if (!exists)
            {
                // TODO: Handle when a user is authenticated, but not in the DB
                return null;
            }
            return user;
        }
    }
}
