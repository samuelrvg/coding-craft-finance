using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Finance.Models;

namespace Finance.Controllers
{
    public class ReceitaCategoriasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ReceitaCategorias
        public async Task<ActionResult> Index()
        {
            return View(await db.ReceitaCategorias.ToListAsync());
        }

        // GET: ReceitaCategorias/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReceitaCategoria receitaCategoria = await db.ReceitaCategorias.FindAsync(id);
            if (receitaCategoria == null)
            {
                return HttpNotFound();
            }
            return View(receitaCategoria);
        }

        // GET: ReceitaCategorias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReceitaCategorias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ReceitaCategoriaId,Nome")] ReceitaCategoria receitaCategoria)
        {
            if (ModelState.IsValid)
            {
                db.ReceitaCategorias.Add(receitaCategoria);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(receitaCategoria);
        }

        // GET: ReceitaCategorias/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReceitaCategoria receitaCategoria = await db.ReceitaCategorias.FindAsync(id);
            if (receitaCategoria == null)
            {
                return HttpNotFound();
            }
            return View(receitaCategoria);
        }

        // POST: ReceitaCategorias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ReceitaCategoriaId,Nome")] ReceitaCategoria receitaCategoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(receitaCategoria).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(receitaCategoria);
        }

        // GET: ReceitaCategorias/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReceitaCategoria receitaCategoria = await db.ReceitaCategorias.FindAsync(id);
            if (receitaCategoria == null)
            {
                return HttpNotFound();
            }
            return View(receitaCategoria);
        }

        // POST: ReceitaCategorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ReceitaCategoria receitaCategoria = await db.ReceitaCategorias.FindAsync(id);
            db.ReceitaCategorias.Remove(receitaCategoria);
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
