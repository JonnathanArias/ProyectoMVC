let datatable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    datatable = $('#tblDatos').DataTable({
        "ajax": {
            "url": "/Usuario/ObtenerTodos", // Esta ruta ya existe en tu controller
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "documento", "width": "10%" },
            { "data": "nombres", "width": "15%" },
            { "data": "apellidos", "width": "15%" },
            { "data": "correo", "width": "15%" },
            { "data": "rol", "width": "10%" },
            { "data": "ciudad", "width": "10%" },
            {
                "data": "fechaCreacion",
                "width": "10%",
                "render": function (data) {
                    // Formatea la fecha para mostrarla mejor
                    return new Date(data).toLocaleDateString('es-ES');
                }
            },
            {
                "data": "estado",
                "width": "10%",
                "render": function (data) {
                    // Muestra "Activo" o "Inactivo" según el valor booleano
                    return data ? "Activo" : "Inactivo";
                }
            },
            {
                "data": "id",
                "render": function (data) {
                    // Botones de acciones
                    return `
                        <div class="text-center">
                            <a href="/Usuario/Editar/${data}" class="btn btn-warning btn-sm">Editar</a>
                            <a onclick="Delete('/Usuario/Eliminar/${data}')" class="btn btn-danger btn-sm">Eliminar</a>
                        </div>
                    `;
                },
                "width": "15%"
            }
        ],
        "language": {
            "lengthMenu": "Mostrar _MENU_ Registros Por Pagina",
            "zeroRecords": "Ningún Registro",
            "info": "Mostrando página _PAGE_ de _PAGES_",
            "infoEmpty": "No hay registros disponibles",
            "infoFiltered": "(filtrado de _MAX_ registros totales)",
            "search": "Buscar:",
            "paginate": {
                "first": "Primero",
                "last": "Último",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        },
        "width": "100%"
    });
}

// Función para eliminar con SweetAlert
function Delete(url) {
    Swal.fire({
        title: "¿Estás seguro?",
        text: "¡No podrás revertir esta acción!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Sí, eliminar"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        datatable.ajax.reload();
                        Swal.fire("¡Eliminado!", data.message, "success");
                    } else {
                        Swal.fire("Error", data.message, "error");
                    }
                }
            });
        }
    });
}