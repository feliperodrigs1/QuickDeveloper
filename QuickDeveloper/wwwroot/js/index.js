window.onload = function () {
    const body = document.querySelector('body');
    const img = document.querySelector('img');
    const text = document.querySelector('p');
    const defaultContent = document.querySelector('p').innerHTML;
    let miliseconds = 20000;
    let flag = 2;

    setInterval(changeBackGround, miliseconds);

    function changeBackGround() {
        if (flag % 2 == 0) {
            secondBackGround();
            flag += 1;
            return;
        }

        else if (flag % 3 == 0) {
            thirdBackGround();
            flag += 4;
            return;
        }

        firstBackGround();
        flag = 2;
    }

    function firstBackGround() {
        body.classList.remove("third-body");
        img.src = 'img/index/team_work_blue.svg';
        text.innerHTML = defaultContent;
    }

    function secondBackGround() {
        body.classList.add("second-body");
        img.src = 'img/index/business_parsered.svg';
        text.innerHTML = "Caso você tenha alguma ideia inovadora, é possível se cadastrar como um solicitante e alinhar o desenvolvimento para torná-la realidade!";
    }

    function thirdBackGround() {
        body.classList.remove("second-body");
        body.classList.add("third-body");
        img.src = 'img/index/dev_red.svg';
        text.innerHTML = "Caso você seja um desenvolvedor com experiência e esteja em busca de projetos para trabalhar, é possível receber ofertas com base nas suas competências!";
    }
    
}