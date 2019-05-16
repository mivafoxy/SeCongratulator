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
                                obj => this.MergeDatabases(),
                                a => this.HasFilledPaths()));
            }
        }

        private RelayCommand abortCommand;
        public RelayCommand AbortCommand
        {
            get => abortCommand ?? (abortCommand = new RelayCommand((obj) => this.Abort()));
        }

        private RelayCommand openFileDialogForOldDbCommand;
        public RelayCommand OpenFileDialogForOldDbCommand
        {
            get
            {
                return
                    openFileDialogForOldDbCommand ??
                        (openFileDialogForOldDbCommand = 
                            new RelayCommand(
                                (
                                    obj =>
                                    {
                                        PathToOldDb = this.GetPathToFile();
                                    })));
            }
        }

        private RelayCommand openFileDialogForNewDbCommand;
        public RelayCommand OpenFileDialogForNewDbCommand
        {
            get
            {
                return
                    openFileDialogForNewDbCommand ??
                        (openFileDialogForNewDbCommand = 
                            new RelayCommand(
                                obj =>
                                {
                                    PathToNewDb = this.GetPathToFile();
                                }));
            }
        }

        private void Abort()
        {
            var displayRoot = (Application.Current as App).DisplayRootRegistry;
            displayRoot.HidePresentation(this);
        }

        private string pathToOldDb;
        public string PathToOldDb
        {
            get => pathToOldDb;
            set
            {
                SetField(ref pathToOldDb, value);
                this.RunMergeCommand.RaiseCanExecuteChanged();
            }
        }

        private string pathToNewDb;
        public string PathToNewDb
        {
            get => pathToNewDb;
            set 
            {
                SetField(ref pathToNewDb, value);
                this.RunMergeCommand.RaiseCanExecuteChanged();
            }
        }

        private void MergeDatabases()
        {
            this.CopyClishes();
            this.CopyPoems();
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

        private bool HasFilledPaths()
        {
            bool isPathToOldDbEmpty = 
                this.PathToOldDb == null || 
                this.PathToOldDb.Equals(string.Empty) || 
                this.PathToOldDb.Length == 0;

            bool isPathToNewDbEmpty =
                this.PathToNewDb == null ||
                this.PathToNewDb.Equals(string.Empty) ||
                this.PathToNewDb.Length == 0;

            bool isAllPathsFilled =
                !(isPathToOldDbEmpty || isPathToNewDbEmpty);

            return isAllPathsFilled;
        }

        private string GetPathToFile()
        {
            var fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == true)
                return fileDialog.FileName;
            else
                return string.Empty;
        }
    }
}
