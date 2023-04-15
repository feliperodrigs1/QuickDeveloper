using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OpenAI;
using OpenAI.Completions;
using System.IO.Compression;
using System.Text;

namespace Services.Services
{
    public class ChatGptServices
    {
        private readonly IConfiguration _configuration;
        private readonly UtilitiesServices _utilities;

        public ChatGptServices(IConfiguration configuration)
        {
            _configuration = configuration;
            _utilities = new UtilitiesServices();
        }

        public async Task<Models.ComplementationResponse> CreateComplementation(Models.ComplementationRequest request)
        {
            string apiKey = _configuration?.GetSection("Api-Key")?.Value ?? throw new Exception("Não foi possível recuperar a API Key da OpenAI na AppSettings. Por favor, verificar!");
            OpenAIClient openAIClient = new OpenAIClient(apiKey);

            if (Guid.TryParse(request.SessionId, out Guid sessionId) == false)
            {
                sessionId = Guid.NewGuid();
            }

            string history = $"O chatbot DevBot está coletando requisitos do cliente {request.Name} para o desenvolvimento de um software.\n";
            if (string.IsNullOrEmpty(request.History) == false)
            {
                history = request.History;
            }

            string? question;
            string? prompt;

            if (string.IsNullOrEmpty(request.Question))
            {
                prompt = history + "Chatbot: ";
                question = await GenerateQuestion(openAIClient, prompt, sessionId);

                return new Models.ComplementationResponse
                {
                    SessionId = sessionId,
                    Name = request.Name,
                    History = history,
                    Message = question,
                };
            }
            if (request.Question == "fim")
            {
                prompt = history + $"{request.Name}: " + "Mê informe um resumo completo do software que solicitei." + "\nChatbot: ";
                question = await GenerateQuestion(openAIClient, prompt, sessionId);

                return new Models.ComplementationResponse
                {
                    SessionId = sessionId,
                    Name = request.Name,
                    History = history + $"{request.Name}: " + request.Question + "\n",
                    Message = question,
                };
            }
            prompt = history + $"{request.Name}: " + request.Question + "\nChatbot: ";
            question = await GenerateQuestion(openAIClient, prompt, sessionId);

            return new Models.ComplementationResponse
            {
                SessionId = sessionId,
                Name = request.Name,
                History = history + $"{request.Name}: " + request.Question + "\n",
                Message = question,
            };
        }
        private async Task<string> GenerateQuestion(OpenAIClient openAIClient, string prompt, Guid sessionId)
        {
            var request = new CompletionRequest
            {
                Prompt = prompt,
                MaxTokens = 500,
                NumChoicesPerPrompt = 1,
                Temperature = 0.8,
                Model = "text-davinci-003",
                User = sessionId.ToString()
            };

            var response = await openAIClient.CompletionsEndpoint.CreateCompletionAsync(request);

            _utilities.GerarLog(sessionId, request, response, "Completion", false);

            return response.Completions[0].Text.Trim();
        }
    }
}
