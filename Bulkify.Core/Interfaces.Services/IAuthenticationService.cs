using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulkify.Core.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task<bool> ValidateUserCredentials(string email, string password, string role);
    }
}
