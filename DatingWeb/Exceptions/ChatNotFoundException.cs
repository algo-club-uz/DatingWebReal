namespace DatingWeb.Exceptions;

public class ChatNotFoundException: Exception
{
    public ChatNotFoundException(string message) : base($"Chat not found with this {message}")
    {

    }
}