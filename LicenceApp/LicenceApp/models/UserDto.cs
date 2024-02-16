using LicenceApp.Enums;

namespace LicenceApp.models;

public class UserDto
{ //this class is for the return of the  data !
    public int Id { get; set; }
    public required string? FirstName { get; set; }

    public required string? LastName { get; set; }

    public required string? Email { get; set; }

    public required Role Role { get; set; }

}
