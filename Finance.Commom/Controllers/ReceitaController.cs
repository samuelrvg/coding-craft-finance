using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;
using Finance.Models;

namespace Finance.Controllers
{
    [Authorize]
    public class ReceitaController : Controller
    {
        protected readonly FinanceContext context = new FinanceContext();

        //
        // GET: /Receitas/

        public virtual async Task<ActionResult> Indice()
        {
            return View(await context.Receitas.Include(receita => receita.ReceitaCategoria).Include(receita => receita.Banco).ToListAsync());
        }

        //
        // GET: /Receitas/Detalhes/5

        public async Task<ActionResult> Detalhes(int id)
        {
            Receita receita = await context.Receitas.SingleAsync(x => x.ReceitaId == id);
            return View(receita);
        }

        //
        // GET: /Receitas/Criar

        public virtual async Task<ActionResult> Criar()
        {
            ViewBag.ReceitaCategoria = await context.ReceitaCategorias.ToListAsync();
            ViewBag.Banco = await context.Bancos.ToListAsync();
            return View();
        } 

        //
        // POST: /Receitas/Criar

        [HttpPost]
        public virtual async Task<ActionResult> Criar(Receita receita)
        {
            if (ModelState.IsValid)
            {
                context.Receitas.Add(receita);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Indice));  
            }

            ViewBag.ReceitaCategoria = context.ReceitaCategorias;
            ViewBag.Banco = context.Bancos;
            return View(receita);
        }
        
        //
        // GET: /Receitas/Editar/5
 
        public async Task<ActionResult> Editar(int id)
        {
            Receita receita = await context.Receitas.SingleAsync(x => x.ReceitaId == id);
            ViewBag.ReceitaCategoria = await context.ReceitaCategorias.ToListAsync();
            ViewBag.Banco = await context.Bancos.ToListAsync();
            return View(receita);
        }

        //
        // POST: /Receitas/Editar/5

        [HttpPost]
        public async Task<ActionResult> Editar(Receita receita)
        {
            if (ModelState.IsValid)
            {
                context.Entry(receita).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Indice));
            }
            ViewBag.ReceitaCategoria = await context.ReceitaCategorias.ToListAsync();
            ViewBag.Banco = await context.Bancos.ToListAsync();
            return View(receita);
        }

        //
        // GET: /Receitas/Excluir/5
 
        public async Task<ActionResult> Excluir(int id)
        {
            Receita receita = await context.Receitas.SingleAsync(x => x.ReceitaId == id);
            return View(receita);
        }

        //
        // POST: /Receitas/Excluir/5

        [HttpPost, ActionName(nameof(Excluir))]
        public async Task<ActionResult> ConfirmarExclusao(int id)
        {
            Receita receita = await context.Receitas.SingleAsync(x => x.ReceitaId == id);
            context.Receitas.Remove(receita);
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