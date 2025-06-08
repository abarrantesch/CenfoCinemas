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
            _connectionString = string.Empty;
        }

        public static SqlDao getInstance()
        {
            if (_instance == null)
            {
                _instance = new SqlDao();
            }
            return _instance;
        }

        //Funcion de ejecucion de SP sin returno de datos
        public void ExecuteProcedure(SqlOperation operation) 
        {
            //conectar a la DB
            //Ejecutar el SP
        }

        //Funcion de ejecucion de SP con retorno de datos
        public List<Dictionary<string, object>> ExecuteQueryProcedure(SqlOperation operation) 
        {
            //conectar a la DB
            //Ejecutar el SP y retornar los datos
            //convertuirlo en DTOs
            
            var List= new List<Dictionary<string, object>>();

            return List;
        }


    }
}
