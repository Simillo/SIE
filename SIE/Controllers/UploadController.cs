using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SIE.Helpers;
using SIE.Auxiliary;

namespace SIE.Controllers
{
    [Route("api/[controller]")]
    public class UploadController : Controller
    {
        private readonly IConfiguration _configuration;
        public UploadController(IConfiguration configuration) => _configuration = configuration;

        [HttpPost]
        [Route("Upload")]
        public IActionResult Upload()
        {
            var files = Request.Form.Files;

            var filesName = FileExtensions.Files(files, _configuration["Directory:TEMP"]);

            return Ok(ResponseContent.Create(filesName, HttpStatusCode.OK, null));

        }
    }
}