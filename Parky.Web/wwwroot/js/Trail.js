//import { ajax } from "jquery";

var datatble;


$(document).ready(function () {
    loadDataTable();
});

function Delete(url) {
    swal({
        title: "Are you sure you want to deelte?",
        text: "You wont be able to get back this data",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "Delete",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        //datatble.ajax.relaod();
                        //datatble.DataTable().ajax.reload()
                        datatble.dataTable().api().ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    })
}

function loadDataTable() {
    datatble = $("#tblData").DataTable({
        "ajax": {
            "url": "/Trail/GetAllTrails",
            "type": "GET",
            "datatype":"json",
        },
        "columns": [
            { "data": "nationalPark.name", "width": "30%" },
            { "data": "name", "width": "50%" },
            { "data": "distance", "width": "50%" },
            { "data": "elevation", "width": "50%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                        <a href="/trail/Upsert/${data}" class="btn btn-success text-white"
                           style="cursor:pointer"><i class="far fa-edit"></i></a>
                        &nbsp;
                    <a onclick=Delete("/trail/Delete/${data}") class="btn btn-danger text-white"
                           style="cursor:pointer"><i class="far fa-trash-alt"></i></a>
                        </div>`; 
                },
                "width": "30%",
            },

        ]
    });
}

