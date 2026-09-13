using Microsoft.AspNetCore.Mvc;
using WhiteLagoon.Domain;
using WhiteLagoon.Infrastructure;
namespace WhiteLagoon.Web
{
    public class VillaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public  VillaController(ApplicationDbContext context , IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var villaList=_context.Villas.ToList();
            return View(villaList);

        }

        public IActionResult Create()
        {
            return View();
        } 
        [HttpPost]
public IActionResult Create(Villa villa)
{
    // Your custom validation remains at the top
    if (villa.Name == villa.Description)
    {
        ModelState.AddModelError("Description", "The Description cannot exactly match the Name.");
    }

    if(ModelState.IsValid)
    {
        // 1. Process the image upload before saving to the database
        if (villa.Image != null && villa.Image.Length > 0)
        {
            string imagePath = Path.Combine(
                _webHostEnvironment.WebRootPath,
                "Images",
                "VillaImage");

            Directory.CreateDirectory(imagePath);

            string fileName = Guid.NewGuid() + Path.GetExtension(villa.Image.FileName);
            string filePath = Path.Combine(imagePath, fileName);

            using var fileStream = new FileStream(filePath, FileMode.Create);
            villa.Image.CopyTo(fileStream);

            villa.ImageUrl = $"/Images/VillaImage/{fileName}";
        }

        // 2. Save the Villa (now containing the new ImageUrl) to the database
        _context.Villas.Add(villa);
        _context.SaveChanges();
        
        return RedirectToAction("Index");
    } 
    
    return View(villa);
} 

        public IActionResult Edit(int id)
        {
            var villa =_context.Villas.Find(id);
            if(villa==null)
            {
                return NotFound();
            }
            
            return View(villa);
        }
        [HttpPost]
        public IActionResult Edit(Villa villa)
        {
            if (villa.Name == villa.Description)
            {
                ModelState.AddModelError("Description", "The Description cannot exactly match the Name.");
            }
            if(ModelState.IsValid)
            {
                _context.Villas.Update(villa);
                _context.SaveChanges();
                return RedirectToAction("Index");
            } 
            return View(villa);
        }

        public IActionResult Delete(int id)
        {
            var villa =_context.Villas.Find(id);
            if(villa==null)
            {
                return NotFound();
            }
            
            return View(villa);
        }
        [HttpPost]
        public IActionResult Delete(Villa villa)
        {
            _context.Villas.Remove(villa);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
