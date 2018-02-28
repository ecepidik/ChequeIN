﻿// <auto-generated />
using ChequeIN;
using ChequeIN.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;

namespace ChequeIN.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452");

            modelBuilder.Entity("ChequeIN.Models.AccountType", b =>
                {
                    b.Property<int>("AccountTypeID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("LedgerAccountID");

                    b.Property<int>("Type");

                    b.Property<int?>("UserProfileID");

                    b.HasKey("AccountTypeID");

                    b.HasIndex("LedgerAccountID")
                        .IsUnique();

                    b.HasIndex("UserProfileID");

                    b.ToTable("AccountType");
                });

            modelBuilder.Entity("ChequeIN.Models.ChequeReq", b =>
                {
                    b.Property<int>("ChequeReqID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApprovedBy");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<bool>("FreeFood");

                    b.Property<float>("GST");

                    b.Property<float>("HST");

                    b.Property<int>("LedgerAccountID");

                    b.Property<int?>("MailingAddressID");

                    b.Property<bool>("OnlinePurchases");

                    b.Property<float>("PST");

                    b.Property<string>("PayeeName")
                        .IsRequired();

                    b.Property<float>("PreTax");

                    b.Property<bool>("ToBeMailed");

                    b.Property<int>("UserProfileID");

                    b.HasKey("ChequeReqID");

                    b.HasIndex("LedgerAccountID");

                    b.HasIndex("MailingAddressID");

                    b.HasIndex("UserProfileID");

                    b.ToTable("ChequeReqs");
                });

            modelBuilder.Entity("ChequeIN.Models.LedgerAccount", b =>
                {
                    b.Property<int>("LedgerAccountID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("Number");

                    b.HasKey("LedgerAccountID");

                    b.ToTable("LedgerAccounts");
                });

            modelBuilder.Entity("ChequeIN.Models.MailingAddress", b =>
                {
                    b.Property<int>("MailingAddressID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City")
                        .IsRequired();

                    b.Property<string>("Line1")
                        .IsRequired();

                    b.Property<string>("Line2");

                    b.Property<string>("PostalCode")
                        .IsRequired();

                    b.Property<int>("Province");

                    b.HasKey("MailingAddressID");

                    b.ToTable("MailingAddress");
                });

            modelBuilder.Entity("ChequeIN.Models.Status", b =>
                {
                    b.Property<int>("StatusID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AdministratorApprover");

                    b.Property<int?>("ChequeReqID");

                    b.Property<string>("Feedback");

                    b.Property<int>("SelectedStatus");

                    b.Property<DateTime>("StatusDate");

                    b.HasKey("StatusID");

                    b.HasIndex("ChequeReqID");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("ChequeIN.Models.SupportingDocument", b =>
                {
                    b.Property<int>("SupportingDocumentID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ChequeReqID");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<long>("FileIdentifier");

                    b.HasKey("SupportingDocumentID");

                    b.HasIndex("ChequeReqID");

                    b.ToTable("SupportingDocument");
                });

            modelBuilder.Entity("ChequeIN.Models.UserProfile", b =>
                {
                    b.Property<int>("UserProfileID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AuthenticationIdentifier")
                        .IsRequired();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.HasKey("UserProfileID");

                    b.ToTable("UserProfiles");

                    b.HasDiscriminator<string>("Discriminator").HasValue("UserProfile");
                });

            modelBuilder.Entity("ChequeIN.Models.FinancialAdministrator", b =>
                {
                    b.HasBaseType("ChequeIN.Models.UserProfile");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.ToTable("FinancialAdministrator");

                    b.HasDiscriminator().HasValue("FinancialAdministrator");
                });

            modelBuilder.Entity("ChequeIN.Models.FinancialOfficer", b =>
                {
                    b.HasBaseType("ChequeIN.Models.UserProfile");


                    b.ToTable("FinancialOfficer");

                    b.HasDiscriminator().HasValue("FinancialOfficer");
                });

            modelBuilder.Entity("ChequeIN.Models.AccountType", b =>
                {
                    b.HasOne("ChequeIN.Models.LedgerAccount")
                        .WithOne("Group")
                        .HasForeignKey("ChequeIN.Models.AccountType", "LedgerAccountID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ChequeIN.Models.UserProfile")
                        .WithMany("AuthorizedAccountGroups")
                        .HasForeignKey("UserProfileID");
                });

            modelBuilder.Entity("ChequeIN.Models.ChequeReq", b =>
                {
                    b.HasOne("ChequeIN.Models.LedgerAccount", "AssociatedAccount")
                        .WithMany("ChequeReqs")
                        .HasForeignKey("LedgerAccountID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ChequeIN.Models.MailingAddress", "MailingAddress")
                        .WithMany()
                        .HasForeignKey("MailingAddressID");

                    b.HasOne("ChequeIN.Models.UserProfile", "Submitter")
                        .WithMany("SubmittedChequeReqs")
                        .HasForeignKey("UserProfileID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ChequeIN.Models.Status", b =>
                {
                    b.HasOne("ChequeIN.Models.ChequeReq")
                        .WithMany("StatusHistory")
                        .HasForeignKey("ChequeReqID");
                });

            modelBuilder.Entity("ChequeIN.Models.SupportingDocument", b =>
                {
                    b.HasOne("ChequeIN.Models.ChequeReq")
                        .WithMany("SupportingDocuments")
                        .HasForeignKey("ChequeReqID");
                });
#pragma warning restore 612, 618
        }
    }
}
