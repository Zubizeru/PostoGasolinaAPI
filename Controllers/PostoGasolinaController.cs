using Microsoft.AspNetCore.Mvc;

namespace PostoGasolinaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostoGasolinaController : Controller
    {
        [HttpGet("ListaCombustiveis")]
        public IActionResult ListaCombustiveis()
        {
            return StatusCode(200, BancoDeDados.Combustiveis);
        }
    }
}
