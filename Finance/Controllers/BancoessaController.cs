using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Finance.Models;

namespace Finance.Controllers
{
    public class BancoessaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Bancoessa
        public ActionResult Index()
        {
            return View(db.Bancos.ToList());
        }

        // GET: Bancoessa/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banco banco = db.Bancos.Find(id);
            if (banco == null)
            {
                return HttpNotFound();
            }
            return View(banco);
        }

        // GET: Bancoessa/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bancoessa/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BancoId,Nome,UltimaModificacao,UsuarioModificacao,DataCriacao,UsuarioCriacao")] Banco banco)
        {
            if (ModelState.IsValid)
            {
                db.Bancos.Add(banco);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(banco);
        }

        // GET: Bancoessa/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banco banco = db.Bancos.Find(id);
            if (banco == null)
            {
                return HttpNotFound();
            }
            return View(banco);
        }

        // POST: Bancoessa/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BancoId,Nome,UltimaModificacao,UsuarioModificacao,DataCriacao,UsuarioCriacao")] Banco banco)
        {
            if (ModelState.IsValid)
            {
                db.Entry(banco).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(banco);
        }

        // GET: Bancoessa/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banco banco = db.Bancos.Find(id);
            if (banco == null)
            {
                return HttpNotFound();
            }
            return View(banco);
        }

        // POST: Bancoessa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Banco banco = db.Bancos.Find(id);
            db.Bancos.Remove(banco);
            db.SaveChanges();
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
