using System.Threading.Tasks;
using System.Web.Mvc;

namespace Finance2.Controllers
{
    public class BancoController : Finance.Controllers.BancoController
    {

        public override Task<ActionResult> Indice()
        {
            return base.Indice();
        }
    }
}