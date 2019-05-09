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
        // Static consts.
        //


        private const string male = "Муж.";
        private const string female = "Жен.";
        private const string bothSex = "Муж./Жен.";

        //
        // Props and fields.
        //

        private readonly bool isEditMode;
        private readonly CongratulationDbContext dbContext = new CongratulationDbContext();

        private List<string> sexChooser;
        public List<string> SexChooser
        {
            get => sexChooser;
            set => SetField(ref sexChooser, value);
        }

        private string selectedSex;
        public string SelectedSex
        {
            get => selectedSex;
            set => SetField(ref selectedSex, value);
        }

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
            get
            {
                return appendCommand ??
                    (appendCommand =
                    new RelayCommand(
                        obj =>
                        {
                            try
                            {
                                this.SaveCongratulation();
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show(e.Message);
                            }
                        }));
            }
        }

        public RelayCommand chooseFirstContentTypeCommand;
        public RelayCommand ChooseFirstContentTypeCommand
        {
            get => chooseFirstContentTypeCommand ?? (chooseFirstContentTypeCommand = new RelayCommand(obj => ChooseContentTypeInModel("LOL")));
        }

        private RelayCommand chooseSecondContentTypeCommand;
        public RelayCommand ChooseSecondContentTypeCommand
        {
            get => chooseSecondContentTypeCommand ?? (chooseSecondContentTypeCommand = new RelayCommand(obj => ChooseContentTypeInModel("VLAD")));
        }

        private RelayCommand chooseThirdContentTypeCommand;
        public RelayCommand ChooseThirdContentTypeCommand
        {
            get => chooseThirdContentTypeCommand ?? (chooseThirdContentTypeCommand = new RelayCommand(obj => ChooseContentTypeInModel("SUSI")));
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
            InitSexChooser();
        }

        public NewCongratulationViewModel(CongratulationModel congratulationModel)
        {
            isEditMode = true;
            CopyCongratulation(congratulationModel);
            InitSexChooser();
        }

        //
        // Private methods.
        //

        private void CopyCongratulation(CongratulationModel congratulationModel)
        {
            this.CongratulationModel = new CongratulationModel();

            this.CongratulationModel.Id = congratulationModel.Id;
            this.CongratulationModel.Kind = congratulationModel.Kind;
            this.CongratulationModel.Content = congratulationModel.Content;
            this.CongratulationModel.Holiday = congratulationModel.Holiday;
            this.CongratulationModel.Interest = congratulationModel.Interest;

            this.CongratulationModel.Sex = congratulationModel.Sex;

            if (congratulationModel.Sex == 1)
                SelectedSex = male;
            else if (congratulationModel.Sex == 0)
                SelectedSex = female;
            else if (congratulationModel.Sex == 2)
                SelectedSex = bothSex;
            else
                throw new ArgumentException("WTF?!!");


            this.CongratulationModel.Age = congratulationModel.Age;
        }

        private void InitSexChooser()
        {
            this.SexChooser = new List<string>()
            {
                male,
                female,
                bothSex
            };
        }

        private void SaveCongratulation()
        {
            this.EnsureCongratulation();

            var congratulation = dbContext.CongratulationsDbModel.FirstOrDefault(congrat => congrat.Id == this.CongratulationModel.Id);
            if (congratulation != null)
                dbContext.Entry(congratulation).CurrentValues.SetValues(CongratulationModel);
            else
            {
                var allCongrats = dbContext.CongratulationsDbModel.ToList();

                // Костыль на идентификатор модели (как сделать автоинкремент?)

                //congratulationModel.Id = allCongrats.Last().Id + 1;

                dbContext.CongratulationsDbModel.Add(CongratulationModel);
            }
            dbContext.SaveChanges();
            this.AbortAppending();
        }

        private int GetCongratulationModelSex()
        {
            if (SelectedSex.SequenceEqual(female))
                return 0;
            else if (SelectedSex.SequenceEqual(male))
                return 1;
            else if (SelectedSex.SequenceEqual(bothSex))
                return 2;
            else
                throw new ArgumentOutOfRangeException("WTF?!!! Only two sex in this world!");
        }

        private void EnsureCongratulation()
        {
            // Проверки
            if (this.CongratulationModel.Kind == null)
                throw new ArgumentNullException("Нельзя оставлять вид поздравления путсым.");

            if (this.CongratulationModel.Age == null || this.CongratulationModel.Age.Length == 0)
                throw new ArgumentException("Необходим возраст.");

            int age = int.Parse(this.CongratulationModel.Age);

            if (age >= 120 || age <= 0)
                throw new ArgumentException("Некорректный возраст. Допустимый дипазон: от 0 до 120 лет.");

            if (this.CongratulationModel.Content == null || this.CongratulationModel.Content.Length == 0)
                throw new ArgumentException("Контент должен быть.");

            if (this.CongratulationModel.Holiday == null || this.CongratulationModel.Content.Length == 0)
                throw new ArgumentException("К какому празднику относится поздравление?");

            if (this.CongratulationModel.Interest == null || this.CongratulationModel.Interest.Length == 0)
                throw new ArgumentException("Какие интересы?");

            if (SelectedSex == null || SelectedSex.Length == 0)
                throw new ArgumentException("Необходимо выбрать пол.");
        }

        private void AbortAppending()
        {
            var displayRoot = (Application.Current as App).DisplayRootRegistry;
            displayRoot.HidePresentation(this);
        }

        private void ChooseContentTypeInModel(string contentType)
        {
            this.CongratulationModel.Kind = contentType.ToString(); // DEBUG - сделать нормальный класс для вида контента
        }
    }
}
