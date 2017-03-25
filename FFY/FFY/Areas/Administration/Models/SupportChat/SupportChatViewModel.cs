using FFY.Models;
using System.Collections.Generic;

namespace FFY.Web.Areas.Administration.Models.SupportChat
{
    public class SupportChatViewModel
    {
        public IEnumerable<ChatUser> ConnectedUsers { get; set; }
    }
}