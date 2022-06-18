
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;


namespace BLL.Model.Request
{
    public class DummyNidOcrRequest
    {
        
        public string id_front { get; set; }
        public string id_back { get; set; }
    }
}