using System.ComponentModel.DataAnnotations;

namespace Bulkify.WebAPI.Models.RequestModels
{
    public class ProductAddModel
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string ImageSource { get; set; } = string.Empty;
        public int SupplierId { get; set; }
        public int CategoryId { get; set; }
    }
}
