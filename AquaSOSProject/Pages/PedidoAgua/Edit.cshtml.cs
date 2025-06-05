using System.Linq;
using System.Threading.Tasks;
using AquaSOS.Data;
using AquaSOS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AquaSOSProject.Pages.PedidoAgua
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AquaSOS.Models.PedidoAgua Pedido { get; set; } = default!;

        public SelectList UsuariosSelect { get; set; } = default!;
        public SelectList PontosSelect { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
                return NotFound();

            Pedido = await _context.PedidosAgua
                .Include(p => p.Usuario)
                .Include(p => p.PontoDistribuicao)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Pedido == null)
                return NotFound();

            CarregarSelects();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Pedido.UsuarioId == 0)
            {
                ModelState.AddModelError("Pedido.UsuarioId", "Selecione um usuário.");
            }

            if (Pedido.PontoDistribuicaoId == 0)
            {
                ModelState.AddModelError("Pedido.PontoDistribuicaoId", "Selecione um ponto de distribuição.");
            }

            if (string.IsNullOrWhiteSpace(Pedido.Status))
            {
                ModelState.AddModelError("Pedido.Status", "O status é obrigatório.");
            }

            if (!ModelState.IsValid)
            {
                CarregarSelects();
                return Page();
            }

            var existingPedido = await _context.PedidosAgua.FindAsync(Pedido.Id);
            if (existingPedido == null)
                return NotFound();

            existingPedido.UsuarioId = Pedido.UsuarioId;
            existingPedido.PontoDistribuicaoId = Pedido.PontoDistribuicaoId;
            existingPedido.QuantidadeLitros = Pedido.QuantidadeLitros;
            existingPedido.Status = Pedido.Status;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoExists(Pedido.Id))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToPage("Index");
        }

        private bool PedidoExists(long id)
        {
            return _context.PedidosAgua.Any(e => e.Id == id);
        }

        private void CarregarSelects()
        {
            var usuarios = _context.Usuarios.OrderBy(u => u.Nome).ToList();
            var pontos = _context.PontosDistribuicao.OrderBy(p => p.Nome).ToList();

            UsuariosSelect = new SelectList(usuarios, "Id", "Nome", Pedido?.UsuarioId);
            PontosSelect = new SelectList(pontos, "Id", "Nome", Pedido?.PontoDistribuicaoId);
        }
    }
}