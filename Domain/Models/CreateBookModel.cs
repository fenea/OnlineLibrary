using Microsoft.AspNetCore.Http;

namespace Domain.Models
{
    public class CreateBookModel
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public IFormFile BookFile { get; set; }

        public string Author { get; set; }

        public IFormFile PhotoPath { get; set; }

        public string Description { get; set; }

        public string Path { get; set; }

       // public string PhotoPath { get; set; }



    }
}
