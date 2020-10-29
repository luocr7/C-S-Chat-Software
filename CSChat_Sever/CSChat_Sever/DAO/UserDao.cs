using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSChat_Sever
{
    class UserDao
    {
        /// <summary>
        /// 数据库帮手
        /// </summary>
        private DBHelper dB = new DBHelper();

        /// <summary>
        /// 添加一个用户
        /// </summary>
        /// <param name="account"></param>
        /// <param name="pwd"></param>
        /// <param name="name"></param>
        public void InsertUser(String account, String pwd, String name)
        {
            dB.ExecuteUpdate("insert into userinfo values (" + "'" + account + "'" + "," + "'" + pwd + "'" + "," + "'" + name + "'" + ")");
        }

        /// <summary>
        /// 查询账号是否存在
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public bool QueryAccount(String account)
        {
            DataTable dt = dB.ExecuteQuery("select * from userinfo where account=" +"'" +account+"'");
            if (dt != null && dt.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 查询网名是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool QueryName(String name)
        {
            DataTable dt = dB.ExecuteQuery("select * from userinfo where name=" + "'" + name + "'");
            if (dt != null && dt.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 查询密码是否正确
        /// </summary>
        /// <param name="account"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public bool QueryPwd(String account, String pwd)
        {
            DataTable dt = dB.ExecuteQuery("select * from userinfo where account=" +"'"+ account+"'" + " and password=" +"'"+ pwd+"'");
            if (dt != null && dt.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 通过账号查找名字
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public String QueryNameByAccount(String account)
        {
            DataTable dt = dB.ExecuteQuery("select * from userinfo where account = " + account);
            DataRow row = dt.Rows[0];
            return row["name"].ToString();
        }

        /// <summary>
        /// 通过名字查找好友
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<String> QueryFriendNameByName(String name)
        {
            DataTable dt = dB.ExecuteQuery("select * from friend where name=" + "'" + name + "'");
            List<String> friendlist = new List<string>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach(DataRow dr in dt.Rows)
                {
                    friendlist.Add(dr["friendName"].ToString());
                }
            }
            return friendlist;
        }

        /// <summary>
        /// 通过网名查询是否存在用户
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public List<String> queryFriendByName(String name)
        {
            DataTable dt= dB.ExecuteQuery("select * from UserInfo where name=" + "'" + name + "'");
            List<String> friendlist = new List<string>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    friendlist.Add(dr["name"].ToString());
                }
            }
            return friendlist;
        }

        /// <summary>
        /// 添加好友
        /// </summary>
        /// <param name="msg"></param>
       public Message addFriend(Message msg)
        {
            List<String> hasFriend = QueryFriendNameByName(msg.Name);
            Message returnMsg = new Message();
            returnMsg.ReturnMsg = "";
            for(int i=0;i<hasFriend.Count;i++)
            {
                if (hasFriend[i].Equals(msg.ObjName)) {
                    returnMsg.ReturnMsg = "exist";
                }
            }
            if(!returnMsg.ReturnMsg.Equals("exist"))
            {
                dB.ExecuteUpdate("insert into friend values (" + "'" + msg.Name + "'," + "'" + msg.ObjName + "')");
                dB.ExecuteUpdate("insert into friend values (" + "'" + msg.ObjName + "'," + "'" + msg.Name + "')");
                returnMsg.ReturnMsg = "success";
            }
            returnMsg.Name = msg.Name;
            returnMsg.ObjName = msg.ObjName;
            returnMsg.Type = msg.Type;
            return returnMsg;
        }

    }
}
