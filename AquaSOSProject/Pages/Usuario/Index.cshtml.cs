using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AquaSOS.Data;

namespace AquaSOSProject.Pages.Usuario
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IList<AquaSOS.Models.Usuario> Usuarios { get; set; }

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            Usuarios = await _context.Usuarios.ToListAsync();
        }
    }
}
