﻿using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;
using Finance.Models;

namespace Finance.Controllers
{
    public class TransferenciasController : Controller
    {
        private FinanceContext context = new FinanceContext();

        //
        // GET: /Transferencia/

        public async Task<ActionResult> Indice()
        {
            return View(await context.Transferencias.Include(transferencia => transferencia.ContaOrigem).Include(transferencia => transferencia.ContaDestino).ToListAsync());
        }

        //
        // GET: /Transferencia/Detalhes/5

        public async Task<ActionResult> Detalhes(int id)
        {
            Transferencia transferencia = await context.Transferencias.SingleAsync(x => x.TransferenciaId == id);
            return View(transferencia);
        }

        //
        // GET: /Transferencia/Criar

        public async Task<ActionResult> Criar()
        {
            ViewBag.PossibleContaOrigem = await context.Contas.ToListAsync();
            ViewBag.PossibleContaDestino = await context.Contas.ToListAsync();
            return View();
        } 

        //
        // POST: /Transferencia/Criar

        [HttpPost]
        public async Task<ActionResult> Criar(Transferencia transferencia)
        {
            if (ModelState.IsValid)
            {
                context.Transferencias.Add(transferencia);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Indice));  
            }

            ViewBag.PossibleContaOrigem = context.Contas;
            ViewBag.PossibleContaDestino = context.Contas;
            return View(transferencia);
        }
        
        //
        // GET: /Transferencia/Editar/5
 
        public async Task<ActionResult> Editar(int id)
        {
            Transferencia transferencia = await context.Transferencias.SingleAsync(x => x.TransferenciaId == id);
            ViewBag.PossibleContaOrigem = await context.Contas.ToListAsync();
            ViewBag.PossibleContaDestino = await context.Contas.ToListAsync();
            return View(transferencia);
        }

        //
        // POST: /Transferencia/Editar/5

        [HttpPost]
        public async Task<ActionResult> Editar(Transferencia transferencia)
        {
            if (ModelState.IsValid)
            {
                context.Entry(transferencia).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Indice));
            }
            ViewBag.PossibleContaOrigem = await context.Contas.ToListAsync();
            ViewBag.PossibleContaDestino = await context.Contas.ToListAsync();
            return View(transferencia);
        }

        //
        // GET: /Transferencia/Excluir/5
 
        public async Task<ActionResult> Excluir(int id)
        {
            Transferencia transferencia = await context.Transferencias.SingleAsync(x => x.TransferenciaId == id);
            return View(transferencia);
        }

        //
        // POST: /Transferencia/Excluir/5

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