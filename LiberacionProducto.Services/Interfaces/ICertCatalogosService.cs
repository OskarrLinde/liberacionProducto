using LiberacionProducto.Entities.CertCatalogos;
using LiberacionProducto.Entities.Entities.CertCatalogos;

namespace LiberacionProducto.Services.Interfaces
{
	public interface ICertCatalogosService
    {
		public Task<int> HandleDeletePlanta(int IdPlanta);

        public Task<List<FuenteSuministroData>> HandleGetFuenteSuministro();
		public Task<List<PlantaAprobadaData>> HandleGetPlantaAprobada();

		public Task<List<TanqueData>> HandleGetTanques();	
		public Task<int> HandleUpdateTanque(TanqueData tanque);
        public Task<int> HandleInsertTanque(TanqueData tanque);

		public Task<List<PlantaParametroAnalizadorData>> HandleGetPlantaParametroAnalizador(int? plantaId);
		public Task<int> HandleUpdatePlantaParametroAnalizador(PlantaParametroAnalizadorData plantaParametroAnalizador);
		public Task<int> HandleInsertPlantaParametroAnalizador(PlantaParametroAnalizadorData plantaParametroAnalizador);

        public Task<List<AnalizadorProductoData>> HandleGetAnalizadorProducto(int? analizadorId);
        public Task<int> HandleInsertAnalizadorProducto(AnalizadorProductoData analizadorProducto);
        public Task<int> HandleUpdateAnalizadorProducto(AnalizadorProductoData analizadorProducto);

		public Task<List<TanqueGradoData>> HandleGetTanqueGrado(int? tanqueId);
		public Task<int> HandleInsertTanqueGrado(TanqueGradoData tanqueGrado);
		public Task<int> HandleUpdateTanqueGrado(TanqueGradoData tanqueGrado);
	}
}