using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddUtil.Models.OldDbModels
{
    public class OldInterestsModel : ModelBase
    {
        private int id;
        public int Id
        {
            get => id;
            set => SetField(ref id, value);
        }

        private string name;
        public string Name
        {
            get => name;
            set => SetField(ref name, value);
        }
    }
}
