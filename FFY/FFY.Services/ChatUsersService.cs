using FFY.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FFY.Models;
using FFY.Data.Contracts;
using Bytes2you.Validation;

namespace FFY.Services
{
    public class ChatUsersService : IChatUsersService
    {
        private readonly IFFYData data;

        public ChatUsersService(IFFYData data)
        {
            Guard.WhenArgument<IFFYData>(data, "Data cannot be null.")
                .IsNull()
                .Throw();

            this.data = data;
        }
        public void AddChatUser(ChatUser user)
        {
            Guard.WhenArgument<ChatUser>(user, "Chat user cannot be null.")
                .IsNull()
                .Throw();

            this.data.ChatUsersRepository.Add(user);
            this.data.SaveChanges();
        }

        public void RemoveChatUser(ChatUser user)
        {
            Guard.WhenArgument<ChatUser>(user, "Chat user cannot be null.")
                .IsNull()
                .Throw();

            // Save changes is not called, since it is called 
            // directly from the dbcontext in the method 
            this.data.ChatUsersRepository.ConnectionDelete(user);
        }

        public ChatUser GetChatUserByEmail(string email)
        {
            Guard.WhenArgument<string>(email, "Chat user email cannot be null or empty.")
                .IsNullOrEmpty()
                .Throw();

            return this.data.ChatUsersRepository.All()
                .FirstOrDefault(cu => cu.Email == email);
        }
    }
}
