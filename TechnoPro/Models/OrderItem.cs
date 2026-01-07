using Microsoft.EntityFrameworkCore;

namespace TechnoPro.Models
{
	public class OrderItem
	{
		public int Id { get; set; }
		public int Quantity { get; set; }

		[Precision(16, 2)]
		public decimal UnitPrice { get; set; }

		// navigation property
		public Product Product { get; set; } = new Product();
	}
}
