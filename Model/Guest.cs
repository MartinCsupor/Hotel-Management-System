using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management_System.Model
{
    abstract class Guest : Person
    {
        protected int reservationId;

        protected Guest(int reservationId) : base()
        {
            this.ReservationId = reservationId;
        }

        protected int ReservationId { get => reservationId; set => reservationId = value; }
    }
}
