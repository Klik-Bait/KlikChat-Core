using MessageCore.Contracts;

namespace MessagesCore.ClientReceiver.Messaging.Publishers;

public interface IMessageReceiver
{
    Task<bool> terminal
        ChatMessage chatMessage);
}