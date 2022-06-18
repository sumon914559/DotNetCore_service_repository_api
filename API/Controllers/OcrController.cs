using System.Threading.Tasks;
using BLL.Model.Request;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OcrController : ControllerBase
    {
        private readonly IOcrDataService _ocrDataService;
       

        public OcrController(IOcrDataService  ocrDataService )
        {
            _ocrDataService = ocrDataService;
           
        }
        
        [HttpPost("dummyNidOcr")]
        public async Task<IActionResult> CustomerNidData([FromBody] DummyNidOcrRequest request)
        {
            return Ok(await _ocrDataService.GetNidData(request));

        }
        
    }
}