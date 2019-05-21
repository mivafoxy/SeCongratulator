using AddUtil.Commands;
using AddUtil.Db;
using AddUtil.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AddUtil.ViewModels
{
    /// <summary>
    /// Прослойка для перекопирования старой базы в новую.
    /// </summary>
    public class MergeViewModel : ViewModelBase
    {
        private CongratulationDbContext newDbContext = new CongratulationDbContext();
        private OldDbContext oldDbContext = new OldDbContext();

        private RelayCommand runMergeCommand;
        public RelayCommand RunMergeCommand
        {
            get
            {
                return
                    runMergeCommand ??
                        (runMergeCommand =
                            new RelayCommand(
                                obj => 
                                    this.MergeDatabases()));
            }
        }

        private RelayCommand abortCommand;
        public RelayCommand AbortCommand
        {
            get => abortCommand ?? (abortCommand = new RelayCommand((obj) => this.Abort()));
        }

        public MergeViewModel()
        {
        }

        private void Abort()
        {
            var displayRoot = (Application.Current as App).DisplayRootRegistry;
            displayRoot.HidePresentation(this);
        }

        private void MergeDatabases()
        {
            try
            {
                this.CopyClishes();
                this.CopyPoems();

                MessageBox.Show("Успех!");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Провал!");
            }

            this.Abort();
        }

        private void CopyClishes()
        {
            var clishes = oldDbContext.ClishesDbModel;

            foreach (var clishe in clishes)
            {
                var congratulation = new CongratulationModel
                {
                    Age = clishe.Age,
                    Content = clishe.Content,
                    Holiday = clishe.Holiday,
                    Interest = clishe.Interests,
                    Sex = this.GetSexFrom(clishe.Sex),
                    Kind = "Клише"
                };

                newDbContext.CongratulationsDbModel.Add(congratulation);
            }

            newDbContext.SaveChanges();
        }

        private void CopyPoems()
        {
            var poems = oldDbContext.PoemsDbModel;

            foreach (var poem in poems)
            {
                var congratulation = new CongratulationModel
                {
                    Age = poem.Age,
                    Content = poem.Content,
                    Holiday = poem.Holiday,
                    Interest = poem.Interests,
                    Sex = this.GetSexFrom(poem.Sex),
                    Kind = "Поэма"
                };

                newDbContext.CongratulationsDbModel.Add(congratulation);
            }

            newDbContext.SaveChanges();
        }

        private int GetSexFrom(string oldSex)
        {
            if (oldSex.Equals("Женский"))
                return 0;
            else if (oldSex.Equals("Мужской"))
                return 1;
            else
                return 2;
        }
    }
}
