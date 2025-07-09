//js que maneja todo el comportamiento de la vista de usuarios
//definir una clase js usando prototype

function UsersViewController() {

    this.ViewName = "Users";
    this.ApiEndPointName = "User";

    //Metodo Constructor
    this.InitView = function () {

        console.log("User init view --> ok");
        this.LoadTable();

        //asociar el evento al boton
        $('#btnCreate').click(function () {
            var vc = new UsersViewController();
            vc.Create();
        });
        $('#btnUpdate').click(function () {
            var vc = new UsersViewController();
            vc.Update();
        });
        $('#btnDelete').click(function () {
            var vc = new UsersViewController();
            vc.Delete();
        });
        $('#btnSearch').click(function () {
            var vc = new UsersViewController();
            vc.IdSearch();
        });
    }


    //metodo para la carga de una table
    this.LoadTable = function () {
        //URL del API a invocar
        //https://localhost:7182/api/User/RetrieveAll

        var ca = new ControlActions();
        var service = this.ApiEndPointName + "/RetrieveAll";


        var urlService = ca.GetUrlApiService(service);

        console.log("🟢 Invocando DataTables con URL:", urlService); //debug

        var columns = [];
        columns[0] = { 'data': 'id' }
        columns[1] = { 'data': 'userCode' }
        columns[2] = { 'data': 'name' }
        columns[3] = { 'data': 'email' }
        columns[4] = { 'data': 'birthDate' }
        columns[5] = { 'data': 'status' }

        //Invocamos a datatables para convertir la tabla simple HTML en una tabla mas robusta
        $("#tblUsers").DataTable({
            "ajax": {
                url: urlService,
                "dataSrc": ""
            },
            columns: columns
        });

        //aseignar eventos de carga de datos o bindings segun el click en la tabla
        $('#tblUsers tbody').on('click', 'tr', function () {
            //extraemos la fila 
            var row = $(this).closest('tr');

            //extraemos el dto
            //Esto nos devuelve json de la fila seleccionada por el usuario segun la data devuelta por el API
            var userDTO = $('#tblUsers').DataTable().row(row).data();

            //binding con el form
            $('#txtId').val(userDTO.id);
            $('#txtUserCode').val(userDTO.userCode);
            $('#txtName').val(userDTO.name);
            $('#txtEmail').val(userDTO.email);
            $('#txtStatus').val(userDTO.status);

            //fecha tiene un formato
            var onlyDate = userDTO.birthDate.split("T");
            $('#txtBirthDate').val(onlyDate[0]);

        })

    }



    this.Create = function () {

        var userDTO = {};
        //atributos con valores defaul, que son controlados por el API

        userDTO.id = 0;
        userDTO.created = "2025-01-01";
        userDTO.updated = "2025-01-01";

        //valores capturados en pantalla
        userDTO.userCode = $('#txtUserCode').val();
        userDTO.name = $('#txtName').val();
        userDTO.email = $('#txtEmail').val();
        userDTO.status = $('#txtStatus').val();
        userDTO.birthDate = $('#txtBirthDate').val();
        userDTO.password = $('#txtPassword').val();

        //enviar la data al API
        var ca = new ControlActions();
        var urlService = this.ApiEndPointName + "/Create";

        ca.PostToAPI(urlService, userDTO, function () {
            //recargar la tabla
            $('#tblUsers').DataTable().ajax.reload();
        })
    }

    this.Update = function () {

        var id = $('#txtId').val();
        if (!id) {
            Swal.fire({
                icon: 'warning',
                title: 'Oops...',
                text: 'Por favor, selecciona un usuario de la tabla antes de actualizar.',
                confirmButtonText: 'Entendido'
            });
            return;
        }// sale de la función, no sigue


        var userDTO = {};
        //atributos con valores defaul, que son controlados por el API

        userDTO.id = $('#txtId').val();
        userDTO.created = "2025-01-01";
        userDTO.updated = "2025-01-01";

        //valores capturados en pantalla
        userDTO.userCode = $('#txtUserCode').val();
        userDTO.name = $('#txtName').val();
        userDTO.email = $('#txtEmail').val();
        userDTO.status = $('#txtStatus').val();
        userDTO.birthDate = $('#txtBirthDate').val();
        userDTO.password = $('#txtPassword').val();

        //enviar la data al API
        var ca = new ControlActions();
        var urlService = this.ApiEndPointName + "/Update";

        ca.PutToAPI(urlService, userDTO, function () {
            //recargar la tabla
            $('#tblUsers').DataTable().ajax.reload();
        })

    }



    this.Delete = function () {
        var id = $('#txtId').val();
        if (!id) {
            Swal.fire({
                icon: 'warning',
                title: 'Oops...',
                text: 'Por favor, selecciona un usuario antes de eliminar.',
                confirmButtonText: 'Entendido'
            });
            return;
        }

        var ca = new ControlActions();
        var urlService = this.ApiEndPointName + "/Delete?id=" + id;

        ca.DeleteToAPI(urlService, null, function () {
            $('#tblUsers').DataTable().ajax.reload();
            Swal.fire('Eliminado!', 'Usuario eliminado con éxito.', 'success');
        });
    }

    this.IdSearch = function () {
        var id = $('#UserIdSearch').val();
        if (!id) {
            Swal.fire({
                icon: 'warning',
                title: 'Oops...',
                text: 'El formato de usuario no es valido, debe ser un numero',
                confirmButtonText: 'Entendido'
            });
            return;
        }

        var ca = new ControlActions();
        var urlService = this.ApiEndPointName + "/RetrieveById?id=" + id;

        ca.GetToApi(urlService, function (response) {
            if (response) {
                // actualiza formulario con el resultado de busqueda
                $('#txtId').val(response.id);
                $('#txtUserCode').val(response.userCode);
                $('#txtName').val(response.name);
                $('#txtEmail').val(response.email);
                $('#txtStatus').val(response.status);
                var onlyDate = response.birthDate.split("T");
                $('#txtBirthDate').val(onlyDate[0]);

                Swal.fire('Encontrado!', 'Usuario recuperado con éxito.', 'success');
            } else {
                Swal.fire('No encontrado', 'No se encontró usuario con ese ID', 'warning');
            }
        });
    }
}


console.log("users.js cargado y ejecutándose");
$(document).ready(function () {
    console.log("Documento listo, creando UsersViewController");
    var vc = new UsersViewController();
    vc.InitView();
});