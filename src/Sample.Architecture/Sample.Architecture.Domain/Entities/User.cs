using System.Text.Json.Serialization;

namespace Sample.Architecture.Domain.Entities;
public class User : Entity
{
    public required string Email { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }

    public required ICollection<Role> Roles { get; set; }
    [JsonIgnore]
    public ICollection<Role> CreatedRoles { get; set; }
    public User? ReferedBy { get; set; }
}
