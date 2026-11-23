using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management_System.Model
{
    abstract class Suite : Room
    {
        protected int balconySize;

        protected Suite(int balconySize) : base()
        {
            this.balconySize = balconySize;
            this.RoomNumber = RoomNumber;
            this.Floor = Floor;
            this.Capacity = 8;
            this.PricePerNight = 100000;
        }
    }
}
