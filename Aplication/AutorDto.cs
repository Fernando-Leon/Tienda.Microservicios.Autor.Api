namespace Tienda.Microservicios.Autor.Api.Aplication
{
    public class AutorDto
    {
        public int AutorLibroId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string AutorLibroGuid { get; set; }
    }
}
