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
            return StatusCode(200, BancoDeDados.ListarCombustiveis());
        }

        [HttpGet("CombustivelEspecifico/{codigoCombustivel}")]
        public IActionResult CombustivelEspecifico(int codigoCombustivel)
        {
           Combustivel? combustivelEncontrado = BancoDeDados.BuscaCombustivelEspecifico(codigoCombustivel);
            if (combustivelEncontrado == null)
                return StatusCode(400, "Nenhum combustivel com esse código foi encontrado");

            return StatusCode(200, combustivelEncontrado);
        }

        [HttpPost("Comprar Combustivel")]
        public IActionResult ComprarCombustivel (int codigoDeCombustivel, double litros)
        {
            Combustivel? combustivelEscolhido = null;
            
            foreach (Combustivel combustivel in BancoDeDados.ListarCombustiveis())
            {
                if(combustivel.CodigoDoProduto == codigoDeCombustivel)
                {
                    combustivelEscolhido = combustivel;
                    break;
                }
            }

            if (combustivelEscolhido == null)
                return StatusCode(400, "Nenhum código foi encontrado");

            Compra compra = new Compra();
            compra.Combustivel = combustivelEscolhido;
            compra.DataCompra = DateTime.Now;
            compra.ValorTotal = combustivelEscolhido.PrecoLitro * litros;

            BancoDeDados.Compras.Add(compra);

            return StatusCode(200, compra);
        }

        [HttpGet("Extrato")]
        public IActionResult Extrato()
        {
            return StatusCode(200, BancoDeDados.Compras);
        }
    } 
}
