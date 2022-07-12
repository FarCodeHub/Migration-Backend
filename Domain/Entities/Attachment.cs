using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
   public class Attachment
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

    }
}
