using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management_System.Model
{
    abstract class Suite : Room
    {
        protected Suite() : base()
        {
            this.RoomNumber = 100;
            this.Floor = 10;
            this.Capacity = 8;
            this.PricePerNight = 100000;
        }
    }
}
