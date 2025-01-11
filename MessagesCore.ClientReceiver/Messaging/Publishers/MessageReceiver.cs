using MassTransit;
using MessageCore.Contracts;

namespace MessagesCore.ClientReceiver.Messaging.Publishers;

public class MessageReceiver(IPublishEndpoint publishEndpoint, ILogger<MessageReceiver> logger): IMessageReceiver
{
    public async Task<bool> PublishChatMessageAsync(ChatMessage chatMessage)
    {
        try
        {
            await publishEndpoint.Publish(chatMessage);
            return true;
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
            return false;
        }
    }
}