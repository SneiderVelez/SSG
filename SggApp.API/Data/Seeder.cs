using Microsoft.EntityFrameworkCore;
using SggApp.DAL.Entidades;

namespace SggApp.API.Data
{
    /// <summary>
    /// Clase para poblar datos iniciales en la base de datos
    /// </summary>
    public static class Seeder
    {
        /// <summary>
        /// Inicializar datos de monedas y categorías
        /// </summary>
        public static async Task SeedData(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // Aplicar migraciones pendientes
            await context.Database.MigrateAsync();

            // Seed de Monedas
            if (!await context.Monedas.AnyAsync())
            {
                await context.Monedas.AddRangeAsync(
                    new Monedas { Codigo = "USD", Nombre = "Dólar Estadounidense", Simbolo = "$" },
                    new Monedas { Codigo = "EUR", Nombre = "Euro", Simbolo = "€" },
                    new Monedas { Codigo = "GBP", Nombre = "Libra Esterlina", Simbolo = "£" },
                    new Monedas { Codigo = "ARS", Nombre = "Peso Argentino", Simbolo = "$" },
                    new Monedas { Codigo = "MXN", Nombre = "Peso Mexicano", Simbolo = "$" },
                    new Monedas { Codigo = "CLP", Nombre = "Peso Chileno", Simbolo = "$" },
                    new Monedas { Codigo = "COP", Nombre = "Peso Colombiano", Simbolo = "$" },
                    new Monedas { Codigo = "PEN", Nombre = "Sol Peruano", Simbolo = "S/" },
                    new Monedas { Codigo = "BRL", Nombre = "Real Brasileño", Simbolo = "R$" },
                    new Monedas { Codigo = "PYG", Nombre = "Guaraní Paraguayo", Simbolo = "₲" },
                    new Monedas { Codigo = "UYU", Nombre = "Peso Uruguayo", Simbolo = "$U" }
                );

                await context.SaveChangesAsync();
            }

            // Seed de Categorías
            if (!await context.Categorias.AnyAsync())
            {
                await context.Categorias.AddRangeAsync(
                    new Categorias { Nombre = "Alimentación", Descripcion = "Gastos relacionados con comida y alimentos" },
                    new Categorias { Nombre = "Transporte", Descripcion = "Gastos de movilidad, combustible, pasajes, etc." },
                    new Categorias { Nombre = "Vivienda", Descripcion = "Alquiler, hipoteca, mantenimiento del hogar" },
                    new Categorias { Nombre = "Servicios", Descripcion = "Electricidad, agua, internet, teléfono, etc." },
                    new Categorias { Nombre = "Salud", Descripcion = "Medicamentos, consultas médicas, seguros de salud" },
                    new Categorias { Nombre = "Educación", Descripcion = "Matrícula, cursos, libros, material educativo" },
                    new Categorias { Nombre = "Ocio", Descripcion = "Entretenimiento, restaurantes, cine, viajes" },
                    new Categorias { Nombre = "Ropa", Descripcion = "Vestimenta, calzado, accesorios" },
                    new Categorias { Nombre = "Tecnología", Descripcion = "Dispositivos electrónicos, software" },
                    new Categorias { Nombre = "Deudas", Descripcion = "Pagos de préstamos, tarjetas de crédito" },
                    new Categorias { Nombre = "Ahorros", Descripcion = "Inversiones, fondos de emergencia" },
                    new Categorias { Nombre = "Otros", Descripcion = "Gastos no categorizados" }
                );

                await context.SaveChangesAsync();
            }
        }
    }
}