using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;
using Finance.Models;

namespace Finance.Controllers
{
    [Authorize]
    public class ReceitaCategoriaController : Controller
    {
        private FinanceContext context = new FinanceContext();

        //
        // GET: /ReceitaCategorias/

        public async Task<ActionResult> Indice()
        {
            return View(await context.ReceitaCategorias.Include(receitacategoria => receitacategoria.Receitas).ToListAsync());
        }

        //
        // GET: /ReceitaCategorias/Detalhes/5

        public async Task<ActionResult> Detalhes(int id)
        {
            ReceitaCategoria receitacategoria = await context.ReceitaCategorias.SingleAsync(x => x.ReceitaCategoriaId == id);
            return View(receitacategoria);
        }

        //
        // GET: /ReceitaCategorias/Criar

        public async Task<ActionResult> Criar()
        {
            return View();
        } 

        //
        // POST: /ReceitaCategorias/Criar

        [HttpPost]
        public async Task<ActionResult> Criar(ReceitaCategoria receitacategoria)
        {
            if (ModelState.IsValid)
            {
                context.ReceitaCategorias.Add(receitacategoria);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Indice));  
            }

            return View(receitacategoria);
        }
        
        //
        // GET: /ReceitaCategorias/Editar/5
 
        public async Task<ActionResult> Editar(int id)
        {
            ReceitaCategoria receitacategoria = await context.ReceitaCategorias.SingleAsync(x => x.ReceitaCategoriaId == id);
            return View(receitacategoria);
        }

        //
        // POST: /ReceitaCategorias/Editar/5

        [HttpPost]
        public async Task<ActionResult> Editar(ReceitaCategoria receitacategoria)
        {
            if (ModelState.IsValid)
            {
                context.Entry(receitacategoria).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Indice));
            }
            return View(receitacategoria);
        }

        //
        // GET: /ReceitaCategorias/Excluir/5
 
        public async Task<ActionResult> Excluir(int id)
        {
            ReceitaCategoria receitacategoria = await context.ReceitaCategorias.SingleAsync(x => x.ReceitaCategoriaId == id);
            return View(receitacategoria);
        }

        //
        // POST: /ReceitaCategorias/Excluir/5

        [HttpPost, ActionName(nameof(Excluir))]
        public async Task<ActionResult> ConfirmarExclusao(int id)
        {
            ReceitaCategoria receitacategoria = await context.ReceitaCategorias.SingleAsync(x => x.ReceitaCategoriaId == id);
            context.ReceitaCategorias.Remove(receitacategoria);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Indice));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}