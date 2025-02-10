using System.Data;

namespace LiberacionProducto.Entities.CertCatalogos
{
    public class TanqueData
    {
        public int IdTanque { get; set; }
        public int IdPlanta { get; set; }
        public int? IdProducto { get; set; }
        public string? Descripcion { get; set; }
        public string? Grados { get; set; }
        public short IdStatus { get; set; }
        public string? ClavePals { get; set; }
        public int UsrAlta { get; set; }
        public int UsrModifica { get; set; }
    }
}