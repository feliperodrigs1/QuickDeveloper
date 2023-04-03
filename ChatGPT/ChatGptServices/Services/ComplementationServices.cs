using ChatGptServices.Models;
using Newtonsoft.Json;
using OpenAI;
using OpenAI.Completions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatGptServices.Services
{
    public class ComplementationServices
    {
        public async Task<ResponseModel> Create(RequestModel request)
        {
            string apiKey = "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";
            var apiClient = new OpenAIClient(apiKey);

            var session = "";

            if (string.IsNullOrEmpty(request.SessionId.ToString()))
            {
                session = Guid.NewGuid().ToString();
            }
            else
            {
                session = request.SessionId.ToString();
            }

            string conversationHistory = "";

            if (string.IsNullOrEmpty(request.History))
            {
                conversationHistory = "O chatbot DevBot está coletando requisitos do cliente Matheus para o desenvolvimento de um software.\n";
            }
            else
            {
                conversationHistory = request.History;
            }

            var question = "";
            var prompt = "";

            if (string.IsNullOrEmpty(request.Question))
            {
                prompt = conversationHistory + "Chatbot: ";
                question = await GenerateQuestion(apiClient, prompt, session);

                return new ResponseModel
                {
                    SessionId = session,
                    History = conversationHistory,
                    Message = question,
                };
            }

            prompt = conversationHistory + "Você: " + request.Question + "\nChatbot: ";
            question = await GenerateQuestion(apiClient, prompt, session);

            return new ResponseModel
            {
                SessionId = session,
                History = conversationHistory + "Você: " + request.Question + "\n",
                Message = question,
            };
        }

        public async Task<string> GenerateQuestion(OpenAIClient apiClient, string prompt, string session)
        {
            var completionRequest = new CompletionRequest
            {
                Prompt = prompt,
                MaxTokens = 500,
                NumChoicesPerPrompt = 1,
                Temperature = 0.8,
                Model = "text-davinci-003",
                User = session
            };

            var completions = await apiClient.CompletionsEndpoint.CreateCompletionAsync(completionRequest);

            await GerarLog(Guid.Parse(session), completionRequest, completions, "Completion", false);

            return completions.Completions[0].Text.Trim();
        }
        public async Task GerarLog(Guid key, object request, object response, string method, bool useCompression)
        {
            var data = new Dictionary<string, object>();
            data.Add("Request", request);
            data.Add("Response", response);

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                Formatting = Formatting.None
            };
            string path = AppDomain.CurrentDomain.BaseDirectory + "/log";
            if (System.IO.Directory.Exists(path) == false)
            {
                System.IO.Directory.CreateDirectory(path);
            }
            path = string.Format("{0}/{1:yyyy-MM-dd}", path, DateTime.Now.Date);
            if (System.IO.Directory.Exists(path) == false)
            {
                System.IO.Directory.CreateDirectory(path);
            }
            var value = JsonConvert.SerializeObject(data, jsonSerializerSettings);
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(string.Format("{0}/{1}_{2:yyyy-MM-dd}_{3}.json", path, method, DateTime.Now, key), true))
            {
                if (useCompression)
                {
                    file.Write(ToCompress(value));
                }
                else
                {
                    file.Write(value);
                }
            }
        }
        public async Task<string> ToCompress(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return "";
            }

            byte[] inArray;
            await using (MemoryStream memoryStream2 = new MemoryStream(Encoding.UTF8.GetBytes(text)))
            {
                using MemoryStream memoryStream = new MemoryStream();
                using (DeflateStream destination = new DeflateStream(memoryStream, CompressionLevel.Optimal, leaveOpen: true))
                {
                    memoryStream2.CopyTo(destination);
                }

                inArray = memoryStream.ToArray();
            }

            return Convert.ToBase64String(inArray);
        }
        //public async Task<object> Create()
        //{
        //    string apiKey = "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";
        //    var apiClient = new OpenAIClient(apiKey);
        //    var session = Guid.NewGuid().ToString();
        //    Console.WriteLine("Olá! Sou um chatbot desenvolvido para ajudar a coletar requisitos para o desenvolvimento de um software. Por favor, responda às minhas perguntas.");

        //    string conversationHistory = "O chatbot DevBot está coletando requisitos do cliente Matheus para o desenvolvimento de um software.\n";

        //    while (true)
        //    {
        //        string prompt = conversationHistory + "Chatbot: ";
        //        string question = await GenerateQuestion(apiClient, prompt, session);
        //        Console.WriteLine($"Chatbot: {question}");

        //        Console.Write("Você: ");
        //        string userResponse = Console.ReadLine();

        //        if (userResponse.ToLower().Contains("fim"))
        //        {
        //            conversationHistory += $"Você: Me dê um resumo detalhado dos requisitos e finalize a conversa.\n";
        //            prompt = conversationHistory + "Chatbot: ";
        //            question = await GenerateQuestion(apiClient, prompt, session);
        //            Console.WriteLine($"Chatbot: {question}");
        //            Console.WriteLine("Obrigado por suas respostas! Encerrando a conversa.");
        //            break;
        //        } 
        //        else
        //        {
        //            conversationHistory += $"Você: {userResponse}\n";
        //        }
        //    }

        //    return new object();
        //}
    }
}
