using System.Net;
using Microsoft.AspNetCore.Mvc;
using SIE.Context;
using SIE.Helpers;
using SIE.Utils;

namespace SIE.Controllers
{
    [Route("api/[controller]")]
    public class InstitutionController : Controller
    {
        private readonly UInstitution _uInstitution;
        public InstitutionController(SIEContext context)
        {
            _uInstitution = new UInstitution(context);
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult Get()
        {
            var res = _uInstitution.Get();
            return Ok(ResponseContent.Create(res, HttpStatusCode.OK, null));
        }

        [HttpGet("{name}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult Get(string name)
        {
            var res = _uInstitution.Get(name);
            return Ok(ResponseContent.Create(res, HttpStatusCode.OK, null));
        }
    }
}