using Bulkify.Core.Entities;
using Bulkify.Core.Interfaces.Repositories;
using Bulkify.Core.Interfaces.Services;
using Bulkify.Repository.Repositories;
using Bulkify.WebApi.Models.RequestModels;
using Bulkify.WebAPI.Controllers;
using Bulkify.WebAPI.Models.RequestModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bulkify.WebApi.Controllers
{
    public class AdminController : BaseApiController
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly ILogger<AdminController> _logger;
        public AdminController(
            ICategoryRepository categoryRepository,
            ILogger<AdminController> logger,
            IProductRepository productRepository)

        {
            _categoryRepository = categoryRepository;
            _logger = logger;
            _productRepository = productRepository;
        }

        [HttpPost("AddCategory")]
        public async Task<IActionResult> AddCategory([FromBody] CategoryAddModel model)
        {
            // Validate the input model
            if (model == null)
            {
                return BadRequest(new { message = "The category data is required." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid data provided.", errors = ModelState });
            }

            try
            {

                var category = new Category
                {
                    Name = model.Name
                };
                // Call the service layer to add the category
                _categoryRepository.AddCategoryAsync(category);

                await _categoryRepository.SaveChangesAsync();

                return Ok(new { message = "Category added successfully." });

            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "An error occurred while adding the category.");

                return StatusCode(500, new { message = "An error occurred while processing your request. Please try again later." });
            }
        }

        [HttpDelete("DeleteCategory/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (id < 1)
            {
                return BadRequest(new { message = "Invalid category ID." });
            }

            try
            {
                // Check if the category exists
                var category = await _categoryRepository.GetCategoryByIdAsync(id);
                if (category == null)
                {
                    return NotFound(new { message = "Category not found." });
                }

                // Delete the category
                _categoryRepository.DeleteCategory(category);

                await _categoryRepository.SaveChangesAsync();

                return Ok(new { message = "Category deleted successfully." });
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "An error occurred while deleting the category.");

                return StatusCode(500, new { message = "An error occurred while processing your request. Please try again later." });
            }
        }

        [HttpPut("UpdateCategory/{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryAddModel model)
        {
            if (id < 1)
            {
                return BadRequest(new { message = "Invalid category ID." });
            }

            if (model == null)
            {
                return BadRequest(new { message = "The category data is required." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid data provided.", errors = ModelState });
            }

            try
            {
                // Check if the category exists
                var existingCategory = await _categoryRepository.GetCategoryByIdAsync(id);
                if (existingCategory == null)
                {
                    return NotFound(new { message = "Category not found." });
                }

                // Update the category fields
                existingCategory.Name = model.Name;
                // Save changes to the database
                await _categoryRepository.SaveChangesAsync();

                return Ok(new { message = "Category updated successfully.", data = existingCategory });
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "An error occurred while updating the category.");

                return StatusCode(500, new { message = "An error occurred while processing your request. Please try again later." });
            }
        }
        [HttpPut("ApproveProduct/{id}")]
        public async Task<IActionResult> ApproveProduct(int id)
        {
            if (id < 1)
            {
                return BadRequest(new { message = "Invalid product ID." });
            }

            try
            {
                // Retrieve the product from the repository
                var product = await _productRepository.GetProductByIdAsync(id);
                if (product == null)
                {
                    return NotFound(new { message = "Product not found." });
                }

                // Check if the product is already approved
                if (product.ApprovalStatus)
                {
                    return BadRequest(new { message = "Product is already approved." });
                }

                // Approve the product
                product.ApprovalStatus = true;

                // Save changes to the database
                await _productRepository.SaveChangesAsync();

                return Ok(new { message = "Product approved successfully.", data = product });
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "An error occurred while approving the product.");

                return StatusCode(500, new { message = "An error occurred while processing your request. Please try again later." });
            }
        }

    }
}
