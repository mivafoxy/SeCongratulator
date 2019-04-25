using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddUtil.Models
{
    /// <summary>
    /// Инкапсуляция бизнес логики поздравления.
    /// </summary>
    public class CongratulationsModel : ModelBase
    {
        //
        // Поля для определения модели поздравляшки.
        //

        private string kind;
        public string Kind
        {
            get => kind;
            set => this.SetField(ref kind, value);
        }

        private string content;
        public string Content
        {
            get => content;
            set => this.SetField(ref content, value);
        }

        private string holiday;
        public string Holiday
        {
            get => holiday;
            set => this.SetField(ref holiday, value);
        }

        private string interest;
        public string Interest
        {
            get => interest;
            set => this.SetField(ref interest, value);
        }

        private int sex;
        public int Sex
        {
            get => sex;
            set => this.SetField(ref sex, value);
        }

        private int age;
        public int Age
        {
            get => age;
            set => SetField(ref age, value);
        }

        public CongratulationsModel(
            string kind, 
            string content, 
            string holiday, 
            string interest, 
            int sex,
            int age)
        {
            this.kind = kind;
            this.content = content;
            this.holiday = holiday;
            this.interest = interest;
            this.sex = sex;
            this.age = age;
        }
    }
}
