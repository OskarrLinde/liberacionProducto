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

		public async Task<int> HandleDeletePlanta(int IdPlanta)
		{
			return await this.certCatalogosRepository.DeletePlanta(IdPlanta);
		}

		public async Task<List<FuenteSuministroData>> HandleGetFuenteSuministro()
		{
			return await this.certCatalogosRepository.GetFuenteSuministro();
		}

		public async Task<List<PlantaAprobadaData>> HandleGetPlantaAprobada()
		{
			return await this.certCatalogosRepository.GetPlantaAprobada();
		}

		#region TANQUES
		public async Task<List<TanqueData>> HandleGetTanques()
        {
            return await this.certCatalogosRepository.GetTanques();
        }

        public async Task<int> HandleUpdateTanque(TanqueData tanque)
        {
            return await this.certCatalogosRepository.UpdateTanque(tanque);
        }
        public async Task<int> HandleInsertTanque(TanqueData tanque)
        {
            return await this.certCatalogosRepository.InsertTanque(tanque);
        }
		#endregion

		#region PLANTA PARAMETRO ANALIZADOR
		public async Task<List<PlantaParametroAnalizadorData>> HandleGetPlantaParametroAnalizador(int? plantaId)
		{
			return await this.certCatalogosRepository.GetPlantaParametroAnalizador(plantaId);
		}

		public async Task<int> HandleUpdatePlantaParametroAnalizador(PlantaParametroAnalizadorData plantaParametroAnalizador)
		{
			return await this.certCatalogosRepository.UpdatePlantaParametroAnalizador(plantaParametroAnalizador);
		}

		public async Task<int> HandleInsertPlantaParametroAnalizador(PlantaParametroAnalizadorData plantaParametroAnalizador)
		{
			return await this.certCatalogosRepository.InsertPlantaParametroAnalizador(plantaParametroAnalizador);
		}
        #endregion

        #region ANALIZADOR PRODUCTO
        public async Task<List<AnalizadorProductoData>> HandleGetAnalizadorProducto(int? analizadorId)
        {
            return await this.certCatalogosRepository.GetAnalizadorProducto(analizadorId);
        }

        public async Task<int> HandleInsertAnalizadorProducto(AnalizadorProductoData analizadorProducto)
        {
			return await this.certCatalogosRepository.InsertAnalizadorProducto(analizadorProducto);
        }

        public async Task<int> HandleUpdateAnalizadorProducto(AnalizadorProductoData analizadorProducto)
        {
			return await this.certCatalogosRepository.UpdateAnalizadorProducto(analizadorProducto);
        }
		#endregion

		#region TANQUE GRADO
		public async Task<List<TanqueGradoData>> HandleGetTanqueGrado(int? tanqueId)
		{
			return await this.certCatalogosRepository.GetTanqueGrado(tanqueId);
		}

		public async Task<int> HandleInsertTanqueGrado(TanqueGradoData tanqueGrado)
		{
			var idGradosArr = tanqueGrado.gradosString.Split(',');

			if (idGradosArr.Length > 0)
			{
                foreach (var idGrado in idGradosArr)
                {
					var tanqueGradoData = new TanqueGradoData()
					{
						IdTanque = tanqueGrado.IdTanque,
						IdGrado = Convert.ToInt32(idGrado),
						IdStatus = 1
					};

                    await this.certCatalogosRepository.InsertTanqueGrado(tanqueGradoData);
                }
            }

			return 0;
		}

		public async Task<int> HandleUpdateTanqueGrado(TanqueGradoData tanqueGrado)
		{
            var idGradosArr = tanqueGrado.gradosString.Split(',');

            if (idGradosArr.Length > 0)
            {
				var counter = 0;
                foreach (var idGrado in idGradosArr)
                {
                    var tanqueGradoData = new TanqueGradoData()
                    {
                        IdTanque = tanqueGrado.IdTanque,
                        IdGrado = Convert.ToInt32(idGrado),
                        IdStatus = 1,
						IsDelete = counter == 0
                    };

                    await this.certCatalogosRepository.UpdateTanqueGrado(tanqueGradoData);

					counter++;
                }
            }

            return 0;
        }
		#endregion
	}
}