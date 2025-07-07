using DataAccess.DAO;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataAccess.CRUD
{
    public class UserCrudFactory : CrudFactory
    {

        public UserCrudFactory()
        {
            _sqlDao = SqlDao.GetInstance();
        }

        public override void Create(BaseDTO baseDTO)
        {
            var user = baseDTO as User;
            var sqlOperation = new SqlOperation() { ProcedureName = "CRE_USER_PR" };
            sqlOperation.AddStringParameter("P_UserCode", user.UserCode);
            sqlOperation.AddStringParameter("P_Name", user.Name);
            sqlOperation.AddStringParameter("P_Email", user.Email);
            sqlOperation.AddStringParameter("P_Password", user.Password);
            sqlOperation.AddStringParameter("P_Status", user.Status);
            sqlOperation.AddDateTimeParam("P_BirthDate", user.BirthDate);

            _sqlDao.ExecuteProcedure(sqlOperation);

        }

        public override void Delete(BaseDTO baseDTO)
        {
            var user = baseDTO as User;
            var sqlOperation = new SqlOperation() {ProcedureName= "DELETE_USER_PR" };
            sqlOperation.AddIntParam("P_Id", user.Id);
            _sqlDao.ExecuteProcedure(sqlOperation);
        }

        public override List<T> RetrieveAll<T>()
        {
            var lstUsers = new List<T>();

            var sqlOperation = new SqlOperation() { ProcedureName = "RET_ALL_USERS_PR" };

            var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation); //devuelve una lista de resultados

            if(lstResults.Count > 0)
            {
                foreach(var row in lstResults)
                {
                    var user = BuildUser(row);
                    lstUsers.Add((T)Convert.ChangeType(user, typeof(T)));
                }
            }
            return lstUsers;
        }

        public override T RetrieveById<T>(BaseDTO baseDTO)
        {
            var user = baseDTO as User;
            var sqlOperation = new SqlOperation() { ProcedureName = "RET_USER_ID" };
            sqlOperation.AddIntParam("UserId", user.Id);

            var result = _sqlDao.ExecuteQueryProcedure(sqlOperation);
            if (result.Count > 0)
            {
                var row = result[0];
                return (T)Convert.ChangeType(BuildUser(row), typeof(T));
            }
            return default(T); //Si no hay resultados, retorna el valor por defecto del tipo T

        }

        public T RetrieveByUserCode<T>(User user)
        {

            var sqlOperation = new SqlOperation() { ProcedureName = "RET_USER_BY_CODE_PR" };
            sqlOperation.AddStringParameter("P_CODE", user.UserCode);
            var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);

            if(lstResults.Count > 0)
            {
                var row= lstResults[0];
                user= BuildUser(row);

                return (T)Convert.ChangeType(user, typeof(T));
            }
            return default(T); //Si no hay resultados, retorna el valor por defecto del tipo T

        }

        public T RetrieveByEmail<T>(User user)
        {

            var sqlOperation = new SqlOperation() { ProcedureName = "RET_USER_BY_EMAIL_PR" };
            sqlOperation.AddStringParameter("P_EMAIL", user.Email);
            var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);

            if (lstResults.Count > 0)
            {
                var row = lstResults[0];
                user = BuildUser(row);

                return (T)Convert.ChangeType(user, typeof(T));
            }
            return default(T); //Si no hay resultados, retorna el valor por defecto del tipo T

        }

        public override T Retrieve<T>()
        {
            throw new NotImplementedException();
        }


        public override void Update(BaseDTO baseDTO)
        {
           var user = baseDTO as User;
            var sqlOperation = new SqlOperation() { ProcedureName = "UPDATE_USER_PR" };
            sqlOperation.AddIntParam("P_Id", user.Id);
            sqlOperation.AddStringParameter("P_UserCode", user.UserCode);
            sqlOperation.AddStringParameter("P_Name", user.Name);
            sqlOperation.AddStringParameter("P_Email", user.Email);
            sqlOperation.AddStringParameter("P_Password", user.Password);
            sqlOperation.AddStringParameter("P_Status", user.Status);
            sqlOperation.AddDateTimeParam("P_BirthDate", user.BirthDate);
            _sqlDao.ExecuteProcedure(sqlOperation);
        }

        //Metodo que convierte el ctcionario de resultados a un objeto User
        private User BuildUser(Dictionary<string, object> row)
        {
            var user = new User()
            {
                Id = (int)row["Id"],
                Created = (DateTime)row["Created"],
               //Updated = (DateTime)row["Updated"],
                UserCode = (string)row["UserCode"],
                Name = (string)row["Name"],
                Email = (string)row["Email"],
                Password = (string)row["Password"],
                Status = (string)row["Status"],
                BirthDate = (DateTime)row["BirthDate"]
            };

            return user;
        }
    }
}
