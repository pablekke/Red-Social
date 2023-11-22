document.querySelector('#publicarForm').addEventListener('submit', validarDatos);

function validarDatos(event) {
    event.preventDefault();

    let titulo = document.querySelector("#titulo").value;
    let contenido = document.querySelector("#contenido").value;
    let imagen = document.querySelector("#imagen").value.toLowerCase();
    let checkbox = document.querySelector('#privacidadCheckbox');
    let valorPrivacidad = checkbox.checked ? 'privada' : 'publica';

    // Extensiones permitidas
    const extensionesPermitidas = [".jpg", ".jpeg", ".png"];

    // Obtener la extensión del archivo seleccionado
    let extension = imagen.substring(imagen.lastIndexOf('.'));

    if ( titulo.length > 2 && contenido != "" &&
        extensionesPermitidas.includes(extension)
    ) {
        document.querySelector('#Privacidad').value = valorPrivacidad;
        this.submit();
        document.querySelector('#mensajePublicar').innerHTML = "";
        document.querySelector('#publicarForm').reset();
    } else {
        if (imagen == "") {

            document.querySelector('#mensajePublicar').innerHTML = "La imagen es requerida.";

        } else if (!extensionesPermitidas.includes(extension)) {
            document.querySelector('#mensajePublicar').innerHTML = "Solo se permiten archivos con extensión JPG, JPEG o PNG.";
        } else {
            document.querySelector('#mensajePublicar').innerHTML = "Los campos no deben estar vacíos o el título debe tener al menos 3 caracteres.";
        }
        return false;
    }

}

