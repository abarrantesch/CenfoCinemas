//js que maneja todo el comportamiento de la vista de usuarios
//definir una clase js usando prototype

function MoviesViewController() {

    this.ViewName = "Movies";
    this.ApiEndPointName = "Movie";

    //Metodo Constructor
    this.InitView = function () {

        console.log("Movie init view --> ok");
        this.LoadTable();

        //asociar el evento al boton
        $('#btnCreate').click(function () {
            var vc = new MoviesViewController();
            vc.Create();
        });
        $('#btnUpdate').click(function () {
            var vc = new MoviesViewController();
            vc.Update();
        });
        $('#btnDelete').click(function () {
            var vc = new MoviesViewController();
            vc.Delete();
        });
    }


    //metodo para la carga de una table
    this.LoadTable = function () {
        //URL del API a invocar
        //https://localhost:7182/api/Movie/RetrieveAll

        var ca = new ControlActions();
        var service = this.ApiEndPointName + "/RetrieveAll";


        var urlService = ca.GetUrlApiService(service);

        console.log("🟢 Invocando DataTables con URL:", urlService); //debug

        var columns = [];
        columns[0] = { 'data': 'id' }
        columns[1] = { 'data': 'title' }
        columns[2] = { 'data': 'description' }
        columns[3] = { 'data': 'releaseDate' }
        columns[4] = { 'data': 'genre' }
        columns[5] = { 'data': 'director' }

        //Invocamos a datatables para convertir la tabla simple HTML en una tabla mas robusta
        $("#tblMovies").DataTable({
            "ajax": {
                url: urlService,
                "dataSrc": ""
            },
            columns: columns
        });

        //aseignar eventos de carga de datos o bindings segun el click en la tabla
        $('#tblMovies tbody').on('click', 'tr', function () {
            //extraemos la fila 
            var row = $(this).closest('tr');

            //extraemos el dto
            //Esto nos devuelve json de la fila seleccionada por el usuario segun la data devuelta por el API
            var movieDTO = $('#tblMovies').DataTable().row(row).data();

            //binding con el form
            $('#txtId').val(movieDTO.id);
            $('#txtTitle').val(movieDTO.title);
            $('#txtDescription').val(movieDTO.description);
            $('#txtReleaseDate').val(movieDTO.releaseDate);
            $('#txtGenre').val(movieDTO.genre);
            $('#txtDirector').val(movieDTO.director);

            //fecha tiene un formato
            var onlyDate = movieDTO.releaseDate.split("T");
            $('#txtReleaseDate').val(onlyDate[0]);

        })

    }



    this.Create = function () {

        var movieDTO = {};
        //atributos con valores defaul, que son controlados por el API

        movieDTO.id = 0;
        movieDTO.created = "2025-01-01";
        movieDTO.updated = "2025-01-01";

        //valores capturados en pantalla
        movieDTO.title = $('#txtTitle').val();
        movieDTO.description = $('#txtDescription').val();
        movieDTO.ReleaseDate = $('#txtReleaseDate').val();
        movieDTO.Genre = $('#txtGenre').val();
        movieDTO.Director = $('#txtDirector').val();

        //enviar la data al API
        var ca = new ControlActions();
        var urlService = this.ApiEndPointName + "/Create";

        ca.PostToAPI(urlService, movieDTO, function () {
            //recargar la tabla
            $('#tblMovies').DataTable().ajax.reload();
        })
    }

    this.Update = function () {

        var id = $('#txtId').val();
        if (!id) {
            Swal.fire({
                icon: 'warning',
                title: 'Oops...',
                text: 'Por favor, selecciona una pelicula de la tabla antes de actualizar.',
                confirmButtonText: 'Entendido'
            });
            return;
        }// sale de la función, no sigue


        var movieDTO = {};
        //atributos con valores defaul, que son controlados por el API

        movieDTO.id = id;
        movieDTO.created = "2025-01-01";
        movieDTO.updated = "2025-01-01";

        //valores capturados en pantalla
        movieDTO.title = $('#txtTitle').val();
        movieDTO.description = $('#txtDescription').val();
        movieDTO.releaseDate = $('#txtReleaseDate').val();
        movieDTO.genre = $('#txtGenre').val();
        movieDTO.director = $('#txtDirector').val();

        //enviar la data al API
        var ca = new ControlActions();
        var urlService = this.ApiEndPointName + "/Update";

        ca.PutToAPI(urlService, movieDTO, function () {
            //recargar la tabla
            $('#tblMovies').DataTable().ajax.reload();
        })

    }



    this.Delete = function () {
        var id = $('#txtId').val();
        if (!id) {
            Swal.fire({
                icon: 'warning',
                title: 'Oops...',
                text: 'Por favor, selecciona una pelicula antes de eliminar.',
                confirmButtonText: 'Entendido'
            });
            return;
        }

        var ca = new ControlActions();
        var urlService = this.ApiEndPointName + "/Delete?id=" + id;

        ca.DeleteToAPI(urlService, null, function () {
            $('#tblMovies').DataTable().ajax.reload();
            Swal.fire('Eliminado!', 'Usuario eliminado con éxito.', 'success');
        });
    }
}


console.log("movies.js cargado y ejecutándose");
$(document).ready(function () {
    console.log("Documento listo, creando MoviesViewController");
    var vc = new MoviesViewController();
    vc.InitView();
});