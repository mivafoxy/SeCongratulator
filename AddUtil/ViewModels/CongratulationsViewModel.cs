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
            get => addRecordCommand ?? (addRecordCommand = new RelayCommand(obj => this.AddNewCongratulation()));
        }

        private RelayCommand removeRecordCommand;
        public RelayCommand RemoveRecordCommand
        {
            get => removeRecordCommand ?? (removeRecordCommand = new RelayCommand(obj => this.DeleteCongratulation()));
        }

        private RelayCommand runMergeCommand;
        public RelayCommand RunMergeCommand
        {
            get => runMergeCommand;
            set =? runMergeCommand ?? (runMergeCommand = new RelayCommand(obj => this.CallToMergeWithOldDb()));
        }

        //
        // Constructors.
        //

        public CongratulationsViewModel()
        {
            Congratulations = new ObservableCollection<CongratulationsModel>();
            foreach (var congrat in CongratulationsDbService.GetCongratulationsModelsFromDb())
                Congratulations.Add(congrat);
        }

        //
        // Private zone.
        //

        private void DeleteCongratulation()
        {
            Congratulations.Remove(SelectedCongratulation);
            this.CallToCommitDelete(SelectedCongratulation);
        }

        private void AddNewCongratulation()
        {
            Congratulations.Add(SelectedCongratulation);
            this.CallToCommitAdd(SelectedCongratulation);
        }

        // Перенос из одной базы в другую - старая база должна подаваться аргументом, на выходе новая база, заполненная значениями со старой.
        private void CallToMergeWithOldDb() => DbMergeService.RunMerge(null); // Как сделать переход на новое окно? Как вообще сделать новое окно? 


        private void CallToCommitAdd(CongratulationsModel congratulation) => CongratulationsDbService.CommitAdd(congratulation);
        private void CallToCommitDelete(CongratulationsModel congratulation) => CongratulationsDbService.CommitDelete(congratulation);
    }
}
