using FCG.Users.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FCG.Users.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
            public void Configure(EntityTypeBuilder<Usuario> builder)
            {
                builder.ToTable("usuarios");
                builder.HasKey(p => p.Id);
                builder.Property(p => p.Id).HasColumnType("INT").ValueGeneratedOnAdd().UseIdentityColumn();
                builder.Property(p => p.Nome).HasColumnType("VARCHAR(50)").IsRequired();
                builder.Property(p => p.Email).HasColumnType("VARCHAR(100)").IsRequired();
                builder.Property(p => p.Senha).HasColumnType("VARCHAR(255)").IsRequired();
                builder.Property(p => p.Situacao).HasColumnType("VARCHAR(10)").IsRequired();
            }
    }
}
