using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management_System.Model
{
    abstract class Person
    {
        protected int id;
        protected string fullName;
        protected string phoneNumber;

        protected int Id { get => id; set => id = value; }
        protected string FullName { get => fullName; set => fullName = value; }
        protected string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }

        protected Person(int id, string fullName, string phoneNumber)
        {
            this.Id = id;
            this.FullName = fullName;
            this.PhoneNumber = phoneNumber;
        }

        protected Person()
        {
        }
    }
}
