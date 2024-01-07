using System.Text.Json.Serialization;

namespace Sample.Architecture.Domain.Entities;
public class Role : Entity
{
    public required string Name { get; set; }

    [JsonIgnore]
    public ICollection<User> Users { get; set; }
    public ICollection<Permission> Permissions { get; set; }
    public User CreatedBy { get; set; }
}