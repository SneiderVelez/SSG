namespace SggApp.DAL.Entidades
{
    public class Gastos
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int CategoriaId { get; set; }
        public int MonedaId { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; } = null!;

        // Relaciones
        public Usuarios Usuario { get; set; } = null!; // Relación con el usuario
        public Categorias Categoria { get; set; } = null!; // Relación con la categoría
        public Monedas Moneda { get; set; } = null!; // Relación con la moneda
    }
}
