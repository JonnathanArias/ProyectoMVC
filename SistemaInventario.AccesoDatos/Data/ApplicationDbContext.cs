using Microsoft.EntityFrameworkCore;
using SistemaInventario.Modelos;

namespace SistemaInventario.AccesoDatos.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>(entity =>
            {
                // Configuración de la tabla
                entity.ToTable("Usuarios"); // Nombre explícito de tabla

                // Configuración de propiedades
                entity.Property(u => u.NumeroDocumento)
                    .HasMaxLength(20)
                    .IsRequired();

                entity.Property(u => u.Nombre)
                    .HasMaxLength(60)
                    .IsRequired();

                entity.Property(u => u.Apellido)
                    .HasMaxLength(60)
                    .IsRequired();

                entity.Property(u => u.Correo)
                    .HasMaxLength(256) // Longitud estándar para emails
                    .IsRequired();

                entity.Property(u => u.ContraseñaHash)
                    .HasColumnType("nvarchar(max)") // Tipo específico para SQL Server
                    .IsRequired();

                entity.Property(u => u.Rol)
                    .IsRequired();

                entity.Property(u => u.Ciudad)
                    .HasMaxLength(40)
                    .IsRequired();

                entity.Property(u => u.NombreUsuario)
                    .HasMaxLength(25)
                    .IsRequired();

                entity.Property(u => u.FechaCreacion)
                    .HasDefaultValueSql("GETDATE()"); // Valor por defecto en BD

                entity.Property(u => u.Estado)
                    .HasDefaultValue(true);

                entity.Property(u => u.DebeCambiarContraseña)
                    .HasDefaultValue(true);

                // Configuración de índices
                entity.HasIndex(u => u.NumeroDocumento)
                    .IsUnique()
                    .HasDatabaseName("IX_Usuarios_NumeroDocumento"); // Nombre personalizado

                entity.HasIndex(u => u.Correo)
                    .IsUnique()
                    .HasDatabaseName("IX_Usuarios_Correo");

                entity.HasIndex(u => u.NombreUsuario)
                    .IsUnique()
                    .HasDatabaseName("IX_Usuarios_NombreUsuario");
            });
        }
    }
}


