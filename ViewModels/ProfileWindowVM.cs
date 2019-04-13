using SeCongratulator.Helper_classes;
using SeCongratulator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SeCongratulator.ViewModels
{
    class ProfileWindowVM: ModelBase
    {
        Profile user;
        private string selectedAge;
        private string selectedInterest;
        private string selectedHoliday;
        private bool isMale;
        private bool isFemale;
        public ProfileWindowVM()
        {
            User = new Profile();
            SelectedAge = String.Empty;
            SelectedHoliday = String.Empty;
            SelectedInterest = String.Empty;
            IsMale = false;
            IsFemale = false;
        }
        /// <summary>
        /// Очистка формы
        /// </summary>
        public ICommand ClearProfile
        {
            get
            {
                return new DelegateCommand(new Action(() =>
                {
                    User.Name = String.Empty;
                    IsMale = false;
                    isFemale = false;
                    SelectedAge = String.Empty;
                    SelectedHoliday = String.Empty;
                    SelectedInterest = String.Empty;
                    MessageBox.Show("Анкета очищена!");
                }));
            }
        }

        /// <summary>
        /// Поиск поздравлений соотвествуюищих данным заполненной анкеты
        /// </summary>
        public ICommand SearchCongratulation
        {
            get
            {
                return new DelegateCommand(new Action(() =>
                {
                    if (User.Name != "" &&
                    SelectedAge != String.Empty &&
                    (IsMale || IsFemale) &&
                    SelectedHoliday != String.Empty)
                    {
                        MessageBox.Show("Поиск поздравления");
                    }
                    else
                    {
                        MessageBox.Show("Не все поля заполнены!");
                    }
                }));
            }
        }

        public Profile User { get => user; set => SetField(ref user, value); }
        public string SelectedAge { get => selectedAge; set => SetField(ref selectedAge, value); }
        public bool IsMale { get => isMale; set => SetField(ref isMale, value); }
        public bool IsFemale { get => isFemale; set => SetField(ref isFemale, value); }
        public string SelectedInterest { get => selectedInterest; set => SetField(ref selectedInterest, value); }
        public string SelectedHoliday { get => selectedHoliday; set => SetField(ref selectedHoliday, value); }
    }
}
