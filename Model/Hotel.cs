using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Hotel_Management_System.Model
{
    public class Hotel
    {
        protected int id;
        protected string hotelName;
        protected string address;
        protected int stars;
        protected List<Room> rooms;
        protected List<Reservation> reservations;
        protected List<Guest> guests;

        public int Id { get => id; set => id = value; }
        public string HotelName { get => hotelName; set => hotelName = value; }
        public string Address { get => address; set => address = value; }
        public int Stars { get => stars; set => stars = value; }
        public List<Room> Rooms { get => rooms; set => rooms = value; }
        public List<Reservation> Reservations { get => reservations; set => reservations = value; }
        public List<Guest> Guests { get => guests; set => guests = value; }

        public int GetId() { return Id; }
        public string GetHotelName() { return HotelName; }
        public string GetAddress() { return Address; }
        public int GetStars() { return Stars; }
        public IEnumerable<Room> GetRooms() { return Rooms; }
        public IEnumerable<Reservation> GetReservations() { return Reservations; }
        public IEnumerable<Guest> GetGuests() { return Guests; }

        public Hotel(int id, string hotelName, string address, int stars)
        {
            if (id <= 0 || string.IsNullOrWhiteSpace(hotelName) || stars < 1 || stars > 5)
                throw new ArgumentException("A szálloda adatai érvénytelenek.");

            Id = id;
            HotelName = hotelName.Trim();
            Address = address ?? string.Empty;
            Stars = stars;
            Rooms = new List<Room>();
            Reservations = new List<Reservation>();
            Guests = new List<Guest>();
        }

        public void AddRoom(Room room)
        {
            if (room == null) throw new ArgumentNullException("room");
            if (Rooms.Any(r => r.GetRoomNumber() == room.GetRoomNumber()))
                throw new InvalidOperationException("Ezzel a számmal már létezik szoba.");
            Rooms.Add(room);
        }

        public Guest CreateGuest(string name)
        {
            int nextId = Guests.Count == 0 ? 1 : Guests.Max(g => g.Id) + 1;
            Guest guest = new Guest(nextId, name);
            Guests.Add(guest);
            return guest;
        }

        public Guest GetGuest(int guestId)
        {
            return Guests.SingleOrDefault(g => g.Id == guestId);
        }

        public IEnumerable<T> GetAvailableRooms<T>(DateTime checkIn, DateTime checkOut) where T : Room
        {
            if (checkOut.Date <= checkIn.Date) throw new ArgumentException("Érvénytelen dátumtartomány.");
            return Rooms.OfType<T>().Where(room => IsRoomAvailable(room.GetRoomNumber(), checkIn, checkOut)).ToList();
        }

        public IEnumerable<Room> GetAvailableRooms(int roomType, DateTime checkIn, DateTime checkOut)
        {
            if (roomType == 1) return GetAvailableRooms<SingleRoom>(checkIn, checkOut);
            if (roomType == 2) return GetAvailableRooms<DoubleRoom>(checkIn, checkOut);
            if (roomType == 3) return GetAvailableRooms<TripleRoom>(checkIn, checkOut);
            if (roomType == 4) return GetAvailableRooms<Suite>(checkIn, checkOut);
            throw new ArgumentException("Érvénytelen szobatípus.");
        }

        public bool IsRoomAvailable(int roomNumber, DateTime checkIn, DateTime checkOut)
        {
            if (checkOut.Date <= checkIn.Date) return false;
            return !Reservations.Any(r => r.GetRoomNumber() == roomNumber && r.GetStatus() == ReservationStatus.Active &&
                r.GetCheckIn() < checkOut.Date && checkIn.Date < r.GetCheckOut());
        }

        public Reservation CreateReservation(int guestId, int roomNumber, DateTime checkIn, DateTime checkOut)
        {
            Guest guest = GetGuest(guestId);
            if (guest == null) throw new InvalidOperationException("A vendég nem található.");
            Room room = Rooms.SingleOrDefault(r => r.GetRoomNumber() == roomNumber);
            if (room == null) throw new InvalidOperationException("A szoba nem található.");
            if (!IsRoomAvailable(roomNumber, checkIn, checkOut))
                throw new InvalidOperationException("A szoba ebben az időszakban foglalt.");

            int nextId = Reservations.Count == 0 ? 1 : Reservations.Max(r => r.GetId()) + 1;
            Reservation reservation = new Reservation(nextId, Id, guest.Id.ToString(CultureInfo.InvariantCulture), room, checkIn, checkOut);
            Reservations.Add(reservation);
            return reservation;
        }

        public void CancelReservation(int reservationId)
        {
            Reservation reservation = Reservations.SingleOrDefault(r => r.GetId() == reservationId);
            if (reservation == null) throw new InvalidOperationException("A foglalás nem található.");
            reservation.Cancel();
        }

        public void SaveData(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                writer.WriteLine("HOTEL|{0}|{1}|{2}|{3}|{4}", Id, Encode(HotelName), Encode(Address), Stars, Rooms.Count);
                foreach (Guest guest in Guests)
                    writer.WriteLine("GUEST|{0}|{1}", guest.Id, Encode(guest.Name));
                foreach (Room room in Rooms)
                {
                    SingleRoom single = room as SingleRoom;
                    DoubleRoom doubleRoom = room as DoubleRoom;
                    TripleRoom triple = room as TripleRoom;
                    Suite suite = room as Suite;
                    if (single != null) writer.WriteLine("ROOM|S|{0}|{1}|{2}|{3}", room.GetRoomNumber(), room.GetFloor(), single.GetYardSize().ToString(CultureInfo.InvariantCulture), room.GetPricePerNight().ToString(CultureInfo.InvariantCulture));
                    else if (doubleRoom != null) writer.WriteLine("ROOM|D|{0}|{1}|{2}|{3}", room.GetRoomNumber(), room.GetFloor(), doubleRoom.GetIsDoubleBed(), room.GetPricePerNight().ToString(CultureInfo.InvariantCulture));
                    else if (triple != null) writer.WriteLine("ROOM|T|{0}|{1}|{2}|{3}", room.GetRoomNumber(), room.GetFloor(), triple.GetIsOneDoubleBed(), room.GetPricePerNight().ToString(CultureInfo.InvariantCulture));
                    else if (suite != null) writer.WriteLine("ROOM|U|{0}|{1}|{2}|{3}|{4}", room.GetRoomNumber(), room.GetFloor(), room.GetCapacity(), suite.GetBalconySize().ToString(CultureInfo.InvariantCulture), room.GetPricePerNight().ToString(CultureInfo.InvariantCulture));
                }
                foreach (Reservation reservation in Reservations)
                    writer.WriteLine("RES|{0}|{1}|{2}|{3}|{4:O}|{5:O}|{6}|{7}", reservation.GetId(), reservation.GetHotelId(), Encode(reservation.GetGuestId()), reservation.GetRoomNumber(), reservation.GetCheckIn(), reservation.GetCheckOut(), reservation.GetStatus(), reservation.GetTotalPrice().ToString(CultureInfo.InvariantCulture));
            }
        }

        public static Hotel LoadData(string filePath)
        {
            if (!File.Exists(filePath)) return null;
            string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);
            if (lines.Length == 0) return null;
            string[] header = lines[0].Split('|');
            if (header.Length < 5 || header[0] != "HOTEL") return null;

            Hotel hotel = new Hotel(int.Parse(header[1]), Decode(header[2]), Decode(header[3]), int.Parse(header[4]));
            foreach (string line in lines.Skip(1))
            {
                string[] parts = line.Split('|');
                try
                {
                    if (parts[0] == "ROOM") hotel.LoadRoom(parts);
                    else if (parts[0] == "GUEST") hotel.LoadGuest(parts);
                    else if (parts[0] == "RES") hotel.LoadReservation(parts);
                }
                catch (Exception) { }
            }
            return hotel;
        }

        private void LoadGuest(string[] parts)
        {
            Guest guest = new Guest(int.Parse(parts[1]), Decode(parts[2]));
            if (!Guests.Any(g => g.Id == guest.Id)) Guests.Add(guest);
        }

        private void LoadRoom(string[] parts)
        {
            string type = parts[1];
            int number = int.Parse(parts[2]);
            int floor = int.Parse(parts[3]);
            if (type == "S") AddRoom(new SingleRoom(number, floor, double.Parse(parts[4], CultureInfo.InvariantCulture), double.Parse(parts[5], CultureInfo.InvariantCulture)));
            else if (type == "D") AddRoom(new DoubleRoom(number, floor, bool.Parse(parts[4]), double.Parse(parts[5], CultureInfo.InvariantCulture)));
            else if (type == "T") AddRoom(new TripleRoom(number, floor, bool.Parse(parts[4]), double.Parse(parts[5], CultureInfo.InvariantCulture)));
            else if (type == "U") AddRoom(new Suite(number, floor, int.Parse(parts[4]), double.Parse(parts[5], CultureInfo.InvariantCulture), double.Parse(parts[6], CultureInfo.InvariantCulture)));
        }

        private void LoadReservation(string[] parts)
        {
            Reservation reservation = Reservation.Restore(int.Parse(parts[1]), int.Parse(parts[2]), Decode(parts[3]), int.Parse(parts[4]), DateTime.Parse(parts[5], null, DateTimeStyles.RoundtripKind), DateTime.Parse(parts[6], null, DateTimeStyles.RoundtripKind), (ReservationStatus)Enum.Parse(typeof(ReservationStatus), parts[7]), double.Parse(parts[8], CultureInfo.InvariantCulture));
            if (Rooms.Any(r => r.GetRoomNumber() == reservation.GetRoomNumber())) Reservations.Add(reservation);
        }

        private static string Encode(string value)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(value ?? string.Empty));
        }

        private static string Decode(string value)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(value));
        }
    }
}