using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using WhiteLagoon.Domain;

namespace WhiteLagoon.Web.ViewModels
{
    public class VillaNumberVM
    {
        // Compartment 1: The actual room data
        public VillaNumber? VillaNumber { get; set; }
        // Compartment 2: The list of available villas
        [ValidateNever]
        public IEnumerable<SelectListItem> VillaList { get; set; } = [];
    }
}