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
                        datatble.ajax.relaod();
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
            "url": "/NationalPark/GetAllNationalParks",
            "type": "GET",
            "datatype":"json",
        },
        "columns": [
            { "data": "name", "width": "30%" },
            { "data": "state", "width": "40%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                        <a href="/NationalPark/Upsert/${data}" class="btn btn-success text-white"
                           style="cursor:pointer"><i class="far fa-edit"></i></a>
                        &nbsp;
                    <a onclick=Delete("/NationalPark/Delete/${data}") class="btn btn-danger text-white"
                           style="cursor:pointer"><i class="far fa-trash-alt"></i></a>
                        </div>`; 
                },
                "width": "50%",
            },

        ]
    });
}

