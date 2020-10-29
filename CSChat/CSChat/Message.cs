using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CSChat
{
    public class Message
    {
        private String name;
        private String msg;
        private String objName;

        /// <summary>
        /// 消息类型,1为登录消息,2为注册消息，3查询用户信息，4添加聊天记录，5查询聊天记录，6查询历史记录，7更新已读状态，
        /// 8添加好友，9通过名字查找好友，10查询群聊记录，11转发群聊消息
        /// </summary>
        private int type;

        private String account;
        private String pwd;
        private String returnMsg;
        private List<String> friendList;
        private DateTime chatTime;
        private String hasRead;

        public Message(int type)
        {
            this.Type = type;
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
