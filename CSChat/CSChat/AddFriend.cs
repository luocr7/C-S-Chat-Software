using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSChat
{
    public partial class AddFriend : Form
    {
        Client client;
        String name;
        bool toRun = true;
        public AddFriend(Client client,String name)
        {
            InitializeComponent();
            this.client = client;
            this.name = name;
        }

        private void AddFriend_Load(object sender, EventArgs e)
        {

        }

        private void queryBtn_Click(object sender, EventArgs e)
        {
            queryResultBox.Text = "";
            String name = nameBox.Text.ToString();
            if (!name.Equals(this.name))
            {
                Message msg = new Message(9);
                msg.Name = name;
                client.SendMsg(msg);
                while (toRun)
                {
                    //多次判断可用异常替代
                    if (client.AddFriendMsg != null)
                    {
                        if (client.AddFriendMsg.FriendList != null)
                        {
                            if (client.AddFriendMsg.FriendList.Count > 0)
                            {
                                queryResultBox.Text = client.AddFriendMsg.FriendList[0];
                                addBtn.Visible = true;
                                break;
                            }
                            else
                            {
                                queryResultBox.Text = "查无此用户!";
                                addBtn.Visible = false;
                                break;
                            }
                        }
                    }
                    Thread.Sleep(10);
                }
                queryResultBox.Visible = true;
            }
            else
            {
                MessageBox.Show("不可添加自己!");
            }
                
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            String friendName = queryResultBox.Text.ToString();
            Message msg = new Message(8);
            msg.Name = name;
            msg.ObjName = friendName;
            client.SendMsg(msg);
            while (toRun)
            {
                if (client.AddFriendMsg != null)
                {
                    if (client.AddFriendMsg.ReturnMsg != null)
                    {
                        if (client.AddFriendMsg.ReturnMsg.Equals("success"))
                        {
                            MessageBox.Show("添加成功!");
                            //client.AddFriendMsg.ReturnMsg = "";
                            break;
                        }
                        else
                        {
                            MessageBox.Show("好友已存在!");
                            //client.AddFriendMsg.ReturnMsg = "";
                            break;
                        }
                        
                    }
                   
                }
                Thread.Sleep(10);
            }
        }

        private void AddFriend_FormClosing(object sender, FormClosingEventArgs e)
        {
            toRun = false;
        }
    }
}
