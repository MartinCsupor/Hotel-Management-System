using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management_System.Model
{
    abstract class Employee : Person
    {
        private string position;

        protected Employee(string position) : base()
        {
            this.Position = position;
        }

        protected string Position { get => position; set => position = value; }
    }
}
