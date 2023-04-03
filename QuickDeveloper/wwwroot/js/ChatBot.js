$(document).keypress(function (e) {
    if (e.which == 13) $('#Send').click();
});

$(function SendMenssage() {
    $("#Send").click(
        function () {
            var menssage = $("#MenssageTxt").val();

            if (menssage != "") { 
                $("#Chatbot").append("Eu: " + menssage + "\n")
                $("#Chatbot").append("ChatBot: " + "Ainda estamos em fase de testes!"+"\n");
                $("#MenssageTxt").val("");
            }

        }
    );
});

