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

// Checkbox para competências
const checkBox = document.getElementById("dev");
const competenceInput = document.getElementsByClassName("wrapper")[0]

function verifyRole() {
    if (checkBox.checked == true) {
        dev = true;
    } else {
        dev = false;
    }
}

// Enviar infos
form.addEventListener("submit", function (event) {
    event.preventDefault();

    if (dev) {        
        $.ajax({
            url: "/Home/CompetencesPath",
            type: "POST",
            success: function (result) {                          
                window.location.replace(result.path);
            }
        });
    } else {
        window.location.href = '/';
    }   
});