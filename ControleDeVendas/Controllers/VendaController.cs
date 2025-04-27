using ControleDeVendas.Application.Services;
using ControleDeVendas.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeVendas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VendaController : ControllerBase
    {
        private readonly VendaService _vendaService;

        public VendaController(VendaService vendaService)
        {
            _vendaService = vendaService;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Venda venda)
        {
            try
            {
                _vendaService.RealizarVenda(venda);
                return Ok("Venda realizada com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
