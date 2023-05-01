const modal = document.querySelector(".modal");
const overlay = document.querySelector(".overlay");
const row = document.querySelectorAll('.dropdown-trigger')
const closeModalBtn = document.querySelector(".btn");
const modalTitle = document.querySelector(".title");
const modalData = document.querySelector(".dateOutput");
const modalDataExp = document.querySelector(".dateExpOut");
const emailData = document.querySelector(".emailOutput");
const modalCompetences = document.querySelector(".competences");
const modalInfos = document.querySelector(".infos");

const openModal = function () {
    modal.classList.remove("hidden");
    overlay.classList.remove("hidden");
};

const closeModal = function () {
    modal.classList.add("hidden");
    overlay.classList.add("hidden");
};

function getUserInfo(email) {
    $.ajax({
        url: "/User/FindUserByEmail",
        type: "POST",
        data: { email },
        success: function (result) {            
            modalCompetences.innerHTML = result["competences"];
            modalInfos.innerHTML = result["aditionalInfo"];
        }
    });
}

for (let i = 0; i < row.length; i++) {
    row[i].addEventListener('click', function (event) {
        let email = document.getElementById(`emailInput${i}`).innerText.trim();
        getUserInfo(email);
        content = event.target.parentElement.innerText.split('\t');

        modalTitle.innerHTML = content[1];
        modalData.innerHTML = document.getElementById(`dateInput${i}`).innerText;       
        modalDataExp.innerHTML = document.getElementById(`dateExp${i}`).innerText;        
        emailData.innerHTML = `<a href="mailto:${email}">${email}</a>`;

        openModal();
    });
}

closeModalBtn.addEventListener("click", closeModal);
overlay.addEventListener("click", closeModal);