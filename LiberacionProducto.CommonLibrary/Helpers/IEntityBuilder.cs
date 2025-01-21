using System.Data;

namespace LiberacionProducto.CommonLibrary.Helpers
{
    public interface IEntityBuilder<T> where T : class
    {
        T BuildEntity(IDataReader reader);
    }
}