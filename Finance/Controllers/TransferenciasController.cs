using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Finance.Models;

namespace Finance.Controllers
{
    public class TransferenciasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Transferencias
        public async Task<ActionResult> Index(string nome)
        {
            var transferencias = db.Transferencias
                .Include(t => t.ContaDestino)
                .Include(t => t.ContaOrigem);

            if (!string.IsNullOrEmpty(nome))
                transferencias = transferencias.Where(e => e.ContaOrigem.Nome.Contains(nome));

            return View(await transferencias.ToListAsync());
        }

        // GET: Transferencias/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transferencia transferencia = await db.Transferencias.FindAsync(id);
            if (transferencia == null)
            {
                return HttpNotFound();
            }
            return View(transferencia);
        }

        // GET: Transferencias/Create
        public ActionResult Create()
        {
            ViewBag.ContaDestinoId = new SelectList(db.Contas, "ContaId", "Nome");
            ViewBag.ContaOrigemId = new SelectList(db.Contas, "ContaId", "Nome");
            return View();
        }

        // POST: Transferencias/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TransferenciaId,ContaOrigemId,ContaDestinoId,Valor")] Transferencia transferencia)
        {
            if (ModelState.IsValid)
            {
                db.Transferencias.Add(transferencia);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ContaDestinoId = new SelectList(db.Contas, "ContaId", "UsuarioId", transferencia.ContaDestinoId);
            ViewBag.ContaOrigemId = new SelectList(db.Contas, "ContaId", "UsuarioId", transferencia.ContaOrigemId);
            return View(transferencia);
        }

        // GET: Transferencias/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transferencia transferencia = await db.Transferencias.FindAsync(id);
            if (transferencia == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContaDestinoId = new SelectList(db.Contas, "ContaId", "UsuarioId", transferencia.ContaDestinoId);
            ViewBag.ContaOrigemId = new SelectList(db.Contas, "ContaId", "UsuarioId", transferencia.ContaOrigemId);
            return View(transferencia);
        }

        // POST: Transferencias/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TransferenciaId,ContaOrigemId,ContaDestinoId,Valor")] Transferencia transferencia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transferencia).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ContaDestinoId = new SelectList(db.Contas, "ContaId", "UsuarioId", transferencia.ContaDestinoId);
            ViewBag.ContaOrigemId = new SelectList(db.Contas, "ContaId", "UsuarioId", transferencia.ContaOrigemId);
            return View(transferencia);
        }

        // GET: Transferencias/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transferencia transferencia = await db.Transferencias.FindAsync(id);
            if (transferencia == null)
            {
                return HttpNotFound();
            }
            return View(transferencia);
        }

        // POST: Transferencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Transferencia transferencia = await db.Transferencias.FindAsync(id);
            db.Transferencias.Remove(transferencia);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
