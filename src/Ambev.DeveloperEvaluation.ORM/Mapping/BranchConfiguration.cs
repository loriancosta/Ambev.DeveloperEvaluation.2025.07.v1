using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class BranchConfiguration : IEntityTypeConfiguration<Branch>
{
    public void Configure(EntityTypeBuilder<Branch> builder)
    {
        builder.ToTable("Branches");

        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id)
            .HasColumnType("uuid")
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(b => b.ExternalId)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(b => b.ExternalId)
            .IsUnique();

        builder.Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(b => b.Address)
            .HasMaxLength(200);

        builder.Property(b => b.City)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(b => b.State)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(b => b.PostalCode)
            .HasMaxLength(20);

        builder.Property(b => b.IsActive)
            .IsRequired()
            .HasDefaultValue(true);
    }
}
