namespace TrabajadoresPrueba.Models
{
    public class Departamento
    {
        public int Id { get; set; }
        public string? NombreDepartamento { get; set; }
        
        public ICollection<Provincia>? Provincia { get; set; }
        public ICollection<Trabajador>? Trabajador { get; set; }
    }
}
