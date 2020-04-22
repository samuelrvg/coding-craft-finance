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
    public class ReceitaController : Controller
    {
        private FinanceContext context = new FinanceContext();

        //
        // GET: /Receitas/

        public async Task<ActionResult> Indice()
        {
            return View(await context.Receita.Include(receita => receita.ReceitaCategoria).Include(receita => receita.Banco).ToListAsync());
        }

        //
        // GET: /Receitas/Detalhes/5

        public async Task<ActionResult> Detalhes(int id)
        {
            Receita receita = await context.Receita.SingleAsync(x => x.ReceitaId == id);
            return View(receita);
        }

        //
        // GET: /Receitas/Criar

        public async Task<ActionResult> Criar()
        {
            ViewBag.ReceitaCategoria = await context.ReceitaCategoria.ToListAsync();
            ViewBag.Banco = await context.Banco.ToListAsync();
            return View();
        } 

        //
        // POST: /Receitas/Criar

        [HttpPost]
        public async Task<ActionResult> Criar(Receita receita)
        {
            if (ModelState.IsValid)
            {
                context.Receita.Add(receita);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Indice));  
            }

            ViewBag.ReceitaCategoria = context.ReceitaCategoria;
            ViewBag.Banco = context.Banco;
            return View(receita);
        }
        
        //
        // GET: /Receitas/Editar/5
 
        public async Task<ActionResult> Editar(int id)
        {
            Receita receita = await context.Receita.SingleAsync(x => x.ReceitaId == id);
            ViewBag.ReceitaCategoria = await context.ReceitaCategoria.ToListAsync();
            ViewBag.Banco = await context.Banco.ToListAsync();
            return View(receita);
        }

        //
        // POST: /Receitas/Editar/5

        [HttpPost]
        public async Task<ActionResult> Editar(Receita receita)
        {
            if (ModelState.IsValid)
            {
                context.Entry(receita).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Indice));
            }
            ViewBag.ReceitaCategoria = await context.ReceitaCategoria.ToListAsync();
            ViewBag.Banco = await context.Banco.ToListAsync();
            return View(receita);
        }

        //
        // GET: /Receitas/Excluir/5
 
        public async Task<ActionResult> Excluir(int id)
        {
            Receita receita = await context.Receita.SingleAsync(x => x.ReceitaId == id);
            return View(receita);
        }

        //
        // POST: /Receitas/Excluir/5

        [HttpPost, ActionName(nameof(Excluir))]
        public async Task<ActionResult> ConfirmarExclusao(int id)
        {
            Receita receita = await context.Receita.SingleAsync(x => x.ReceitaId == id);
            context.Receita.Remove(receita);
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