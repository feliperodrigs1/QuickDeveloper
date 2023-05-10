const chatArea = document.getElementById('chat-area');
const input = document.getElementById('input');
const sendButton = document.getElementById('send-button');
const callButton = document.getElementById('callbutton');
const button = document.querySelector('.chat-button');
const tip = document.getElementById("text-finalization");
var user = document.getElementById("username").value;
var description = "";
var messagelast = "";
var userCompetence = "";

/---------- Adiciona um listener ao evento DOMContentLoaded ----------/
document.addEventListener('DOMContentLoaded', startChat);

/* --------Função para carregar mensagens de boas-vindas---------*/
async function startChat() {
    input.placeholder = "Aguarde um momento";
    input.disabled = true;
    callButton.disabled = true;
    sendButton.disabled = true;

    let sessionId = sessionStorage.getItem("sessionId");

    if (sessionId == null || sessionId.trim() === '') {
        await sendRequest(null, user, null, null);
    } else {
        let sessionHistory = sessionStorage.getItem("sessionHistory");

        var messages = sessionHistory.split('*');
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
        input.placeholder = "Digite sua mensagem";
    }, 3000);
}

async function Analyze() {
    tip.textContent = "Para dar continuidade com o projeto e receber um resumo detalhado, pressione o botão azul 'Resumo Técnico' no canto superior direito";
    callButton.innerText = 'Resumo Técnico';
    callButton.style.backgroundColor = '#303457';
    input.value = '';
    sendMessage("Analisando Requisitos", 'bot');
    sendMessage("Aqui está alguns softwares ja existentes no mercado que podem ajudar com seu problema", 'bot');
    input.placeholder = "Chamada encerrada";
    input.disabled = true;
    callButton.disabled = false;
    sendButton.disabled = true;

    let sessionId = sessionStorage.getItem("sessionId");
    let history = sessionStorage.getItem("history");
    await sendRequest(sessionId, user, history, "Verificar");

    callButton.disabled = false;
};

async function Resume() {
    chatArea.innerHTML = "";
    callButton.innerText = 'Recrutar Devs';
    tip.textContent = "Para recrutar desenvolvedores, pressione o botão azul 'Recrutar Desenvolvedores' no canto superior direito";
    callButton.style.backgroundColor = '#303457';
    input.value = '';
    sendMessage('Gerando Resumo Técnico', 'bot');
    let sessionId = sessionStorage.getItem("sessionId");
    let history = sessionStorage.getItem("history");
    await sendRequest(sessionId, user, history, "Resumo");
};


async function RecDevs() {
    let sessionId = sessionStorage.getItem("sessionId");
    let history = sessionStorage.getItem("history");
    await sendRequest(sessionId, user, history, "lista");

    sendMessage('Escolha quem fara parte de sua equipe de desenvolvimento', 'bot');

    await sendRequest(sessionId, user, history, "Resumo detalhado");

    $.ajax({
        url: "/Chat/ReturnUsersByCompetence",
        type: "POST",
        data: { userCompetence },
        success: function (listUsers) {
            for (let i = 0; i < listUsers.length; i++) {
                FindDevs(listUsers[i]["id"], listUsers[i]["username"], listUsers[i]["competences"], listUsers[i]["aditionalInfo"]);
            }
        }
    });

    tip.textContent = "Para encerrar a chamada e iniciar uma nova, pressione o botão verde 'Finalizar Chamada' no canto superior direito";
    callButton.innerText = 'Finalizar Chamada';
    callButton.style.backgroundColor = '#057507';
    input.value = ''; 
};



async function EndCall() {
    tip.textContent = "Para finalizar a coleta de requesitos, pressione o botão vermelho no canto superior direito";
    callButton.innerText = 'Analisar Requisitos';
    callButton.style.backgroundColor = '#94333a';
    input.placeholder = 'Digite sua mensagem';
    chatArea.innerHTML = "";
    await sendRequest(null, user, null, null);
    input.disabled = false;
};


/---------- Evento de clicar no botão encerrar chamada--------/
callButton.addEventListener('click', function () {
    var buttonText = callButton.textContent;

    if (buttonText == 'Analisar Requisitos') {
        Analyze();
    }
    if (buttonText == 'Resumo Técnico') {
        Resume();
    }
    if (buttonText == 'Recrutar Devs') {
        RecDevs();
    }
    if (buttonText == 'Finalizar Chamada') {
        EndCall();
    }
});

function FindDevs(vid, vname, vcompetences, vinfos) {
    const idDeveloper = vid;
    const name = vname;
    const competences = vcompetences;
    const info = vinfos;

    const div = document.createElement('div');
    div.innerHTML = `<span class="recruitMessage">Desenvolvedor: ${name}<br>Competências: ${competences}<br><br>Informações Adicionais: ${info}<br></span><button class="recruit" id="dev-${idDeveloper}">Recrutar</button>`;

    const message = div.outerHTML;
    sendMessage(message, 'bot');

    const recruitButton = document.getElementById(`dev-${idDeveloper}`);
    recruitButton.addEventListener('click', () => {        
        $.ajax({
            url: "/Chat/RegisterRequisition",
            type: "POST",
            data: { idDeveloper, description, idUser },
            success: function (result){
                if (result == true) {
                    sendMessage('Requisição cadastrada com sucesso, aguarde que o desenvolvedor selecionado irá entrar em contato com você!', 'bot');
                } else {
                    sendMessage('Tivemos um problema ao cadastrar sua requisição, tente novamente!', 'bot');
                }
            }
        });
    });
}

/*---------- Evento de clicar no botão de enviar --------*/
sendButton.addEventListener('click', sendChat);
/*---------- Evento pressionar enter e enviar --------*/
input.addEventListener('keydown', function (event) {
    if (event.key === 'Enter') {
        sendChat();
    }
});
/--------- Função capturar a mensagem e enviar para o chat --------------/
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
    messagelast = message;
    var sessionHistory = sessionStorage.getItem("sessionHistory");
    if (sessionHistory == null || sessionHistory.trim() === '') {
        sessionStorage.setItem("sessionHistory", `${sender}---${message}`);
    } else {
        sessionStorage.setItem("sessionHistory", `${sessionHistory}*${sender}---${message}`);
    }

    const chatBubble = document.createElement('div');
    chatBubble.classList.add('chat-bubble');
    chatBubble.innerHTML = message;
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
    chatArea.scrollTop = chatArea.scrollHeight; //faz a barra de rolagem acompanhar a ultima mensagem digitada
}

async function sendRequest(sessionId, name, history, message) { //recebe a mensagem a postar na tela e quem é o remetente
    await fetch('http://localhost:5126/api/ChatGpt/CreateComplementation', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            sessionId: sessionId ?? "",
            name: name,
            history: history ?? "",
            question: message ?? "",
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

            if (message === "lista") {
                userCompetence = customReturn.data.message;
            } else if (message === "Resumo detalhado"){
                description = customReturn.data.message;
            }
            
            sendMessage(customReturn.data.message, "bot");
        })
        .catch(error => {
            console.error('Erro ao enviar mensagem para a API:', error);
            return false
        });
}