var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Employee/Home/GetAll"
        },
        "columns": [
            { "data": "employeeNumber", "width": "5%" },
            { "data": "firstName", "width": "5%" },
            { "data": "otherNames", "width": "10%" },
            { "data": "surname", "width": "10%" },
            { "data": "birthDate", "width": "10%" },
            { "data": "salary", "width": "5%" },
            { "data": "level", "width": "5%" },
            { "data": "role", "width": "10%" },
            { "data": "managerNames", "width": "10%" },
            {
                "data": "employeeNumber",
                "render": function (data) {
                    return `
                        <div class=" btn-group" role="group">
                        <a href="/Admin/Employee/Edit?id=${data}"
                           class="btn btn-primary mx-2"> <i class="bi bi-pen"></i> &nbsp; Edit</a>
                        </div>
                        <div class="btn-group" role="group">
                            <a href="/Admin/Employee/Delete?id=${data}"
                           class="btn btn-danger mx-2"> <i class="bi bi-trash"></i> &nbsp; Delete</a>
                        </div>
                        `
                }, 
                "width": "20%"
                
            }
        ]
    });
}


