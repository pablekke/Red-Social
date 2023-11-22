
document.querySelector('#registroForm').addEventListener('submit', validarDatos);

function validarDatos(event) {
    event.preventDefault();

function validarDatos(event){
    event.preventDefault();

    let nombre = document.querySelector("#nombre").value;
    let apellido = document.querySelector("#apellido").value;
    let email = document.querySelector("#email").value;
    let pass = document.querySelector("#pass").value;
    let fNac = document.querySelector("#fNac").value;

    if (nombre != "" && apellido != "" && email != "" && pass != "" && fNac != "") {
        document.querySelector('#registroForm').reset();
        nombre.innerHTML = "";
        this.submit();
    } else {
        document.querySelector('#registroForm h4').innerHTML = "Los campos no deben estar vacíos.";
    }

}