using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AquaSOS.Data;

namespace AquaSOSProject.Pages.PontoDistribuicao
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AquaSOS.Models.PontoDistribuicao Ponto { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
                return NotFound();

            Ponto = await _context.PontosDistribuicao.FindAsync(id);
            if (Ponto == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Ponto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PontoExists(Ponto.Id))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToPage("Index");
        }

        private bool PontoExists(long id)
        {
            return _context.PontosDistribuicao.Any(e => e.Id == id);
        }
    }
}
