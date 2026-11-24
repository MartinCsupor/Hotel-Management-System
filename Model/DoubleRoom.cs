namespace Hotel_Management_System.Model
{
    public class DoubleRoom : Room
    {
        protected bool isDoubleBed;
        public bool IsDoubleBed { get => isDoubleBed; set => isDoubleBed = value; }

        public DoubleRoom(int roomNumber, int floor, bool isDoubleBed, double pricePerNight = 22000)
            : base(roomNumber, floor, 2, pricePerNight)
        {
            IsDoubleBed = isDoubleBed;
        }

        public bool GetIsDoubleBed() { return IsDoubleBed; }

        public override double CalculatePrice()
        {
            return PricePerNight * 1.2 + (IsDoubleBed ? 3000 : 0);
        }

        public override string GetDetails()
        {
            return base.GetDetails() + (IsDoubleBed ? " | Franciaágy" : " | Különálló ágyak");
        }
    }
}
