using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management_System.Model
{
    internal class Hotel
    {
        private string hotelName;
        private int roomCount;
        private int reservations;

        protected string HotelName { get => hotelName; set => hotelName = value; }
        protected int RoomCount { get => roomCount; set => roomCount = value; }
        protected int Reservations { get => reservations; set => reservations = value; }

        public Hotel(string hotelName, int roomCount, int reservations)
        {
            this.HotelName = hotelName;
            this.RoomCount = roomCount;
            this.Reservations = reservations;
        }
    }
}
