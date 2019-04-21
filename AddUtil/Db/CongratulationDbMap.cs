using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddUtil.Db
{
    public class CongratulationDbMap : EntityTypeConfiguration<CongratulationsDbModel>
    {
        public CongratulationDbMap()
        {
            ToTable("Congratulations");

            Property(
                p => p.Id).
                    IsRequired().
                    HasDatabaseGeneratedOption(
                        DatabaseGeneratedOption.None);
        }
    }
}
