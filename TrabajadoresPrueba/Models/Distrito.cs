namespace TrabajadoresPrueba.Models
{
    public class Distrito
    {
        public int Id { get; set; }
        public string? NombreDistrito { get; set; }
        public int IdProvincia { get; set; }
        
        public Provincia? Provincia { get; set; }
        public ICollection<Trabajador>? Trabajador { get; set; }
    }
}