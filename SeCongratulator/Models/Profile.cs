using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeCongratulator.Models
{
    class Profile: ModelBase
    {
        private string name = string.Empty;
        private List<string> age = new List<string>();
        private List<string> interest = new List<string>();
        private List<string> holiday = new List<string>();

        public Profile()
        {
            for (int i = 3; i < 55; i++) Age.Add(i.ToString());
            Age.Add("55+");

            Interest.AddRange(new string[] 
            { String.Empty,
              "Спорт",
              "Музыка",
              "Рукоделие",
              "Литература",
              "Рисование",
              "Танцы",
              "Рыбалка",
              "Охота",
              "Работа",
              "Языкознание",
              "Программирование" });

            var holidaysToBrushes = ProfileConsts.GetHolidayNamesToBrushes();
            Holiday.AddRange(holidaysToBrushes.Keys);
        }

        public string Name { get => name; set => SetField(ref name, value); }
        public List<string> Age { get => age; set => SetField(ref age, value); }
        public List<string> Interest { get => interest; set => SetField(ref interest, value); }
        public List<string> Holiday { get => holiday; set => SetField(ref holiday, value); }
    }
}
