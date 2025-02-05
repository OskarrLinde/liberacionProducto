using LiberacionProducto.Entities.CertCatalogos;
using LiberacionProducto.Entities.Entities.CertCatalogos;

namespace LiberacionProducto.Services.Interfaces
{
	public interface ICertCatalogosService
    {
		public Task<int> DeletePlanta(int IdPlanta);

        public Task<List<FuenteSuministroData>> GetFuenteSuministro();
		public Task<List<PlantaAprobadaData>> GetPlantaAprobada();

		public Task<List<TanqueData>> GetTanques();	
		public Task<int> UpdateTanque(TanqueData tanque);
        public Task<int> InsertTanque(TanqueData tanque);

		public Task<List<PlantaParametroAnalizadorData>> GetPlantaParametroAnalizador(int? plantaId);
		public Task<int> UpdatePlantaParametroAnalizador(PlantaParametroAnalizadorData plantaParametroAnalizador);
		public Task<int> InsertPlantaParametroAnalizador(PlantaParametroAnalizadorData plantaParametroAnalizador);

        public Task<List<AnalizadorProductoData>> GetAnalizadorProducto(int? analizadorId);
        public Task<int> InsertAnalizadorProducto(AnalizadorProductoData analizadorProducto);
        public Task<int> UpdateAnalizadorProducto(AnalizadorProductoData analizadorProducto);

		public Task<List<TanqueGradoData>> GetTanqueGrado(int? tanqueId);
		public Task<int> InsertTanqueGrado(TanqueGradoData tanqueGrado);
		public Task<int> UpdateTanqueGrado(TanqueGradoData tanqueGrado);
	}
}