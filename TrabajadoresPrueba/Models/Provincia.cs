namespace TrabajadoresPrueba.Models
{
    public class Provincia
    {
        public int Id { get; set; }
        public string? NombreProvincia { get; set; }
        public int IdDepartamento { get; set; }
        public Departamento? Departamento { get; set; }
        public ICollection<Distrito>? Distrito { get; set; }
        public ICollection<Trabajador>? Trabajador { get; set; }
    }
}
