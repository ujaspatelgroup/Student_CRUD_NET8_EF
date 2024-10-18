namespace StudentCRUD.DTOs.Shared
{
    public record class UserSession(string? Id, string FirstName, string LastName, string Email, string Role);
}
