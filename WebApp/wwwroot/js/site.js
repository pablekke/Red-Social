/* MODO CLARO - OSCURO */

let darkModeToggle = document.getElementById("dark-mode-toggle");
let body = document.body;

// Función para cambiar al modo oscuro
function enableDarkMode() {
    body.classList.add("bg-dark", "text-white");
    darkModeToggle.textContent = "☀️";
    darkModeToggle.classList.remove("btn-light");
    darkModeToggle.classList.add("btn-dark");

    let navLinks = document.querySelectorAll('nav a');
    navLinks.forEach(function (link) {
        link.classList.add("text-white");
    });

    // Guardar la elección del usuario en localStorage
    localStorage.setItem("darkMode", "enabled");
}

// Función para cambiar al modo claro
function disableDarkMode() {
    body.classList.remove("bg-dark", "text-white");
    darkModeToggle.textContent = "🌙";
    darkModeToggle.classList.remove("btn-dark");
    darkModeToggle.classList.add("btn-light");

    let navLinks = document.querySelectorAll('nav a');
    navLinks.forEach(function (link) {
        link.classList.remove("text-white");
    });

    // Guardar la elección del usuario en localStorage
    localStorage.setItem("darkMode", "disabled");
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

/* MODO CLARO - OSCURO */




