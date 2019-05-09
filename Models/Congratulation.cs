using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SeCongratulator.Models
{
    class Congratulation : ModelBase
    {
        private int id = -1;
        private string kind;
        private string content=String.Empty;
        private string holiday;
        private string interest;
        private int sex;
        private string age = String.Empty;

        public int Id
        {
            get => id;
            set => SetField(ref id, value);
        }

        public string Kind
        {
            get => kind;
            set => SetField(ref kind, value);
        }

        public string Content
        {
            get => content;
            set => SetField(ref content, value);
        }

        public string Holiday
        {
            get => holiday;
            set => SetField(ref holiday, value);
        }

        public string Interest
        {
            get => interest;
            set => SetField(ref interest, value);
        }

        public int Sex
        {
            get => sex;
            set => SetField(ref sex, value);
        }

        public string Age
        {
            get => age;
            set => SetField(ref age, value);
        }

        public override string ToString()
        {
            return "Id: " + Id + "\nKind: " + Kind + "\nContent: " + Content + 
                "\nHoliday: " + Holiday + "\nInterest: " + Interest +
                "\nSex: " + Sex + "\nAge: " + Age;
        }
    }
}
