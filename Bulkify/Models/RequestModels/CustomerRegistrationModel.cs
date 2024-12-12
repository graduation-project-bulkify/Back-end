namespace Bulkify.WebAPI.Models.RequestModels;

public class CustomerRegistrationModel
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Gender { get; set; }
    public required string PhoneNumber { get; set; }
    public required string NationalId { get; set; }
    public required string City { get; set; }
    public required string Street { get; set; }
    public int HomeNumber { get; set; }
    public decimal XCoordinate { get; set; }
    public decimal YCoordinate { get; set; }

}
