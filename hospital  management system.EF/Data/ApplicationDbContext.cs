using hospital__management_system.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using hospital__management_system.Core.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital__management_system.EF.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<Patient>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<Appointment>().HasQueryFilter(e => !e.IsDeleted);

            builder.Entity<IdentityRole>().ToTable("Role", "Security");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRole", "Security");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim", "Security");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin", "Security");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim", "Security");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserToken", "Security");
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "R1",
                    Name = Core.Constants.Roles.Admin.ToString(),
                    NormalizedName = Core.Constants.Roles.Admin.ToString().ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new IdentityRole
                {
                    Id = "R2",
                    Name = Core.Constants.Roles.Patient.ToString(),
                    NormalizedName = Core.Constants.Roles.Patient.ToString().ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new IdentityRole
                {
                    Id = "R3",
                    Name = Core.Constants.Roles.Doctor.ToString(),
                    NormalizedName = Core.Constants.Roles.Doctor.ToString().ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                }
                );

            


            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

        }
    }
    }
