using LiberacionProducto.CommonLibrary.Helpers;
using LiberacionProducto.Entities.Entities.CertCatalogos;
using System.Data;

namespace LiberacionProducto.Data.Builders
{
    public class AnalizadorProductoBuilder : IEntityBuilder<AnalizadorProductoData>
    {
        public AnalizadorProductoData BuildEntity(IDataReader reader)
        {
            return new AnalizadorProductoData
            {
                IdAnalizadorProducto = Convert.ToInt32(reader["ID_ANALIZADOR_PRODUCTO"]),
                IdAnalizador = Convert.ToInt32(reader["ID_ANALIZADOR"]),
                IdProducto = Convert.ToInt32(reader["ID_PRODUCTO"]),
                IdStatus = Convert.ToInt16(reader["ID_STATUS"])
            };
        }
    }
}
