using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management_System.Model
{
    abstract class DoubleRoom : Room
    {
        protected DoubleRoom() : base()
        {
            this.RoomNumber = 3;
            this.Floor = 1;
            this.Capacity = 2;
            this.PricePerNight = 22000;
        }
    }
}
