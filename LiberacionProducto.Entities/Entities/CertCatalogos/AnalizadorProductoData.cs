namespace LiberacionProducto.Entities.Entities.CertCatalogos
{
    public class AnalizadorProductoData
    {
        public int IdAnalizadorProducto { get; set; }
        public int IdAnalizador { get; set; }
        public int IdProducto { get; set; }
        public short IdStatus { get; set; }
        public int UsrAlta { get; set; }
        public int UsrModifica { get; set; }
        public bool IsDelete { get; set; }
    }
}
