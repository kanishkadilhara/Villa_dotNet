using Microsoft.AspNetCore.Mvc;
using WhiteLagoon.Infrastructure;
namespace WhiteLagoon.Web
{
    public class VillaController : Controller
    {
        private readonly ApplicationDbContext _context;
        public  VillaController(ApplicationDbContext context)
        {
            _context = context;
        }
        public ActionResult Index()
        {
            return View();
        }

    }
}
