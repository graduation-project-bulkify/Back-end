using Bulkify.Core.Entities;
using Bulkify.Core.Interfaces.Repositories;
using Bulkify.Core.Interfaces.Services;
using Bulkify.Repository.Repositories;
using Bulkify.WebAPI.Models.RequestModels;
using Bulkify.WebAPI.Models.ResponseModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bulkify.WebApi.Controllers
{
    public class SuppliersController : BaseApiController
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITokenService _tokenService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IPasswordHasher<Supplier> _passwordHasher;
        private readonly ILogger<SuppliersController> _logger;


        public SuppliersController(
            ISupplierRepository supplierRepository,
            ITokenService tokenService,
            IAuthenticationService authenticationService,
            IPasswordHasher<Supplier> passwordHasher,
            ILogger<SuppliersController> logger,
            ICategoryRepository categoryRepository)
        {
            _supplierRepository = supplierRepository;
            _tokenService = tokenService;
            _authenticationService = authenticationService;
            _passwordHasher = passwordHasher;
            _logger = logger;
            _categoryRepository = categoryRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] SupplierRegistrationModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existingSupplier = await _supplierRepository.GetSupplierByEmailAsync(model.Email);
                if (existingSupplier != null)
                {
                    return BadRequest("Email already exists.");
                }


                var supplier = new Supplier
                {
                    FullName = model.FullName,
                    Email = model.Email,
                    Gender = model.Gender,
                    PhoneNumber = model.PhoneNumber,
                    CommercialRegister = model.CommercialRegister,
                    SupplierRate = model.SupplierRate,
                    SupplierAddresses = model.SupplierAddressesmodel.Select(addressModel => new SupplierAddress
                    {
                        City = addressModel.City,
                        Street = addressModel.Street,
                        HomeNumber = addressModel.HomeNumber
                    }).ToList()
                };

                supplier.Password = _passwordHasher.HashPassword(supplier, model.Password);

                _supplierRepository.Add(supplier);
                await _supplierRepository.SaveChangesAsync();

                return Ok(new { Message = "Supplier registered successfully", SupplierId = supplier.Id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during supplier registration.");

                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _authenticationService.ValidateUserCredentials(model.Email, model.Password, "Supplier"))
                {
                    var supplier = await _supplierRepository.GetSupplierByEmailAsync(model.Email);
                    if (supplier == null)
                    {
                        return NotFound("Email not found!!!!!");
                    }

                    var token = _tokenService.CreateToken(supplier, "Supplier");
                    return Ok(new AuthResponseModel { Token = token });
                }

                return Unauthorized();
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An error occurred during supplier login.");

                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
        [HttpPost("add-product")]
        public async Task<IActionResult> AddProduct([FromBody] ProductAddModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var supplier = await _supplierRepository.GetSupplierByIdAsync(model.SupplierId);
                if (supplier == null)
                    return NotFound("Supplier not found");

                var category = await _categoryRepository.GetByIdAsync(model.CategoryId);
                if (category == null)
                    return NotFound("Category not found");

                var product = new Product
                {
                    Name = model.Name,
                    Price = model.Price,
                    Quantity = model.Quantity,
                    ImageSource = model.ImageSource,
                    SupplierId = model.SupplierId,
                    CategoryId = model.CategoryId,
                    ApprovalStatus = false,
                    Supplier = supplier,  // Load Supplier entity from DB
                    Category = category  // Load Category entity from DB
                };

                // Add product to repository
                _supplierRepository.AddProduct(product);
                await _supplierRepository.SaveChangesAsync();

                return Ok(new { Message = "Product added successfully", ProductId = product.Id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a product.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
        [HttpPut("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductAddModel model)
        {
            try
            {
                if (id < 1)
                {
                    return BadRequest(new { message = "Invalid product ID." });
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Check if the product exists
                var product = await _supplierRepository.GetProductByIdAsync(id);
                if (product == null)
                {
                    return NotFound(new { message = "Product not found." });
                }

                // Check if the supplier exists
                var supplier = await _supplierRepository.GetSupplierByIdAsync(model.SupplierId);
                if (supplier == null)
                {
                    return NotFound(new { message = "Supplier not found." });
                }

                // Check if the category exists
                var category = await _categoryRepository.GetByIdAsync(model.CategoryId);
                if (category == null)
                {
                    return NotFound(new { message = "Category not found." });
                }

                // Update the product fields
                product.Name = model.Name;
                product.Price = model.Price;
                product.Quantity = model.Quantity;
                product.ImageSource = model.ImageSource;
                product.SupplierId = model.SupplierId;
                product.CategoryId = model.CategoryId;
                product.Supplier = supplier;
                product.Category = category;

                // Save changes to the repository
                await _supplierRepository.SaveChangesAsync();

                return Ok(new { Message = "Product updated successfully", ProductId = product.Id });
            }
            catch (Exception ex)
            {
                // Log error
                _logger.LogError(ex, "An error occurred while updating the product.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

    }
}
