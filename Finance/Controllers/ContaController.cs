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
    public class ContaController : Controller
    {
        private FinanceContext context = new FinanceContext();

        //
        // GET: /Contas/

        public async Task<ActionResult> Indice()
        {
            return View(await context.Conta.Include(conta => conta.Banco).Include(conta => conta.Usuario).Include(conta => conta.TransferenciasComoOrigem).Include(conta => conta.TransferenciasComoDestino).Include(conta => conta.Receitas).ToListAsync());
        }

        //
        // GET: /Contas/Detalhes/5

        public async Task<ActionResult> Detalhes(int id)
        {
            Conta conta = await context.Conta.SingleAsync(x => x.ContaId == id);
            return View(conta);
        }

        //
        // GET: /Contas/Criar

        public async Task<ActionResult> Criar()
        {
            ViewBag.Banco = new SelectList(context.Banco, "BancoId", "Nome");
            ViewBag.Usuario = new SelectList(context.Set<Usuario>(), "Id", "Email");
            return View();
        } 

        //
        // POST: /Contas/Criar

        [HttpPost]
        public async Task<ActionResult> Criar(Conta conta)
        {
            if (ModelState.IsValid)
            {
                context.Conta.Add(conta);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Indice));  
            }

            ViewBag.Banco = new SelectList(context.Banco, "BancoId", "Nome");
            ViewBag.Usuario = new SelectList(context.Set<Usuario>(), "Id", "Email");
            return View(conta);
        }
        
        //
        // GET: /Contas/Editar/5
 
        public async Task<ActionResult> Editar(int id)
        {
            Conta conta = await context.Conta.SingleAsync(x => x.ContaId == id);
            ViewBag.Banco = new SelectList(context.Banco, "BancoId", "Nome");
            ViewBag.Usuario = new SelectList(context.Set<Usuario>(), "Id", "Email");
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
            ViewBag.Banco = new SelectList(context.Banco, "BancoId", "Nome");
            ViewBag.Usuario = new SelectList(context.Set<Usuario>(), "Id", "Email");
            return View(conta);
        }

        //
        // GET: /Contas/Excluir/5
 
        public async Task<ActionResult> Excluir(int id)
        {
            Conta conta = await context.Conta.SingleAsync(x => x.ContaId == id);
            return View(conta);
        }

        //
        // POST: /Contas/Excluir/5

        [HttpPost, ActionName(nameof(Excluir))]
        public async Task<ActionResult> ConfirmarExclusao(int id)
        {
            Conta conta = await context.Conta.SingleAsync(x => x.ContaId == id);
            context.Conta.Remove(conta);
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