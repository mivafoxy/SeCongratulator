using AddUtil.Models.OldDbModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddUtil.Db
{
    public class OldDbContext : DbContext
    {
        public DbSet<ClishesModel> ClishesDbModel { get; set; }
        public DbSet<OldHolidayModel> OldHolidays { get; set; }
        public DbSet<OldInterestsModel> OldInterests { get; set; }
        public DbSet<OldPicturesModel> OldPictures { get; set; }
        public DbSet<PoemsModel> PoemsDbModel { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
