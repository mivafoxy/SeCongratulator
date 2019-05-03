using AddUtil.Commands;
using AddUtil.Db;
using AddUtil.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AddUtil.ViewModels
{
    public class CongratulationsViewModel : ViewModelBase
    {
        //
        // Fields and properties.
        //

        private CongratulationDbContext dbContext = new CongratulationDbContext();

        private List<CongratulationModel> congratulations;
        public List<CongratulationModel> Congratulations
        {
            get => congratulations;
            set => SetField(ref congratulations, value);
        }
        //public ObservableCollection<CongratulationModel> Congratulations
        //{
        //    get;
        //    set;
        //}

        private CongratulationModel selectedCongratulation;
        public CongratulationModel SelectedCongratulation
        {
            get => selectedCongratulation;
            set => SetField(ref selectedCongratulation, value);
        }

        //
        // Commands.
        //

        private RelayCommand goToCongratulationAppendingCommand;
        public RelayCommand GoToCongratulationAppendingCommand
        {
            get => goToCongratulationAppendingCommand ?? (goToCongratulationAppendingCommand = new RelayCommand(obj => this.GoToCongratulationAppending()));
        }

        private RelayCommand removeRecordCommand;
        public RelayCommand RemoveRecordCommand
        {
            get => removeRecordCommand ?? (removeRecordCommand = new RelayCommand(obj => this.DeleteCongratulation()));
        }

        private RelayCommand goToMergeCommand;
        public RelayCommand GoToMergeCommand
        {
            get => goToMergeCommand ?? (goToMergeCommand = new RelayCommand(obj => this.GoToMergeWithOldDb()));
        }

        private RelayCommand goToCongratulationEditCommand;
        public RelayCommand GoToCongratulationEditCommand
        {
            get => goToCongratulationEditCommand ?? (goToCongratulationEditCommand = new RelayCommand(obj => this.GoToCongratulationEdit()));
        }

        //
        // Constructors.
        //

        public CongratulationsViewModel()
        {
            this.InitCongratulationsCollection();
        }

        //
        // Private zone.
        //
        private void InitCongratulationsCollection()
        {
            Congratulations = dbContext.CongratulationsDbModel.ToList();
        }

        private void DeleteCongratulation()
        {
            Congratulations.Remove(SelectedCongratulation);
            dbContext.SaveChanges();
        }

        private async void GoToCongratulationAppending()
        {
            var displayRootRegistry = (Application.Current as App).DisplayRootRegistry;

            var newCongratulationViewModel = new NewCongratulationViewModel();
            await displayRootRegistry.ShowModalPresentation(newCongratulationViewModel);

            this.UpdateContext();
        }

        private async void GoToCongratulationEdit()
        {
            if (SelectedCongratulation == null)
            {
                MessageBox.Show(
                    "Выберите поздравление для редактирования.",
                    "Ошибка редактирования");

                return;
            }

            var displayRootRegistry = (Application.Current as App).DisplayRootRegistry;

            await displayRootRegistry.ShowModalPresentation(
                new NewCongratulationViewModel(
                    this.SelectedCongratulation));

            this.UpdateContext();
        }

        private void CopyEditedToCurrent(
            CongratulationModel editedCongratulation, 
            CongratulationModel currentCongratulation)
        {

        }

        private void UpdateContext()
        {
            this.InitCongratulationsCollection();
        }

        // Перенос из одной базы в другую - старая база должна подаваться аргументом, на выходе новая база, заполненная значениями со старой.
        private async void GoToMergeWithOldDb()
        {
            var displayRootRegistry = (Application.Current as App).DisplayRootRegistry;

            await displayRootRegistry.ShowModalPresentation(new MergeViewModel());

            this.UpdateContext();
        }
    }
}
