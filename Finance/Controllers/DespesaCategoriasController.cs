using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Finance.Models;
using System.Transactions;

namespace Finance.Controllers
{
    public class DespesaCategoriasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DespesaCategorias
        public async Task<ActionResult> Index()
        {
            return View(await db.DespesaCategorias.ToListAsync());
        }

        // GET: DespesaCategorias/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DespesaCategoria despesaCategoria = await db.DespesaCategorias.FindAsync(id);
            if (despesaCategoria == null)
            {
                return HttpNotFound();
            }
            return View(despesaCategoria);
        }

        // GET: DespesaCategorias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DespesaCategorias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "DespesaCategoriaId,Nome")] DespesaCategoria despesaCategoria)
        {
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    db.DespesaCategorias.Add(despesaCategoria);
                    await db.SaveChangesAsync();

                    scope.Complete();
                }

                return RedirectToAction("Index");
            }

            return View(despesaCategoria);
        }

        // GET: DespesaCategorias/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DespesaCategoria despesaCategoria = await db.DespesaCategorias.FindAsync(id);
            if (despesaCategoria == null)
            {
                return HttpNotFound();
            }
            return View(despesaCategoria);
        }

        // POST: DespesaCategorias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "DespesaCategoriaId,Nome")] DespesaCategoria despesaCategoria)
        {
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    db.Entry(despesaCategoria).State = EntityState.Modified;
                    await db.SaveChangesAsync();

                    scope.Complete();
                }

                return RedirectToAction("Index");
            }
            return View(despesaCategoria);
        }

        // GET: DespesaCategorias/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DespesaCategoria despesaCategoria = await db.DespesaCategorias.FindAsync(id);
            if (despesaCategoria == null)
            {
                return HttpNotFound();
            }
            return View(despesaCategoria);
        }

        // POST: DespesaCategorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                DespesaCategoria despesaCategoria = await db.DespesaCategorias.FindAsync(id);
                db.DespesaCategorias.Remove(despesaCategoria);
                await db.SaveChangesAsync();

                scope.Complete();
            }

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
