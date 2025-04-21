namespace SggApp.DAL.Entidades
{
    public class Presupuestos
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int CategoriaId { get; set; }
        public int MonedaId { get; set; }
        public decimal Limite { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        // Relaciones
        public Usuarios Usuario { get; set; } = null!;
        public Categorias Categoria { get; set; } = null!;
        public Monedas Moneda { get; set; } = null!;
    }
}
