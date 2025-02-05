namespace LiberacionProducto.Entities.CertCatalogos
{
	public class PlantaParametroAnalizadorData
	{
        public int IdPlantaParametroAnalizador { get; set; }
        public int IdAnalizador { get; set; }
        public int IdPlanta { get; set; }
        public int IdParametro { get; set; }
        public int IdMetodo { get; set; }
        public decimal LimiteInferior { get; set; }
        public string? LeyendaReporte { get; set; }
		public string? ClavePals { get; set; }
		public short IdStatus { get; set; }
        public int UsrAlta { get; set; }
		public int UsrModifica { get; set; }
        public bool IsDelete { get; set; }
    }
}
