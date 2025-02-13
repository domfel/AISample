namespace CursorProjects.Helpers
{
    public class ApiInputMessage
    {
        public List<MessageChat> Messages { get; set; }
    }

    public class MessageChat
    {
        public string Text { get; set; }
        public string Sender { get; set; }
        public long Id { get; set; } 
    }

    public static class MessagesExteniosns
    {
        public static string ToAICall(this IEnumerable<MessageChat> messages)
        {
            return string.Join(" ", messages.Select(x=> $"""<message role="{x.Sender}">{x.Text}</message>\r\n"""));
        }
    }
}
