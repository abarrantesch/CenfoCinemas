//js que maneja todo el comportamiento de la vista de usuarios
//definir una clase js usando prototype

function UsersViewController() {

    this.ViewName = "Users";
    this.ApiEndPointName = "User";

    //Metodo Constructor
    this.InitView = function () {

        console.log("User init view --> ok");
    }


    //metodo para la carga de una table
    this.loadTable = function () {
        //URL del API a invocar
        //https://localhost:7182/api/User/RetrieveAll

        var ca = new ControlActions();
        var service = this.ApiEndPointName + "/RetrieveAll";

        var urlService = ca.GetUrlApiService(service);

        /* PARA REFERENCIA DE ESTRUCTURA
          {
            "userCode": "abarrantes",
            "name": "angel b",
            "email": "angeljosue07@gmail.com",
            "password": "pass12",
            "birthDate": "1998-10-10T00:00:00",
            "status": "AC",
            "id": 15,
            "created": "2025-06-28T04:29:37.347",
            "updated": "0001-01-01T00:00:00"
          }
          
                     <tr>
                        <th>Id</th>
                        <th>User Code</th>
                        <th>Name</th>
                        <th>Email</th>
                        <th>Birthdate</th>
                        <th>Status</th>
                    </tr>
        */

        var columns = [];
        colums[0] = { 'data': 'id' }
        colums[1] = { 'data': 'userCode' }
        colums[2] = { 'data': 'name' }
        colums[3] = { 'data': 'email' }
        colums[4] = { 'data': 'birthDate' }
        colums[5] = { 'data': 'status' }

        //Invocamos a datatables para convertir la tabla simple HTML en una tabla mas robusta
        $("#tblUsers").dataTables({
            "ajax": {
                url: urlService,
                "dataSrc": ""
            },
            coluns: columns
        });

    }
}

$(document).ready(function () {
    var vc = new UsersViewController();
    vc.InitView();
})