namespace SggApp.DAL.Entidades
{
    public class Usuarios
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public DateTime FechaRegistro { get; set; }

        // Relaciones
        public ICollection<Gastos> Gastos { get; set; } = null!;// Un usuario puede tener muchos gastos
        public ICollection<Presupuestos> Presupuestos { get; set; } = null!; // Un usuario puede tener muchos presupuestos
    }
}
