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

        public RelayCommand chooseFirstContentTypeCommand;
        public RelayCommand ChooseFirstContentTypeCommand
        {
            get => chooseFirstContentTypeCommand ?? (chooseFirstContentTypeCommand = new RelayCommand(obj => ChooseContentTypeInModel(null)));
        }

        private RelayCommand chooseSecondContentTypeCommand;
        public RelayCommand ChooseSecondContentTypeCommand
        {
            get => chooseSecondContentTypeCommand ?? (chooseSecondContentTypeCommand = new RelayCommand(obj => ChooseContentTypeInModel(null)));
        }

        private RelayCommand chooseThirdContentTypeCommand;
        public RelayCommand ChooseThirdContentTypeCommand
        {
            get => chooseThirdContentTypeCommand ?? (chooseThirdContentTypeCommand = new RelayCommand(obj => ChooseContentTypeInModel(null)));
        }

        private RelayCommand abortCommand;
        public RelayCommand AbortCommand
        {
            get => abortCommand ?? (abortCommand = new RelayCommand(obj => this.AbortAppending()));
        }

        public NewCongratulationViewModel()
        {
            isEditMode = false;
            this.CongratulationModel = new CongratulationModel();
        }

        public NewCongratulationViewModel(CongratulationModel congratulationModel)
        {
            isEditMode = true;
            this.CongratulationModel = new CongratulationModel();

            this.CongratulationModel.Kind = congratulationModel.Kind;
            this.CongratulationModel.Content = congratulationModel.Content;
            this.CongratulationModel.Holiday = congratulationModel.Holiday;
            this.CongratulationModel.Interest = congratulationModel.Interest;
            this.CongratulationModel.Sex = congratulationModel.Sex;
            this.CongratulationModel.Age = congratulationModel.Age;
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

        private void ChooseContentTypeInModel(object contentType)
        {
            this.CongratulationModel.Kind = contentType.ToString(); // DEBUG - сделать нормальный класс для вида контента
        }
    }
}
