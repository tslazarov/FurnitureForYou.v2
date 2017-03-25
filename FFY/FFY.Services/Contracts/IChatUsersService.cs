using FFY.Models;
using System.Collections.Generic;

namespace FFY.Services.Contracts
{
    public interface IChatUsersService
    {
        void AddChatUser(ChatUser user);

        void RemoveChatUser(ChatUser user);

        IEnumerable<ChatUser> GetChatUsers();

        ChatUser GetChatUserByEmail(string email);

    }
}
