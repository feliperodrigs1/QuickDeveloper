const modal = document.querySelector(".modal");
const overlay = document.querySelector(".overlay");
const row = document.querySelectorAll('.dropdown-trigger')
const closeModalBtn = document.querySelector(".btn");
const modalTitle = document.querySelector(".title");
const modalData = document.querySelector(".dateOutput");
const modalDataExp = document.querySelector(".dateExpOut");
const emailData = document.querySelector(".emailOutput");

const openModal = function () {
    modal.classList.remove("hidden");
    overlay.classList.remove("hidden");
};

const closeModal = function () {
    modal.classList.add("hidden");
    overlay.classList.add("hidden");
};


for (let i = 0; i < row.length; i++) {
    row[i].addEventListener('click', function (event) {
        var content = event.target.parentElement.innerText.split('\t');       
        modalTitle.innerHTML = content[1];
        modalData.innerHTML = document.querySelector(".dateInput").innerText;       
        modalDataExp.innerHTML = document.querySelector(".dateExp").innerText;
        emailData.innerHTML = `<a href="mailto:${document.querySelector(".emailInput").innerText}">${document.querySelector(".emailInput").innerText}</a>`;;
        openModal();        
    });
}

closeModalBtn.addEventListener("click", closeModal);
overlay.addEventListener("click", closeModal);