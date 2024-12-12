using Bulkify.Core.Entities;
using Bulkify.Core.Interfaces.Repositories;
using Bulkify.Core.Interfaces.Services;
using Bulkify.WebAPI.Models.RequestModels;
using Bulkify.WebAPI.Models.ResponseModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bulkify.WebApi.Controllers
{
    public class SuppliersController : BaseApiController
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly ITokenService _tokenService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IPasswordHasher<Supplier> _passwordHasher;
        private readonly ILogger<SuppliersController> _logger;

        public SuppliersController(
            ISupplierRepository supplierRepository,
            ITokenService tokenService,
            IAuthenticationService authenticationService,
            IPasswordHasher<Supplier> passwordHasher,
            ILogger<SuppliersController> logger)
        {
            _supplierRepository = supplierRepository;
            _tokenService = tokenService;
            _authenticationService = authenticationService;
            _passwordHasher = passwordHasher;
            _logger = logger;
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
    }
}
