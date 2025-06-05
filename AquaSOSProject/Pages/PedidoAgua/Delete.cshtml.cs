using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AquaSOS.Data;

namespace AquaSOSProject.Pages.PedidoAgua
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AquaSOS.Models.PedidoAgua Pedido { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
                return NotFound();

            Pedido = await _context.PedidosAgua
                .Include(p => p.Usuario)
                .Include(p => p.PontoDistribuicao)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (Pedido == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
                return NotFound();

            Pedido = await _context.PedidosAgua.FindAsync(id);

            if (Pedido != null)
            {
                _context.PedidosAgua.Remove(Pedido);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Index");
        }
    }
}
