using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AquaSOS.Data;

namespace AquaSOSProject.Pages.PedidoAgua
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IList<AquaSOS.Models.PedidoAgua> Pedidos { get; set; }

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            Pedidos = await _context.PedidosAgua
                .Include(p => p.Usuario)
                .Include(p => p.PontoDistribuicao)
                .ToListAsync();
        }
    }
}
