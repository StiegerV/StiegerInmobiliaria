// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function Sweet(url, id) {
    const titulo = "¿Estás seguro que querés eliminar?";
    const texto = "¡No se podrá revertir esta acción!";
    const boton = "Sí, eliminar";

    Swal.fire({
        title: titulo,
        text: texto,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: boton
    }).then((result) => {
        if (result.isConfirmed) {
            fetch(url + id, {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json'
                },
            }).then(response => response.json())
                .then(respuesta => {
                    if (respuesta.success) {
                        Swal.fire({
                            title: "¡Eliminado!",
                            text: "▲",
                            icon: "success"
                        }).then(() => location.reload());
                    } else {
                        Swal.fire({
                            title: "¡Error!",
                            text: respuesta.mensaje,
                            icon: "error"
                        });
                    }
                })
                .catch(error => {
                    console.error("Error en la solicitud:", error);
                    Swal.fire({
                        title: "¡Error!",
                        text: "Ocurrió un error inesperado.",
                        icon: "error"
                    });
                });
        }
    });
}