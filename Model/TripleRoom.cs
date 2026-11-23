using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management_System.Model
{
    abstract class TripleRoom : Room
    {
        public bool isOneDoubleBed;
        protected TripleRoom(bool isOneDoubleBed) : base()
        {
            this.isOneDoubleBed = isOneDoubleBed;
            this.RoomNumber = RoomNumber;
            this.Floor = Floor;
            this.Capacity = 3;
            this.PricePerNight = 27000;
        }
    }
}
