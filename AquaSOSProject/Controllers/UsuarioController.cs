using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AquaSOS.Data;
using AquaSOS.Models;

namespace AquaSOS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsuarioController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Usuario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
            => Ok(await _context.Usuarios.ToListAsync());

        // GET: api/Usuario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(long id)
        {
            var user = await _context.Usuarios.FindAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        // POST: api/Usuario
        [HttpPost]
        public async Task<ActionResult<Usuario>> CreateUsuario(Usuario dto)
        {
            _context.Usuarios.Add(dto);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUsuario), new { id = dto.Id }, dto);
        }

        // PUT: api/Usuario/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(long id, Usuario dto)
        {
            if (id != dto.Id) return BadRequest();
            _context.Entry(dto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Usuario/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(long id)
        {
            var user = await _context.Usuarios.FindAsync(id);
            if (user == null) return NotFound();
            _context.Usuarios.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
