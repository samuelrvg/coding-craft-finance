﻿using Finance.Models;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Finance2.Controllers
{
    public class BancoController : Finance.Controllers.BancoController
    {
        //private FinanceContext context = new FinanceContext();

        public override async Task<ActionResult> Indice()
        {
            return View(await context.Bancos.ToListAsync());
        }

        public override Task<ActionResult> Criar()
        {
            return base.Criar();
        }

        [HttpPost]
        public override Task<ActionResult> Criar(Banco banco)
        {
            return base.Criar(banco);
        }
    }
}