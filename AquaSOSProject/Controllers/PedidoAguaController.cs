using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AquaSOS.Data;
using AquaSOS.Models;
using AquaSOS.DTOs;

namespace AquaSOS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoAguaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PedidoAguaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PedidoAguaDTO>>> GetPedidos()
        {
            var pedidos = await _context.PedidosAgua
                .Include(p => p.Usuario)
                .Include(p => p.PontoDistribuicao)
                .Select(p => new PedidoAguaDTO
                {
                    Id = p.Id,
                    UsuarioId = p.UsuarioId,
                    PontoDistribuicaoId = p.PontoDistribuicaoId,
                    QuantidadeLitros = p.QuantidadeLitros,
                    DataSolicitacao = p.DataSolicitacao,
                    Status = p.Status
                }).ToListAsync();

            return Ok(pedidos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PedidoAguaDTO>> GetPedido(long id)
        {
            var pedido = await _context.PedidosAgua
                .Include(p => p.Usuario)
                .Include(p => p.PontoDistribuicao)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null) return NotFound();

            return Ok(new PedidoAguaDTO
            {
                Id = pedido.Id,
                UsuarioId = pedido.UsuarioId,
                PontoDistribuicaoId = pedido.PontoDistribuicaoId,
                QuantidadeLitros = pedido.QuantidadeLitros,
                DataSolicitacao = pedido.DataSolicitacao,
                Status = pedido.Status
            });
        }

        [HttpPost]
        public async Task<ActionResult> CreatePedido(PedidoAguaDTO dto)
        {
            var pedido = new PedidoAgua
            {
                UsuarioId = dto.UsuarioId,
                PontoDistribuicaoId = dto.PontoDistribuicaoId,
                QuantidadeLitros = dto.QuantidadeLitros,
                DataSolicitacao = dto.DataSolicitacao,
                Status = dto.Status
            };

            _context.PedidosAgua.Add(pedido);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPedido), new { id = pedido.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePedido(long id, PedidoAguaDTO dto)
        {
            if (id != dto.Id) return BadRequest();

            var pedido = await _context.PedidosAgua.FindAsync(id);
            if (pedido == null) return NotFound();

            pedido.UsuarioId = dto.UsuarioId;
            pedido.PontoDistribuicaoId = dto.PontoDistribuicaoId;
            pedido.QuantidadeLitros = dto.QuantidadeLitros;
            pedido.Status = dto.Status;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePedido(long id)
        {
            var pedido = await _context.PedidosAgua.FindAsync(id);
            if (pedido == null) return NotFound();

            _context.PedidosAgua.Remove(pedido);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
