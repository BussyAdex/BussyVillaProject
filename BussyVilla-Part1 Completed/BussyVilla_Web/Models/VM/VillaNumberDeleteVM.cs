using BussyVilla_Web.Models.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BussyVilla_Web.Models.VM
{
	public class VillaNumberDeleteVM
	{
		public VillaNumberDTO VillaNumber { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>  VillaList {  get; set; }
		public VillaNumberDeleteVM()
        {
            VillaNumber = new VillaNumberDTO();
        }
    }
}
