using LiberacionProducto.CommonLibrary.Helpers;
using LiberacionProducto.CommonLibrary.SQL;
using LiberacionProducto.Data.Builders;
using LiberacionProducto.Data.Interfaces;
using LiberacionProducto.Entities.Entities.CertCatalogos;
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

		public Task<List<FuenteSuministro>> GetFuenteSuministro()
		{
			var sqlProcedure = new SQLProcedure<FuenteSuministro>();
			sqlProcedure.StoredProcedureIdentifier = "[dbo].[CAB_SP_GetFuenteSuminisro]";
			sqlProcedure.AddBuilder(new FuenteSuministroBuilder());

			return AdoHelper.FindList(sqlProcedure, new SqlConnection(_connectionString));
		}

		public Task<List<PlantaAprobada>> GetPlantaAprobada()
		{
			var sqlProcedure = new SQLProcedure<PlantaAprobada>();
			sqlProcedure.StoredProcedureIdentifier = "[dbo].[CAB_SP_GetPlantaAprobada]";
			sqlProcedure.AddBuilder(new PlantaAprobadaBuilder());

			return AdoHelper.FindList(sqlProcedure, new SqlConnection(_connectionString));
		}
	}
}