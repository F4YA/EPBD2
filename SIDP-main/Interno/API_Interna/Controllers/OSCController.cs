using API_Interna.Business;
using API_Interna.DTOs;
using API_Interna.Interfaces.Business;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Interna.Controllers
{
    [Route("api/osc")]
    [ApiController]
    public class OSCController : ControllerBase
    {
        private readonly IOSCBusiness _oscBusiness;
        public OSCController(IOSCBusiness oscBusiness)
        {
            _oscBusiness = oscBusiness;
        }
        // GET: api/osc/c
        [HttpGet]
        [Route("/")]
        public ActionResult<OSCDTO> Get([FromQuery] string cnpj)
        {
            if (cnpj == null || cnpj.Length == 0) return BadRequest();

            OSCDTO? result = _oscBusiness.Get(cnpj);

            if (result == null) return NotFound();

            return Ok(result);
        }

        // POST api/osc
        [HttpPost]
        public ActionResult<Boolean> Post([FromBody] OSCDTO osc)
        {
            if (osc == null) return BadRequest();

            var result = _oscBusiness.Save(osc);

            if(result) return Ok(result);

            return BadRequest();
        }
    }
}
