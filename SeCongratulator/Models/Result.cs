using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeCongratulator.Models
{
    class Result : ModelBase
    {
        string poem = string.Empty;
        string cliche = string.Empty;
        string img = string.Empty;

        public string Poem { get => poem; set => SetField(ref poem, value); }
        public string Cliche { get => cliche; set => SetField(ref cliche, value); }
        public string Img { get => img; set => SetField(ref img, value); }
    }
}
