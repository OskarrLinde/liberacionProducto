using LiberacionProducto.CommonLibrary.Helpers;
using LiberacionProducto.Entities.Entities.CertCatalogos;
using System.Data;

namespace LiberacionProducto.Data.Builders
{
	public class PlantaAprobadaBuilder : IEntityBuilder<PlantaAprobada>
	{
		PlantaAprobada IEntityBuilder<PlantaAprobada>.BuildEntity(IDataReader reader)
		{
			return new PlantaAprobada
			{
				IdPlantaAprobada = Convert.ToInt32(reader["ID_PLANTA_APROBADA"]),
				Descripcion = reader["DESCRIPCION"].ToString(),
				IdStatus = Convert.ToInt16(reader["ID_STATUS"])
			};
		}
	}
}