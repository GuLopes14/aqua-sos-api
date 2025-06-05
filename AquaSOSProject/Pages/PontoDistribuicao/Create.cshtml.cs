using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AquaSOS.Data;

namespace AquaSOSProject.Pages.PontoDistribuicao
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AquaSOS.Models.PontoDistribuicao Ponto { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.PontosDistribuicao.Add(Ponto);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
