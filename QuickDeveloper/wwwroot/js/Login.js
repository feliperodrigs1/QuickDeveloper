const form = document.getElementById("login-form");

form.addEventListener("submit", function (event) {
    
    event.preventDefault();
    const username = document.getElementById("username").value;
    const password = document.getElementById("password").value;

    if (username === "" || password === "") {
        alert("Por favor, preencha todos os campos!");
    } else {
        window.location.href = '/';
    }
});
