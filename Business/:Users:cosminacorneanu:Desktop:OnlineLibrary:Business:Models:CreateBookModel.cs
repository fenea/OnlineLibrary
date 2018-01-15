using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

namespace Business.Models
{
    public class CreateBookModel
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public IFormFile BookFile { get; set; }

        public string Author { get; set; }

        public IFormFile PhotoPath { get; set; }

        public string Description { get; set; }


    }
}
