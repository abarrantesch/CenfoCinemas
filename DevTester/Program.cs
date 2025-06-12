using DataAccess.DAO;

public class Program
{
    public static void Main(string[] args)
    {
        var sqlOperation = new SqlOperation();
        sqlOperation.ProcedureName = "CRE_USER_PR";

        sqlOperation.AddStringParameter("P_UserCode", "abarrantes");
        sqlOperation.AddStringParameter("P_Name", "Angel Barrantes");
        sqlOperation.AddStringParameter("P_Email", "abarrantesc@ucenfotec.ac.cr");
        sqlOperation.AddStringParameter("P_Password", "Angel");
        sqlOperation.AddStringParameter("P_Status", "AC");
        sqlOperation.AddDateTimeParam("P_BirthDate", DateTime.Now);

        var sqlDao= SqlDao.getInstance();
        sqlDao.ExecuteProcedure(sqlOperation);

    }
}