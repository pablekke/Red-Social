/* MODO CLARO - OSCURO */

/*let darkModeToggle = document.getElementById("dark-mode-toggle");
let body = document.body;

// Función para cambiar al modo oscuro
function enableDarkMode() {
    // Background
    body.classList.add("bg-dark", "text-white");
    darkModeToggle.textContent = "☀️";
    darkModeToggle.classList.remove("btn-light");
    darkModeToggle.classList.add("btn-dark");

    // H2 elements
    let h2Elements = document.querySelectorAll('h2');
    h2Elements.forEach(function (h2) {
        h2.classList.add("text-black");
    });

    // A tags
    let navLinks = document.querySelectorAll('nav a');
    navLinks.forEach(function (link) {
        link.classList.add("text-white");
    });

    // Buttons
    let buttons = document.querySelectorAll('button');
    buttons.forEach(function (button) {
        button.classList.add("btn-dark");
    });

    // Spans
    let spans = document.querySelectorAll('span');
    spans.forEach(function (span) {
        span.classList.add("text-black");
    });

    // Guardar la elección del usuario en localStorage
    localStorage.setItem("darkMode", "enabled");
}


// Función para cambiar al modo claro
function disableDarkMode() {
    // Background
    body.classList.remove("bg-dark", "text-white");
    darkModeToggle.textContent = "🌙";
    darkModeToggle.classList.remove("btn-dark");
    darkModeToggle.classList.add("btn-light");

    // H2 elements
    let h2Elements = document.querySelectorAll('h2');
    h2Elements.forEach(function (h2) {
        h2.classList.remove("text-black");
    });

    // Buttons
    let buttons = document.querySelectorAll('button');
    buttons.forEach(function (button) {
        button.classList.add("btn-dark");
    });

    // A tags
    let navLinks = document.querySelectorAll('nav a');
    navLinks.forEach(function (link) {
        link.classList.remove("text-white");
    });

    // Spans
    let spans = document.querySelectorAll('span');
    spans.forEach(function (span) {
        span.classList.remove("text-black");
    });

    // Guardar la elección del usuario en localStorage
    localStorage.setItem("darkMode", "enabled");
}

// Verificar si el usuario ya ha seleccionado un modo previamente
if (localStorage.getItem("darkMode") === "enabled") {
    enableDarkMode();
    }

darkModeToggle.addEventListener("click", function () {
    if (body.classList.contains("bg-dark")) {
        disableDarkMode();
    } else {
        enableDarkMode();
    }
});
*/
/* MODO CLARO - OSCURO */