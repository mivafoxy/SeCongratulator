using AddUtil.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AddUtil.Db
{
    public static class CongratulationsDbService
    {
        public static ObservableCollection<CongratulationsModel> GetCongratulationsModelsFromDb()
        {
            using (var dbContext = new CongratulationDbContext())
            {
                List<CongratulationsModel> dbCongrats = dbContext.CongratulationsDbModel.ToList();
                var congratulations = new ObservableCollection<CongratulationsModel>();

                foreach (var congratulation in dbCongrats)
                    congratulations.Add(congratulation);

                return congratulations;
            }
        }

        public static void CommitAdd(CongratulationsModel congratulation)
        {
            using (var dbContext = new CongratulationDbContext())
                dbContext.AddNewRecord(congratulation);
        }

        public static void CommitDelete(CongratulationsModel congratulation)
        {
            using (var dbContext = new CongratulationDbContext())
                dbContext.DeleteAllSimilarRecords(congratulation);
        }
    }
}
