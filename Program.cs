using Hotel_Management_System.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Hotel_Management_System
{
    internal class Program
    {
        private static readonly string DataFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "hotel-data.txt");

        private static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Hotel hotel = Hotel.LoadData(DataFile) ?? CreateDefaultHotel();
            bool running = true;

            while (running)
            {
                ShowMenu(hotel);
                switch (Console.ReadLine())
                {
                    case "1": ShowRooms(hotel); break;
                    case "2": CreateReservation(hotel); break;
                    case "3": ShowReservations(hotel); break;
                    case "4": ManageGuests(hotel); break;
                    case "5": CancelReservation(hotel); break;
                    case "6": Save(hotel); break;
                    case "0": Save(hotel); running = false; break;
                    default: Console.WriteLine("Kérem, válasszon a menüpontok közül."); break;
                }

                if (running) Pause();
            }
        }

        private static Hotel CreateDefaultHotel()
        {
            Hotel hotel = new Hotel(1, "Napfény Hotel", "Budapest, Minta utca 1.", 4);
            hotel.AddRoom(new SingleRoom(101, 1, 8));
            hotel.AddRoom(new SingleRoom(102, 1, 14));
            hotel.AddRoom(new DoubleRoom(201, 2, true));
            hotel.AddRoom(new DoubleRoom(202, 2, false));
            hotel.AddRoom(new TripleRoom(301, 3, true));
            hotel.AddRoom(new TripleRoom(302, 3, false));
            hotel.AddRoom(new Suite(401, 4, 4, 20));
            return hotel;
        }

        private static void ShowMenu(Hotel hotel)
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("          {0} ({1} csillag)", hotel.GetHotelName(), hotel.GetStars());
            Console.WriteLine("========================================");
            Console.WriteLine("1 - Szobák megtekintése");
            Console.WriteLine("2 - Új foglalás");
            Console.WriteLine("3 - Foglalások megtekintése");
            Console.WriteLine("4 - Vendégek kezelése");
            Console.WriteLine("5 - Foglalás lemondása");
            Console.WriteLine("6 - Adatok mentése");
            Console.WriteLine("0 - Kilépés");
            Console.Write("Választás: ");
        }

        private static void ShowRooms(Hotel hotel)
        {
            Console.WriteLine("\nSzobák:");
            foreach (Room room in hotel.GetRooms())
                Console.WriteLine(room.GetDetails());
        }

        private static void CreateReservation(Hotel hotel)
        {
            Console.WriteLine("\n--- Új foglalás ---");
            Guest guest = ChooseGuest(hotel);
            if (guest == null) return;

            DateTime checkIn, checkOut;
            ReadDates(out checkIn, out checkOut);

            List<Room> availableRooms = ChooseAvailableRooms(hotel, checkIn, checkOut);
            if (availableRooms.Count == 0)
            {
                Console.WriteLine("Ebben az időszakban nincs szabad szoba a kiválasztott típusból.");
                return;
            }

            Console.WriteLine("\nSzabad szobák:");
            foreach (Room room in availableRooms)
                Console.WriteLine(room.GetDetails());

            int roomNumber = ReadInteger("Szobaszám (0 = első szabad szoba): ", 0, 99999);
            if (roomNumber == 0)
                roomNumber = availableRooms[0].GetRoomNumber();

            while (!availableRooms.Any(room => room.GetRoomNumber() == roomNumber))
            {
                Console.WriteLine("Ez a szoba nem választható.");
                roomNumber = ReadInteger("Szobaszám (0 = első szabad szoba): ", 0, 99999);
                if (roomNumber == 0)
                    roomNumber = availableRooms[0].GetRoomNumber();
            }

            try
            {
                Reservation reservation = hotel.CreateReservation(guest.Id, roomNumber, checkIn, checkOut);
                Save(hotel);
                Console.WriteLine("\nA foglalás sikeres:");
                Console.WriteLine(reservation);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hiba: " + ex.Message);
            }
        }

        private static Guest ChooseGuest(Hotel hotel)
        {
            List<Guest> guests = hotel.GetGuests().ToList();
            if (guests.Count == 0)
            {
                Console.WriteLine("Még nincs vendég rögzítve.");
                return CreateGuest(hotel);
            }

            Console.WriteLine("Válasszon vendéget:");
            foreach (Guest guest in guests)
                Console.WriteLine(guest);

            int guestId = ReadInteger("Vendég sorszáma: ", 1, guests.Max(g => g.Id));
            while (hotel.GetGuest(guestId) == null)
            {
                Console.WriteLine("Ilyen vendég nem létezik.");
                guestId = ReadInteger("Vendég sorszáma: ", 1, guests.Max(g => g.Id));
            }
            return hotel.GetGuest(guestId);
        }

        private static List<Room> ChooseAvailableRooms(Hotel hotel, DateTime checkIn, DateTime checkOut)
        {
            Console.WriteLine("\nMilyen szobát szeretne?");
            Console.WriteLine("1 - Egyágyas");
            Console.WriteLine("2 - Kétágyas");
            Console.WriteLine("3 - Háromágyas");
            Console.WriteLine("4 - Lakosztály");

            int type = ReadInteger("Szobatípus: ", 1, 4);
            return hotel.GetAvailableRooms(type, checkIn, checkOut).ToList();
        }

        private static Guest CreateGuest(Hotel hotel)
        {
            Console.Write("Vendég neve: ");
            try
            {
                Guest guest = hotel.CreateGuest(Console.ReadLine());
                Save(hotel);
                Console.WriteLine("Vendég létrehozva: " + guest);
                return guest;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hiba: " + ex.Message);
                return null;
            }
        }

        private static void ManageGuests(Hotel hotel)
        {
            Console.WriteLine("\n--- Vendégek ---");
            foreach (Guest guest in hotel.GetGuests())
                Console.WriteLine(guest);

            Console.WriteLine("\n1 - Új vendég | 0 - Vissza");
            if (Console.ReadLine() == "1")
                CreateGuest(hotel);
        }

        private static void ShowReservations(Hotel hotel)
        {
            Console.WriteLine("\nFoglalások:");
            if (!hotel.GetReservations().Any())
            {
                Console.WriteLine("Még nincs foglalás.");
                return;
            }
            foreach (Reservation reservation in hotel.GetReservations())
                Console.WriteLine(reservation);
        }

        private static void CancelReservation(Hotel hotel)
        {
            int id = ReadInteger("Lemondandó foglalás azonosítója: ", 1, 99999);
            try
            {
                hotel.CancelReservation(id);
                Save(hotel);
                Console.WriteLine("A foglalás lemondva.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hiba: " + ex.Message);
            }
        }

        private static void ReadDates(out DateTime checkIn, out DateTime checkOut)
        {
            checkIn = ReadDate("Érkezés");
            do
            {
                checkOut = ReadDate("Távozás");
                if (checkOut <= checkIn)
                    Console.WriteLine("A távozásnak az érkezés után kell lennie.");
            } while (checkOut <= checkIn);
        }

        private static DateTime ReadDate(string label)
        {
            while (true)
            {
                Console.WriteLine(label + " dátuma:");
                int year = ReadInteger("  Év: ", 1, 9999);
                int month = ReadInteger("  Hónap: ", 1, 12);
                int day = ReadInteger("  Nap: ", 1, 31);
                try { return new DateTime(year, month, day); }
                catch (ArgumentOutOfRangeException) { Console.WriteLine("Ez a dátum nem létezik."); }
            }
        }

        private static int ReadInteger(string prompt, int minimum, int maximum)
        {
            while (true)
            {
                Console.Write(prompt);
                int value;
                if (int.TryParse(Console.ReadLine(), out value) && value >= minimum && value <= maximum)
                    return value;
                Console.WriteLine("Érvénytelen érték, próbálja újra.");
            }
        }

        private static void Save(Hotel hotel)
        {
            hotel.SaveData(DataFile);
            Console.WriteLine("Az adatok mentve.");
        }

        private static void Pause()
        {
            Console.WriteLine("\nNyomjon Entert a főmenühöz...");
            Console.ReadLine();
        }
    }
}