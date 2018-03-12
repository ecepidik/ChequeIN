﻿using ChequeIN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Microsoft.EntityFrameworkCore;

namespace ChequeIN.Tests
{
    public class ControllersTests
    {

        private ChequeReq _cheque;
        private FinancialOfficer _officer;

        private DatabaseContext createContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseSqlite("Data Source=chequein_test_controllers.db.sqlite");
            return new DatabaseContext(optionsBuilder.Options);
        }

        private void SetupDatabase(DatabaseContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var ledger = new LedgerAccount()
            {
                Name = "General Expenses",
                Number = 6530,
                Group = Enums.Group.COPIEUS,
            };

            _officer = new FinancialOfficer()
            {
                Email = "alex@hotmail.com",
                AuthenticationIdentifier = "auth0|5a84eafef5c8213cb27c27e2" // TODO: use the real test user ID
            };

            context.Add(ledger as LedgerAccount);
            context.Add(_officer as FinancialOfficer);
            context.SaveChanges();


        }

        [Fact]
        public void ControllerChequeReqs()
        {
            using (var context = createContext())
            {
                SetupDatabase(context);

                var ledgerAccounts = context.LedgerAccounts
                                    .ToList();

                var officers = context.FinancialOfficers
                                    .ToList();
                
                _cheque = new ChequeReq()
                {
                    PreTax = 1,
                    ChequeReqID = 1,
                    GST = 1,
                    PST = 1,
                    HST = 1,
                    PayeeName = "User",
                    Description = "Desc",
                    ApprovedBy = "Kareem Halabi",
                    FreeFood = false,
                    OnlinePurchases = false,
                    ToBeMailed = true,
                    MailingAddress = new MailingAddress() { Line1 = "1645 rue des rigoles", City = "Sherb", PostalCode = "J1M2H2" },
                    SupportingDocuments = new List<SupportingDocument>() { new SupportingDocument() { Description = "blank", FileIdentifier = "123" } },
                    StatusHistory = new List<Status>() { new Status() { } },
                    LedgerAccountID = ledgerAccounts.ElementAt(0).LedgerAccountID,
                    UserProfileID = officers.ElementAt(0).UserProfileID
                };

                context.Add(_cheque as ChequeReq);
                context.SaveChanges();





                var getAllChequeReqs = Database.ChequeReqs.GetAllChequeReqs(context) as List<ChequeReq>;

                var outChequeList = new List<ChequeReq>();
                var tryGetAllChequeReqs = Database.ChequeReqs.TryGetAllChequeReqs(context, _officer.AuthenticationIdentifier, out outChequeList);

                var outCheque = new ChequeReq();
                var get = Database.ChequeReqs.TryGetChequeReq(context, _cheque.ChequeReqID, out outCheque);

                _cheque.PayeeName = "User2";
                Database.ChequeReqs.UpdateChequeReq(context, _cheque);

                _cheque.ChequeReqID = 2;
                Database.ChequeReqs.StoreChequeReq(context, _cheque);

                var outChequeList_2 = new List<ChequeReq>();
                var tryGetAllChequeReqs_2 = Database.ChequeReqs.TryGetAllChequeReqs(context, _officer.AuthenticationIdentifier, out outChequeList_2);

                Assert.Equal(1, getAllChequeReqs.Count);
                Assert.Equal(1, outChequeList.Count);
                Assert.True(outCheque.Equals(_cheque));
                Assert.True(0 == String.Compare(outCheque.PayeeName, _cheque.PayeeName));
                Assert.Equal(2, outChequeList_2.Count);


            }
        }
    }
}
