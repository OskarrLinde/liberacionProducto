using LiberacionProducto.Entities.CertCatalogos;

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
    }
}