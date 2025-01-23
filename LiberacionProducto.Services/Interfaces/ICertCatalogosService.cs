using LiberacionProducto.Entities.Entities.CertCatalogos;

namespace LiberacionProducto.Services.Interfaces
{
	public interface ICertCatalogosService
    {
		public Task<int> DeletePlanta(int IdPlanta);
        public Task<List<FuenteSuministro>> GetFuenteSuministro();
		public Task<List<PlantaAprobada>> GetPlantaAprobada();
	}
}