using System.ComponentModel.DataAnnotations;

namespace Bulkify.WebApi.Models.RequestModels
{
    public class CategoryAddModel
    {

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Category name must be between 3 and 100 characters.")]
        public string Name { get; set; }

    }
}
