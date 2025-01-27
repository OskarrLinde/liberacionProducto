using LiberacionProducto.CommonLibrary.Helpers;
using LiberacionProducto.Entities.CertCatalogos;
using System.Data;

namespace LiberacionProducto.Data.Builders
{
    public class TanqueBuilder : IEntityBuilder<TanqueData>
    {
        public TanqueData BuildEntity(IDataReader reader)
        {
            return new TanqueData
            {
                IdTanque = Convert.ToInt32(reader["ID_TANQUE"]),
                IdPlanta = Convert.ToInt32(reader["ID_PLANTA"]),
                IdProducto = !(reader["ID_PRODUCTO"] is DBNull) ? Convert.ToInt32(reader["ID_PRODUCTO"]) : (int?)null,
                IdGrado = !(reader["ID_GRADO"] is DBNull) ? Convert.ToInt32(reader["ID_GRADO"]) : (int?)null,
                Descripcion = reader["DESCRIPCION"].ToString(),
                IdStatus = Convert.ToInt16(reader["ID_STATUS"]),
                ClavePals = reader["CLAVE_PALS"].ToString()
            };
        }
    }
}