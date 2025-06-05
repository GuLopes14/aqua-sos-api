using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AquaSOS.Data;

namespace AquaSOSProject.Pages.PontoDistribuicao
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
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

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
                return NotFound();

            var pontoParaExcluir = await _context.PontosDistribuicao.FindAsync(id);
            if (pontoParaExcluir != null)
            {
                _context.PontosDistribuicao.Remove(pontoParaExcluir);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Index");
        }
    }
}
