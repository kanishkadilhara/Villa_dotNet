using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WhiteLagoon.Domain;
using WhiteLagoon.Infrastructure;
using WhiteLagoon.Web.ViewModels;

namespace WhiteLagoon.Web
{
    public class VillaNumberController : Controller
    {
        private readonly ApplicationDbContext _context;
        public VillaNumberController(ApplicationDbContext context )
        {
            _context = context;
    
        }

        public IActionResult Index()
        {
            var villaNumberList = _context.VillaNumbers.Include(v => v.Villa).ToList();
            return View(villaNumberList);
        }
        public IActionResult Create()
        {
            VillaNumberVM villaNumberVM = new VillaNumberVM()
            {
                VillaList = _context.Villas.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.VillaId.ToString()
                }),
                VillaNumber = new VillaNumber()
            };
            return View(villaNumberVM);
           
        }
        [HttpPost]
        public IActionResult Create(VillaNumberVM obj)
        {
            if (obj.VillaNumber == null)
            {
                return View(obj);
            }

            bool isVillaNumberUnique = _context.VillaNumbers
                .Any(v => v.Villa_Number == obj.VillaNumber.Villa_Number);

            ModelState.Remove("VillaNumber.Villa");

            if (ModelState.IsValid && !isVillaNumberUnique)
            {
                _context.VillaNumbers.Add(obj.VillaNumber);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            if (isVillaNumberUnique)
            {
                ModelState.AddModelError(
                    "VillaNumber.Villa_Number",
                    "Villa Number must be unique.");
            }

            obj.VillaList = _context.Villas.Select(v => new SelectListItem
            {
                Text = v.Name,
                Value = v.VillaId.ToString()
            });

            return View(obj);

        }

        public IActionResult Edit(int id)
        {
            var villaNumberFromDb = _context.VillaNumbers.Find(id);
            if(villaNumberFromDb==null)
            {
                return NotFound();
            }
            VillaNumberVM villaNumberVM = new VillaNumberVM()
            {
                VillaNumber = villaNumberFromDb,
                VillaList = _context.Villas.Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.VillaId.ToString()
                    })
            };
            return View(villaNumberVM);
        }
        [HttpPost]
        public IActionResult Edit(VillaNumberVM obj)
        {
            if (obj.VillaNumber == null)
            {
                return View(obj);
            }
            
            ModelState.Remove("VillaNumber.Villa");
            if (ModelState.IsValid)
            {
                _context.VillaNumbers.Update(obj.VillaNumber);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Delete(int id)
        {
            var villaNumberFromDb = _context.VillaNumbers.Find(id);
            if(villaNumberFromDb==null)
            {
                return NotFound();
            }
            return View(villaNumberFromDb);
        }
        [HttpPost]
        public IActionResult Delete(VillaNumber obj)
        {
            _context.VillaNumbers.Remove(obj);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
