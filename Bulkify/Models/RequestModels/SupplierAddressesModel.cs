namespace Bulkify.WebApi.Models.RequestModels
{
    public class SupplierAddressesModel
    {
        public required string City { get; set; }
        public required string Street { get; set; }
        public int HomeNumber { get; set; }

    }
}
