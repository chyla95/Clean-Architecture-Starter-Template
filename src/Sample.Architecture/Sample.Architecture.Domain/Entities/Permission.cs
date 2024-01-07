using System.Text.Json.Serialization;

namespace Sample.Architecture.Domain.Entities;
public class Permission : Entity
{
    public required string Name { get; set; }

    [JsonIgnore]
    public ICollection<Role> Roles { get; set; }
}