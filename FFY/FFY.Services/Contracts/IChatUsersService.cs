using FFY.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFY.Services.Contracts
{
    public interface IChatUsersService
    {
        void AddChatUser(ChatUser user);

        void RemoveChatUser(ChatUser user);

        ChatUser GetChatUserByEmail(string email);

    }
}
