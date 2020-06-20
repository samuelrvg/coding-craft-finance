using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;
using Finance.Models;

namespace Finance.Controllers
{
    [Authorize]
    public class TransferenciaController : Controller
    {
        private FinanceContext context = new FinanceContext();

        //
        // GET: /Transferencias/

        public async Task<ActionResult> Indice()
        {
            return View(await context.Transferencias.Include(transferencia => transferencia.ContaOrigem).Include(transferencia => transferencia.ContaDestino).ToListAsync());
        }

        //
        // GET: /Transferencias/Detalhes/5

        public async Task<ActionResult> Detalhes(int id)
        {
            Transferencia transferencia = await context.Transferencias.SingleAsync(x => x.TransferenciaId == id);
            return View(transferencia);
        }

        //
        // GET: /Transferencias/Criar

        public async Task<ActionResult> Criar()
        {
            ViewBag.ContaOrigem = await context.Contas.ToListAsync();
            ViewBag.ContaDestino = await context.Contas.ToListAsync();
            return View();
        } 

        //
        // POST: /Transferencias/Criar

        [HttpPost]
        public async Task<ActionResult> Criar(Transferencia transferencia)
        {
            if (ModelState.IsValid)
            {
                context.Transferencias.Add(transferencia);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Indice));  
            }

            ViewBag.ContaOrigem = context.Contas;
            ViewBag.ContaDestino = context.Contas;
            return View(transferencia);
        }
        
        //
        // GET: /Transferencias/Editar/5
 
        public async Task<ActionResult> Editar(int id)
        {
            Transferencia transferencia = await context.Transferencias.SingleAsync(x => x.TransferenciaId == id);
            ViewBag.ContaOrigem = await context.Contas.ToListAsync();
            ViewBag.ContaDestino = await context.Contas.ToListAsync();
            return View(transferencia);
        }

        //
        // POST: /Transferencias/Editar/5

        [HttpPost]
        public async Task<ActionResult> Editar(Transferencia transferencia)
        {
            if (ModelState.IsValid)
            {
                context.Entry(transferencia).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Indice));
            }
            ViewBag.ContaOrigem = await context.Contas.ToListAsync();
            ViewBag.ContaDestino = await context.Contas.ToListAsync();
            return View(transferencia);
        }

        //
        // GET: /Transferencias/Excluir/5
 
        public async Task<ActionResult> Excluir(int id)
        {
            Transferencia transferencia = await context.Transferencias.SingleAsync(x => x.TransferenciaId == id);
            return View(transferencia);
        }

        //
        // POST: /Transferencias/Excluir/5

        [HttpPost, ActionName(nameof(Excluir))]
        public async Task<ActionResult> ConfirmarExclusao(int id)
        {
            Transferencia transferencia = await context.Transferencias.SingleAsync(x => x.TransferenciaId == id);
            context.Transferencias.Remove(transferencia);
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