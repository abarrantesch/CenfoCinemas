using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{

    /// Clase u objeto que se encarga de la comunicacion de la base de datos. Solo ejecuta Store Procedures. Patron Singleton opara asegugar una unica instancia de la clase SqlDao por aplicacion.
    public class SqlDao
    {

        // 1. Crea una instancia privada esttica de la clase SqlDao.
        private static SqlDao _instance;

        private string _connectionString;

        //2. Redefine el constructor por defecto y convertir en privado

        private SqlDao()
        {
            _connectionString = @"Data Source=srv-sqldatabase-abarrantesc.database.windows.net;Initial Catalog=cenfocinemas-db;Persist Security Info=True;User ID=sysman;Password=Cenfotec123!;Trust Server Certificate=True";
        }

        public static SqlDao GetInstance()
        {
            if (_instance == null)
            {
                _instance = new SqlDao();
            }
            return _instance;
        }

        //Funcion de ejecucion de SP sin returno de datos
        public void ExecuteProcedure(SqlOperation sqlOperation) 
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(sqlOperation.ProcedureName, conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    //Set de los parametros
                    foreach (var param in sqlOperation.Parameters)
                    {
                        command.Parameters.Add(param);
                    }
                    //Ejectura el SP
                    conn.Open();
                    //command.ExecuteNonQuery();
                }

            }
        }

        //Funcion de ejecucion de SP con retorno de datos
        public List<Dictionary<string, object>> ExecuteQueryProcedure(SqlOperation sqlOperation) 
        {
            
            var lstResults= new List<Dictionary<string, object>>();

            using (var conn = new SqlConnection(_connectionString))

            {
                using (var command = new SqlCommand(sqlOperation.ProcedureName, conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    //Set de los parametros
                    foreach (var param in sqlOperation.Parameters)
                    {
                        command.Parameters.Add(param);
                    }
                    //Ejectura el SP
                    conn.Open();

                    //de aca en adelante la implementacion es distinta con respecto al procedure anterior
                    // sentencia que ejectua el SP y captura el resultado
                    var reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            var rowDict = new Dictionary<string, object>();

                            for (var index = 0; index < reader.FieldCount; index++)
                            {
                                var key = reader.GetName(index);
                                var value = reader.GetValue(index);
                                //aca agregamos los valores al diccionario de esta fila
                                rowDict[key] = value;
                            }
                            lstResults.Add(rowDict);
                        }
                    }

                }
            }

            return lstResults;
        }


    }
}
