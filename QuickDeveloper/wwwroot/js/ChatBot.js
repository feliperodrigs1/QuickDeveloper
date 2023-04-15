const chatArea = document.getElementById('chat-area');
const input = document.getElementById('input');
const sendButton = document.getElementById('send-button');
const endButton = document.getElementById('endcall-button');
const button = document.querySelector('.chat-button');
let name = "";


/*---------- Adiciona um listener ao evento DOMContentLoaded ----------*/
document.addEventListener('DOMContentLoaded', iniciarChat);

/* --------Função para carregar mensagens de boas-vindas---------*/
function iniciarChat() {
    input.placeholder = "Aguarde um momento";
    input.disabled = true;
    endButton.disabled = true;
    sendButton.disabled = true;
    sendMessage("Bem-vindo ao QuickChat", 'bot');
    setTimeout(() => {//aguarda 2 segundos APÓS CHAMADA DA FUNÇÃO
        sendMessage("Informe o seu nome!", 'bot');
    }, 2000);
    setTimeout(() => {//aguarda 2 segundos APÓS CHAMADA DA FUNÇÃO
        input.disabled = false;
        endButton.disabled = false;
        sendButton.disabled = false;
        input.placeholder = "Digite sua menssagem";
    }, 3000);
}


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
        sendMessage(`${name}, por favor aguarde enquanto carregamos os resultados para você`, 'bot');
    }, 2000);
});

/*---------- Evento de clicar no botão de enviar --------*/
sendButton.addEventListener('click', sendChat);
/*---------- Evento precionar enter e enviar --------*/
input.addEventListener('keydown', function (event) {
    if (event.key === 'Enter') {
        sendChat();
    }
});
/*--------- Função capturar a mensagem e enviar para o chat --------------*/
function sendChat() {
    const message = input.value.trim(); // armazena em 'message' o valor do textbox
    if (message === '') return; // não enviar mensagem vazia
    input.value = ''; // limpa o texbox
    sendMessage(message, 'user'); // chama a função passando a mensagem da variável e o remetente como usuário
    setTimeout(() => { // aguarda 0,3 segundos
        if (name == "") {
            name = message;
            //setTimeout(() => {//aguarda 2 segundos APÓS CHAMADA DA FUNÇÃO
            //    sendMessage(`Seja bem-vindo ${name}`, 'bot');
            //}, 1000);

            fetch('http://localhost:5126/api/ChatGpt/CreateComplementation', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    sessionId: "",
                    name: `${name}`,
                    history: "",
                    message: "",
                })
            })
                .then(response => response.json())
                .then(data => {
                    console.log('Resposta da API:', data);
                    sendMessage(data.resposta, 'bot');
                })
                .catch(error => {
                    console.error('Erro ao enviar mensagem para a API:', error);
                });

            setTimeout(() => {//aguarda 2 segundos APÓS CHAMADA DA FUNÇÃO
                sendMessage("Como posso ajuda-lo?", 'bot');
            }, 3000);
        } else {
            sendMessage(`${name} sinto muito, ainda não é possível estabelecer uma comunicação, aguarde`, 'bot'); // chama a função com uma mensagem padrão tendo o bot como remetente
        }
    }, 300);
}

/* --------Função para devolver as mensagens para a tela ---------*/
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
