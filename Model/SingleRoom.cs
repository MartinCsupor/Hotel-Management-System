using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management_System.Model
{
    abstract class SingleRoom : Room
    {
        protected SingleRoom() : base()
        {
            this.RoomNumber = 2;
            this.Floor = 1;
            this.Capacity = 1;
            this.PricePerNight = 15000;
        }
    }
}
