using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;
using Finance.Models;

namespace Finance.Controllers
{
    [Authorize]
    public class ContaController : Controller
    {
        private FinanceContext context = new FinanceContext();

        //
        // GET: /Contas/

        public async Task<ActionResult> Indice()
        {
            return View(await context.Contas.Include(conta => conta.Banco).Include(conta => conta.Usuario).Include(conta => conta.TransferenciasComoOrigem).Include(conta => conta.TransferenciasComoDestino).Include(conta => conta.Receitas).ToListAsync());
        }

        //
        // GET: /Contas/Detalhes/5

        public async Task<ActionResult> Detalhes(int id)
        {
            Conta conta = await context.Contas.SingleAsync(x => x.ContaId == id);
            return View(conta);
        }

        //
        // GET: /Contas/Criar

        public async Task<ActionResult> Criar()
        {
            ViewBag.Banco = await context.Bancos.ToListAsync();
            ViewBag.Usuario = await context.Set<Usuario>().ToListAsync();
            return View();
        } 

        //
        // POST: /Contas/Criar

        [HttpPost]
        public async Task<ActionResult> Criar(Conta conta)
        {
            if (ModelState.IsValid)
            {
                context.Contas.Add(conta);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Indice));  
            }

            ViewBag.Banco = await context.Bancos.ToListAsync();
            ViewBag.Usuario = await context.Set<Usuario>().ToListAsync();
            return View(conta);
        }
        
        //
        // GET: /Contas/Editar/5
 
        public async Task<ActionResult> Editar(int id)
        {
            Conta conta = await context.Contas.SingleAsync(x => x.ContaId == id);
            ViewBag.Banco = await context.Bancos.ToListAsync();
            ViewBag.Usuario = await context.Set<Usuario>().ToListAsync();
            return View(conta);
        }

        //
        // POST: /Contas/Editar/5

        [HttpPost]
        public async Task<ActionResult> Editar(Conta conta)
        {
            if (ModelState.IsValid)
            {
                context.Entry(conta).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Indice));
            }
            ViewBag.Banco = await context.Bancos.ToListAsync();
            ViewBag.Usuario = await context.Set<Usuario>().ToListAsync();
            return View(conta);
        }

        //
        // GET: /Contas/Excluir/5
 
        public async Task<ActionResult> Excluir(int id)
        {
            Conta conta = await context.Contas.SingleAsync(x => x.ContaId == id);
            return View(conta);
        }

        //
        // POST: /Contas/Excluir/5

        [HttpPost, ActionName(nameof(Excluir))]
        public async Task<ActionResult> ConfirmarExclusao(int id)
        {
            Conta conta = await context.Contas.SingleAsync(x => x.ContaId == id);
            context.Contas.Remove(conta);
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