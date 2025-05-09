using SggApp.DAL.Entidades;
using Microsoft.EntityFrameworkCore;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Usuarios> Usuarios { get; set; }
    public DbSet<Categorias> Categorias { get; set; }
    public DbSet<Monedas> Monedas { get; set; }
    public DbSet<Gastos> Gastos { get; set; }
    public DbSet<Presupuestos> Presupuestos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configurar relaciones y restricciones

        // Relaciones de Usuario
        modelBuilder.Entity<Usuarios>()
            .HasMany(u => u.Gastos) // Un usuario tiene muchos gastos
            .WithOne(g => g.Usuario) // Un gasto pertenece a un usuario
            .HasForeignKey(g => g.UsuarioId) // Clave foránea en Gasto
            .OnDelete(DeleteBehavior.Restrict); // Evitar eliminación en cascada
        modelBuilder.Entity<Usuarios>()
            .HasMany(u => u.Presupuestos) // Un usuario tiene muchos presupuestos
            .WithOne(p => p.Usuario) // Un presupuesto pertenece a un usuario
            .HasForeignKey(p => p.UsuarioId) // Clave foránea en Presupuesto
            .OnDelete(DeleteBehavior.Restrict); // Evitar eliminación en cascada
                                                // Relaciones de Categoria
        modelBuilder.Entity<Categorias>()
            .HasMany(c => c.Gastos) // Una categoría tiene muchos gastos
            .WithOne(g => g.Categoria) // Un gasto pertenece a una categoría
            .HasForeignKey(g => g.CategoriaId) // Clave foránea en Gasto
            .OnDelete(DeleteBehavior.Restrict); // Evitar eliminación en cascada
        modelBuilder.Entity<Categorias>()
            .HasMany(c => c.Presupuestos) // Una categoría tiene muchos presupuestos
            .WithOne(p => p.Categoria) // Un presupuesto pertenece a una categoría
            .HasForeignKey(p => p.CategoriaId) // Clave foránea en Presupuesto
            .OnDelete(DeleteBehavior.Restrict); // Evitar eliminación en cascada
                                                // Relaciones de Moneda
        modelBuilder.Entity<Monedas>()
            .HasMany(m => m.Gastos) // Una moneda tiene muchos gastos
            .WithOne(g => g.Moneda) // Un gasto pertenece a una moneda
            .HasForeignKey(g => g.MonedaId) // Clave foránea en Gasto
            .OnDelete(DeleteBehavior.Restrict); // Evitar eliminación en cascada
        modelBuilder.Entity<Monedas>()
            .HasMany(m => m.Presupuestos) // Una moneda tiene muchos presupuestos
            .WithOne(p => p.Moneda) // Un presupuesto pertenece a una moneda
            .HasForeignKey(p => p.MonedaId) // Clave foránea en Presupuesto
            .OnDelete(DeleteBehavior.Restrict); // Evitar eliminación en cascada

        // Configuración de propiedades decimales
        modelBuilder.Entity<Gastos>()
            .Property(g => g.Monto)
            .HasPrecision(18, 2); // 18 dígitos en total, 2 decimales

        modelBuilder.Entity<Presupuestos>()
            .Property(p => p.Limite)
            .HasPrecision(18, 2); // 18 dígitos en total, 2 decimales

        // Configuraciones adicionales (opcional)
        modelBuilder.Entity<Usuarios>()
            .HasIndex(u => u.Email) // Índice único para el correo electrónico
            .IsUnique();
        modelBuilder.Entity<Categorias>()
            .HasIndex(c => c.Nombre) // Índice único para el nombre de la categoría
            .IsUnique();
        modelBuilder.Entity<Monedas>()
            .HasIndex(m => m.Codigo) // Índice único para el código ISO de la moneda
            .IsUnique();
    }
}
