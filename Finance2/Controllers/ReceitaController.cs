using Finance.Models;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Finance2.Controllers
{
    public class ReceitaController : Finance.Controllers.ReceitaController
    {
        //private FinanceContext context = new FinanceContext();

        public override async Task<ActionResult> Indice()
        {
            return View(await context.Receitas.ToListAsync());
        }

        public override Task<ActionResult> Criar()
        {
            return base.Criar();
        }

        [HttpPost]
        public override Task<ActionResult> Criar(Receita receita)
        {
            return base.Criar(receita);
        }
    }
}