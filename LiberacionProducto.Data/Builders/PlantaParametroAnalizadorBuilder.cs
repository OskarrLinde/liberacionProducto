using LiberacionProducto.CommonLibrary.Helpers;
using LiberacionProducto.Entities.CertCatalogos;
using System.Data;

namespace LiberacionProducto.Data.Builders
{
	public class PlantaParametroAnalizadorBuilder : IEntityBuilder<PlantaParametroAnalizadorData>
	{
		public PlantaParametroAnalizadorData BuildEntity(IDataReader reader)
		{
			return new PlantaParametroAnalizadorData
			{
				IdPlantaParametroAnalizador = Convert.ToInt32(reader["ID_PLANTA_PARAMETRO_ANALIZADOR"]),
				IdPlanta = Convert.ToInt32(reader["ID_PLANTA"]),
				IdAnalizador = Convert.ToInt32(reader["ID_ANALIZADOR"]),
				IdParametro = Convert.ToInt32(reader["ID_PARAMETRO"]),
				IdMetodo = Convert.ToInt32(reader["ID_METODO"]),
				LimiteInferior = Convert.ToInt32(reader["LIMITE_INFERIOR"]),
				LeyendaReporte = reader["LEYENDA_REPORTE"].ToString(),
				ClavePals = reader["CLAVE_PALS"].ToString(),
				IdStatus = Convert.ToInt16(reader["ID_STATUS"])
			};
		}
	}
}