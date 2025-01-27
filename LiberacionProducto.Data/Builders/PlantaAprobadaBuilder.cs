using LiberacionProducto.CommonLibrary.Helpers;
using LiberacionProducto.Entities.CertCatalogos;
using System.Data;

namespace LiberacionProducto.Data.Builders
{
	public class PlantaAprobadaBuilder : IEntityBuilder<PlantaAprobadaData>
	{
        PlantaAprobadaData IEntityBuilder<PlantaAprobadaData>.BuildEntity(IDataReader reader)
		{
			return new PlantaAprobadaData
            {
				IdPlantaAprobada = Convert.ToInt32(reader["ID_PLANTA_APROBADA"]),
				Descripcion = reader["DESCRIPCION"].ToString(),
				IdStatus = Convert.ToInt16(reader["ID_STATUS"])
			};
		}
	}
}