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
        public ObservableCollection<CongratulationsModel> Congratulations;

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

        //
        // Constructors.
        //

        public CongratulationsViewModel()
        {
            Congratulations = CongratulationsDbService.GetCongratulationsModelsFromDb();

            MessageBox.Show(Congratulations[0].Id.ToString());
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

        private void CallToCommitAdd(CongratulationsModel congratulation) => CongratulationsDbService.CommitAdd(congratulation);
        private void CallToCommitDelete(CongratulationsModel congratulation) => CongratulationsDbService.CommitDelete(congratulation);
    }
}
