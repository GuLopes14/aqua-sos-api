using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AquaSOS.Data;
using AquaSOS.Models;
using AquaSOS.DTOs;

namespace AquaSOS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PontoDistribuicaoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PontoDistribuicaoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PontoDistribuicaoDTO>>> GetAll()
        {
            var pontos = await _context.PontosDistribuicao
                .Select(p => new PontoDistribuicaoDTO
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Endereco = p.Endereco,
                    Cidade = p.Cidade,
                    CapacidadeTotalLitros = p.CapacidadeTotalLitros
                }).ToListAsync();

            return Ok(pontos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PontoDistribuicaoDTO>> GetById(long id)
        {
            var ponto = await _context.PontosDistribuicao.FindAsync(id);
            if (ponto == null) return NotFound();

            return Ok(new PontoDistribuicaoDTO
            {
                Id = ponto.Id,
                Nome = ponto.Nome,
                Endereco = ponto.Endereco,
                Cidade = ponto.Cidade,
                CapacidadeTotalLitros = ponto.CapacidadeTotalLitros
            });
        }

        [HttpPost]
        public async Task<ActionResult> Create(PontoDistribuicaoDTO dto)
        {
            var ponto = new PontoDistribuicao
            {
                Nome = dto.Nome,
                Endereco = dto.Endereco,
                Cidade = dto.Cidade,
                CapacidadeTotalLitros = dto.CapacidadeTotalLitros
            };

            _context.PontosDistribuicao.Add(ponto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = ponto.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(long id, PontoDistribuicaoDTO dto)
        {
            if (id != dto.Id) return BadRequest();

            var ponto = await _context.PontosDistribuicao.FindAsync(id);
            if (ponto == null) return NotFound();

            ponto.Nome = dto.Nome;
            ponto.Endereco = dto.Endereco;
            ponto.Cidade = dto.Cidade;
            ponto.CapacidadeTotalLitros = dto.CapacidadeTotalLitros;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            var ponto = await _context.PontosDistribuicao.FindAsync(id);
            if (ponto == null) return NotFound();

            _context.PontosDistribuicao.Remove(ponto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
