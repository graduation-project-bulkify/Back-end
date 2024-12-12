using Bulkify.Core.Entities;
using Bulkify.Core.Interfaces.Repositories;
using Bulkify.Core.Interfaces.Services;
using Bulkify.WebApi.Controllers;
using Bulkify.WebAPI.Models.RequestModels;
using Bulkify.WebAPI.Models.ResponseModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bulkify.WebAPI.Controllers
{
    public class CustomersController : BaseApiController
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ITokenService _tokenService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IPasswordHasher<Customer> _passwordHasher;
        private readonly ILogger<CustomersController> _logger;
        public CustomersController(
            ICustomerRepository customerRepository,
            ITokenService tokenService,
            IAuthenticationService authenticationService,
            IPasswordHasher<Customer> passwordHasher,
            ILogger<CustomersController> logger)
            
        {
            _customerRepository = customerRepository;
            _tokenService = tokenService;
            _authenticationService = authenticationService;
            _passwordHasher = passwordHasher;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CustomerRegistrationModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existingCustomer = await _customerRepository.GetCustomerByEmailAsync(model.Email);
                if (existingCustomer != null)
                {
                    return BadRequest("Email already exists.");
                }

                var customer = new Customer
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Gender = model.Gender,
                    PhoneNumber = model.PhoneNumber,
                    NationalId = model.NationalId,
                    City = model.City,
                    Street = model.Street,
                    HomeNumber = model.HomeNumber,
                    XCoordinate = model.XCoordinate,
                    YCoordinate = model.YCoordinate
                };

                customer.Password = _passwordHasher.HashPassword(customer, model.Password);

                _customerRepository.Add(customer);
                await _customerRepository.SaveChangesAsync();

                return Ok(new { Message = "Customer registered successfully", CustomerId = customer.Id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during customer registration.");

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

                if (await _authenticationService.ValidateUserCredentials(model.Email, model.Password, "Customer"))
                {
                    var customer = await _customerRepository.GetCustomerByEmailAsync(model.Email);
                    if (customer == null)
                    {
                        return BadRequest("Email Not Found!!!");
                    }
                    var token = _tokenService.CreateToken(customer, "Customer");
                    return Ok(new AuthResponseModel { Token = token });
                }

                return Unauthorized();
            }
            catch (Exception ex)
            {
                // Log the exception
                // Example:
                 _logger.LogError(ex, "An error occurred during customer login.");

                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}