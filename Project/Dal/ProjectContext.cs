using JWTAuthentication.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Dal
{
    public class ProjectContext : IdentityDbContext<ApplicationUser>
    {
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
        {

        }
        public DbSet<Donation> Donation { get; set; }
        public DbSet<Donor> Donor { get; set; }
        public DbSet<Gift> Gift { get; set; }
        public DbSet<TypeOfDonation> TypeOfDonation { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Purchase> Purchase { get; set; }
        public DbSet<PurchaseDetails> PurchaseDetails { get; set; }
        public DbSet<Winner> Winner { get; set; }
        public DbSet<Cart> Cart { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PurchaseDetails>(b =>
            {
                // ...

                b.HasOne(p => p.Purchase)
                .WithMany(p => p.PurchaseDetails)
                .HasForeignKey(p => p.PurchaseId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

                // ...
            });
        }
    }
}
