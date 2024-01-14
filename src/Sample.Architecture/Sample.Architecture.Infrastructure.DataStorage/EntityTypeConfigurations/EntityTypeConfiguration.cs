using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Architecture.Domain.Entities;

namespace Sample.Architecture.Infrastructure.DataStorage.EntityTypeConfigurations;
internal class EntityTypeConfiguration<TId> : IEntityTypeConfiguration<Entity<TId>>
    where TId : struct
{
    public void Configure(EntityTypeBuilder<Entity<TId>> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();
    }
}
