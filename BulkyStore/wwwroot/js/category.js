﻿var dataTable;
$(document).ready(function () {
    loadDataTable();

});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Category/GetAll"
        },
        "columns": [
            { "data": "name", "width": "60%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                        <a href="/Admin/Category/Upsert/${data}" class="btn btn-success text-white"
                            style="cursor:pointer;">
                            <i class="fas fa-edit"></i>
                        </a>
                        <a onclick=Delete("/Admin/Category/Delete/${data}") class="btn btn-danger text-white" 
                             style="cursor:pointer;">
                            <i class="fas fa-trash-alt"></i>
                        </a>
                        </div>    
                       `;
                }, "width": "40%"
            }
        ]

    });
}
function Delete(url) {
    swal({
        title: "are you sure to delete?",
        text: "you won't be able to restore data !",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.messsage);
                        dataTable.ajax.reload();
                    } else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    }); 
}