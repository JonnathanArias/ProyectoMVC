let datatable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    datatable = $('#tblDatos').DataTable({
        "language": {
            "lengthMenu": "Mostrar _MENU_ Registros Por Pagina",
            "zeroRecords": "Ningun Registro",
            "info": "Mostrar page _PAGE_ de _PAGES_",
            "infoEmpty": "no hay registros",
            "infoFiltered": "(filtered from _MAX_ total registros)",
            "search": "Buscar",
            "paginate": {
                "first": "Primero",
                "last": "Último",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        },
        "ajax": {
            "url": "/Admin/Usuario/ObtenerTodos"
        },
        "columns": [
            { "data": "NumeroDocumento" },
            { "data": "Nombre" },
            { "data": "Apellido" },
            { "data": "Correo" },
            { "data": "ContraseñaHash" },
            { "data": "Rol" },
            { "data": "Ciudad" },
            { "data": "NombreUsuario" },
            { "data": "ContraseñaHash" },
            { "data": "FechaCreacion" },
            { "data": "Estado" },
            {
                "data": "id",
                "render": function (data) {
                    return '<button class="btn btn-primary">Editar</button>';
                }
            }
        ]
    });
}