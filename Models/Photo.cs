using Microsoft.AspNetCore.Http;
using System;

namespace Models
{
    public class Photo
    {
        public IFormFile photo { get; set; }
        public string fileName { get; set; }
        public string filePath { get; set; }
        public string fileExt { get; set; }
        public string ImageUrl { get; set; }
    }
}
