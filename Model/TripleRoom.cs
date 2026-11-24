namespace Hotel_Management_System.Model
{
    public class TripleRoom : Room
    {
        protected bool isOneDoubleBed;
        public bool IsOneDoubleBed { get => isOneDoubleBed; set => isOneDoubleBed = value; }

        public TripleRoom(int roomNumber, int floor, bool isOneDoubleBed, double pricePerNight = 27000)
            : base(roomNumber, floor, 3, pricePerNight)
        {
            IsOneDoubleBed = isOneDoubleBed;
        }

        public bool GetIsOneDoubleBed() { return IsOneDoubleBed; }

        public override double CalculatePrice()
        {
            return PricePerNight * (IsOneDoubleBed ? 1.4 : 1.35);
        }

        public override string GetDetails()
        {
            return base.GetDetails() + (IsOneDoubleBed ? " | 1 franciaágy + 1 ágy" : " | 3 különálló ágy");
        }
    }
}
