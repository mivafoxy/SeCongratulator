using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddUtil.Models.OldDbModels
{
    public class OldPoemsModel : ModelBase
    {
        private int id;
        public int Id
        {
            get => id;
            set => SetField(ref id, value);
        }

        private string content;
        public string Content
        {
            get => content;
            set => SetField(ref content, value);
        }

        private string holiday;
        public string Holiday
        {
            get => holiday;
            set => SetField(ref holiday, value);
        }

        private string interests;
        public string Interests
        {
            get => interests;
            set => SetField(ref interests, value);
        }

        private string sex;
        public string Sex
        {
            get => sex;
            set => SetField(ref sex, value);
        }


        private string age;
        public string Age
        {
            get => age;
            set => SetField(ref age, value);
        }
    }
}
