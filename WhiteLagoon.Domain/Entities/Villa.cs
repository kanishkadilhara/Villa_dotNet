using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
namespace WhiteLagoon.Domain
{
	public class Villa
	{
		public int  VillaId { get; set; }
		[MaxLength(50,ErrorMessage ="Name cannot exceed 50 characters")]
		public required string Name { get; set; }
		public  string ? Description { get; set; }
		[Display(Name = "Price Per Night")]

		[Range(1,10000,ErrorMessage ="Price must be between 1 and 10000")]
		public double Price { get; set; }
		public int SquareMeters { get; set; }
		[Range(1,20,ErrorMessage ="Occupancy must be between 1 and 20")]
		public int occupancy { get; set; }
		[Display(Name = "Image URL")]
		public string ? ImageUrl { get; set; }
		[NotMapped]
		public IFormFile ? Image { get; set; }
		public DateTime ? CreatedDate { get; set; }
		public DateTime ? UpdatedDate { get; set; }
		
		
	}

    
}

