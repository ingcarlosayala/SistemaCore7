

var datatable;

$(document).ready(function () {
    CargarTabla();
});

const CargarTabla = () => {
    datatable = $("#tblDato").DataTable({
        "ajax": {
            "url": "/Admin/Categorias/ObtenerTodos"
        },
        "columns": [
            {"data": "nombre"},
            {
                "data": "estado",
                "render": function (estado) {
                    if (estado == true) {
                        return "Activo";
                    } else {
                        return "Inactivo";
                    }
                }
            },
            {
                "data": "id",
                "render": function (id) {
                    return `<div class="text-center">
                                <a href="/Admin/Categorias/Editar/${id}" class="btn btn-warning btn-sm"> <i class="bi bi-pen"></i> </a>
                                <a onclick=Delete("/Admin/Categorias/Delete/${id}") class="btn btn-danger btn-sm"> <i class="bi bi-trash3"></i> </a>
                            </div>`
                    ;
                }
            }
        ]
    });
}

const Delete = url => {

    Swal.fire({
        title: 'Estas Seguro?',
        text: "No se podra recuperar el registro!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si, Eliminar!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data) {
                        toastr.success(data.message);
                        datatable.ajax.reload();
                    } else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    })
}