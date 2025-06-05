using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AquaSOS.Data;

namespace AquaSOSProject.Pages.PontoDistribuicao
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IList<AquaSOS.Models.PontoDistribuicao> Pontos { get; set; }

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            Pontos = await _context.PontosDistribuicao.ToListAsync();
        }
    }
}
