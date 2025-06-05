using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AquaSOS.Data;

namespace AquaSOSProject.Pages.Usuario
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(Usuario.Id))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToPage("Index");
        }

        private bool UsuarioExists(long id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
    }
}
