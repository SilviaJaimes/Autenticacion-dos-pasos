using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        {
            builder.ToTable("user");

            builder.Property(p => p.Id)
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .IsRequired();

            builder.Property(p => p.Usuario)
                .HasColumnName("usuario")
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.Email)
                .HasColumnName("email")
                .HasColumnType("varchar")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(p => p.Password)
                .HasColumnName("password")
                .HasColumnType("varchar")
                .HasMaxLength(255);

            builder.Property(p => p.Fecha)
                .HasColumnName("fecha")
                .IsRequired();
        }
    }
}
