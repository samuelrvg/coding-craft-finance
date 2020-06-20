using Finance.Models;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Finance2.Controllers
{
    public class DespesaController : Finance.Controllers.DespesaController
    {
        //private FinanceContext context = new FinanceContext();

        public override async Task<ActionResult> Indice()
        {
            return View(await context.Despesas.ToListAsync());
        }

        public override Task<ActionResult> Criar()
        {
            return base.Criar();
        }

        [HttpPost]
        public override Task<ActionResult> Criar(Despesa despesa)
        {
            return base.Criar(despesa);
        }
    }
}