using LiberacionProducto.Entities.Entities.CertCatalogos;

namespace LiberacionProducto.Data.Interfaces
{
	public interface ICertCatalogosRepository
    {
		public Task<int> DeletePlanta(int IdPlanta);
        public Task<List<FuenteSuministro>> GetFuenteSuministro();
		public Task<List<PlantaAprobada>> GetPlantaAprobada();
	}
}