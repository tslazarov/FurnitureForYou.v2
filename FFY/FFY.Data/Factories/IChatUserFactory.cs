using FFY.Models;

namespace FFY.Data.Factories
{
    public interface IChatUserFactory
    {
        ChatUser CreateChatUser(string email, string role);
    }
}
