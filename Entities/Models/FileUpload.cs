using Microsoft.AspNetCore.Http;

namespace Entities.Models
{
    public class FileUpload
    {
        public IFormFile files { get; set; }
            
    }
}