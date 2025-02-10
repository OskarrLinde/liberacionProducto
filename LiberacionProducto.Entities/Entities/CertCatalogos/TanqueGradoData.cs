namespace LiberacionProducto.Entities.Entities.CertCatalogos
{
	public class TanqueGradoData
	{
        public int IdTanqueGrado { get; set; }
        public int IdTanque { get; set; }
        public int IdGrado { get; set; }
        public string? gradosString { get; set; }
        public short IdStatus { get; set; }
		public int UsrAlta { get; set; }
		public int UsrModifica { get; set; }
		public bool IsDelete { get; set; }
	}
}
