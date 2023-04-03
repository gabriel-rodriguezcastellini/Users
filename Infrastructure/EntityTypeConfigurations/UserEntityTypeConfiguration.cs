using ApplicationCore.Entities.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace Infrastructure.EntityTypeConfigurations;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Money).HasPrecision(14, 2).HasComment("The money of the user");
        builder.Property(u => u.Type).HasComment("The user type");
        builder.Property(u => u.Address).HasMaxLength(256).IsRequired().HasComment("The address of the user");
        builder.Property(u => u.Email).HasMaxLength(500).IsRequired().HasComment("The email of the user");
        builder.Property(u => u.Name).HasMaxLength(256).IsRequired().UseCollation("SQL_Latin1_General_CP1_CI_AS").HasComment("The name of the user");
        builder.Property(u => u.Phone).HasMaxLength(256).IsRequired().HasComment("The phone of the user");
        builder.HasIndex(u => u.Email).IsUnique();
        builder.HasIndex(u => u.Phone).IsUnique();
        builder.HasIndex("Name", "Address").IsUnique();
    }
}
