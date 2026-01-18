namespace BuildingBlocks.Common.Contracts.Messages
{
    /// <summary>
    /// Interface for future localization support
    /// </summary>
    public interface IMessageProvider
    {
        string GetMessage(string key, params object[] args);
    }

    public class MessageProvider : IMessageProvider
    {
        private readonly Dictionary<string, string> _messages = new()
        {
            ["Auth.LoginSuccess"] = ResponseMessages.Auth.LoginSuccess,
            ["Auth.LogoutSuccess"] = ResponseMessages.Auth.LogoutSuccess,
            // ... more messages
        };

        public string GetMessage(string key, params object[] args)
        {
            if (_messages.TryGetValue(key, out var message))
            {
                return args.Length > 0 ? string.Format(message, args) : message;
            }

            return "Message not found";
        }
    }
} 
