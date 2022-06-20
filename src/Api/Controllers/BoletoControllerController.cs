using Api.Models;
using BoletoNetCore.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/boletos")]
    [ApiController]
    public class BoletoController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] BoletoModel inputModel)
        {
            return Ok(TipoImpressaoBoleto.Banco);
        }
    }
}