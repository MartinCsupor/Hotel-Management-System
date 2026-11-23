using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management_System.Model
{
    abstract class Room
    {
        private int roomNumber;
        private int floor;
        private int capacity;

        private double pricePerNight;

        protected int RoomNumber { get => roomNumber; set => roomNumber = value; }
        protected int Floor { get => floor; set => floor = value; }
        protected int Capacity { get => capacity; set => capacity = value; }
        protected double PricePerNight { get => pricePerNight; set => pricePerNight = value; }

        protected Room(int roomNumber, int floor, int capacity, double pricePerNight)
        {
            this.RoomNumber = roomNumber;
            this.Floor = floor;
            this.Capacity = capacity;
            this.PricePerNight = pricePerNight;
        }

        protected Room() { }

        public abstract double CalculatePrice(int nights);
        public virtual void GetDetails()
        {
        }
    }
}
