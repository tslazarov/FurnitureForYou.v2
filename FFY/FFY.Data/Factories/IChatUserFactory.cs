using FFY.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFY.Data.Factories
{
    public interface IChatUserFactory
    {
        ChatUser CreateChatUser(string email, string role);
    }
}
