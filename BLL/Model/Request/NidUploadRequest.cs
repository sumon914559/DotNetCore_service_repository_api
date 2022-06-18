
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;


namespace BLL.Model.Request
{
    public class NidUploadRequest
    {
        // [AllowedExtensions(new string[] { ".jpg", ".png" ".jpg"})] 
        public IFormFile nidFont { get; set; }
        public IFormFile nidBack { get; set; }
    }
}