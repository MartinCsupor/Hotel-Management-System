using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management_System.Model
{
    abstract class SingleRoom : Room
    {
        protected int udvarMerete;
        protected SingleRoom(int udvarMerete) : base()
        {
            this.udvarMerete = udvarMerete;
            this.RoomNumber = RoomNumber;
            this.Floor = Floor;
            this.Capacity = 1;
            this.PricePerNight = 15000;
        }
    }
}
