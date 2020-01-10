using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Finance.Models;

namespace Finance.Controllers
{
    public class DespesasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Despesas
        public async Task<ActionResult> Index()
        {
            var despesas = db.Despesas.Include(d => d.DespesaCategoria);
            return View(await despesas.ToListAsync());
        }

        // GET: Despesas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Despesa despesa = await db.Despesas.FindAsync(id);
            if (despesa == null)
            {
                return HttpNotFound();
            }
            return View(despesa);
        }

        // GET: Despesas/Create
        public ActionResult Create()
        {
            ViewBag.DespesaCategoriaId = new SelectList(db.DespesaCategorias, "DespesaCategoriaId", "Nome");
            return View();
        }

        // POST: Despesas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "DespesaId,DespesaCategoriaId,Descricao,Valor")] Despesa despesa)
        {
            if (ModelState.IsValid)
            {
                db.Despesas.Add(despesa);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.DespesaCategoriaId = new SelectList(db.DespesaCategorias, "DespesaCategoriaId", "Nome", despesa.DespesaCategoriaId);
            return View(despesa);
        }

        // GET: Despesas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Despesa despesa = await db.Despesas.FindAsync(id);
            if (despesa == null)
            {
                return HttpNotFound();
            }
            ViewBag.DespesaCategoriaId = new SelectList(db.DespesaCategorias, "DespesaCategoriaId", "Nome", despesa.DespesaCategoriaId);
            return View(despesa);
        }

        // POST: Despesas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "DespesaId,DespesaCategoriaId,Descricao,Valor")] Despesa despesa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(despesa).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.DespesaCategoriaId = new SelectList(db.DespesaCategorias, "DespesaCategoriaId", "Nome", despesa.DespesaCategoriaId);
            return View(despesa);
        }

        // GET: Despesas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Despesa despesa = await db.Despesas.FindAsync(id);
            if (despesa == null)
            {
                return HttpNotFound();
            }
            return View(despesa);
        }

        // POST: Despesas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Despesa despesa = await db.Despesas.FindAsync(id);
            db.Despesas.Remove(despesa);
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
