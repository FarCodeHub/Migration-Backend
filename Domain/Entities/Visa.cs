using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Visa
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string VisaType { get; set; }
        public string VisaStatus { get; set; }
        public DateTime VisaExpairationDate { get; set; }
        public Person Person { get; set; }

    }
}
