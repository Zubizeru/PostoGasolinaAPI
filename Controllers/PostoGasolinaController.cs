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

        [HttpGet("CombustivelEspecificoForeach/{codigoDeCombustivel}")]
        public IActionResult CombustivelEspecificoForeach(int codigoCombustivel)
        {
            foreach (Combustivel combustivel in BancoDeDados.Combustiveis)
            {
                if (combustivel.CodigoDoProduto == codigoCombustivel)
                {
                    return StatusCode(200,combustivel);
                }
            }
                return StatusCode(400, "Nenhum produto com esse código informado");
        }

        [HttpGet("CombustivelEspecificoLinq/{codigoDeCombustivel}")]
        public IActionResult CombustivelEspecificoLinq(int codigoCombustivel)
        {
            Combustivel? combustivel = BancoDeDados.Combustiveis.Find(c=> c.CodigoDoProduto.Equals(codigoCombustivel));
            if (combustivel == null)
                return StatusCode(400, "Nenhum produto com esse código informado");
            else
                return StatusCode(200, combustivel);
        }

        [HttpPost("Comprar Combustivel")]
        public IActionResult ComprarCombustivel (int codigoDeCombustivel, double litros)
        {
            Combustivel? combustivelEscolhido = null;
            
            foreach (Combustivel combustivel in BancoDeDados.Combustiveis)
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
