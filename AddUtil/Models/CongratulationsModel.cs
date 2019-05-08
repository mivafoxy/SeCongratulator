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
    public class CongratulationModel : ModelBase
    {
        //
        // Поля для определения модели поздравляшки.
        //

        private int id;
        public int Id
        {
            get => id;
            set => this.SetField(ref id, value);
        }

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
            set
            {
                if (value.Length < 3)
                    throw new ArgumentException("Слишком короткое значение");
                this.SetField(ref interest, value);
            }
        }

        private int sex;
        public int Sex
        {
            get => sex;
            set => this.SetField(ref sex, value);
        }

        private string age;
        public string Age
        {
            get => age;
            set => SetField(ref age, value);
        }
    }
}
