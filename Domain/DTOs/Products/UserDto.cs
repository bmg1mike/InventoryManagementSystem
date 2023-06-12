namespace Domain;

public record AddUserDto(string FirstName,
                         string LastName,
                         string CompanyName,
                         string Password,
                         string Email);
