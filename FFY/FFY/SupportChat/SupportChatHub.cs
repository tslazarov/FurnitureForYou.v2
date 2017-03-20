using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FFY.Web.SupportChat
{
    public class SupportChatHub : Hub
    {
        public void Send(string name, string message)
        {
            var context = this.Context;
            var clients = this.Clients;
            this.Clients.All.addMessage(name, message);
        }
    }
}