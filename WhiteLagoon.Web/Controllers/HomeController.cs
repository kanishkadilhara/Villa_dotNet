using Microsoft.AspNetCore.Mvc;
using WhiteLagoon.Domain;
using WhiteLagoon.Infrastructure;
using WhiteLagoon.Web.ViewModels;



namespace WhiteLagoon.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        // 1. Inject the database context
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // 2. Create a new instance of your View Model
            HomeVM homeVM = new HomeVM()
            {
                // Grab all villas from the database
                Villas = _context.Villas.ToList(),
                
                // Set some default values for the search bar
                CheckInDate = DateOnly.FromDateTime(DateTime.Now),
                Nights = 1
            };

            // 3. Pass the View Model to the view
            return View(homeVM);
        }
    }

   
}