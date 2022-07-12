using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Migration.Models
{
    public class FileModel
    {
        public IFormFile File { get; set; }
        public int UserId { get; set; }
        
    }
}
