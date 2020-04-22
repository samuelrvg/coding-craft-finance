using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Finance.Models;

namespace Finance.Controllers
{   
    public class BancoController : Controller
    {
        private FinanceContext context = new FinanceContext();

        //
        // GET: /Bancos/

        public async Task<ActionResult> Indice()
        {
            return View(await context.Banco.Include(banco => banco.Contas).ToListAsync());
        }

        //
        // GET: /Bancos/Detalhes/5

        public async Task<ActionResult> Detalhes(int id)
        {
            Banco banco = await context.Banco.SingleAsync(x => x.BancoId == id);
            return View(banco);
        }

        //
        // GET: /Bancos/Criar

        public async Task<ActionResult> Criar()
        {
            return View();
        } 

        //
        // POST: /Bancos/Criar

        [HttpPost]
        public async Task<ActionResult> Criar(Banco banco)
        {
            if (ModelState.IsValid)
            {
                context.Banco.Add(banco);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Indice));  
            }

            return View(banco);
        }
        
        //
        // GET: /Bancos/Editar/5
 
        public async Task<ActionResult> Editar(int id)
        {
            Banco banco = await context.Banco.SingleAsync(x => x.BancoId == id);
            return View(banco);
        }

        //
        // POST: /Bancos/Editar/5

        [HttpPost]
        public async Task<ActionResult> Editar(Banco banco)
        {
            if (ModelState.IsValid)
            {
                context.Entry(banco).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Indice));
            }
            return View(banco);
        }

        //
        // GET: /Bancos/Excluir/5
 
        public async Task<ActionResult> Excluir(int id)
        {
            Banco banco = await context.Banco.SingleAsync(x => x.BancoId == id);
            return View(banco);
        }

        //
        // POST: /Bancos/Excluir/5

        [HttpPost, ActionName(nameof(Excluir))]
        public async Task<ActionResult> ConfirmarExclusao(int id)
        {
            Banco banco = await context.Banco.SingleAsync(x => x.BancoId == id);
            context.Banco.Remove(banco);
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