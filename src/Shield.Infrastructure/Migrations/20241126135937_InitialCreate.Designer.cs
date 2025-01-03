﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using XkliburSolutions.Shield.Infrastructure.Data;

#nullable disable

namespace XkliburSolutions.Shield.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241126135937_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.11");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ClaimType = "Permission",
                            ClaimValue = "UserManagement.Create",
                            RoleId = new Guid("1a16cfa9-2e08-49e4-b802-04cbf967e509")
                        },
                        new
                        {
                            Id = 2,
                            ClaimType = "Permission",
                            ClaimValue = "UserManagement.Read",
                            RoleId = new Guid("1a16cfa9-2e08-49e4-b802-04cbf967e509")
                        },
                        new
                        {
                            Id = 3,
                            ClaimType = "Permission",
                            ClaimValue = "UserManagement.Update",
                            RoleId = new Guid("1a16cfa9-2e08-49e4-b802-04cbf967e509")
                        },
                        new
                        {
                            Id = 4,
                            ClaimType = "Permission",
                            ClaimValue = "UserManagement.Delete",
                            RoleId = new Guid("1a16cfa9-2e08-49e4-b802-04cbf967e509")
                        },
                        new
                        {
                            Id = 5,
                            ClaimType = "Permission",
                            ClaimValue = "UserManagement.ManageRoles",
                            RoleId = new Guid("1a16cfa9-2e08-49e4-b802-04cbf967e509")
                        },
                        new
                        {
                            Id = 6,
                            ClaimType = "Permission",
                            ClaimValue = "UserManagement.ManageClaims",
                            RoleId = new Guid("1a16cfa9-2e08-49e4-b802-04cbf967e509")
                        },
                        new
                        {
                            Id = 7,
                            ClaimType = "Permission",
                            ClaimValue = "UserManagement.Lock",
                            RoleId = new Guid("1a16cfa9-2e08-49e4-b802-04cbf967e509")
                        },
                        new
                        {
                            Id = 8,
                            ClaimType = "Permission",
                            ClaimValue = "UserManagement.Unlock",
                            RoleId = new Guid("1a16cfa9-2e08-49e4-b802-04cbf967e509")
                        },
                        new
                        {
                            Id = 9,
                            ClaimType = "Permission",
                            ClaimValue = "UserManagement.ResetPassword",
                            RoleId = new Guid("1a16cfa9-2e08-49e4-b802-04cbf967e509")
                        },
                        new
                        {
                            Id = 10,
                            ClaimType = "Permission",
                            ClaimValue = "RoleManagement.Create",
                            RoleId = new Guid("1a16cfa9-2e08-49e4-b802-04cbf967e509")
                        },
                        new
                        {
                            Id = 11,
                            ClaimType = "Permission",
                            ClaimValue = "RoleManagement.Read",
                            RoleId = new Guid("1a16cfa9-2e08-49e4-b802-04cbf967e509")
                        },
                        new
                        {
                            Id = 12,
                            ClaimType = "Permission",
                            ClaimValue = "RoleManagement.Update",
                            RoleId = new Guid("1a16cfa9-2e08-49e4-b802-04cbf967e509")
                        },
                        new
                        {
                            Id = 13,
                            ClaimType = "Permission",
                            ClaimValue = "RoleManagement.Delete",
                            RoleId = new Guid("1a16cfa9-2e08-49e4-b802-04cbf967e509")
                        },
                        new
                        {
                            Id = 14,
                            ClaimType = "Permission",
                            ClaimValue = "RoleManagement.ManageClaims",
                            RoleId = new Guid("1a16cfa9-2e08-49e4-b802-04cbf967e509")
                        },
                        new
                        {
                            Id = 15,
                            ClaimType = "Permission",
                            ClaimValue = "Security.EnableTwoFactorAuthentication",
                            RoleId = new Guid("1a16cfa9-2e08-49e4-b802-04cbf967e509")
                        },
                        new
                        {
                            Id = 16,
                            ClaimType = "Permission",
                            ClaimValue = "Security.DisableTwoFactorAuthentication",
                            RoleId = new Guid("1a16cfa9-2e08-49e4-b802-04cbf967e509")
                        },
                        new
                        {
                            Id = 17,
                            ClaimType = "Permission",
                            ClaimValue = "Security.ViewLoginHistory",
                            RoleId = new Guid("1a16cfa9-2e08-49e4-b802-04cbf967e509")
                        },
                        new
                        {
                            Id = 19,
                            ClaimType = "Permission",
                            ClaimValue = "AccessControl.Grant",
                            RoleId = new Guid("1a16cfa9-2e08-49e4-b802-04cbf967e509")
                        },
                        new
                        {
                            Id = 20,
                            ClaimType = "Permission",
                            ClaimValue = "AccessControl.Revoke",
                            RoleId = new Guid("1a16cfa9-2e08-49e4-b802-04cbf967e509")
                        },
                        new
                        {
                            Id = 21,
                            ClaimType = "Permission",
                            ClaimValue = "AccessControl.ViewLogs",
                            RoleId = new Guid("1a16cfa9-2e08-49e4-b802-04cbf967e509")
                        },
                        new
                        {
                            Id = 22,
                            ClaimType = "Permission",
                            ClaimValue = "ApplicationSettings.View",
                            RoleId = new Guid("1a16cfa9-2e08-49e4-b802-04cbf967e509")
                        },
                        new
                        {
                            Id = 23,
                            ClaimType = "Permission",
                            ClaimValue = "ApplicationSettings.Update",
                            RoleId = new Guid("1a16cfa9-2e08-49e4-b802-04cbf967e509")
                        },
                        new
                        {
                            Id = 24,
                            ClaimType = "Permission",
                            ClaimValue = "ApplicationSettings.ManageAPIKeys",
                            RoleId = new Guid("1a16cfa9-2e08-49e4-b802-04cbf967e509")
                        },
                        new
                        {
                            Id = 31,
                            ClaimType = "Permission",
                            ClaimValue = "Analytics.View",
                            RoleId = new Guid("1a16cfa9-2e08-49e4-b802-04cbf967e509")
                        },
                        new
                        {
                            Id = 32,
                            ClaimType = "Permission",
                            ClaimValue = "Analytics.Generate",
                            RoleId = new Guid("1a16cfa9-2e08-49e4-b802-04cbf967e509")
                        },
                        new
                        {
                            Id = 33,
                            ClaimType = "Permission",
                            ClaimValue = "Analytics.Export",
                            RoleId = new Guid("1a16cfa9-2e08-49e4-b802-04cbf967e509")
                        },
                        new
                        {
                            Id = 37,
                            ClaimType = "Permission",
                            ClaimValue = "UserManagement.Read",
                            RoleId = new Guid("ab6b5dbb-c72e-4c5c-ae29-ff03873d1eb8")
                        },
                        new
                        {
                            Id = 38,
                            ClaimType = "Permission",
                            ClaimValue = "UserManagement.Update",
                            RoleId = new Guid("ab6b5dbb-c72e-4c5c-ae29-ff03873d1eb8")
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = new Guid("e0643abd-e50a-4683-b8a0-daa6cb9ea098"),
                            RoleId = new Guid("1a16cfa9-2e08-49e4-b802-04cbf967e509")
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens", (string)null);
                });

            modelBuilder.Entity("XkliburSolutions.Shield.Core.Entities.ApplicationRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("Roles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("1a16cfa9-2e08-49e4-b802-04cbf967e509"),
                            Name = "Administrator",
                            NormalizedName = "ADMINISTRATOR"
                        },
                        new
                        {
                            Id = new Guid("ab6b5dbb-c72e-4c5c-ae29-ff03873d1eb8"),
                            Name = "User",
                            NormalizedName = "USER"
                        });
                });

            modelBuilder.Entity("XkliburSolutions.Shield.Core.Entities.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("AlternateEmail")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ProfilePictureUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("e0643abd-e50a-4683-b8a0-daa6cb9ea098"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "02b42c44-ca84-42df-bb78-1a0d5b395d26",
                            Email = "opencode@xklibursolutions.io",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "OPENCODE@XKLIBURSOLUTIONS.IO",
                            NormalizedUserName = "ADMIN",
                            PasswordHash = "AQAAAAIAAYagAAAAEK9rpIPyZfP9B9pcvQByMA5cycI0rvnNfvBEDjCKxM7UO0qzfYxVEKeRxfIdZQAwHQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "",
                            Status = 0,
                            TwoFactorEnabled = false,
                            UserName = "admin"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("XkliburSolutions.Shield.Core.Entities.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("XkliburSolutions.Shield.Core.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("XkliburSolutions.Shield.Core.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("XkliburSolutions.Shield.Core.Entities.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("XkliburSolutions.Shield.Core.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("XkliburSolutions.Shield.Core.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("XkliburSolutions.Shield.Core.Entities.ApplicationUser", b =>
                {
                    b.OwnsMany("XkliburSolutions.Shield.Core.Entities.Address", "Addresses", b1 =>
                        {
                            b1.Property<Guid>("ApplicationUserId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("INTEGER");

                            b1.Property<string>("AddressLine1")
                                .HasColumnType("TEXT");

                            b1.Property<string>("AddressLine2")
                                .HasColumnType("TEXT");

                            b1.Property<string>("AddressLine3")
                                .HasColumnType("TEXT");

                            b1.Property<string>("City")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Code")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Country")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Region")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Type")
                                .HasColumnType("INTEGER");

                            b1.HasKey("ApplicationUserId", "Id");

                            b1.ToTable("Address");

                            b1.WithOwner()
                                .HasForeignKey("ApplicationUserId");
                        });

                    b.Navigation("Addresses");
                });
#pragma warning restore 612, 618
        }
    }
}
