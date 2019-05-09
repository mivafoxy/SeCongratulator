using AddUtil.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace AddUtil.Db
{
    public class CongratulationDbContext : DbContext
    {
        public DbSet<CongratulationModel> CongratulationsDbModel { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CongratulationModel>()
                .ToTable("Congratulations")
                .Property(p => p.Id)
                .IsRequired()
                    .HasDatabaseGeneratedOption(
                        DatabaseGeneratedOption.Identity);
            base.OnModelCreating(modelBuilder);
        }

    }
}
