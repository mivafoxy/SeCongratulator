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

        public ObservableCollection<CongratulationsModel> Congratulations
        {
            get;
            set;
        }

        private CongratulationsModel selectedCongratulation;
        public CongratulationsModel SelectedCongratulation
        {
            get => selectedCongratulation;
            set => SetField(ref selectedCongratulation, value);
        }

        //
        // Commands.
        //

        private RelayCommand addRecordCommand;
        public RelayCommand AddRecordCommand
        {
            get => addRecordCommand ?? (addRecordCommand = new RelayCommand(obj => this.GoToCongratulationAppending()));
        }

        private RelayCommand removeRecordCommand;
        public RelayCommand RemoveRecordCommand
        {
            get => removeRecordCommand ?? (removeRecordCommand = new RelayCommand(obj => this.DeleteCongratulation()));
        }

        private RelayCommand runMergeCommand;
        public RelayCommand RunMergeCommand
        {
            get => runMergeCommand ?? (runMergeCommand = new RelayCommand(obj => this.CallToMergeWithOldDb()));
        }

        //
        // Constructors.
        //

        public CongratulationsViewModel()
        {
            this.initCongratulationsCollection();
        }

        //
        // Private zone.
        //

        private void initCongratulationsCollection()
        {
            List<CongratulationsModel> dbCongrats = dbContext.CongratulationsDbModel.ToList();
            Congratulations = new ObservableCollection<CongratulationsModel>();

            foreach (var congratulation in dbCongrats)
                Congratulations.Add(congratulation);
        }

        private void DeleteCongratulation()
        {
        }

        private async void GoToCongratulationAppending()
        {
            var displayRootRegistry = (Application.Current as App).DisplayRootRegistry;

            var newCongratulationViewModel = new NewCongratulationViewModel();
            await displayRootRegistry.ShowModalPresentation(newCongratulationViewModel);
        }

        // Перенос из одной базы в другую - старая база должна подаваться аргументом, на выходе новая база, заполненная значениями со старой.
        private void CallToMergeWithOldDb() => DbMergeService.RunMerge(null); // Как сделать переход на новое окно? Как вообще сделать новое окно? 
    }
}
