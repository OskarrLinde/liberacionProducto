using LiberacionProducto.Entities.CertCatalogos;
using LiberacionProducto.Entities.Entities.CertCatalogos;

namespace LiberacionProducto.Data.Interfaces
{
	public interface ICertCatalogosRepository
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
        public Task<int> InsertAnalizadorProducto(AnalizadorProductoData plantaParametroAnalizador);
        public Task<int> UpdateAnalizadorProducto(AnalizadorProductoData plantaParametroAnalizador);

		public Task<List<TanqueGradoData>> GetTanqueGrado(int? tanqueId);
		public Task<int> InsertTanqueGrado(TanqueGradoData tanqueGrado);
		public Task<int> UpdateTanqueGrado(TanqueGradoData tanqueGrado);

	}
}