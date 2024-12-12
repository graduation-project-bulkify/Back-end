using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulkify.Core.Interfaces.Services
{
    public interface ITokenService
    {
        string CreateToken(object user, string role);
    }
}
