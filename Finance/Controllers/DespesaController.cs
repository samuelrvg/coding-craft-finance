using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;
using Finance.Models;

namespace Finance.Controllers
{
    public class DespesaController : Controller
    {
        private FinanceContext context = new FinanceContext();

        //
        // GET: /Despesas/

        public async Task<ActionResult> Indice()
        {
            return View(await context.Despesas.Include(despesa => despesa.DespesaCategoria).ToListAsync());
        }

        //
        // GET: /Despesas/Detalhes/5

        public async Task<ActionResult> Detalhes(int id)
        {
            Despesa despesa = await context.Despesas.SingleAsync(x => x.DespesaId == id);
            return View(despesa);
        }

        //
        // GET: /Despesas/Criar

        public async Task<ActionResult> Criar()
        {
            ViewBag.DespesaCategoria = await context.DespesaCategorias.ToListAsync();
            return View();
        } 

        //
        // POST: /Despesas/Criar

        [HttpPost]
        public async Task<ActionResult> Criar(Despesa despesa)
        {
            if (ModelState.IsValid)
            {
                context.Despesas.Add(despesa);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Indice));  
            }

            ViewBag.DespesaCategoria = context.DespesaCategorias;
            return View(despesa);
        }
        
        //
        // GET: /Despesas/Editar/5
 
        public async Task<ActionResult> Editar(int id)
        {
            Despesa despesa = await context.Despesas.SingleAsync(x => x.DespesaId == id);
            ViewBag.DespesaCategoria = await context.DespesaCategorias.ToListAsync();
            return View(despesa);
        }

        //
        // POST: /Despesas/Editar/5

        [HttpPost]
        public async Task<ActionResult> Editar(Despesa despesa)
        {
            if (ModelState.IsValid)
            {
                context.Entry(despesa).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Indice));
            }
            ViewBag.DespesaCategoria = await context.DespesaCategorias.ToListAsync();
            return View(despesa);
        }

        //
        // GET: /Despesas/Excluir/5
 
        public async Task<ActionResult> Excluir(int id)
        {
            Despesa despesa = await context.Despesas.SingleAsync(x => x.DespesaId == id);
            return View(despesa);
        }

        //
        // POST: /Despesas/Excluir/5

        [HttpPost, ActionName(nameof(Excluir))]
        public async Task<ActionResult> ConfirmarExclusao(int id)
        {
            Despesa despesa = await context.Despesas.SingleAsync(x => x.DespesaId == id);
            context.Despesas.Remove(despesa);
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