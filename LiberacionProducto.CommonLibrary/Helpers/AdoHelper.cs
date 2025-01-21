using System.Data;
using System.Data.SqlClient;
using LiberacionProducto.CommonLibrary.SQL;

namespace LiberacionProducto.CommonLibrary.Helpers
{
    public static class AdoHelper
    {
        public static async Task<List<T>> FindList<T>(SQLProcedure<T> procedure, SqlConnection connection) where T : class
        {
            return await buildEntityListFromQuery(procedure, connection);
        }

        public static async Task<T> FindItem<T>(SQLProcedure<T> procedure, SqlConnection connection) where T : class
        {

            return await buildEntityItemFromQuery(procedure, connection);
        }

        public async static Task<int> ExecuteNonQueryAsync(string procedure, List<SqlParameter> parameters, SqlConnection connection)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlCommand command = new SqlCommand(procedure, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(parameters.ToArray());
                command.CommandTimeout = 120;

                return await command.ExecuteNonQueryAsync();
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        private static async Task<IDataReader> executeReaderAsync<T>(SQLProcedure<T> procedure, SqlConnection connection) where T : class
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            SqlCommand command = new SqlCommand(procedure.StoredProcedureIdentifier, connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddRange(procedure.Parameters.ToArray());
            command.CommandTimeout = 120;
            return await command.ExecuteReaderAsync(CommandBehavior.CloseConnection);
        }

        private static async Task<T> buildEntityItemFromQuery<T>(SQLProcedure<T> procedure, SqlConnection connection) where T : class
        {
            IDataReader reader = null;
            try
            {
                T entity = default(T);
                reader = await executeReaderAsync(procedure, connection);
                if (reader.Read())
                {
                    entity = buildEntityFromReader(procedure.Builder, reader);
                }
                return entity;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                    reader.Dispose();
                }

                if (connection != null)
                {
                    connection.Dispose();
                }
            }

        }
        
        private static async Task<List<T>> buildEntityListFromQuery<T>(SQLProcedure<T> procedure, SqlConnection connection) where T : class
        {
            IDataReader reader = null;
            try
            {
                List<T> entities = new List<T>();
                reader = await executeReaderAsync(procedure, connection);

                while (reader.Read())
                {
                    entities.Add(buildEntityFromReader(procedure.Builder, reader));
                }

                return entities;
            }
            catch (Exception ex)
            {
                var exception = ex;
                return new List<T>();
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                    reader.Dispose();
                }

                if (connection != null)
                {
                    connection.Dispose();
                }
            }

        }

        private static T buildEntityFromReader<T>(IEntityBuilder<T> builder, IDataReader reader) where T : class
        {
            return builder.BuildEntity(reader);
        }
    }
}
