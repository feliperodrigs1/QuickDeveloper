const chatArea = document.getElementById('chat-area');
const input = document.getElementById('input');
const sendButton = document.getElementById('send-button');
const endButton = document.getElementById('endcall-button');
const button = document.querySelector('.chat-button');


/*---------- Adiciona um listener ao evento DOMContentLoaded ----------*/
document.addEventListener('DOMContentLoaded', iniciarChat); 

/*---------- Evento de clicar no botão encerrar chamada--------*/

endButton.addEventListener('click', function () {
    const message = "Chamada encerrada"; //armazena em 'message' o valor do textbox
    input.value = '';//limpa o texbox
    
    sendMessage(message, 'bot');
    input.placeholder = "Chamada encerrada";
    input.disabled = true;
    endButton.disabled = true;
    sendButton.disabled = true;

    setTimeout(() => {//aguarda 0,3segundos
        sendMessage("Por favor aguarde enquanto carregamos os resultados para você", 'bot');
    }, 2000);
});

/*---------- Evento de clicar no botão de enviar --------*/
sendButton.addEventListener('click', function () {
    const message = input.value.trim(); //armazena em 'message' o valor do textbox
    if (message === '') return; // não enviar mensagem vazia
    input.value = '';//limpa o texbox
    sendMessage(message, 'user');//chama a funcão passando a menssagem da variavel e o remetente como usuario
    setTimeout(() => {//aguarda 0,3segundos
        sendMessage("Sinto muito, ainda não é possivel estabelecer uma comunicação, Aguardes", 'bot'); //chama a funcao com uma mensagem padrao tendo o bot como remetente
    }, 300);
});

/*---------- Evento de precionar Enter e enviar --------*/
input.addEventListener('keydown', function (event) {
    if (event.key === 'Enter') {//Avalia se a tecla precisonada é o 'Enter'
        const message = input.value.trim();//armazena em 'message' o valor do textbox
        if (message === '') return;// não enviar mensagem vazia
        input.value = '';//limpa o texbox
        sendMessage(message, 'user');//chama a funcão passando a menssagem da variavel e o remetente como usuario
        setTimeout(() => {//aguarda 0,3segundos
            sendMessage("Sinto muito, ainda não é possivel estabelecer uma comunicação, Aguarde", 'bot');//chama a funcao com uma mensagem padrao tendo o bot como remetente
        }, 300);
    }
});

/* --------Função para carregar mensagens de boas-vindas---------*/
function iniciarChat() {
    input.placeholder = "Aguarde um momento";
    input.disabled = true;
    endButton.disabled = true;
    sendButton.disabled = true;
    sendMessage("Bem-vindo ao nosso chat", 'bot');
    setTimeout(() => {//aguarda 2 segundos APÓS CHAMADA DA FUNÇÃO
        sendMessage("Como posso ajuda-lo?", 'bot');
    }, 2000);
    setTimeout(() => {//aguarda 2 segundos APÓS CHAMADA DA FUNÇÃO
        input.disabled = false;
        endButton.disabled = false;
        sendButton.disabled = false;
        input.placeholder = "Digite sua menssagem";
    }, 3000);
}



/* --------Função para enviar mensagens---------*/
function sendMessage(message, sender) { //recebe a mensagem a postar na tela e quem é o remetente
    const chatBubble = document.createElement('div'); //constroe um novo elemento "Bolha"
    chatBubble.classList.add('chat-bubble');//atribui o style da classe 'chat-bubble'
    chatBubble.innerHTML = message; //atribui a div o texto recebido em message
    if (message == "Chamada encerrada") {
        chatBubble.classList.add('endcall');
    } else {
        chatBubble.classList.add(sender); //adiciona o style da classe remetente
    }
    
    if (chatArea.children.length === 0) {
        chatBubble.classList.add('first'); //Adiciona classe 'first' para a primeira mensagem do chat
    }

    chatArea.appendChild(chatBubble); //Adiciona a nova div criada no final da tela do chat
    chatArea.scrollTop = chatArea.scrollHeight; //faz a barra de rolagem acompanhar a ultima menssagem digitada
}

