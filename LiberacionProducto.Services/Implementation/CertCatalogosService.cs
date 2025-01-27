using LiberacionProducto.Data.Interfaces;
using LiberacionProducto.Entities.CertCatalogos;
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

		public async Task<List<FuenteSuministroData>> GetFuenteSuministro()
		{
			return await this.certCatalogosRepository.GetFuenteSuministro();
		}

		public async Task<List<PlantaAprobadaData>> GetPlantaAprobada()
		{
			return await this.certCatalogosRepository.GetPlantaAprobada();
		}

        public async Task<List<TanqueData>> GetTanques()
        {
            return await this.certCatalogosRepository.GetTanques();
        }

        public async Task<int> UpdateTanque(TanqueData tanque)
        {
            return await this.certCatalogosRepository.UpdateTanque(tanque);
        }
        public async Task<int> InsertTanque(TanqueData tanque)
        {
            return await this.certCatalogosRepository.InsertTanque(tanque);
        }
    }
}