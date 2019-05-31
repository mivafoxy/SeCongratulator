using SeCongratulator.Helper_classes;
using SeCongratulator.Models;
using SeCongratulator.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace SeCongratulator.ViewModels
{
    class ProfileWindowVM: ModelBase
    {
        Profile user;
        Congratulation profile=new  Congratulation();
        private string selectedAge;
        private string selectedInterest;
        private string selectedHoliday;
        private bool isMale;
        private bool isFemale;
        private SolidColorBrush nameColor;
        private float nameOpacity;
        private Regex rule = new Regex(@"[^A-Za-zА-Яа-я]");
        /*private SystemColors bgCol =*/
        public ProfileWindowVM()
        {
            User = new Profile();
            SelectedAge = String.Empty;
            SelectedHoliday = String.Empty;
            SelectedInterest = String.Empty;
            IsMale = false;
            IsFemale = false;
            NameOpacity = 1;
            NameColor = Brushes.White;
        }

        public ProfileWindowVM(Congratulation profile)
        {
            User = new Profile();
            User.Name = ProfileConsts.nameProfile;
            SelectedHoliday = profile.Holiday;
            SelectedInterest = profile.Interest;
            SelectedAge = profile.Age;
            SelectedAge = profile.Age;
            SelectedHoliday = profile.Holiday;
            SelectedInterest = profile.Interest;
            if (profile.Sex == 0)
            {
                IsFemale = true;
                IsMale = false;
            }
            else
            {
                IsMale = true;
                IsFemale = false;
            }
            NameColor = Brushes.White;
            NameOpacity = 1;
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
                    //MessageBox.Show(User.Name);
                    User.Name = String.Empty;
                    IsMale = false;
                    IsFemale = false;
                    SelectedAge = String.Empty;
                    SelectedInterest = String.Empty;
                    SelectedHoliday = null;
                    NameColor = new SolidColorBrush(Color.FromArgb(100, 255, 255, 255));
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
                    (!rule.IsMatch(User.Name)) &&
                    SelectedAge != String.Empty &&
                    (IsMale || IsFemale) &&
                    SelectedHoliday != String.Empty)
                    {
                        profile.Age = SelectedAge;
                        profile.Holiday = SelectedHoliday;
                        profile.Interest = SelectedInterest;
                        if (IsMale) profile.Sex = 1;
                        else profile.Sex = 0;
                        ProfileConsts.nameProfile = User.Name;
                        СonstructionWindow window = new СonstructionWindow();
                        window.DataContext = new СonstructionWindowVM(profile);
                        window.Show();
                        Application.Current.Windows[0].Close();
                    }
                    else
                    {
                        MessageBox.Show("Не все поля заполнены или поле Имя заполнено некорректно!" + '\n'+"В поле Имя можно вводить только буквы!");
                    }
                }));
            }
        }

        public ICommand CheckValid
        {
            get
            {
                return new DelegateCommand(new Action(() =>
                {
                    //NameOpacity = 0.3f;
                    if (User.Name != null)
                    {   
                        NameColor = new SolidColorBrush(Color.FromArgb(100,0,200,0));
                    }
                    if (User.Name == String.Empty || rule.IsMatch(User.Name)) 
                    {
                        NameColor = new SolidColorBrush(Color.FromArgb(100, 200, 0, 0));
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
        public SolidColorBrush NameColor { get => nameColor; set => SetField(ref nameColor, value); }
        public float NameOpacity { get => nameOpacity; set => SetField(ref nameOpacity, value); }
    }
}
