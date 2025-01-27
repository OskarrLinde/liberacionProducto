using LiberacionProducto.CommonLibrary.Helpers;
using LiberacionProducto.CommonLibrary.SQL;
using LiberacionProducto.Data.Builders;
using LiberacionProducto.Data.Interfaces;
using LiberacionProducto.Entities.CertCatalogos;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace LiberacionProducto.Data.Implementation
{
    public class CertCatalogosRepository : ICertCatalogosRepository
	{
		private readonly string _connectionString;

		public CertCatalogosRepository(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("CertAnaliticosBulkConnection");
		}

		public Task<int> DeletePlanta(int IdPlanta)
		{
			var lstParameters = new List<SqlParameter>();
			lstParameters.Add(new SqlParameter("@IdPlanta", IdPlanta));

			return AdoHelper.ExecuteNonQueryAsync("[dbo].[CAB_SP_DeletePlanta]", lstParameters, new SqlConnection(_connectionString));
		}

		public Task<List<FuenteSuministroData>> GetFuenteSuministro()
		{
			var sqlProcedure = new SQLProcedure<FuenteSuministroData>();
			sqlProcedure.StoredProcedureIdentifier = "[dbo].[CAB_SP_GetFuenteSuminisro]";
			sqlProcedure.AddBuilder(new FuenteSuministroBuilder());

			return AdoHelper.FindList(sqlProcedure, new SqlConnection(_connectionString));
		}

		public Task<List<PlantaAprobadaData>> GetPlantaAprobada()
		{
			var sqlProcedure = new SQLProcedure<PlantaAprobadaData>();
			sqlProcedure.StoredProcedureIdentifier = "[dbo].[CAB_SP_GetPlantaAprobada]";
			sqlProcedure.AddBuilder(new PlantaAprobadaBuilder());

			return AdoHelper.FindList(sqlProcedure, new SqlConnection(_connectionString));
		}

        #region TANQUES
        public Task<List<TanqueData>> GetTanques()
        {
            var sqlProcedure = new SQLProcedure<TanqueData>();
            sqlProcedure.StoredProcedureIdentifier = "[dbo].[CAB_SP_GetTanques]";
            sqlProcedure.AddBuilder(new TanqueBuilder());

            return AdoHelper.FindList(sqlProcedure, new SqlConnection(_connectionString));
        }

        public Task<int> InsertTanque(TanqueData tanque)
        {
            var lstParameters = new List<SqlParameter>();
            lstParameters.Add(new SqlParameter("@ID_PLANTA", tanque.IdPlanta));
            lstParameters.Add(new SqlParameter("@ID_PRODUCTO", tanque.IdProducto));
            lstParameters.Add(new SqlParameter("@ID_GRADO", tanque.IdGrado));
            lstParameters.Add(new SqlParameter("@DESCRIPCION", tanque.Descripcion));
            lstParameters.Add(new SqlParameter("@ID_STATUS", tanque.IdStatus));
            lstParameters.Add(new SqlParameter("@CLAVE_PALS", tanque.ClavePals));
            lstParameters.Add(new SqlParameter("@USR_ALTA", tanque.UsrAlta));

            return AdoHelper.ExecuteNonQueryAsync("[dbo].[CAB_SP_InsertTanque]", lstParameters, new SqlConnection(_connectionString));
        }

        public Task<int> UpdateTanque(TanqueData tanque)
        {
            var lstParameters = new List<SqlParameter>();
			lstParameters.Add(new SqlParameter("@ID_TANQUE", tanque.IdTanque));
			lstParameters.Add(new SqlParameter("@ID_PLANTA", tanque.IdPlanta));
			lstParameters.Add(new SqlParameter("@ID_PRODUCTO", tanque.IdProducto));
			lstParameters.Add(new SqlParameter("@ID_GRADO", tanque.IdGrado));
			lstParameters.Add(new SqlParameter("@DESCRIPCION", tanque.Descripcion));
			lstParameters.Add(new SqlParameter("@ID_STATUS", tanque.IdStatus));
			lstParameters.Add(new SqlParameter("@CLAVE_PALS", tanque.ClavePals));
			lstParameters.Add(new SqlParameter("@USR_MODIFICA", tanque.UsrModifica));

            return AdoHelper.ExecuteNonQueryAsync("[dbo].[CAB_SP_UpdateTanque]", lstParameters, new SqlConnection(_connectionString));
        }
        #endregion TANQUES
    }
}