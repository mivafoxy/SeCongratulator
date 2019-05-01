using AddUtil.Commands;
using AddUtil.Db;
using AddUtil.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AddUtil.ViewModels
{
    /// <summary>
    /// Инкапсулирует логику для вызова добавления новой модели в БД.
    /// </summary>
    public class NewCongratulationViewModel : ViewModelBase
    {
        //
        // Props and fields.
        //

        private readonly bool isEditMode;
        private readonly CongratulationDbContext dbContext = new CongratulationDbContext();

        private CongratulationModel congratulationModel;
        public CongratulationModel CongratulationModel
        {
            get => congratulationModel;
            set => SetField(ref congratulationModel, value);
        }

        //
        // Commands.
        //

        private RelayCommand appendCommand;
        public RelayCommand AppendCommand
        {
            get => appendCommand ?? (appendCommand = new RelayCommand(obj => this.SaveCongratulation()));
        }

        private RelayCommand abortCommand;
        public RelayCommand AbortCommand
        {
            get => abortCommand ?? (abortCommand = new RelayCommand(obj => this.AbortAppending()));
        }

        public NewCongratulationViewModel()
        {
            isEditMode = false;
        }

        public NewCongratulationViewModel(CongratulationModel congratulationModel)
        {
            isEditMode = true;
            this.CongratulationModel = congratulationModel;
        }

        //
        // Private methods.
        //

        private void SaveCongratulation()
        {
            if (isEditMode)
            {
                var congratulation = 
                    dbContext.CongratulationsDbModel.First(
                        congrat => 
                            congrat.Id == this.CongratulationModel.Id);

                congratulation.Kind = this.CongratulationModel.Kind;
                congratulation.Content = this.CongratulationModel.Content;
                congratulation.Holiday = this.CongratulationModel.Holiday;
                congratulation.Interest = this.CongratulationModel.Interest;
                congratulation.Sex = this.CongratulationModel.Sex;
                congratulation.Age = this.CongratulationModel.Age;

                dbContext.SaveChanges();
            }
            else
            {
                var allCongrats = dbContext.CongratulationsDbModel.ToList();
                allCongrats.Add(CongratulationModel);
                dbContext.SaveChanges();
            }
        }

        private void AbortAppending()
        {
            var displayRoot = (Application.Current as App).DisplayRootRegistry;
            displayRoot.HidePresentation(this);
        }
    }
}
