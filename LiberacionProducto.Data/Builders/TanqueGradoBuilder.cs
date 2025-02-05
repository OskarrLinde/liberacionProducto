using LiberacionProducto.CommonLibrary.Helpers;
using LiberacionProducto.Entities.Entities.CertCatalogos;
using System.Data;

namespace LiberacionProducto.Data.Builders
{
	public class TanqueGradoBuilder : IEntityBuilder<TanqueGradoData>
	{
		public TanqueGradoData BuildEntity(IDataReader reader)
		{
			return new TanqueGradoData
			{
				IdTanqueGrado = Convert.ToInt32(reader["ID_TANQUE_GRADO"]),
				IdTanque = Convert.ToInt32(reader["ID_TANQUE"]),
				IdGrado = Convert.ToInt32(reader["ID_GRADO"]),
				IdStatus = Convert.ToInt16(reader["ID_STATUS"])
			};
		}
	}
}