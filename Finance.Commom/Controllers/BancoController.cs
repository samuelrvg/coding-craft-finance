using System.Data.Entity;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Finance.Models;
using Microsoft.AspNet.Identity.Owin;
using static Finance.IdentityConfig;

namespace Finance.Controllers
{   
    [Authorize]
    public class BancoController : Controller
    {
        protected readonly FinanceContext context = new FinanceContext();

        //private ApplicationSignInManager _signInManager;
        //private ApplicationUserManager _userManager;

        //public BancoController()
        //{
        //}

        //public BancoController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        //{
        //    UserManager = userManager;
        //    SignInManager = signInManager;
        //}

        //public ApplicationSignInManager SignInManager
        //{
        //    get
        //    {
        //        return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>("TESTKEY");
        //    }
        //    private set
        //    {
        //        _signInManager = value;
        //    }
        //}

        //public ApplicationUserManager UserManager
        //{
        //    get
        //    {
        //        return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //    }
        //    private set
        //    {
        //        _userManager = value;
        //    }
        //}

        // GET: /Bancos/
        public virtual async Task<ActionResult> Indice()
        {
            return View(await context.Bancos.Include(banco => banco.Contas).ToListAsync());
        }

        // GET: /Bancos/Detalhes/5

        public virtual async Task<ActionResult> Detalhes(int id)
        {
            Banco banco = await context.Bancos.SingleAsync(x => x.BancoId == id);
            return View(banco);
        }

        // GET: /Bancos/Criar
        public virtual async Task<ActionResult> Criar()
        {
            return View();
        } 

        // POST: /Bancos/Criar
        [HttpPost]
        public virtual async Task<ActionResult> Criar(Banco banco)
        {
            if (ModelState.IsValid)
            {
                context.Bancos.Add(banco);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Indice));  
            }

            return View(banco);
        }
        
        // GET: /Bancos/Editar/5
        public virtual async Task<ActionResult> Editar(int id)
        {
            Banco banco = await context.Bancos.SingleAsync(x => x.BancoId == id);
            return View(banco);
        }

        // POST: /Bancos/Editar/5
        [HttpPost]
        public virtual async Task<ActionResult> Editar(Banco banco)
        {
            if (ModelState.IsValid)
            {
                context.Entry(banco).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Indice));
            }
            return View(banco);
        }

        // GET: /Bancos/Excluir/5
        public virtual async Task<ActionResult> Excluir(int id)
        {
            Banco banco = await context.Bancos.SingleAsync(x => x.BancoId == id);
            return View(banco);
        }

        // POST: /Bancos/Excluir/5
        [HttpPost, ActionName(nameof(Excluir))]
        public virtual async Task<ActionResult> ConfirmarExclusao(int id)
        {
            Banco banco = await context.Bancos.SingleAsync(x => x.BancoId == id);
            context.Bancos.Remove(banco);
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