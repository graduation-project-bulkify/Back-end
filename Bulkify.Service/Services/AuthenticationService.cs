using Bulkify.Core.Interfaces.Repositories;
using Bulkify.Core.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulkify.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IPasswordHasher<object> _passwordHasher;

        public AuthenticationService(ICustomerRepository customerRepository, ISupplierRepository supplierRepository, IPasswordHasher<object> passwordHasher)
        {
            _customerRepository = customerRepository;
            _supplierRepository = supplierRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<bool> ValidateUserCredentials(string email, string password, string role)
        {
            if (role == "Customer")
            {
                var customer = await _customerRepository.GetCustomerByEmailAsync(email);
                return customer != null && _passwordHasher.VerifyHashedPassword(customer, customer.Password, password) == PasswordVerificationResult.Success;
            }
            else if (role == "Supplier")
            {
                var supplier = await _supplierRepository.GetSupplierByEmailAsync(email);
                return supplier != null && _passwordHasher.VerifyHashedPassword(supplier, supplier.Password, password) == PasswordVerificationResult.Success;
            }

            return false; // Invalid role
        }
    }
}