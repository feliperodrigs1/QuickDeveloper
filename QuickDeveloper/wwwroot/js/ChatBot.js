const chatArea = document.getElementById('chat-area');
const input = document.getElementById('input');
const sendButton = document.getElementById('send-button');
const callButton = document.getElementById('callbutton');
const button = document.querySelector('.chat-button');
const tip = document.getElementById("text-finalization");
var user = document.getElementById("username").value;

/*---------- Adiciona um listener ao evento DOMContentLoaded ----------*/
document.addEventListener('DOMContentLoaded', startChat);

/* --------Função para carregar mensagens de boas-vindas---------*/
function startChat() {
    input.placeholder = "Aguarde um momento";
    input.disabled = true;
    callButton.disabled = true;
    sendButton.disabled = true;

    let sessionId = sessionStorage.getItem("sessionId");

    if (sessionId == null || sessionId.trim() === '') {
        sendRequest(null, user, null, null);
    } else {
        let sessionHistory = sessionStorage.getItem("sessionHistory");

        var messages = sessionHistory.split('***');
        messages.forEach(function (message) {
            console.log(message);
            var parts = message.split('---');
            console.log(parts);
            var sender = parts[0];
            var text = parts[1];
            sendHistory(text, sender, false);
        });
    }

    setTimeout(() => {//aguarda 2 segundos APÓS CHAMADA DA FUNÇÃO
        input.disabled = false;
        callButton.disabled = false;
        sendButton.disabled = false;
        input.placeholder = "Digite sua menssagem";
    }, 3000);
}

function endCall() {
    sendMessage("Analisando Requisitos", 'bot');
    input.placeholder = "Chamada encerrada";
    input.disabled = true;
    callButton.disabled = false;
    sendButton.disabled = true;

    let sessionId = sessionStorage.getItem("sessionId");
    let history = sessionStorage.getItem("history");
    sendRequest(sessionId, user, history, "Verificar");



    setTimeout(() => {//aguarda 0,3segundos
        callButton.disabled = false;
    }, 8000);

};


/*---------- Evento de clicar no botão encerrar chamada--------*/
callButton.addEventListener('click', function () {
    var buttonText = callButton.textContent;

    if (buttonText == 'Analisar Requisitos') {
        tip.textContent = "Para dar continuidade com o projeto e receber um resumo detalhado, pressione o botão azul 'Resumo Técnico' no canto superior direito";
        callButton.innerText = 'Resumo Técnico';
        callButton.style.backgroundColor = '#303457';
        input.value = '';
        endCall();
        //sendRequest(sessionId, user, history, "Verificar");
    }
    if (buttonText == 'Resumo Técnico') {
        callButton.innerText = 'Finalizar Chamada';
        tip.textContent = "Para finalizar este projeto e iniciar uma nova chamada, pressione o botão verde 'Finalizar Chamada' no canto superior direito";
        callButton.style.backgroundColor = '#228B22';
        input.value = '';
        //$("#chat-area").empty();
        sendMessage('Gerando Resumo Detalhado', 'bot');
        let sessionId = sessionStorage.getItem("sessionId");
        let history = sessionStorage.getItem("history");
        sendRequest(sessionId, user, history, "Resumo");
    }
    if (buttonText == 'Finalizar Chamada') {
        tip.textContent = "Para dar continuidade com o projeto e receber um resumo detalhado, pressione o botão azul 'Resumo detalhado' no canto superior direito";
        callButton.innerText = 'Analisar Requisitos';
        callButton.style.backgroundColor = '#94333a';
        input.value = '';
        $("#chat-area").empty();
        sendRequest(null, user, null, null);
        input.placeholder = "Digite sua menssagem";
        input.disabled = false;
        callButton.disabled = false;
        sendButton.disabled = false;
    }
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
        let sessionId = sessionStorage.getItem("sessionId");

        if (sessionId == null || sessionId.trim() === '') {
            sendRequest(null, user, null, null);
        } else {
            let history = sessionStorage.getItem("history");
            sendRequest(sessionId, user, history, message);
        }

    }, 100);
}

/* --------Função para devolver as mensagens para a tela ---------*/

function sendMessage(message, sender) {
    // divide a mensagem em um array usando o separador "!!"
    const messages = message.split("!!");
    // junta todos os elementos do array usando a tag <br> para criar uma quebra de linha
    const fullMessage = messages.join("<br>");

    // código restante da função
    var sessionHistory = sessionStorage.getItem("sessionHistory");
    if (sessionHistory == null || sessionHistory.trim() === '') {
        sessionStorage.setItem("sessionHistory", `${sender}---${message}`);
    } else {
        sessionStorage.setItem("sessionHistory", `${sessionHistory}***${sender}---${message}`);
    }

    const chatBubble = document.createElement('div');
    chatBubble.classList.add('chat-bubble');
    chatBubble.innerHTML = fullMessage;
    if (message == "Analisando Requisitos") {
        chatBubble.classList.add('endcall');
    } else {
        chatBubble.classList.add(sender);
    }

    if (chatArea.children.length === 0) {
        chatBubble.classList.add('first');
    }

    chatArea.appendChild(chatBubble);
    chatArea.scrollTop = chatArea.scrollHeight;
}


/* --------Função para devolver o historico das mensagens para a tela ---------*/
function sendHistory(message, sender) { //recebe a mensagem a postar na tela e quem é o remetente
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

function sendRequest(sessionId, name, history, messsage) { //recebe a mensagem a postar na tela e quem é o remetente
    fetch('http://localhost:5126/api/ChatGpt/CreateComplementation', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            sessionId: sessionId ?? "",
            name: name,
            history: history ?? "",
            question: messsage ?? "",
        })
    })
        .then(response => response.json())
        .then(data => {
            const responseData = data.data;

            const customReturn = {
                transactionId: data.transactionId,
                failure: data.failure,
                data: responseData,
                errors: data.errors,
                code: data.code,
                date: new Date(data.date),
            };

            sessionStorage.setItem("sessionId", customReturn.data.sessionId);
            sessionStorage.setItem("name", customReturn.data.name);
            sessionStorage.setItem("history", customReturn.data.history);
            sessionStorage.setItem("message", customReturn.data.message);

            sendMessage(customReturn.data.message, "bot");
        })
        .catch(error => {
            console.error('Erro ao enviar mensagem para a API:', error);
        });
}
