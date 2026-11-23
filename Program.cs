using Hotel_Management_System.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management_System
{
    internal class Program
    {
        public static List<Hotel> hotelek = new List<Hotel>();
        private static void Main(string[] args)
        {
            Hotel hotel1 = new Hotel("Napfény Hotel", 20, 5);
            hotelek.Add(hotel1);

            Console.WriteLine("=================================");
            Console.WriteLine("     HOTEL MANAGEMENT SYSTEM     ");
            Console.WriteLine("=================================");
            Console.WriteLine("1. Hotelek megtekintése");
            Console.WriteLine("2. Szobák megtekintése");
            Console.WriteLine("3. Szabad szobák megtekintése");
            Console.WriteLine("4. Új foglalás létrehozása");
            Console.WriteLine("5. Foglalások megtekintése");
            Console.WriteLine("7. Foglalás módosítása");
            Console.WriteLine("8. Foglalás törlése");
            Console.WriteLine("0. Kilépés");
            Console.Write("Válasszon egy menüpontot: ");

            try
            {
                var valasztas = int.Parse(Console.ReadLine());
                HotelekListazasa();
               
            }
            catch {
                Console.WriteLine("Hiba");
            }
        }

        private static void HotelekListazasa()
        {
            Console.WriteLine("Hotelnév Szobák száma Fogalalások száma");
            hotelek.ForEach(hotel => {
                hotel.hotelDetails();
            });
        }
    }
}