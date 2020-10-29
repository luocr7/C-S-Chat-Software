using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSChat_Sever
{
    class User
    {
        private String account;
        private String password;
        private String chatName;

        public User(string account, string password, string chatName)
        {
            this.Account = account;
            this.Password = password;
            this.ChatName = chatName;
        }

        public string Account { get => account; set => account = value; }
        public string Password { get => password; set => password = value; }
        public string ChatName { get => chatName; set => chatName = value; }
    }
}
