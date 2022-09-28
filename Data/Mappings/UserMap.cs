using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Data.Mappings;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {

        builder.ToTable("User");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
        .ValueGeneratedOnAdd()
        .UseIdentityColumn();

        builder.Property(x => x.Name)
             .IsRequired()
             .HasColumnName("Name")
             .HasColumnType("NVARCHAR")
             .HasMaxLength(80);

        builder.Property(x => x.Email)
        .IsRequired()
        .HasColumnName("Email")
        .HasColumnType("NVARCHAR")
        .HasMaxLength(80);

        builder
        .HasIndex(x => x.Email)
        .IsUnique();

        builder.Property(x => x.PasswordHash)
        .IsRequired()
        .HasColumnName("passwordHash")
        .HasColumnType("NVARCHAR")
        .HasMaxLength(100);

    }
}
