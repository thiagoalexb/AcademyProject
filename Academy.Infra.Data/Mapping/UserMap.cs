using Academy.Domain.Entities;
using Academy.Infra.Data.Mapping.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Academy.Infra.Data.Mapping
{
    public class UserMap : IEntityTypeConfiguration<User>, IMapping
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(c => c.UserId)
                .HasColumnName("UserId");

            builder.Property(c => c.FirstName)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.LastName)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.Email)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.Password)
                .HasColumnType("varchar(50)")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(c => c.DateOfBirth)
                .IsRequired();
        }
    }
}
