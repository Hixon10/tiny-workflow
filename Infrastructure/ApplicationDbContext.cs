using System.Data.Entity;
using Domain;
using Infrastructure.Migrations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("local")
        {
        }
        
        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Application> Applications { get; set; }

        public DbSet<ApplicationLog> ApplicationLogs { get; set; }

        public DbSet<ApplicationLogStatus> ApplicationLogStatuses { get; set; }

        /// <summary>
        /// Enable-Migrations -ProjectName "Infrastructure" -StartUpProjectName "Bizagi" -Force
        /// Add-Migration -ProjectName "Infrastructure" -StartUpProjectName "Bizagi" MyNewMigration
        /// Update-Database -ProjectName "Infrastructure" -StartUpProjectName "Bizagi" -Verbose
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //one-to-many
            modelBuilder.Entity<Application>().HasRequired(p => p.ProductCategory)
                .WithMany(b => b.Applications)
                .HasForeignKey(p => p.ProductCategoryId);

            modelBuilder.Entity<Application>().HasRequired(p => p.ApplicationUser)
                .WithMany(b => b.Applications)
                .HasForeignKey(p => p.ApplicationUserId);

            modelBuilder.Entity<ApplicationLog>().HasRequired(p => p.ApplicationUser)
                .WithMany(b => b.ApplicationLogs)
                .HasForeignKey(p => p.ApplicationUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationLog>().HasRequired(p => p.Application)
                .WithMany(b => b.ApplicationLogs)
                .HasForeignKey(p => p.ApplicationId);

            modelBuilder.Entity<ApplicationLog>().HasRequired(p => p.ApplicationLogStatus)
                .WithMany(b => b.ApplicationLogs)
                .HasForeignKey(p => p.ApplicationLogStatusId);

            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
        }
		
		public void FixEfProviderServicesProblem()
		{
			//The Entity Framework provider type 'System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer'
			//for the 'System.Data.SqlClient' ADO.NET provider could not be loaded. 
			//Make sure the provider assembly is available to the running application. 
			//See http://go.microsoft.com/fwlink/?LinkId=260882 for more information.

			var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
		}
    }
}