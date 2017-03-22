using FFY.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FFY.Web.Areas.Administration.Models.SupportChat
{
    public class SupportChatViewModel
    {
        public IEnumerable<ChatUser> ConnectedUsers { get; set; }
    }
}