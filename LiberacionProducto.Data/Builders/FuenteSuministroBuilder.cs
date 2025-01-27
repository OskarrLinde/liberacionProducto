using LiberacionProducto.CommonLibrary.Helpers;
using LiberacionProducto.Entities.CertCatalogos;
using System.Data;

namespace LiberacionProducto.Data.Builders
{
	public class FuenteSuministroBuilder : IEntityBuilder<FuenteSuministroData>
	{
		public FuenteSuministroData BuildEntity(IDataReader reader)
		{
			return new FuenteSuministroData
            {
				IdFuenteSuministro = Convert.ToInt32(reader["ID_FUENTE_SUMINISTRO"]),
				Descripcion = reader["DESCRIPCION"].ToString(),
				IdStatus = Convert.ToInt16(reader["ID_STATUS"])
			};
		}
	}
}