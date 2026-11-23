using Hotel_Management_System.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        }
    }
}