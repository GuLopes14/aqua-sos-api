using System;
using System.Linq;
using System.Threading.Tasks;
using AquaSOS.Data;
using AquaSOS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AquaSOSProject.Pages.PedidoAgua
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AquaSOS.Models.PedidoAgua Pedido { get; set; } = new AquaSOS.Models.PedidoAgua();

        public SelectList UsuariosSelect { get; set; } = default!;
        public SelectList PontosSelect { get; set; } = default!;

        public void OnGet()
        {
            CarregarSelects();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Console.WriteLine($"DEBUG - UsuarioId: {Pedido.UsuarioId}");
            Console.WriteLine($"DEBUG - PontoDistribuicaoId: {Pedido.PontoDistribuicaoId}");
            Console.WriteLine($"DEBUG - QuantidadeLitros: {Pedido.QuantidadeLitros}");
            Console.WriteLine($"DEBUG - Status: '{Pedido.Status}'");
            Console.WriteLine($"DEBUG - ModelState.IsValid: {ModelState.IsValid}");

            if (Pedido.UsuarioId == 0)
            {
                ModelState.AddModelError("Pedido.UsuarioId", "Selecione um usuário.");
                Console.WriteLine("DEBUG - Erro: UsuarioId é 0");
            }

            if (Pedido.PontoDistribuicaoId == 0)
            {
                ModelState.AddModelError("Pedido.PontoDistribuicaoId", "Selecione um ponto de distribuição.");
                Console.WriteLine("DEBUG - Erro: PontoDistribuicaoId é 0");
            }

            if (string.IsNullOrWhiteSpace(Pedido.Status))
            {
                ModelState.AddModelError("Pedido.Status", "O status é obrigatório.");
                Console.WriteLine("DEBUG - Erro: Status está vazio");
            }

            if (Pedido.QuantidadeLitros <= 0)
            {
                ModelState.AddModelError("Pedido.QuantidadeLitros", "A quantidade deve ser maior que zero.");
                Console.WriteLine("DEBUG - Erro: QuantidadeLitros é <= 0");
            }

            if (!ModelState.IsValid)
            {
                Console.WriteLine("DEBUG - ModelState Errors:");
                foreach (var modelError in ModelState)
                {
                    var key = modelError.Key;
                    var errors = modelError.Value.Errors;
                    foreach (var error in errors)
                    {
                        Console.WriteLine($"  {key}: {error.ErrorMessage}");
                    }
                }

                CarregarSelects();
                return Page();
            }

            try
            {
                Pedido.DataSolicitacao = DateTime.Now;

                Console.WriteLine("DEBUG - Tentando salvar no banco...");

                _context.PedidosAgua.Add(Pedido);
                await _context.SaveChangesAsync();

                Console.WriteLine("DEBUG - Salvo com sucesso!");

                return RedirectToPage("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DEBUG - Erro ao salvar: {ex.Message}");
                Console.WriteLine($"DEBUG - Stack trace: {ex.StackTrace}");

                ModelState.AddModelError("", $"Erro ao salvar: {ex.Message}");
                CarregarSelects();
                return Page();
            }
        }

        private void CarregarSelects()
        {
            try
            {
                var usuarios = _context.Usuarios.OrderBy(u => u.Nome).ToList();
                var pontos = _context.PontosDistribuicao.OrderBy(p => p.Nome).ToList();

                Console.WriteLine($"DEBUG - CarregarSelects: {usuarios.Count} usuários, {pontos.Count} pontos");

                UsuariosSelect = new SelectList(usuarios, "Id", "Nome");
                PontosSelect = new SelectList(pontos, "Id", "Nome");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DEBUG - Erro em CarregarSelects: {ex.Message}");
                UsuariosSelect = new SelectList(new List<AquaSOS.Models.Usuario>(), "Id", "Nome");
                PontosSelect = new SelectList(new List<AquaSOS.Models.PontoDistribuicao>(), "Id", "Nome");
            }
        }
    }
}