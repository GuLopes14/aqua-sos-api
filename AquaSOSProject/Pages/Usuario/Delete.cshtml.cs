using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AquaSOS.Data;

namespace AquaSOSProject.Pages.Usuario
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AquaSOS.Models.Usuario Usuario { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
                return NotFound();

            Usuario = await _context.Usuarios.FindAsync(id);
            if (Usuario == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
                return NotFound();

            var usuarioParaExcluir = await _context.Usuarios.FindAsync(id);
            if (usuarioParaExcluir != null)
            {
                _context.Usuarios.Remove(usuarioParaExcluir);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Index");
        }
    }
}
