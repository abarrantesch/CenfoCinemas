using DataAccess.CRUD;
using DataAccess.DAO;
using DTOs;
using Newtonsoft.Json;

public class Program
{
    public static void Main(string[] args) {


        Console.WriteLine("Seleccione la opcion deseada:");
        Console.WriteLine("1. Crear Usuario");
        Console.WriteLine("2. Consultar Usuarios");
        Console.WriteLine("3. Actualizar Usuarios");
        Console.WriteLine("4. Eliminar Usuarios");
        Console.WriteLine("5. Crear Pelicula");
        Console.WriteLine("6. Consultar Peliculas");
        Console.WriteLine("7. Actualizar Peliculas");
        Console.WriteLine("8. Peliculas");

        var option=int.Parse(Console.ReadLine());
        var sqlOperation = new SqlOperation();

        //switch de menu
        switch (option)
        {
            case 1: //USUARIO
                Console.WriteLine("Digite el Codigo de Usuario");
                var userCode=Console.ReadLine();

                Console.WriteLine("Digite el Nombre de Usuario");
                var name = Console.ReadLine();

                Console.WriteLine("Digite el Email de Usuario");
                var email = Console.ReadLine();

                Console.WriteLine("Digite el password de Usuario");
                var password = Console.ReadLine();

                var status = "AC";

                Console.WriteLine("Digite la fecha de nacimiento:");
                var bdate = DateTime.Parse(Console.ReadLine());

                //CREAR OBJETO USUARIO A PARTIR DE LOS VALORES CAPTURADOS EN CONSOLA
                var user = new User()
                {
                    UserCode = userCode,
                    Name = name,
                    Email = email,
                    Password = password,
                    Status = status,
                    BirthDate = bdate,
                };

                var uCrud = new UserCrudFactory();
                uCrud.Create(user);




                break;
            case 2: //CONSULTAR USUARIOS
                var uCrud2=new UserCrudFactory();
                var listUsers = uCrud2.RetrieveAll<User>();
                foreach(var u in listUsers)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(u));
                }
                break;

            case 5:
                Console.WriteLine("");
                Console.WriteLine("Digite el nombre de la pelicula");
                var title = Console.ReadLine();

                Console.WriteLine("Digite la descripcion de la pelicula");
                var description = Console.ReadLine();

                Console.WriteLine("Digite la fecha de lanzamiento");
                var releasedate = DateTime.Parse(Console.ReadLine());

                Console.WriteLine("Digite el genero de la pelicula");
                var genre = Console.ReadLine();

                Console.WriteLine("Digite el nombre del director:");
                var director = Console.ReadLine();



                sqlOperation.AddStringParameter("P_Title", title);
                sqlOperation.AddStringParameter("P_Description", description);
                sqlOperation.AddDateTimeParam("P_ReleaseDate", releasedate);
                sqlOperation.AddStringParameter("P_Genre", genre);
                sqlOperation.AddStringParameter("P_Director", director);


                sqlOperation.ProcedureName = "CRE_MOVIE_PR";

                Console.WriteLine("");
                break;
        }




        var sqlDao= SqlDao.GetInstance();
        sqlDao.ExecuteProcedure(sqlOperation);

    }
}