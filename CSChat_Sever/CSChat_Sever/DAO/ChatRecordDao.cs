using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSChat_Sever
{
    class ChatRecordDao
    {
        /// <summary>
        /// 数据库帮手
        /// </summary>
        private DBHelper dB = new DBHelper();

        /// <summary>
        /// 添加聊天记录
        /// </summary>
        /// <param name="msg"></param>
        public void AddRecord(Message msg)
        {
            dB.ExecuteUpdate("insert into Chat_Content values (" + "'" + msg.Name + "'" + "," + "'" + msg.ObjName + "'" + "," + "'" + msg.Msg + 
                "'"+","+"'"+msg.ChatTime+"',0)");
        }

        /// <summary>
        /// 查询聊天记录
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public Queue<Message> QueryChatRecord(Message msg)
        {
            DataTable dt = dB.ExecuteQuery("select * from Chat_Content where (name=" + "'" + msg.Name + "'" + " and objname=" + "'" + msg.ObjName + "'" +
                ") or " + "(name=" + "'"+msg.ObjName+"'"+" and objname="+"'"+msg.Name+"'"+")");
            Queue<Message> msgList = new Queue<Message>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Message record = new Message();
                    record.Name = dr["name"].ToString();
                    record.ObjName = dr["objname"].ToString();
                    record.Msg = dr["chatContent"].ToString();
                    record.ChatTime = (DateTime)dr["time"];
                    record.Type = 5;
                    msgList.Enqueue(record);
                }
            }
            return msgList;
        }

        /// <summary>
        /// 查询群聊消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public Queue<Message> QueryGroupChatRecord(Message msg)
        {
            DataTable dt = dB.ExecuteQuery("select * from Chat_Content where (objname=" + "'" + msg.ObjName + "'" + ")");
            Queue<Message> msgList = new Queue<Message>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Message record = new Message();
                    record.Name = dr["name"].ToString();
                    record.ObjName = dr["objname"].ToString();
                    record.Msg = dr["chatContent"].ToString();
                    record.ChatTime = (DateTime)dr["time"];
                    record.Type = 5;
                    msgList.Enqueue(record);
                }
            }
            return msgList;
        }

        /// <summary>
        /// 查询历史消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public Queue<Message> QueryHistoryChat(Message msg)
        {
            DataTable dt = dB.ExecuteQuery("select * from Chat_Content where (objname=" + "'" + msg.Name + "'" +")");
            Queue<Message> msgList = new Queue<Message>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Message record = new Message();
                    record.Name = dr["name"].ToString();
                    record.ObjName = dr["objname"].ToString();
                    record.Msg = dr["chatContent"].ToString();
                    record.HasRead = dr["hasRead"].ToString(); ;
                    record.ChatTime = (DateTime)dr["time"];
                    record.Type = 6;
                    msgList.Enqueue(record);
                }
            }
            return msgList;
        }

        /// <summary>
        /// 更新已读状态
        /// </summary>
        /// <param name="msg"></param>
        public void UpdateHasRead(Message msg)
        {
            dB.ExecuteUpdate("update chat_content set hasRead=1 where name=" + "'" + msg.Name + "' and " + "objname=" + "'" + msg.ObjName + "'");
        }
    }
}
