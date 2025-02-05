using LiberacionProducto.Data.Interfaces;
using LiberacionProducto.Entities.CertCatalogos;
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

		public async Task<List<FuenteSuministroData>> GetFuenteSuministro()
		{
			return await this.certCatalogosRepository.GetFuenteSuministro();
		}

		public async Task<List<PlantaAprobadaData>> GetPlantaAprobada()
		{
			return await this.certCatalogosRepository.GetPlantaAprobada();
		}

		#region TANQUES
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
		#endregion

		#region PLANTA PARAMETRO ANALIZADOR
		public async Task<List<PlantaParametroAnalizadorData>> GetPlantaParametroAnalizador(int? plantaId)
		{
			return await this.certCatalogosRepository.GetPlantaParametroAnalizador(plantaId);
		}

		public async Task<int> UpdatePlantaParametroAnalizador(PlantaParametroAnalizadorData plantaParametroAnalizador)
		{
			return await this.certCatalogosRepository.UpdatePlantaParametroAnalizador(plantaParametroAnalizador);
		}

		public async Task<int> InsertPlantaParametroAnalizador(PlantaParametroAnalizadorData plantaParametroAnalizador)
		{
			return await this.certCatalogosRepository.InsertPlantaParametroAnalizador(plantaParametroAnalizador);
		}
        #endregion

        #region ANALIZADOR PRODUCTO
        public async Task<List<AnalizadorProductoData>> GetAnalizadorProducto(int? analizadorId)
        {
            return await this.certCatalogosRepository.GetAnalizadorProducto(analizadorId);
        }

        public async Task<int> InsertAnalizadorProducto(AnalizadorProductoData analizadorProducto)
        {
			return await this.certCatalogosRepository.InsertAnalizadorProducto(analizadorProducto);
        }

        public async Task<int> UpdateAnalizadorProducto(AnalizadorProductoData analizadorProducto)
        {
			return await this.certCatalogosRepository.UpdateAnalizadorProducto(analizadorProducto);
        }
		#endregion

		#region TANQUE GRADO
		public async Task<List<TanqueGradoData>> GetTanqueGrado(int? tanqueId)
		{
			return await this.certCatalogosRepository.GetTanqueGrado(tanqueId);
		}

		public async Task<int> InsertTanqueGrado(TanqueGradoData tanqueGrado)
		{
			return await this.certCatalogosRepository.InsertTanqueGrado(tanqueGrado);
		}

		public async Task<int> UpdateTanqueGrado(TanqueGradoData tanqueGrado)
		{
			return await this.certCatalogosRepository.UpdateTanqueGrado(tanqueGrado);
		}
		#endregion
	}
}