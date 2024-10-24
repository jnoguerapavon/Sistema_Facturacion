namespace SistemaVentaAngular.DTOs
{
    public class CategoriaDTO
    {
        public int IdCategoria { get; set; }
        public string? Descripcion { get; set; }

        public bool? esActivo { get; set; }

        public DateTime fechaRegistro { get; set; }
    }
}
