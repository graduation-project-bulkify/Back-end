using Bulkify.Core.Entities;
using Bulkify.WebApi.Models.RequestModels;

namespace Bulkify.WebAPI.Models.RequestModels;

public class SupplierRegistrationModel
{
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Gender { get; set; }
    public required string PhoneNumber { get; set; }
    public required string CommercialRegister { get; set; }
    public decimal SupplierRate { get; set; }
    public required ICollection<SupplierAddressesModel> SupplierAddressesmodel { get; set; }

}
