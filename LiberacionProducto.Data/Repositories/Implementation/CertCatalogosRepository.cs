using LiberacionProducto.CommonLibrary.Helpers;
using LiberacionProducto.CommonLibrary.SQL;
using LiberacionProducto.Data.Builders;
using LiberacionProducto.Data.Interfaces;
using LiberacionProducto.Entities.CertCatalogos;
using LiberacionProducto.Entities.Entities.CertCatalogos;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.Net.NetworkInformation;

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
			lstParameters.Add(new SqlParameter("@DESCRIPCION", tanque.Descripcion));
			lstParameters.Add(new SqlParameter("@ID_STATUS", tanque.IdStatus));
			lstParameters.Add(new SqlParameter("@CLAVE_PALS", tanque.ClavePals));
			lstParameters.Add(new SqlParameter("@USR_MODIFICA", tanque.UsrModifica));

            return AdoHelper.ExecuteNonQueryAsync("[dbo].[CAB_SP_UpdateTanque]", lstParameters, new SqlConnection(_connectionString));
        }
		#endregion TANQUES

		#region PLANTA PARAMETRO ANALIZADOR
		public Task<List<PlantaParametroAnalizadorData>> GetPlantaParametroAnalizador(int? plantaId)
		{
			var sqlProcedure = new SQLProcedure<PlantaParametroAnalizadorData>();
			sqlProcedure.StoredProcedureIdentifier = "[dbo].[CAB_SP_GetParametroAnalizador]";
			sqlProcedure.AddBuilder(new PlantaParametroAnalizadorBuilder());
			sqlProcedure.AddParameter("@ID_PLANTA", plantaId);

			return AdoHelper.FindList(sqlProcedure, new SqlConnection(_connectionString));
		}

		public Task<int> InsertPlantaParametroAnalizador(PlantaParametroAnalizadorData plantaParametroAnalizador)
		{
			var lstParameters = new List<SqlParameter>();

			lstParameters.Add(new SqlParameter("@ID_ANALIZADOR", plantaParametroAnalizador.IdAnalizador));
			lstParameters.Add(new SqlParameter("@ID_PLANTA", plantaParametroAnalizador.IdPlanta));
			lstParameters.Add(new SqlParameter("@ID_PARAMETRO", plantaParametroAnalizador.IdParametro));
			lstParameters.Add(new SqlParameter("@ID_METODO", plantaParametroAnalizador.IdMetodo));
			lstParameters.Add(new SqlParameter("@LIMITE_INFERIOR", plantaParametroAnalizador.LimiteInferior));
			lstParameters.Add(new SqlParameter("@LEYENDA_REPORTE", plantaParametroAnalizador.LeyendaReporte));
			lstParameters.Add(new SqlParameter("@CLAVE_PALS", plantaParametroAnalizador.ClavePals));// --> AGREGAR EN EL FORMULARIOOOO 
			lstParameters.Add(new SqlParameter("@ID_STATUS", plantaParametroAnalizador.IdStatus));
			lstParameters.Add(new SqlParameter("@USR_ALTA", plantaParametroAnalizador.UsrAlta));

			return AdoHelper.ExecuteNonQueryAsync("[dbo].[CAB_SP_InsertParametroAnalizador]", lstParameters, new SqlConnection(_connectionString));
		}

		public Task<int> UpdatePlantaParametroAnalizador(PlantaParametroAnalizadorData plantaParametroAnalizador)
		{
			var lstParameters = new List<SqlParameter>();

			lstParameters.Add(new SqlParameter("@ID_PLANTA_PARAMETRO_ANALIZADOR", plantaParametroAnalizador.IdPlantaParametroAnalizador));
			lstParameters.Add(new SqlParameter("@ID_PARAMETRO", plantaParametroAnalizador.IdParametro));
			lstParameters.Add(new SqlParameter("@ID_ANALIZADOR", plantaParametroAnalizador.IdAnalizador));
			lstParameters.Add(new SqlParameter("@ID_METODO", plantaParametroAnalizador.IdMetodo));
			lstParameters.Add(new SqlParameter("@LIMITE_INFERIOR", plantaParametroAnalizador.LimiteInferior));
			lstParameters.Add(new SqlParameter("@LEYENDA_REPORTE", plantaParametroAnalizador.LeyendaReporte));
			lstParameters.Add(new SqlParameter("@CLAVE_PALS", plantaParametroAnalizador.ClavePals));
			lstParameters.Add(new SqlParameter("@ID_STATUS", plantaParametroAnalizador.IdStatus));
			lstParameters.Add(new SqlParameter("@USR_MODIFICA", plantaParametroAnalizador.UsrModifica));
			lstParameters.Add(new SqlParameter("@IS_DELETE", plantaParametroAnalizador.IsDelete));

			return AdoHelper.ExecuteNonQueryAsync("[dbo].[CAB_SP_UpdateParametroAnalizador]", lstParameters, new SqlConnection(_connectionString));
		}

        #endregion

        #region ANALIZADOR PRODUCTO
        public Task<List<AnalizadorProductoData>> GetAnalizadorProducto(int? analizadorId)
        {
            var sqlProcedure = new SQLProcedure<AnalizadorProductoData>();
            sqlProcedure.StoredProcedureIdentifier = "[dbo].[CAB_SP_GetAnalizadorProducto]";
            sqlProcedure.AddBuilder(new AnalizadorProductoBuilder());
            sqlProcedure.AddParameter("@ID_ANALIZADOR", analizadorId);

            return AdoHelper.FindList(sqlProcedure, new SqlConnection(_connectionString));
        }

        public Task<int> InsertAnalizadorProducto(AnalizadorProductoData analizadorProducto)
        {
            var lstParameters = new List<SqlParameter>();

            lstParameters.Add(new SqlParameter("@ID_ANALIZADOR", analizadorProducto.IdAnalizador));
			lstParameters.Add(new SqlParameter("@ID_PRODUCTO", analizadorProducto.IdProducto));
            lstParameters.Add(new SqlParameter("@USR_ALTA", analizadorProducto.UsrAlta));

            return AdoHelper.ExecuteNonQueryAsync("[dbo].[CAB_SP_InsertAnalizadorProducto]", lstParameters, new SqlConnection(_connectionString));
        }

        public Task<int> UpdateAnalizadorProducto(AnalizadorProductoData analizadorProducto)
        {
            var lstParameters = new List<SqlParameter>();

            lstParameters.Add(new SqlParameter("@ID_ANALIZADOR_PRODUCTO", analizadorProducto.IdAnalizadorProducto));
            lstParameters.Add(new SqlParameter("@ID_ANALIZADOR", analizadorProducto.IdAnalizador));
            lstParameters.Add(new SqlParameter("@ID_PRODUCTO", analizadorProducto.IdProducto));
            lstParameters.Add(new SqlParameter("@ID_STATUS", analizadorProducto.IdStatus));
            lstParameters.Add(new SqlParameter("@USR_MODIFICA", analizadorProducto.UsrModifica));
            lstParameters.Add(new SqlParameter("@IS_DELETE", analizadorProducto.IsDelete));

            return AdoHelper.ExecuteNonQueryAsync("[dbo].[CAB_SP_UpdateAnalizadorProducto]", lstParameters, new SqlConnection(_connectionString));
        }
		#endregion

		#region TANQUE GRADO
		public Task<List<TanqueGradoData>> GetTanqueGrado(int? tanqueId)
		{
			var sqlProcedure = new SQLProcedure<TanqueGradoData>();
			sqlProcedure.StoredProcedureIdentifier = "[dbo].[CAB_SP_GetTanqueGrado]";
			sqlProcedure.AddBuilder(new TanqueGradoBuilder());
			sqlProcedure.AddParameter("@ID_TANQUE", tanqueId);

			return AdoHelper.FindList(sqlProcedure, new SqlConnection(_connectionString));
		}

		public Task<int> InsertTanqueGrado(TanqueGradoData tanqueGrado)
		{
			var lstParameters = new List<SqlParameter>();

			lstParameters.Add(new SqlParameter("@ID_TANQUE", tanqueGrado.IdTanque));
			lstParameters.Add(new SqlParameter("@ID_GRADO", tanqueGrado.IdGrado));
			lstParameters.Add(new SqlParameter("@USR_ALTA", tanqueGrado.UsrAlta));

			return AdoHelper.ExecuteNonQueryAsync("[dbo].[CAB_SP_InsertTanqueGrado]", lstParameters, new SqlConnection(_connectionString));
		}

		public Task<int> UpdateTanqueGrado(TanqueGradoData tanqueGrado)
		{
			var lstParameters = new List<SqlParameter>();

			lstParameters.Add(new SqlParameter("@ID_TANQUE_GRADO", tanqueGrado.IdTanqueGrado));
			lstParameters.Add(new SqlParameter("@ID_GRADO", tanqueGrado.IdGrado));
			lstParameters.Add(new SqlParameter("@ID_STATUS", tanqueGrado.IdStatus));
			lstParameters.Add(new SqlParameter("@USR_MODIFICA", tanqueGrado.UsrModifica));
			lstParameters.Add(new SqlParameter("@IS_DELETE", tanqueGrado.IsDelete));

			return AdoHelper.ExecuteNonQueryAsync("[dbo].[CAB_SP_UpdateTanqueGrado]", lstParameters, new SqlConnection(_connectionString));
		}
		#endregion
	}
}