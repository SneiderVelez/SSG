namespace SggApp.DAL.Entidades
{
    public class Categorias
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;

        // Relaciones
        public ICollection<Gastos> Gastos { get; set; } = null!;
        public ICollection<Presupuestos> Presupuestos { get; set; } = null!;
    }
}
