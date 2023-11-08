const signUpButton = document.getElementById('signUp');
const signInButton = document.getElementById('signIn');
const container = document.getElementById('containerLogin');

signUpButton.addEventListener('click', () => {
	container.classList.add("right-panel-active");
});

signInButton.addEventListener('click', () => {
	container.classList.remove("right-panel-active");
});


document.addEventListener("DOMContentLoaded", () => {
    const registroForm = document.getElementById("registroForm");

    registroForm.addEventListener("submit", (event) => {
        if (!formIsValid) {
            event.preventDefault();
        }
    });
});