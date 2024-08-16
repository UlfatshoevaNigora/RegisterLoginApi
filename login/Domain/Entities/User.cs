namespace login.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public required string Name { get; set; }
    public required string Login { get; set; }
    public required string PasswordHashed { get; set; }
    public string Role { get; set; }
}