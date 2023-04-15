//Visualizações de signIn e signUp
const sign_in_btn = document.querySelector("#sign-in-btn");
const sign_up_btn = document.querySelector("#sign-up-btn");
const container = document.querySelector(".container");
const form = document.getElementById("signup-form");
let dev = false;

sign_up_btn.addEventListener("click", () => {
    container.classList.add("sign-up-mode");
});

sign_in_btn.addEventListener("click", () => {
    container.classList.remove("sign-up-mode");
});

const dateInput = document.querySelector('#Birthdate');

dateInput.addEventListener('input', (event) => {
    const input = event.target;
    const value = input.value.replace(/\D/g, '');

    if (value.length > 8) {
        input.value = value.slice(0, 8);
    }

    if (value.length >= 2 && value.length < 4) {
        input.value = value.slice(0, 2) + '/' + value.slice(2);
    } else if (value.length >= 4) {
        input.value =
            value.slice(0, 2) + '/' + value.slice(2, 4) + '/' + value.slice(4);
    }
});