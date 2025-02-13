namespace CursorProjects.Helpers
{
    public class Validator
    {
        public static bool MessageIsValid(ApiInputMessage message)
        {
            return message.Messages.All( x=>!string.IsNullOrEmpty(x.Text) && x.Text.Length <= 800);
        }
    }
}
