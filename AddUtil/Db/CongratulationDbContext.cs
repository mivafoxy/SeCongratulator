using AddUtil.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddUtil.Db
{
    public class CongratulationDbContext : DbContext
    {
        public DbSet<CongratulationsDbModel> CongratulationsDbModel { get; set; }

        public CongratulationDbContext() : base(GetDbConnection(), false)
        {

        }

        private static DbConnection GetDbConnection()
        {
            ConnectionStringSettings connection = ConfigurationManager.ConnectionStrings["DefaultConnection"];
            DbProviderFactory factory = DbProviderFactories.GetFactory(connection.ProviderName);
            DbConnection dbConnection = factory.CreateConnection();

            dbConnection.ConnectionString = connection.ConnectionString;

            return dbConnection;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new CongratulationDbMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
