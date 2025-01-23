using LiberacionProducto.Data.Interfaces;
using LiberacionProducto.Entities.Entities.CertCatalogos;
using LiberacionProducto.Services.Interfaces;

namespace LiberacionProducto.Services.Implementation
{
	public class CertCatalogosService : ICertCatalogosService
	{
		private readonly ICertCatalogosRepository certCatalogosRepository;

		public CertCatalogosService(ICertCatalogosRepository certCatalogosRepository)
		{
			this.certCatalogosRepository = certCatalogosRepository;
		}

		public async Task<int> DeletePlanta(int IdPlanta)
		{
			return await this.certCatalogosRepository.DeletePlanta(IdPlanta);
		}

		public async Task<List<FuenteSuministro>> GetFuenteSuministro()
		{
			return await this.certCatalogosRepository.GetFuenteSuministro();
		}

		public async Task<List<PlantaAprobada>> GetPlantaAprobada()
		{
			return await this.certCatalogosRepository.GetPlantaAprobada();
		}
	}
}