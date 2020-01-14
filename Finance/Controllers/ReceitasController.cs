﻿using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Finance.Models;
using System.Transactions;
using System.Linq;
using Finance.ViewModels;

namespace Finance.Controllers
{
    public class ReceitasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Receitas
        public async Task<ActionResult> Index(PesquisaViewModel pesquisaViewModel)
        {
            var receitas = db.Receitas
                .Include(r => r.Banco)
                .Include(r => r.ReceitaCategoria);

            if (!string.IsNullOrEmpty(pesquisaViewModel.Nome))
                receitas = receitas.Where(e => e.ReceitaCategoria.Nome.Contains(pesquisaViewModel.Nome));
            if (!string.IsNullOrEmpty(pesquisaViewModel.Descricao))
                receitas = receitas.Where(e => e.Descricao.Contains(pesquisaViewModel.Descricao));

            return View(await receitas.ToListAsync());
        }

        // GET: Receitas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receita receita = await db.Receitas.FindAsync(id);
            if (receita == null)
            {
                return HttpNotFound();
            }
            return View(receita);
        }

        // GET: Receitas/Create
        public ActionResult Create()
        {
            ViewBag.BancoId = new SelectList(db.Bancos, "BancoId", "Nome");
            ViewBag.ReceitaCategoriaId = new SelectList(db.ReceitaCategorias, "ReceitaCategoriaId", "Nome");
            return View();
        }

        // POST: Receitas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ReceitaId,ReceitaCategoriaId,BancoId,Descricao,Valor")] Receita receita)
        {
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    db.Receitas.Add(receita);
                    await db.SaveChangesAsync();

                    scope.Complete();
                }

                return RedirectToAction("Index");
            }

            ViewBag.BancoId = new SelectList(db.Bancos, "BancoId", "Nome", receita.BancoId);
            ViewBag.ReceitaCategoriaId = new SelectList(db.ReceitaCategorias, "ReceitaCategoriaId", "Nome", receita.ReceitaCategoriaId);
            return View(receita);
        }

        // GET: Receitas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receita receita = await db.Receitas.FindAsync(id);
            if (receita == null)
            {
                return HttpNotFound();
            }
            ViewBag.BancoId = new SelectList(db.Bancos, "BancoId", "Nome", receita.BancoId);
            ViewBag.ReceitaCategoriaId = new SelectList(db.ReceitaCategorias, "ReceitaCategoriaId", "Nome", receita.ReceitaCategoriaId);
            return View(receita);
        }

        // POST: Receitas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ReceitaId,ReceitaCategoriaId,BancoId,Descricao,Valor")] Receita receita)
        {
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    db.Entry(receita).State = EntityState.Modified;
                    await db.SaveChangesAsync();

                    scope.Complete();
                }

                return RedirectToAction("Index");
            }
            ViewBag.BancoId = new SelectList(db.Bancos, "BancoId", "Nome", receita.BancoId);
            ViewBag.ReceitaCategoriaId = new SelectList(db.ReceitaCategorias, "ReceitaCategoriaId", "Nome", receita.ReceitaCategoriaId);
            return View(receita);
        }

        // GET: Receitas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receita receita = await db.Receitas.FindAsync(id);
            if (receita == null)
            {
                return HttpNotFound();
            }
            return View(receita);
        }

        // POST: Receitas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                Receita receita = await db.Receitas.FindAsync(id);
                db.Receitas.Remove(receita);
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
