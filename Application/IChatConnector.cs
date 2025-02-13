using CursorProjects.Helpers;

public interface IChatConnector
{
    Task<string> GetChatResponse(ApiInputMessage message,
        CancellationToken cancellationToken = default);

    Task<AIResponseMessage> GetChatSecureResponse(IEnumerable<MessageChat> message,
        CancellationToken cancellationToken = default);
}