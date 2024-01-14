namespace Sample.Architecture.Domain.Entities;
public class User : Entity<int>
{
    public required string Email { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
}
