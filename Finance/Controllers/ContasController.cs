using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Finance.Models;

namespace Finance.Controllers
{
    public class ContasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Contas
        public async Task<ActionResult> Index()
        {
            var contas = db.Contas.Include(c => c.Banco).Include(c => c.Usuario);
            return View(await contas.ToListAsync());
        }

        // GET: Contas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Conta conta = await db.Contas.FindAsync(id);
            if (conta == null)
            {
                return HttpNotFound();
            }
            return View(conta);
        }

        // GET: Contas/Create
        public ActionResult Create()
        {
            ViewBag.BancoId = new SelectList(db.Bancos, "BancoId", "Nome");
            ViewBag.UsuarioId = new SelectList(db.Set<Usuario>(), "Id", "Email");
            return View();
        }

        // POST: Contas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ContaId,BancoId,UsuarioId,Nome,Descricao,SaldoAtual")] Conta conta)
        {
            if (ModelState.IsValid)
            {
                db.Contas.Add(conta);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.BancoId = new SelectList(db.Bancos, "BancoId", "Nome", conta.BancoId);
            ViewBag.UsuarioId = new SelectList(db.Set<Usuario>(), "Id", "Email", conta.UsuarioId);
            return View(conta);
        }

        // GET: Contas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Conta conta = await db.Contas.FindAsync(id);
            if (conta == null)
            {
                return HttpNotFound();
            }
            ViewBag.BancoId = new SelectList(db.Bancos, "BancoId", "Nome", conta.BancoId);
            ViewBag.UsuarioId = new SelectList(db.Set<Usuario>(), "Id", "Email", conta.UsuarioId);
            return View(conta);
        }

        // POST: Contas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ContaId,BancoId,UsuarioId,Nome,Descricao,SaldoAtual")] Conta conta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(conta).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.BancoId = new SelectList(db.Bancos, "BancoId", "Nome", conta.BancoId);
            ViewBag.UsuarioId = new SelectList(db.Set<Usuario>(), "Id", "Email", conta.UsuarioId);
            return View(conta);
        }

        // GET: Contas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Conta conta = await db.Contas.FindAsync(id);
            if (conta == null)
            {
                return HttpNotFound();
            }
            return View(conta);
        }

        // POST: Contas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Conta conta = await db.Contas.FindAsync(id);
            db.Contas.Remove(conta);
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
