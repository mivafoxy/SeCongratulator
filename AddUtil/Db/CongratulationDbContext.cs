using AddUtil.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace AddUtil.Db
{
    public class CongratulationDbContext : DbContext
    {
        public DbSet<CongratulationsModel> CongratulationsDbModel { get; set; }

        public CongratulationDbContext() : base(GetDbConnection(), false)
        {

        }

        public void AddNewRecord(CongratulationsModel dbCongratulation)
        {
            CongratulationsDbModel.Add(dbCongratulation);
            this.SaveChanges();
        }
        
        public void DeleteRecordFromDb(CongratulationsModel dbCongratulation)
        {
            CongratulationsDbModel.Remove(dbCongratulation);
            this.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new CongratulationDbMap());
            base.OnModelCreating(modelBuilder);
        }

        private static DbConnection GetDbConnection()
        {
            ConnectionStringSettings connection = ConfigurationManager.ConnectionStrings["DefaultConnection"];
            DbProviderFactory factory = DbProviderFactories.GetFactory(connection.ProviderName);
            DbConnection dbConnection = factory.CreateConnection();

            dbConnection.ConnectionString = connection.ConnectionString;

            return dbConnection;
        }
    }
}
