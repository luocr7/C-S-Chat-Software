using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSChat_Sever
{
    class MsgService
    {
        ///<summary>
        ///数据库用户实例
        /// </summary>
        private UserDao userDao = new UserDao();

        ///<summary>
        ///数据库聊天内容实例
        /// </summary>
        private ChatRecordDao chatDao = new ChatRecordDao();

        /// <summary>
        /// 判断登录并返回馈消息
        /// <paramref name="msg"/>
        /// </summary>
        public Message loginJudge(Message msg)
        {
            Message returnMsg = new Message();
            if (!userDao.QueryAccount(msg.Account))
            {
                returnMsg.ReturnMsg = "AccountError";
                returnMsg.Type = 1;
            }
            else if (!userDao.QueryPwd(msg.Account, msg.Pwd))
            {
                returnMsg.ReturnMsg = "PwdError";
                returnMsg.Type = 1;
            }
            else
            {
                String name = userDao.QueryNameByAccount(msg.Account);
                returnMsg.ReturnMsg = "loginSuccess";
                returnMsg.Type = 1;
                returnMsg.Name = name;
            }
            return returnMsg;
        }

        ///<summary>
        ///注册账号服务以及消息反馈
        ///<paramref name="msg"/>
        /// </summary>
        public Message registerUser(Message msg)
        {
            Message returnMsg = new Message();
            if (userDao.QueryAccount(msg.Account))
            {
                returnMsg.ReturnMsg = "AccountExist";
            }
            else if (userDao.QueryName(msg.Name))
            {
                returnMsg.ReturnMsg = "NameExist";
            }
            else
            {
                userDao.InsertUser(msg.Account, msg.Pwd, msg.Name);
                returnMsg.ReturnMsg = "registerSuccess";
            }
            returnMsg.Type = 2;
            return returnMsg;
        }

        /// <summary>
        /// 查询用户相关信息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public Message queryUserInfo(Message msg)
        {
            Message returnMsg = new Message();
            returnMsg.FriendList=userDao.QueryFriendNameByName(msg.Name);
            returnMsg.Type = 3;
            return returnMsg;
        }

        /// <summary>
        /// 存储聊天内容
        /// </summary>
        /// <param name="msg"></param>
        public void AddChatRecord(Message msg)
        {
            chatDao.AddRecord(msg);
        }

        /// <summary>
        /// 查询聊天记录
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public Queue<Message> QueryChat(Message msg)
        {
            return chatDao.QueryChatRecord(msg);
        }

        /// <summary>
        /// 查询群聊消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public Queue<Message> QueryGroupChat(Message msg)
        {
            return chatDao.QueryGroupChatRecord(msg);
        }

        /// <summary>
        /// 查询历史消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public Queue<Message> QueryHistoryChat(Message msg)
        {
            return chatDao.QueryHistoryChat(msg);
        }

        /// <summary>
        /// 更新已读状态
        /// </summary>
        /// <param name="msg"></param>
        public void UpdateHasRead(Message msg)
        {
            chatDao.UpdateHasRead(msg);
        }
        
        /// <summary>
        /// 查询是否存在网游
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public Message queryFriendByName(Message msg)
        {
            Message returnMsg = new Message();
            returnMsg.FriendList = userDao.queryFriendByName(msg.Name);
            returnMsg.Type = 9;
            return returnMsg;
        }

        /// <summary>
        /// 添加好友
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public Message addFriend(Message msg)
        {
            return userDao.addFriend(msg);
        }
    }
}
