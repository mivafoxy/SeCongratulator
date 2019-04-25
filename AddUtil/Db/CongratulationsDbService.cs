using AddUtil.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddUtil.Db
{
    public static class CongratulationsDbService
    {
        public static ObservableCollection<CongratulationsModel> GetCongratulationsModelsFromDb()
        {
            using (var dbContext = new CongratulationDbContext())
            {
                List<CongratulationsDbModel> dbCongrats = dbContext.CongratulationsDbModel.ToList();
                var congratulations = new ObservableCollection<CongratulationsModel>();

                foreach (var dbCongrat in dbCongrats)
                {
                    var congratulation =
                        new CongratulationsModel(
                            dbCongrat.Kind,
                            dbCongrat.Content,
                            dbCongrat.Holiday,
                            dbCongrat.Interest,
                            dbCongrat.Sex,
                            int.Parse(dbCongrat.Age));

                    congratulations.Add(congratulation);
                }

                return congratulations;
            }
        }

        public static void CommitAdd(CongratulationsModel congratulation)
        {
            using (var dbContext = new CongratulationDbContext())
            {
                CongratulationsDbModel dbCongratulation = GetDbModelFrom(congratulation);
                dbContext.AddNewRecord(dbCongratulation);
            }
        }

        public static void CommitDelete(CongratulationsModel congratulation)
        {
            using (var dbContext = new CongratulationDbContext())
            {
                CongratulationsDbModel dbCongratulation = GetDbModelFrom(congratulation);
                dbContext.DeleteAllSimilarRecords(dbCongratulation);
            }
        }

        private static CongratulationsDbModel GetDbModelFrom(CongratulationsModel congratulation)
        {
            var dbCongratulation = new CongratulationsDbModel();

            dbCongratulation.Kind = congratulation.Kind;
            dbCongratulation.Content = congratulation.Content;
            dbCongratulation.Holiday = congratulation.Holiday;
            dbCongratulation.Interest = congratulation.Interest;
            dbCongratulation.Sex = congratulation.Sex;
            dbCongratulation.Age = congratulation.Age.ToString();

            return dbCongratulation;
        }
    }
}
