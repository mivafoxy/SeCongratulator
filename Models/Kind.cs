using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeCongratulator.Models
{
    class Kind
    {
        private int id = -1;
        private string name= String.Empty;

        public override string ToString()
        {
            return "Id: " + Id + "\nName: " + Name;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
    }
}
