namespace Hotel_Management_System.Model
{
    public class SingleRoom : Room
    {
        protected double yardSize;
        public double YardSize { get => yardSize; set => yardSize = value; }

        public SingleRoom(int roomNumber, int floor, double yardSize, double pricePerNight = 15000)
            : base(roomNumber, floor, 1, pricePerNight)
        {
            if (yardSize < 0) throw new System.ArgumentException("Az udvar mérete nem lehet negatív.");
            YardSize = yardSize;
        }

        public double GetYardSize() { return YardSize; }

        public override double CalculatePrice()
        {
            return PricePerNight + (YardSize > 10 ? 2000 : 0);
        }

        public override string GetDetails()
        {
            return base.GetDetails() + string.Format(" | Udvar/terasz: {0:0.##} m²", YardSize);
        }
    }
}
