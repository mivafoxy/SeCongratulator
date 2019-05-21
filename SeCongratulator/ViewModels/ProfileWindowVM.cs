﻿using SeCongratulator.Helper_classes;
using SeCongratulator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

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
        private SolidColorBrush nameColor;
        private float nameOpacity;
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
                    if (User.Name == String.Empty) 
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
