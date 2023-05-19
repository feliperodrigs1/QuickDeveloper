//Visualizações de signIn e signUp
const sign_in_btn = document.querySelector("#sign-in-btn");
const sign_up_btn = document.querySelector("#sign-up-btn");
const container = document.querySelector(".container");
const form = document.getElementById("signup-form");
const passwordInput = document.querySelector("#input-password");
let dev = false;

sign_up_btn.addEventListener("click", () => {
    container.classList.add("sign-up-mode");
});

sign_in_btn.addEventListener("click", () => {
    container.classList.remove("sign-up-mode");
});

const dateInput = document.querySelector('#Birthdate');

dateInput.addEventListener('input', (event) => {
    let input = event.target;
    let value = input.value.replace(/\D/g, '');

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

passwordInput.addEventListener('input', () => {
    let value = passwordInput.value;

    validateUpperCase(value);

    validateLowerCase(value);

    validateNumber(value);
    
    validateSymbol(value);

    validateSyze(value);
});

function validateUpperCase(value) {
    let upperCase = /[A-Z]/.test(value);
    let upperTxt = document.querySelector("#upkey");

    upperCase ? upperTxt.classList.add("hidden") : upperTxt.classList.remove("hidden");
}

function validateLowerCase(value) {
    let lowerCase = /[a-z]/.test(value);
    let lowerTxt = document.querySelector("#lowerkey");

    lowerCase ? lowerTxt.classList.add("hidden") : lowerTxt.classList.remove("hidden");
}

function validateNumber(value) {
    let hasNumber = /[0-9]/.test(value);
    let numberTxt = document.querySelector("#number");

    hasNumber ? numberTxt.classList.add("hidden") : numberTxt.classList.remove("hidden");
}

function validateSymbol(value) {
    let hasSymbol = /[!@#$%^&+=]/.test(value);
    let symbolTxt = document.querySelector("#symbol");

    hasSymbol ? symbolTxt.classList.add("hidden") : symbolTxt.classList.remove("hidden");
}

function validateSyze(value) {
    let validSize = value.length >= 5;
    let sizeTxt = document.querySelector("#size");

    validSize ? sizeTxt.classList.add("hidden") : sizeTxt.classList.remove("hidden");
}