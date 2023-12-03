namespace Sample.Architecture.Domain.Entities;
public class User : Entity
{
    public required string Email { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
}
