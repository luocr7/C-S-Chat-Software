using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CSChat_Sever
{
    public class Message
    {
        private String name;
        private String msg;
        private String objName;
        private int type;
        private String account;
        private String pwd;
        private String returnMsg;
        private List<String> friendList;
        private DateTime chatTime;
        private String hasRead;

        public Message()
        {
 
        }

        public string Name { get => name; set => name = value; }
        public string Msg { get => msg; set => msg = value; }
        public string ObjName { get => objName; set => objName = value; }
        public int Type { get => type; set => type = value; }
        public string Account { get => account; set => account = value; }
        public string Pwd { get => pwd; set => pwd = value; }
        public string ReturnMsg { get => returnMsg; set => returnMsg = value; }
        public List<string> FriendList { get => friendList; set => friendList = value; }
        public DateTime ChatTime { get => chatTime; set => chatTime = value; }
        public String HasRead { get => hasRead; set => hasRead = value; }
    }
}
