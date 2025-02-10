
using LiberacionProducto.Entities.Entities.Lotificacion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static LiberacionProducto.Entities.Entities.Lotificacion.ListadoLotificacion;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LiberacionProducto.Data.Repositories.Lotificacion
{
    public class LotificacionRepository
    {
        private readonly string _connectionString;

        public LotificacionRepository(string connectionString)
        {
            _connectionString = connectionString;
        }


        public async Task<CAT_TR_PlantaUsuario> GetPlantasUsuario(string idUsuario)
        {
            CAT_TR_PlantaUsuario trPlantaUsuario = new CAT_TR_PlantaUsuario();
            var _connectionString2 = "Data Source=MLGMTY00DBTST01;Database=LIB_PROD_MED_DB;User Id=syslpm_a; Password=testlpm01;";
            try
            {
                using (var connection = new SqlConnection(_connectionString2))
                {
                    using (var command = new SqlCommand("sp_ConsultarPlantasUsuario", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@idUsuario", idUsuario));

                        await connection.OpenAsync();

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new CAT_TR_PlantaUsuario
                                {
                                    idUsuario = reader.GetString(reader.GetOrdinal("MexeUsuario")),
                                    PlantasUsuario = reader.GetString(reader.GetOrdinal("PlantaUsuario"))
                                };
                            }
                            else
                            {
                                return trPlantaUsuario;
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Manejo de excepciones específicas de SQL
                Console.WriteLine($"SQL Error: {sqlEx.Message}");
                // Aquí podrías registrar el error o tomar alguna acción específica
                return trPlantaUsuario;
            }
            catch (Exception ex)
            { // Manejo de excepciones generales
                Console.WriteLine($"Error: {ex.Message}");
                // Aquí podrías registrar el error o tomar alguna acción específica
            }
            return trPlantaUsuario;
        }

        public async Task<List<CatPlanta>> GetPlantasAsync(string plantasArray)
        {
            var lstPlantas = new List<CatPlanta>();

            var _connectionString2 = "Data Source=MLGMTY00DBTST01;Database=CertAnaliticosBulkDB;User Id=syscer_a; Password=Test394mx;";
            try
            {
                using (var connection = new SqlConnection(_connectionString2))
                {
                    using (var command = new SqlCommand("sp_ConsultarPlantas", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@PlantasStr", plantasArray));
                        await connection.OpenAsync();

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var planta = new CatPlanta
                                {

                                    idPlanta = reader.GetInt32(0),
                                    DescPlanta = reader.GetString(1)
                                };
                                lstPlantas.Add(planta);
                            }
                        }
                    }
                    return lstPlantas;
                }
            }

            catch (SqlException sqlEx)
            {
                // Manejo de excepciones específicas de SQL
                Console.WriteLine($"SQL Error: {sqlEx.Message}");
                // Aquí podrías registrar el error o tomar alguna acción específica
            }
            catch (Exception ex)
            { // Manejo de excepciones generales
                Console.WriteLine($"Error: {ex.Message}");
                // Aquí podrías registrar el error o tomar alguna acción específica
            }
            return lstPlantas;
        }


        public async Task<List<CatProducto>> GetProductosAsync(int idPlanta)
        {
            var lstProductos = new List<CatProducto>();

            var _connectionString2 = "Data Source=MLGMTY00DBTST01;Database=CertAnaliticosBulkDB;User Id=syscer_a; Password=Test394mx;";
            try
            {
                using (var connection = new SqlConnection(_connectionString2))
                {
                    using (var command = new SqlCommand("sp_ConsultarProductos", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@idPlanta", idPlanta));
                        await connection.OpenAsync();

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var producto = new CatProducto
                                {
                                    idProducto = reader.GetInt32(0),
                                    DescProducto = reader.GetString(1)
                                };
                                lstProductos.Add(producto);
                            }
                        }
                    }
                    return lstProductos;
                }
            }

            catch (SqlException sqlEx)
            {
                // Manejo de excepciones específicas de SQL
                Console.WriteLine($"SQL Error: {sqlEx.Message}");
                // Aquí podrías registrar el error o tomar alguna acción específica
            }
            catch (Exception ex)
            { // Manejo de excepciones generales
                Console.WriteLine($"Error: {ex.Message}");
                // Aquí podrías registrar el error o tomar alguna acción específica
            }
            return lstProductos;
        }



        public async Task<List<CAB_EspecificacionProducto>> GetEspecificacionProductsAsync(int IdPlanta, int IdProducto, int IdTipoEspecificacion/*int IdEspecificacionProducto*/)
        {
            var EspecificacionProductos = new List<CAB_EspecificacionProducto>();

            var _connectionString2 = "Data Source=MLGMTY00DBTST01;Database=CertAnaliticosBulkDB;User Id=syscer_a; Password=Test394mx;";

            try
            {
                using (var connection = new SqlConnection(_connectionString2))
                {
                    using (var command = new SqlCommand("sp_ConsultarTDEspecificacion", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@IdPlanta", IdPlanta));
                        command.Parameters.Add(new SqlParameter("@IdProducto", IdProducto));
                        command.Parameters.Add(new SqlParameter("@idTipoEspecificacion", IdTipoEspecificacion));
                        await connection.OpenAsync();

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var product = new CAB_EspecificacionProducto
                                {
                                    IdProducto = reader.GetInt32(0),
                                    IdParametro = reader.GetInt32(1),
                                    DescripcionParametro = reader.GetString(2),
                                    ValorLimiteInf = reader.GetDecimal(3),
                                    ValorLimiteSup = reader.GetDecimal(4),
                                    IdUnidadMedida = reader.GetInt32(5),
                                    DescripcionUnidadMedida = reader.GetString(6),
                                    Observaciones = reader.GetString("OBSERVACIONES")

                                };

                                EspecificacionProductos.Add(product);
                            }
                        }
                    }
                    return EspecificacionProductos;
                }
            }

            catch (SqlException sqlEx)
            {
                // Manejo de excepciones específicas de SQL
                Console.WriteLine($"SQL Error: {sqlEx.Message}");
                // Aquí podrías registrar el error o tomar alguna acción específica
            }
            catch (Exception ex)
            { // Manejo de excepciones generales
                Console.WriteLine($"Error: {ex.Message}");
                // Aquí podrías registrar el error o tomar alguna acción específica
            }

            return EspecificacionProductos;
        }

        public async Task<List<CAB_TC_Analizadores>> GetAnalizadoresporParametroAsync(int idPlanta, int idParametro)
        {
            var lstAnalizadores = new List<CAB_TC_Analizadores>();

            var _connectionString2 = "Data Source=MLGMTY00DBTST01;Database=CertAnaliticosBulkDB;User Id=syscer_a; Password=Test394mx;";
            try
            {
                using (var connection = new SqlConnection(_connectionString2))
                {
                    using (var command = new SqlCommand("sp_ConsultarAnalizadores", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@IdPlanta", idPlanta));
                        command.Parameters.Add(new SqlParameter("@IdParametro", idParametro));
                        await connection.OpenAsync();

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                //for (int i = 0; i < reader.FieldCount; i++)
                                //{
                                //    Console.WriteLine($"{reader.GetName(i)}: {reader.GetFieldType(i)}");
                                //}

                                var analizador = new CAB_TC_Analizadores
                                {
                                    IdAnalizador = reader.GetInt32(0),
                                    DescAnalizador = reader.GetString(1),
                                    IdMetodo = reader.GetInt32(2),
                                    descMetodo = reader.GetString(3)
                                };
                                lstAnalizadores.Add(analizador);
                            }
                        }
                    }
                    return lstAnalizadores;
                }
            }

            catch (SqlException sqlEx)
            {
                // Manejo de excepciones específicas de SQL
                Console.WriteLine($"SQL Error: {sqlEx.Message}");
                // Aquí podrías registrar el error o tomar alguna acción específica
            }
            catch (Exception ex)
            { // Manejo de excepciones generales
                Console.WriteLine($"Error: {ex.Message}");
                // Aquí podrías registrar el error o tomar alguna acción específica
            }
            return lstAnalizadores;
        }

        public async Task<CatPlanta> GetDescripcionPlanta(int idPlanta)
        {
            CatPlanta catplanta = new CatPlanta();
            var _connectionString2 = "Data Source=MLGMTY00DBTST01;Database=CertAnaliticosBulkDB;User Id=syscer_a; Password=Test394mx;";
            try
            {
                using (var connection = new SqlConnection(_connectionString2))
                {
                    using (var command = new SqlCommand("sp_ConsultarDescPlanta", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@IdPlanta", idPlanta));

                        await connection.OpenAsync();

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new CatPlanta
                                {
                                    idPlanta = reader.GetInt32(reader.GetOrdinal("ID_PLANTA")),
                                    DescPlanta = reader.GetString(reader.GetOrdinal("CLAVE_CERTIFICADO"))
                                };
                            }
                            else
                            {
                                return catplanta;
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Manejo de excepciones específicas de SQL
                Console.WriteLine($"SQL Error: {sqlEx.Message}");
                // Aquí podrías registrar el error o tomar alguna acción específica
                return catplanta;
            }
            catch (Exception ex)
            { // Manejo de excepciones generales
                Console.WriteLine($"Error: {ex.Message}");
                // Aquí podrías registrar el error o tomar alguna acción específica
            }
            return catplanta;
        }

        public async Task<CatProducto> GetDescripcionProducto(int idProducto)
        {
            CatProducto catproducto = new CatProducto();
            var _connectionString2 = "Data Source=MLGMTY00DBTST01;Database=CertAnaliticosBulkDB;User Id=syscer_a; Password=Test394mx;";
            try
            {
                using (var connection = new SqlConnection(_connectionString2))
                {
                    using (var command = new SqlCommand("sp_ConsultarDescProducto", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@idProducto", idProducto));

                        await connection.OpenAsync();

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new CatProducto
                                {
                                    idProducto = reader.GetInt32(reader.GetOrdinal("ID_PRODUCTO")),
                                    DescProducto = reader.GetString(reader.GetOrdinal("CLAVE_PALS"))
                                };
                            }
                            else
                            {
                                return catproducto;
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Manejo de excepciones específicas de SQL
                Console.WriteLine($"SQL Error: {sqlEx.Message}");
                // Aquí podrías registrar el error o tomar alguna acción específica
                return catproducto;
            }
            catch (Exception ex)
            { // Manejo de excepciones generales
                Console.WriteLine($"Error: {ex.Message}");
                // Aquí podrías registrar el error o tomar alguna acción específica
            }
            return catproducto;
        }

        public async Task<List<CatTanques>> GetTanquesPlantaProducto(int idPlanta, int idProducto)
        {
            List<CatTanques> lstcattanque = new List<CatTanques>();
            var _connectionString2 = "Data Source=MLGMTY00DBTST01;Database=CertAnaliticosBulkDB;User Id=syscer_a; Password=Test394mx;";
            try
            {
                using (var connection = new SqlConnection(_connectionString2))
                {
                    using (var command = new SqlCommand("sp_ConsultarTanques", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@idPlanta", idPlanta));
                        command.Parameters.Add(new SqlParameter("@idProducto", idProducto));

                        await connection.OpenAsync();

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var tanque = new CatTanques
                                {
                                    idTanque = reader.GetInt32(reader.GetOrdinal("idTanque")),
                                    DescTanque = reader.GetString(reader.GetOrdinal("DescTanque")),
                                    descGrado = reader.GetString(reader.GetOrdinal("descGrado"))
                                };
                                lstcattanque.Add(tanque);
                            }
                        }
                        return lstcattanque;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Manejo de excepciones específicas de SQL
                Console.WriteLine($"SQL Error: {sqlEx.Message}");
                // Aquí podrías registrar el error o tomar alguna acción específica
                return lstcattanque;
            }
            catch (Exception ex)
            { // Manejo de excepciones generales
                Console.WriteLine($"Error: {ex.Message}");
                // Aquí podrías registrar el error o tomar alguna acción específica
            }
            return lstcattanque;
        }

        public string GuardarDatos(LotificacionData data)
        {
            var _connectionString2 = "Data Source=MLGMTY00DBTST01;Database=CertAnaliticosBulkDB;User Id=syscer_a; Password=Test394mx;";
            CultureInfo culture = new CultureInfo("es-ES");

            using (var connection = new SqlConnection(_connectionString2))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Declarar el parámetro de salida para el nuevo IdAnalisis
                        var nuevoIdAnalisisParam = new SqlParameter("@NuevoIdAnalisis", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };

                        // Reemplazar punto por coma si es necesario                            
                        if (data.MasterData.NivelIni.Contains(".") && !data.MasterData.NivelIni.Contains(","))
                        {
                            data.MasterData.NivelIni = data.MasterData.NivelIni.Replace(".", ",");
                        }
                        if (data.MasterData.NivelFin.Contains(".") && !data.MasterData.NivelFin.Contains(","))
                        {
                            data.MasterData.NivelFin = data.MasterData.NivelFin.Replace(".", ",");
                        }

                        // Guardar datos en la tabla maestra y obtener el nuevo IdAnalisis
                        using (var command = new SqlCommand("sp_GuardarLotificacionMaestro", connection, transaction))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add(new SqlParameter("@IdLote", data.MasterData.IdLote));
                            command.Parameters.Add(new SqlParameter("@IdPlanta", data.MasterData.IdPlanta));
                            command.Parameters.Add(new SqlParameter("@IdTanque", Convert.ToInt32(data.MasterData.IdTanque)));
                            command.Parameters.Add(new SqlParameter("@IdProducto", Convert.ToInt32(data.MasterData.IdProducto)));
                            command.Parameters.Add(new SqlParameter("@NivelIni", Convert.ToDecimal(data.MasterData.NivelIni, culture)));
                            command.Parameters.Add(new SqlParameter("@IdUMedidaIni", Convert.ToInt32(data.MasterData.IdUMedidaIni)));
                            command.Parameters.Add(new SqlParameter("@NivelFin", Convert.ToDecimal(data.MasterData.NivelFin, culture)));
                            command.Parameters.Add(new SqlParameter("@IdUMedidaFin", Convert.ToInt32(data.MasterData.IdUMedidaFin)));
                            command.Parameters.Add(new SqlParameter("@Comentarios", data.MasterData.Comentarios));
                            command.Parameters.Add(new SqlParameter("@LoteOrigen", data.MasterData.LoteOrigen));
                            command.Parameters.Add(new SqlParameter("@UsrAlta", Convert.ToInt32(data.MasterData.UsrAlta)));
                            command.Parameters.Add(new SqlParameter("@EstatusAnalisis", Convert.ToInt32(data.MasterData.Estatus_Analisis)));
                            command.Parameters.Add(new SqlParameter("@EstatusRevision", Convert.ToInt32(data.MasterData.Estatus_Revision)));
                            command.Parameters.Add(nuevoIdAnalisisParam);

                            command.ExecuteNonQuery();
                        }

                        // Obtener el nuevo IdAnalisis generado
                        int nuevoIdAnalisis = (int)nuevoIdAnalisisParam.Value;

                        // Guardar datos en la tabla detalle utilizando el nuevo IdAnalisis
                        foreach (var item in data.DetailData)
                        {
                            // Reemplazar punto por coma si es necesario                            
                            if (item.ValorLimiteInf.Contains(".") && !item.ValorLimiteInf.Contains(","))
                            {
                                item.ValorLimiteInf = item.ValorLimiteInf.Replace(".", ",");
                            }
                            if (item.ValorLimiteSup.Contains(".") && !item.ValorLimiteSup.Contains(","))
                            {
                                item.ValorLimiteSup = item.ValorLimiteSup.Replace(".", ",");
                            }
                            if (item.ValorAnalisis.Contains(".") && !item.ValorAnalisis.Contains(","))
                            {
                                item.ValorAnalisis = item.ValorAnalisis.Replace(".", ",");
                            }
                            //try
                            //{
                            //    //decimal numero = Convert.ToDecimal(item.ValorAnalisis);
                            //}
                            //catch (FormatException)
                            //{
                            //    Console.WriteLine("Formato inválido");
                            //}

                            using (var command = new SqlCommand("sp_GuardarLotificacionDetalle", connection, transaction))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.Add(new SqlParameter("@IdAnalisis", nuevoIdAnalisis));
                                command.Parameters.Add(new SqlParameter("@IdParametro", Convert.ToInt32(item.IdParametro)));
                                command.Parameters.Add(new SqlParameter("@ValorLimiteInf", Convert.ToDecimal(item.ValorLimiteInf, culture)));
                                command.Parameters.Add(new SqlParameter("@ValorLimiteSup", Convert.ToDecimal(item.ValorLimiteSup, culture)));
                                command.Parameters.Add(new SqlParameter("@ValorAnalisis", Convert.ToDecimal(item.ValorAnalisis, culture)));
                                command.Parameters.Add(new SqlParameter("@IdAnalizador", Convert.ToInt32(item.IdAnalizador)));
                                command.Parameters.Add(new SqlParameter("@IdMetodo", Convert.ToInt32(item.IdMetodo)));
                                command.Parameters.Add(new SqlParameter("@UsrAlta", Convert.ToInt32(item.UsrAlta)));
                                command.Parameters.Add(new SqlParameter("@EstatusAnalisis", Convert.ToInt32(data.MasterData.Estatus_Analisis)));
                                command.Parameters.Add(new SqlParameter("@IdUnidadMedida", Convert.ToInt32(item.IdUnidadMedida)));

                                foreach (SqlParameter param in command.Parameters)
                                {
                                    if (param.Value == null)
                                    {
                                        throw new ArgumentNullException(param.ParameterName, "El valor del parámetro no puede ser nulo.");
                                    }
                                    Console.WriteLine($"Parámetro: {param.ParameterName}, Valor: {param.Value}");
                                }

                                command.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();

                        return data.MasterData.IdLote;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return ("Error al guardar los datos: " + ex.Message);
                    }
                }
            }
        }


        public string EditarDatosLote(LotificacionData data)
        {
            var _connectionString2 = "Data Source=MLGMTY00DBTST01;Database=CertAnaliticosBulkDB;User Id=syscer_a; Password=Test394mx;";
            CultureInfo culture = new CultureInfo("es-ES");

            using (var connection = new SqlConnection(_connectionString2))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Declarar el parámetro de salida para validar que se realizo correctamente commit
                        var CommitIdAnalisis = new SqlParameter("@Resultado", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };

                        // Reemplazar punto por coma si es necesario                            
                        if (data.MasterData.NivelIni.Contains(".") && !data.MasterData.NivelIni.Contains(","))
                        {
                            data.MasterData.NivelIni = data.MasterData.NivelIni.Replace(".", ",");
                        }
                        if (data.MasterData.NivelFin.Contains(".") && !data.MasterData.NivelFin.Contains(","))
                        {
                            data.MasterData.NivelFin = data.MasterData.NivelFin.Replace(".", ",");
                        }

                        // Guardar datos en la tabla maestra y obtener el nuevo IdAnalisis
                        using (var command = new SqlCommand("sp_EditarLotificacionMaestro", connection, transaction))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add(new SqlParameter("@IdAnalisis", data.MasterData.IdAnalisis));
                            command.Parameters.Add(new SqlParameter("@IdPlanta", data.MasterData.IdPlanta));
                            command.Parameters.Add(new SqlParameter("@IdProducto", Convert.ToInt32(data.MasterData.IdProducto)));
                            command.Parameters.Add(new SqlParameter("@NivelIni", Convert.ToDecimal(data.MasterData.NivelIni, culture)));
                            command.Parameters.Add(new SqlParameter("@IdUMedidaIni", Convert.ToInt32(data.MasterData.IdUMedidaIni)));
                            command.Parameters.Add(new SqlParameter("@NivelFin", Convert.ToDecimal(data.MasterData.NivelFin, culture)));
                            command.Parameters.Add(new SqlParameter("@IdUMedidaFin", Convert.ToInt32(data.MasterData.IdUMedidaFin)));
                            command.Parameters.Add(new SqlParameter("@EstatusAnalisis", Convert.ToInt32(data.MasterData.Estatus_Analisis)));
                            command.Parameters.Add(new SqlParameter("@EstatusRevision", Convert.ToInt32(data.MasterData.Estatus_Revision)));

                            command.Parameters.Add(CommitIdAnalisis);

                            command.ExecuteNonQuery();
                        }

                        // Obtener el el resultado de commit del IdAnalisis Maestra
                        //int nuevoIdAnalisis = (int)nuevoIdAnalisisParam.Value;

                        // Guardar datos en la tabla detalle utilizando el nuevo IdAnalisis
                        foreach (var item in data.DetailData)
                        {
                            // Declarar el parámetro de salida para validar que se realizo correctamente commit
                            var CommitIdAnalisisDetalle = new SqlParameter("@Resultado", SqlDbType.Int)
                            {
                                Direction = ParameterDirection.Output
                            };

                            // Reemplazar punto por coma si es necesario                            
                            if (item.ValorAnalisis.Contains(".") && !item.ValorAnalisis.Contains(","))
                            {
                                item.ValorAnalisis = item.ValorAnalisis.Replace(".", ",");
                            }

                            using (var command = new SqlCommand("sp_EditarLotificacionDetalle", connection, transaction))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.Add(new SqlParameter("@IdAnalisis", Convert.ToInt32(item.IdAnalisis)));
                                command.Parameters.Add(new SqlParameter("@IdParametro", Convert.ToInt32(item.IdParametro)));
                                command.Parameters.Add(new SqlParameter("@ValorAnalisis", Convert.ToDecimal(item.ValorAnalisis, culture)));
                                command.Parameters.Add(new SqlParameter("@IdAnalizador", Convert.ToInt32(item.IdAnalizador)));
                                command.Parameters.Add(new SqlParameter("@IdMetodo", Convert.ToInt32(item.IdMetodo)));
                                command.Parameters.Add(new SqlParameter("@UsrEdit", Convert.ToInt32(item.UsrAlta)));
                                command.Parameters.Add(new SqlParameter("@EstatusAnalisis", Convert.ToInt32(data.MasterData.Estatus_Analisis)));
                                command.Parameters.Add(new SqlParameter("@IdUnidadMedida", Convert.ToInt32(item.IdUnidadMedida)));

                                command.Parameters.Add(CommitIdAnalisisDetalle);

                                //foreach (SqlParameter param in command.Parameters)
                                //{
                                //    if (param.Value == null)
                                //    {
                                //        throw new ArgumentNullException(param.ParameterName, "El valor del parámetro no puede ser nulo.");
                                //    }
                                //    Console.WriteLine($"Parámetro: {param.ParameterName}, Valor: {param.Value}");
                                //}

                                command.ExecuteNonQuery();

                                // Obtener el el resultado de commit del IdAnalisis detalle
                                //int nuevoIdAnalisis = (int)nuevoIdAnalisisParam.Value;
                            }
                        }

                        transaction.Commit();

                        return data.MasterData.IdLote;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return ("Error al guardar los datos: " + ex.Message);
                    }
                }
            }
        }


        public async Task<List<AnalisisTanque>> ObtenerAnalisisTanque(ListadoLotificacionData dataBusqueda)
        {
            var resultados = new List<AnalisisTanque>();
            var analisisDict = new Dictionary<int, AnalisisTanque>();
            var _connectionString2 = "Data Source=MLGMTY00DBTST01;Database=CertAnaliticosBulkDB;User Id=syscer_a; Password=Test394mx;";

            try
            {
                using (var connection = new SqlConnection(_connectionString2))
                {
                    using (var command = new SqlCommand("sp_ObtenerListadoLotes", connection))
                    {
                        DateTime fechaInicialFormatted = DateTime.ParseExact(dataBusqueda.FechaInicial.ToString("yyyy-MM-dd"), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                        DateTime fechaFinalFormatted = DateTime.ParseExact(dataBusqueda.FechaFinal.ToString("yyyy-MM-dd"), "yyyy-MM-dd", CultureInfo.InvariantCulture);

                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@idPlanta", dataBusqueda.IdPlanta));
                        command.Parameters.Add(new SqlParameter("@idProducto", dataBusqueda.IdProducto));
                        command.Parameters.Add(new SqlParameter("@FechaInicial", fechaInicialFormatted));
                        command.Parameters.Add(new SqlParameter("@FechaFinal", fechaFinalFormatted));

                        await connection.OpenAsync();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                int idAnalisis = reader.GetInt32(0);

                                if (!analisisDict.ContainsKey(idAnalisis))
                                {
                                    var analisis = new AnalisisTanque
                                    {
                                        IdAnalisis = idAnalisis,
                                        IdLote = reader.GetString("id_lote"),
                                        LoteOrigen = reader.IsDBNull(reader.GetOrdinal("Lote_Origen")) ? null : reader.GetString(reader.GetOrdinal("Lote_Origen")),
                                        IdTanque = reader.IsDBNull(reader.GetOrdinal("descTanque")) ? null : reader.GetString(reader.GetOrdinal("descTanque")),

                                        //IdTanque = reader.GetString("descTanque"),
                                        //IdTanque = reader.GetInt32(3), 
                                        NivelIni = reader.GetString("nivel_iniDesc"),

                                        NivelIniVal = reader.IsDBNull(reader.GetOrdinal("NivelInicialVal")) ? null : reader.GetDecimal(reader.GetOrdinal("NivelInicialVal")),
                                        UMInicial = reader.IsDBNull(reader.GetOrdinal("UnidadMedidaIni")) ? null : reader.GetInt32(reader.GetOrdinal("UnidadMedidaIni")),

                                        NivelFin = reader.GetString("nivel_finDesc"),

                                        NivelFinVal = reader.IsDBNull(reader.GetOrdinal("NivelFinalVal")) ? null : reader.GetDecimal(reader.GetOrdinal("NivelFinalVal")),
                                        UMFinal = reader.IsDBNull(reader.GetOrdinal("UnidadMedidaFin")) ? null : reader.GetInt32(reader.GetOrdinal("UnidadMedidaFin")),

                                        Comentarios = reader.IsDBNull(reader.GetOrdinal("comentarios")) ? null : reader.GetString(reader.GetOrdinal("comentarios")),
                                        FechaAlta = reader.IsDBNull(reader.GetOrdinal("FEC_ALTA")) ? null : reader.GetDateTime(reader.GetOrdinal("FEC_ALTA")),
                                        Estatus = reader.IsDBNull(reader.GetOrdinal("DESCRIPCION_ESTATUS")) ? null : reader.GetString(reader.GetOrdinal("DESCRIPCION_ESTATUS")),
                                        Estatus_RevisionVal = reader.IsDBNull(reader.GetOrdinal("ESTATUS_REVISION")) ? null : reader.GetInt32(reader.GetOrdinal("ESTATUS_REVISION")),
                                        Estatus_Revision = reader.IsDBNull(reader.GetOrdinal("descEstatusRevision")) ? null : reader.GetString(reader.GetOrdinal("descEstatusRevision")),

                                        UsrAlta = reader.IsDBNull(reader.GetOrdinal("nomUsuarioAlta")) ? null : reader.GetString(reader.GetOrdinal("nomUsuarioAlta")),
                                        GetUseralta = reader.GetInt32("Usr_Alta"),

                                        motivo_bitacLotif_RevisionCumple = reader.IsDBNull(reader.GetOrdinal("motivo_bitacLotif_RevisionCumple")) ? null : reader.GetString(reader.GetOrdinal("motivo_bitacLotif_RevisionCumple")),
                                        fec_bitaclotif_RevisionCumple = reader.IsDBNull(reader.GetOrdinal("fec_bitaclotif_RevisionCumple")) ? null : reader.GetDateTime(reader.GetOrdinal("fec_bitaclotif_RevisionCumple")),
                                        usr_bitaclotif_RevisionCumple = reader.IsDBNull(reader.GetOrdinal("usr_bitaclotif_RevisionCumple")) ? null : reader.GetInt32("usr_bitaclotif_RevisionCumple"),
                                        descusr_bitaclotif_RevisionCumple = reader.IsDBNull(reader.GetOrdinal("descUsr_bitaclotifCumple")) ? null : reader.GetString(reader.GetOrdinal("descUsr_bitaclotifCumple")),


                                        motivo_bitacLotif_RevisionNoCumple = reader.IsDBNull(reader.GetOrdinal("motivo_bitacLotif_RevisionNoCumple")) ? null : reader.GetString(reader.GetOrdinal("motivo_bitacLotif_RevisionNoCumple")),
                                        fec_bitaclotif_RevisionNoCumple = reader.IsDBNull(reader.GetOrdinal("fec_bitaclotif_RevisionNoCumple")) ? null : reader.GetDateTime(reader.GetOrdinal("fec_bitaclotif_RevisionNoCumple")),
                                        usr_bitaclotif_RevisionNoCumple = reader.IsDBNull(reader.GetOrdinal("usr_bitaclotif_RevisionNoCumple")) ? null : reader.GetInt32("usr_bitaclotif_RevisionNoCumple"),
                                        descusr_bitaclotif_RevisionNoCumple = reader.IsDBNull(reader.GetOrdinal("descUsr_bitaclotifNoCumple")) ? null : reader.GetString(reader.GetOrdinal("descUsr_bitaclotifNoCumple")),





                                        //UsrAlta = reader.GetInt32(9), 
                                        //IdProducto = reader.GetInt32(10), 
                                        Detalles = new List<DetalleAnalisisTanque>()
                                    };
                                     
                                    analisisDict[idAnalisis] = analisis; 
                                }

                                // Agregar el detalle solo si id_parametro no es nulo
                                if (!reader.IsDBNull(15))
                                {
                                    var detalle = new DetalleAnalisisTanque
                                    {
                                        IdPlanta = reader.IsDBNull(reader.GetOrdinal("ID_PLANTA")) ? null : reader.GetInt32(reader.GetOrdinal("ID_PLANTA")),
                                        IdParametro = reader.IsDBNull(reader.GetOrdinal("ID_PARAMETRO")) ? null : reader.GetInt32(reader.GetOrdinal("ID_PARAMETRO")),
                                        descParametro = reader.IsDBNull(reader.GetOrdinal("descParametro")) ? null : reader.GetString(reader.GetOrdinal("descParametro")),
                                        IdAnalisisDetail = idAnalisis,
                                        ValorLimiteInf = reader.GetDecimal("valor_limite_inf"),
                                        ValorLimiteSup = reader.GetDecimal("valor_limite_sup"),
                                        ValorAnalisis = reader.GetDecimal("valor_analisis"),
                                        IdAnalizador = reader.IsDBNull(reader.GetOrdinal("descAnalizador")) ? null : reader.GetString(reader.GetOrdinal("descAnalizador")),
                                        IdMetodo = reader.IsDBNull(reader.GetOrdinal("descMetodo")) ? null : reader.GetString(reader.GetOrdinal("descMetodo")),
                                        IdMetodoVal = reader.IsDBNull(reader.GetOrdinal("ID_METODO")) ? null : reader.GetInt32(reader.GetOrdinal("ID_METODO")),
                                        DescUnidadMedida = reader.IsDBNull(reader.GetOrdinal("descUMedidaDetalle")) ? null : reader.GetString(reader.GetOrdinal("descUMedidaDetalle")),
                                        MotivoCancelBitacora = reader.IsDBNull(reader.GetOrdinal("motivo_bitacLotif")) ? null : reader.GetString(reader.GetOrdinal("motivo_bitacLotif")),
                                        FechaCancelBitacora = reader.IsDBNull(reader.GetOrdinal("Fec_bitaclotif")) ? null : reader.GetDateTime(reader.GetOrdinal("Fec_bitaclotif")),
                                        UserCancelBitacora = reader.IsDBNull(reader.GetOrdinal("usr_bitaclotif")) ? null : reader.GetInt32(reader.GetOrdinal("usr_bitaclotif")),
                                        UserNameCancelBitacora = reader.IsDBNull(reader.GetOrdinal("descUsr_bitaclotif")) ? null : reader.GetString(reader.GetOrdinal("descUsr_bitaclotif")),
                                        IdAnalizadorVal = reader.IsDBNull(reader.GetOrdinal("ID_ANALIZADOR")) ? null : reader.GetInt32(reader.GetOrdinal("ID_ANALIZADOR"))
                                    };

                                    // Verificar si el detalle ya existe en la lista antes de agregarlo
                                    if (!analisisDict[idAnalisis].Detalles.Any(d =>
                                        d.IdParametro == detalle.IdParametro && 
                                        d.IdAnalisisDetail == detalle.IdAnalisisDetail))
                                    {
                                        analisisDict[idAnalisis].Detalles.Add(detalle);
                                    }

                                                                   
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Manejo de excepciones específicas de SQL
                Console.WriteLine($"SQL Error: {sqlEx.Message}");
                // Aquí podrías registrar el error o tomar alguna acción específica
                return resultados;
            }
            catch (Exception ex)
            {
                // Manejo de excepciones generales
                Console.WriteLine($"Error: {ex.Message}");
                // Aquí podrías registrar el error o tomar alguna acción específica
            }

            // Convertir el diccionario a una lista
            resultados = analisisDict.Values.ToList();

            return resultados;
        }


        public string CancelarLote(CancelarLoteData data)
        {
            var _connectionString2 = "Data Source=MLGMTY00DBTST01;Database=CertAnaliticosBulkDB;User Id=syscer_a; Password=Test394mx;";
            CultureInfo culture = new CultureInfo("es-ES");

            using (var connection = new SqlConnection(_connectionString2))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Declarar el parámetro de salida para el Resultado
                        var ResultadoParam = new SqlParameter("@Resultado", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };

                        using (var command = new SqlCommand("sp_CancelaLotificacion", connection, transaction))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add(new SqlParameter("@IdAnalisis", data.IdAnalisis));
                            command.Parameters.Add(new SqlParameter("@MotivoCancelacion", data.MotivoCancelacion));
                            command.Parameters.Add(new SqlParameter("@UserCancel", data.UserCancel));
                            command.Parameters.Add(ResultadoParam);

                            command.ExecuteNonQuery();
                        }

                        int ResultadoCancelacionLote = (int)ResultadoParam.Value;

                        transaction.Commit();

                        return ResultadoCancelacionLote.ToString();
                    }

                    catch (SqlException sqlEx)
                    {
                        // Manejo de excepciones específicas de SQL
                        //Console.WriteLine($"SQL Error: {sqlEx.Message}");
                        // Aquí podrías registrar el error o tomar alguna acción específica
                        transaction.Rollback();
                        return sqlEx.Message;
                    }
                    catch (Exception ex)
                    {
                        // Manejo de excepciones generales
                        //Console.WriteLine($"Error: {ex.Message}");
                        // Aquí podrías registrar el error o tomar alguna acción específica
                        transaction.Rollback();
                        return ex.Message;
                    }
                }
            }
        }

        public async Task<List<PermisosUsuarioData>> GetPermisosUsuario(int idUsuario)
        {
            List<PermisosUsuarioData> lstPermisosUsuario = new List<PermisosUsuarioData>();
            var _connectionString2 = "Data Source=MLGMTY00DBTST01;Database=CertAnaliticosBulkDB;User Id=syscer_a; Password=Test394mx;";
            try
            {
                using (var connection = new SqlConnection(_connectionString2))
                {
                    using (var command = new SqlCommand("sp_ObtienePermisosUsuario", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@idUsuario", idUsuario));                       

                        await connection.OpenAsync();

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var permisosusuario = new PermisosUsuarioData
                                {
                                    IdUsuario = reader.GetInt32(reader.GetOrdinal("idUsuario")),
                                    nombrePermiso = reader.GetString(reader.GetOrdinal("nombrePermiso")),                                    
                                    tienePermiso = Convert.ToBoolean(reader.GetByte(reader.GetOrdinal("tienePermiso")))
                                };
                                lstPermisosUsuario.Add(permisosusuario);
                            }
                        }
                        return lstPermisosUsuario;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Manejo de excepciones específicas de SQL
                Console.WriteLine($"SQL Error: {sqlEx.Message}");
                // Aquí podrías registrar el error o tomar alguna acción específica
                return lstPermisosUsuario;
            }
            catch (Exception ex)
            { // Manejo de excepciones generales
                Console.WriteLine($"Error: {ex.Message}");
                // Aquí podrías registrar el error o tomar alguna acción específica
            }
            return lstPermisosUsuario;
        }

        public string EditarLoteEstatusRevision(EditarEstatusRevisionData data)
        {
            var _connectionString2 = "Data Source=MLGMTY00DBTST01;Database=CertAnaliticosBulkDB;User Id=syscer_a; Password=Test394mx;";
            CultureInfo culture = new CultureInfo("es-ES");

            using (var connection = new SqlConnection(_connectionString2))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Declarar el parámetro de salida para el Resultado
                        var ResultadoParam = new SqlParameter("@Resultado", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };


                        // Guardar datos en la tabla maestra y obtener el nuevo IdAnalisis
                        using (var command = new SqlCommand("sp_EditarLoteEstatusRevision", connection, transaction))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add(new SqlParameter("@IdAnalisis", data.IdAnalisis));                            
                            command.Parameters.Add(new SqlParameter("@EstatusRevision", Convert.ToInt32(data.Estatus_Revision)));
                            command.Parameters.Add(new SqlParameter("@Comentarios", data.Comentarios));
                            command.Parameters.Add(new SqlParameter("@UserBitacora", data.UsrBitac));
                            command.Parameters.Add(ResultadoParam);

                            command.ExecuteNonQuery();
                        }

                        int ResultadoEstatusRevisionLote = (int)ResultadoParam.Value;

                        transaction.Commit();

                        return ResultadoEstatusRevisionLote.ToString();
                    }
                    catch (SqlException sqlEx)
                    {
                        // Manejo de excepciones específicas de SQL
                        //Console.WriteLine($"SQL Error: {sqlEx.Message}");
                        // Aquí podrías registrar el error o tomar alguna acción específica
                        transaction.Rollback();
                        return sqlEx.Message;
                    }
                    catch (Exception ex)
                    {
                        // Manejo de excepciones generales
                        //Console.WriteLine($"Error: {ex.Message}");
                        // Aquí podrías registrar el error o tomar alguna acción específica
                        transaction.Rollback();
                        return ex.Message;
                    }
                }
            }
        }

    }
}
