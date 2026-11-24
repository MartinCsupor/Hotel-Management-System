using System;

namespace Hotel_Management_System.Model
{
    public enum ReservationStatus
    {
        Active,
        Cancelled,
        Completed
    }

    public class Reservation
    {
        protected int id;
        protected int hotelId;
        protected string guestId;
        protected int roomNumber;
        protected DateTime checkIn;
        protected DateTime checkOut;
        protected ReservationStatus status;
        protected double totalPrice;

        public int Id { get => id; set => id = value; }
        public int HotelId { get => hotelId; set => hotelId = value; }
        public string GuestId { get => guestId; set => guestId = value; }
        public int RoomNumber { get => roomNumber; set => roomNumber = value; }
        public DateTime CheckIn { get => checkIn; set => checkIn = value; }
        public DateTime CheckOut { get => checkOut; set => checkOut = value; }
        public ReservationStatus Status { get => status; set => status = value; }
        public double TotalPrice { get => totalPrice; set => totalPrice = value; }

        public Reservation(int id, int hotelId, string guestId, Room room, DateTime checkIn, DateTime checkOut)
        {
            if (room == null) throw new ArgumentNullException("room");
            if (string.IsNullOrWhiteSpace(guestId)) throw new ArgumentException("A vendég azonosítója kötelező.");
            if (checkOut.Date <= checkIn.Date) throw new ArgumentException("A távozásnak az érkezés után kell történnie.");

            Id = id;
            HotelId = hotelId;
            GuestId = guestId.Trim();
            RoomNumber = room.GetRoomNumber();
            CheckIn = checkIn.Date;
            CheckOut = checkOut.Date;
            Status = ReservationStatus.Active;
            TotalPrice = CalculatePrice(room);
        }

        private Reservation()
        {
        }

        public static Reservation Restore(int id, int hotelId, string guestId, int roomNumber,
            DateTime checkIn, DateTime checkOut, ReservationStatus status, double totalPrice)
        {
            if (checkOut.Date <= checkIn.Date) throw new ArgumentException("Érvénytelen foglalási időszak.");
            return new Reservation
            {
                Id = id,
                HotelId = hotelId,
                GuestId = guestId,
                RoomNumber = roomNumber,
                CheckIn = checkIn.Date,
                CheckOut = checkOut.Date,
                Status = status,
                TotalPrice = totalPrice
            };
        }

        public int GetId() { return Id; }
        public int GetHotelId() { return HotelId; }
        public string GetGuestId() { return GuestId; }
        public int GetRoomNumber() { return RoomNumber; }
        public DateTime GetCheckIn() { return CheckIn; }
        public DateTime GetCheckOut() { return CheckOut; }
        public ReservationStatus GetStatus() { return Status; }
        public double GetTotalPrice() { return TotalPrice; }
        public int GetNights() { return (CheckOut - CheckIn).Days; }

        public double CalculatePrice(Room room)
        {
            if (room == null || room.GetRoomNumber() != RoomNumber)
                throw new ArgumentException("A foglaláshoz tartozó szoba nem található.");
            return GetNights() * room.CalculatePrice();
        }

        public void Cancel()
        {
            if (Status != ReservationStatus.Active)
                throw new InvalidOperationException("Csak aktív foglalás mondható le.");
            Status = ReservationStatus.Cancelled;
        }

        public void Complete()
        {
            if (Status == ReservationStatus.Active)
                Status = ReservationStatus.Completed;
        }

        public override string ToString()
        {
            return string.Format("#{0} | Vendég: {1} | Szoba: {2} | {3:yyyy-MM-dd} - {4:yyyy-MM-dd} | {5} éj | {6:0} Ft | {7}",
                Id, GuestId, RoomNumber, CheckIn, CheckOut, GetNights(), TotalPrice, Status);
        }
    }
}