using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management_System.Model
{
    abstract class TripleRoom : Room
    {
        protected TripleRoom() : base()
        {
            this.RoomNumber = 11;
            this.Floor = 2;
            this.Capacity = 3;
            this.PricePerNight = 27000;
        }
    }
}
