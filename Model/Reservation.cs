using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management_System.Model
{
    internal class Reservation
    {
        protected int id;
        protected int guestId;
        protected int roomNumber;
        protected DateTime checkIn;
        protected DateTime checkOut;
        protected string status;

        public double calculatePrice(double pricePerNight)
        {
            return Convert.ToDouble(checkIn- checkOut) * pricePerNight;
        }
    }
}
