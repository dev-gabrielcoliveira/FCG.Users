using FGC.CatalogAPI.Application.DTOs;
using FGC.Contracts.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace FGC.CatalogAPI.Controllers
{
    [ApiController]
    [Route("controller")]
    public class ComprasController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public ComprasController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost]
        public async Task<IActionResult> EfetuarCompra([FromBody] CompraInput input)
        {
            // Quando for inclementado o endpoint de jogos validar se o jogo existe.

            var orderPlacedEvent = new OrderPlacedEvent
            (
                input.IdUsuario,
                input.IdJogo,
                input.Preco
            );

            await _publishEndpoint.Publish(orderPlacedEvent);

            Console.WriteLine($"[CatalogAPI] Pedido publicado para o Usuário {input.IdUsuario} e Jogo {input.IdJogo}");

            return Accepted();

        }
    }
}
