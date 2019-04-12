using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SeCongratulator.Models
{
    class Congratulation : INotifyPropertyChanged
    {
        private int id = -1;
        private int kind;
        private string content=String.Empty;
        private int holiday;
        private int interest;
        private int sex;
        private string age = String.Empty;

        public int Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        public int Kind
        {
            get => kind;
            set
            {
                kind = value;
                OnPropertyChanged("Kind");
            }
        }

        public string Content
        {
            get => content;
            set
            {
                content = value;
                OnPropertyChanged("Content");
            }
        }

        public int Holiday
        {
            get => holiday;
            set
            {
                holiday = value;
                OnPropertyChanged("Holiday");
            }
        }

        public int Interest
        {
            get => interest;
            set
            {
                interest = value;
                OnPropertyChanged("Interest");
            }
        }

        public int Sex
        {
            get => sex;
            set
            {
                sex = value;
                OnPropertyChanged("Sex");
            }
        }

        public string Age
        {
            get => age;
            set
            {
                age = value;
                OnPropertyChanged("Age");
            }
        }

        public override string ToString()
        {
            return "Id: " + Id + "\nKind: " + Kind + "\nContent: " + Content + 
                "\nHoliday: " + Holiday + "\nInterest: " + Interest +
                "\nSex: " + Sex + "\nAge: " + Age;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
