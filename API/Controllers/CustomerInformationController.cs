using BLL.Model.Request;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerInformationController : ControllerBase
    {
        private readonly INidUploadService _nidUploadService;
        public CustomerInformationController(INidUploadService nidUploadService)
        {
            _nidUploadService = nidUploadService;
        }
        [HttpPost("Nid-upload")]
        public async Task<ActionResult> CustomerNid([FromForm] NidUploadRequest request)
        {
            return Ok(await _nidUploadService.GetNidInfo(request));

        }
    }
}
