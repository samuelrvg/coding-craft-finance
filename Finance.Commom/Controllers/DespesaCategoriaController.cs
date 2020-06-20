using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;
using Finance.Models;

namespace Finance.Controllers
{
    [Authorize]
    public class DespesaCategoriaController : Controller
    {
        private FinanceContext context = new FinanceContext();

        //
        // GET: /DespesaCategorias/

        public async Task<ActionResult> Indice()
        {
            return View(await context.DespesaCategorias.Include(despesacategoria => despesacategoria.Despesas).ToListAsync());
        }

        //
        // GET: /DespesaCategorias/Detalhes/5

        public async Task<ActionResult> Detalhes(int id)
        {
            DespesaCategoria despesacategoria = await context.DespesaCategorias.SingleAsync(x => x.DespesaCategoriaId == id);
            return View(despesacategoria);
        }

        //
        // GET: /DespesaCategorias/Criar

        public async Task<ActionResult> Criar()
        {
            return View();
        } 

        //
        // POST: /DespesaCategorias/Criar

        [HttpPost]
        public async Task<ActionResult> Criar(DespesaCategoria despesacategoria)
        {
            if (ModelState.IsValid)
            {
                context.DespesaCategorias.Add(despesacategoria);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Indice));  
            }

            return View(despesacategoria);
        }
        
        //
        // GET: /DespesaCategorias/Editar/5
 
        public async Task<ActionResult> Editar(int id)
        {
            DespesaCategoria despesacategoria = await context.DespesaCategorias.SingleAsync(x => x.DespesaCategoriaId == id);
            return View(despesacategoria);
        }

        //
        // POST: /DespesaCategorias/Editar/5

        [HttpPost]
        public async Task<ActionResult> Editar(DespesaCategoria despesacategoria)
        {
            if (ModelState.IsValid)
            {
                context.Entry(despesacategoria).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Indice));
            }
            return View(despesacategoria);
        }

        //
        // GET: /DespesaCategorias/Excluir/5
 
        public async Task<ActionResult> Excluir(int id)
        {
            DespesaCategoria despesacategoria = await context.DespesaCategorias.SingleAsync(x => x.DespesaCategoriaId == id);
            return View(despesacategoria);
        }

        //
        // POST: /DespesaCategorias/Excluir/5

        [HttpPost, ActionName(nameof(Excluir))]
        public async Task<ActionResult> ConfirmarExclusao(int id)
        {
            DespesaCategoria despesacategoria = await context.DespesaCategorias.SingleAsync(x => x.DespesaCategoriaId == id);
            context.DespesaCategorias.Remove(despesacategoria);
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