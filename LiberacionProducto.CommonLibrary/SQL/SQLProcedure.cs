using System.Data.SqlClient;
using LiberacionProducto.CommonLibrary.Helpers;

namespace LiberacionProducto.CommonLibrary.SQL
{
    public class SQLProcedure<T> where T : class
    {
        public string StoredProcedureIdentifier { get; set; }

        private List<SqlParameter> parameters;

        public List<SqlParameter> Parameters => parameters;

        private IEntityBuilder<T> builder;

        public IEntityBuilder<T> Builder => builder;

        public void AddParameter(string name, object value)
        {
            parameters = parameters ?? new List<SqlParameter>();
            parameters.Add(new SqlParameter(name, value));
        }

        public void AddBuilder(IEntityBuilder<T> builderIn)
        {
            builder = builderIn;
        }
    }
}