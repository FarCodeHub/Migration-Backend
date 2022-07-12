using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string IsMarried { get; set; }
        public string PhoneNumber { get; set; }
        public string VisaType { get; set; }
        public string VisaStatus { get; set; }
        public DateTime VisaExpirationDate { get; set; }
        public User User { get; set; }
        public Visa Visa { get; set; }



    }
}
