using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddUtil.Db
{
    /// <summary>
    /// Инкапсуляция модели для работы с БД.
    /// </summary>
    public class CongratulationsDbModel
    {
        public int Id { get; set; }
        public string Kind { get; set; }
        public string Content { get; set; }
        public string Holiday { get; set; }
        public string Interest { get; set; }
        public int Sex { get; set; }
        public string Age { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is CongratulationsDbModel))
                return false;

            var anotherCongrat = (CongratulationsDbModel)obj;

            bool isEqual =
                this.Kind.SequenceEqual(anotherCongrat.Kind) &&
                this.Content.SequenceEqual(anotherCongrat.Content) &&
                this.Holiday.SequenceEqual(anotherCongrat.Holiday) &&
                this.Interest.SequenceEqual(anotherCongrat.Interest) &&
                this.Sex == anotherCongrat.Sex &&
                this.Age.SequenceEqual(anotherCongrat.Age);


            return isEqual;
        }
    }
}
