using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management_System.Model
{
    abstract class DoubleRoom : Room
    {
        public bool isDoubleBed;
        protected DoubleRoom(bool isDoubleBed) : base()
        {
            this.isDoubleBed = isDoubleBed;
            this.Capacity = 2;
            this.PricePerNight = 22000;
        }
    }
}
