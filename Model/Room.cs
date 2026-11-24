namespace Hotel_Management_System.Model
{
    public abstract class Room
    {
        protected int roomNumber;
        protected int floor;
        protected int capacity;
        protected double pricePerNight;

        public int RoomNumber { get => roomNumber; set => roomNumber = value; }
        public int Floor { get => floor; set => floor = value; }
        public int Capacity { get => capacity; set => capacity = value; }
        public double PricePerNight { get => pricePerNight; set => pricePerNight = value; }

        protected Room(int roomNumber, int floor, int capacity, double pricePerNight)
        {
            if (roomNumber <= 0 || floor < 0 || capacity <= 0 || pricePerNight < 0)
                throw new System.ArgumentException("A szoba adatai érvénytelenek.");

            RoomNumber = roomNumber;
            Floor = floor;
            Capacity = capacity;
            PricePerNight = pricePerNight;
        }

        public abstract double CalculatePrice();

        public int GetRoomNumber() { return RoomNumber; }
        public int GetFloor() { return Floor; }
        public int GetCapacity() { return Capacity; }
        public double GetPricePerNight() { return PricePerNight; }

        public virtual string GetDetails()
        {
            return string.Format("{0}. szoba | {1}. emelet | {2} fő | {3:0} Ft/éj", RoomNumber, Floor, Capacity, CalculatePrice());
        }

        protected string FormatPrice(double price)
        {
            return string.Format("{0:0} Ft/éj", price);
        }
    }
}
