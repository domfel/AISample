
using CursorProjects;
using CursorProjects.Application;
using CursorProjects.Helpers;
using CursorProjects.Plugins;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.AzureOpenAI;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using OpenAI.Chat;
using System.ComponentModel;
using System.Text.Json;

public class ChatConnector : IChatConnector
{
    private readonly IOrderStore _orderStore;

    public ChatConnector(IOrderStore orderStore)
    {
        _orderStore = orderStore;

    }
    public async Task<string> GetChatResponse(
        ApiInputMessage messages,
        CancellationToken cancellationToken = default)
    {

        try
        {
#pragma warning disable SKEXP0001
            var kernelBuilder = Kernel.CreateBuilder()
 .AddAzureOpenAIChatCompletion(
     deploymentName: ChatConfigs.ChatGPT4o.DeploymentName,
     endpoint: ChatConfigs.ChatGPT4o.Endpoint,
     apiKey: ChatConfigs.ChatGPT4o.ApiKey
     );

            kernelBuilder.Services.AddDbContext<ApplicationDbContext>(options => { options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TechLunch;Trusted_Connection=True;MultipleActiveResultSets=true"); });
            kernelBuilder.Plugins.AddFromType<ListItems>();
            kernelBuilder.Plugins.AddFromType<PlaceOrder>();
            kernelBuilder.Services.AddScoped(typeof(IOrderStore), (provider) => { return _orderStore; });


            var kernel = kernelBuilder.Build();

            AzureOpenAIPromptExecutionSettings settings = new()
            {
                FunctionChoiceBehavior = FunctionChoiceBehavior.Auto(),
                MaxTokens = 1000,

            };

            var message = messages.Messages.ToAICall();

            var result = await kernel.InvokePromptAsync(message, new(settings), cancellationToken: cancellationToken);


            return result.ToString();


        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return ex.Message;
        }
    }

    public async Task<AIResponseMessage> GetChatSecureResponse(
        IEnumerable<MessageChat> messages,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _orderStore.Clear();

            var kernelBuilder = Kernel.CreateBuilder()
      .AddAzureOpenAIChatCompletion(
          deploymentName: ChatConfigs.ChatGPT4o.DeploymentName,
          endpoint: ChatConfigs.ChatGPT4o.Endpoint,
          apiKey: ChatConfigs.ChatGPT4o.ApiKey
          );
            kernelBuilder.Services.AddDbContext<ApplicationDbContext>(options => { options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TechLunch;Trusted_Connection=True;MultipleActiveResultSets=true"); });
            kernelBuilder.Services.AddScoped(typeof(IOrderStore), (provider) => { return _orderStore; });
            kernelBuilder.Plugins.AddFromType<ChatFunction2>();
            kernelBuilder.Plugins.AddFromType<ChatPlugins>();
            var kernel = kernelBuilder.Build();


#pragma warning disable SKEXP0001
            AzureOpenAIPromptExecutionSettings settings = new()
            {
                FunctionChoiceBehavior = FunctionChoiceBehavior.Auto(),
                MaxTokens = 1000,
                Temperature = 0.1,
                TopP = 1.0,
               // ChatSystemPrompt =" You are a sales assistant, you can avice customers about the funitures. You are not allowed under any condition to make deals with the clients"
            };
#pragma warning restore SKEXP0001

           // messages.Add(() => new MessageChat() { Text = "You are a sales assistant, you can avice customers about the funitures. You are not allowed under any condition to make deals with the clients", Sender = "user" });
            var message = messages.ToAICall();

            var result = await kernel.InvokePromptAsync(message, new(settings), cancellationToken: cancellationToken);

            if (_orderStore.IsEmpty())
            {
                return new AIResponseMessage()
                {
                    Text = result.ToString(),
                    HasOrder = false
                };
            }
            else
            {
                return new AIResponseMessage()
                {
                    Text = "Seems you are trying to put an order to the system,please confirm that this is your order",
                    HasOrder = true,
                    Orders = await _orderStore.GetOrders()
                };
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new AIResponseMessage()
            {
                Text = ex.Message,
                HasOrder = false
            };
        }
    }
}



