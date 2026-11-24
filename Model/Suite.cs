namespace Hotel_Management_System.Model
{
    public class Suite : Room
    {
        protected double balconySize;
        public double BalconySize { get => balconySize; set => balconySize = value; }

        public Suite(int roomNumber, int floor, int capacity, double balconySize, double pricePerNight = 100000)
            : base(roomNumber, floor, capacity, pricePerNight)
        {
            if (balconySize < 0) throw new System.ArgumentException("Az erkély mérete nem lehet negatív.");
            BalconySize = balconySize;
        }

        public double GetBalconySize() { return BalconySize; }

        public override double CalculatePrice()
        {
            return PricePerNight * 1.5 + BalconySize * 500;
        }

        public override string GetDetails()
        {
            return base.GetDetails() + string.Format(" | Panoráma erkély: {0:0.##} m²", BalconySize);
        }
    }
}
