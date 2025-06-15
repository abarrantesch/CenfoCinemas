using DataAccess.CRUD;
using DataAccess.DAO;
using DTOs;
using Microsoft.Web.Services3.Security.Utility;
using Newtonsoft.Json;
using System.Data.Common;

public class Program
{
    public static void Main(string[] args) {


        Console.WriteLine("Seleccione la opcion deseada:");
        Console.WriteLine("1. Crear Usuario");
        Console.WriteLine("2. Consultar Usuarios");
        Console.WriteLine("3. Consultar Usuario por ID");
        Console.WriteLine("4. Actualizar Usuarios");
        Console.WriteLine("5. Eliminar Usuarios");
        Console.WriteLine("6. Crear Pelicula");
        Console.WriteLine("7. Consultar Peliculas");
        Console.WriteLine("8. Consultar Pelicula por ID");
        Console.WriteLine("9. Actualizar Peliculas");
        Console.WriteLine("10. Peliculas");

        var option=int.Parse(Console.ReadLine());
        var sqlOperation = new SqlOperation();

        //switch de menu
        switch (option)
        {
            case 1: // CREAR USUARIO
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
            case 3: //Consultar Usuario por ID
                Console.WriteLine("Falta por implementar la actualizacion de usuarios");
                break;
            case 4: //Actualizar Usuarios
                Console.WriteLine("Falta por implementar la eliminacion de usuarios");
                break;
            case 5: //Eliminar Usuarios
                Console.WriteLine("Falta por implementar la eliminacion de usuarios");
                break;
            case 6: //Crear Pelicula
                Console.WriteLine("");
                Console.WriteLine("Digite el nombre de la pelicula");
                var title = Console.ReadLine();

                Console.WriteLine("Digite la descripcion de la pelicula");
                var description = Console.ReadLine();

                Console.WriteLine("Digite la fecha de lanzamiento");
                var releasedate = DateTime.Parse(Console.ReadLine());
                //CREAR VALIDACION PARA DATETIME?

                Console.WriteLine("Digite el genero de la pelicula");
                var genre = Console.ReadLine();

                Console.WriteLine("Digite el nombre del director:");
                var director = Console.ReadLine();
                break;
            case 7: //CONSULTAR PELICULAS
                var mCrud2 = new MovieCrudFactory();
                var listMovies = mCrud2.RetrieveAll<Movie>();
                foreach (var m in listMovies)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(m));
                }
                break;
            case 8: //Consultar Pelicula por ID
                Console.WriteLine("Falta por implementar la consulta de peliculas por ID");
                break;
            case 9: //Actualizar Peliculas
                Console.WriteLine("Falta por implementar la actualizacion de peliculas");
                break;
            case 10: //Eliminar Peliculas
                Console.WriteLine("Falta por implementar la eliminacion de peliculas");
                break;


                //CREAR OBJETO Pelicula A PARTIR DE LOS VALORES CAPTURADOS EN CONSOLA
                var movie = new Movie()
                {
                    Title = title,
                    Description = description,
                    ReleaseDate = releasedate,
                    Genre = genre,
                    Director = director
                };

                var mCrud = new MovieCrudFactory();
                mCrud.Create(movie);
                break;
        }




        var sqlDao= SqlDao.GetInstance();
        sqlDao.ExecuteProcedure(sqlOperation);

    }
}