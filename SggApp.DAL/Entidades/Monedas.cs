namespace SggApp.DAL.Entidades
{
    public class Monedas
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Simbolo { get; set; } = null!;

        // Relaciones
        public ICollection<Gastos> Gastos { get; set; } = null!;
        public ICollection<Presupuestos> Presupuestos { get; set; } = null!;
    }
}
