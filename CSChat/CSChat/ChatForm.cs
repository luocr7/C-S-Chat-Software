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
    public partial class ChatForm : Form
    {
        private String friendname;
        private Client client;
        private String name;
        private List<Message> chatList = new List<Message>();
        private bool toRun = true;

        public ChatForm(ref Client client,String friendName,String name)
        {
            InitializeComponent();
            friendname = friendName;
            this.client = client;
            this.name = name;
        }

        private void ChatForm_Load(object sender, EventArgs e)
        {
            Text = friendname;
            if (!friendname.Equals("群聊"))
            {
                Message msg = new Message(5);
                msg.Name = name;
                msg.ObjName = friendname;
                //发送消息，获取历史消息
                client.SendMsg(msg);
            }
            else
            {
                Message msg = new Message(10);
                msg.Name = name;
                msg.ObjName = friendname;
                //发送消息，获取历史消息
                client.SendMsg(msg);
            }
            //发送更新指令，表示已查看消息
            Message updateMsg = new Message(7);
            updateMsg.Name = friendname;
            updateMsg.ObjName = name;
            client.SendMsg(updateMsg);
            //监听消息
            Thread msgThead = new Thread(getMsgThread);
            msgThead.Start();

        }

        private void input_chatText_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    String chatContent = input_chatText.Text.ToString();
                    if (!friendname.Equals("群聊"))
                    {
                        Message msg = new Message(4);
                        msg.ObjName = friendname;
                        msg.Msg = chatContent;
                        msg.Name = name;
                        msg.ChatTime = DateTime.Now;
                        client.SendMsg(msg);
                    }
                    else
                    {
                        Message msg = new Message(11);
                        msg.ObjName = friendname;
                        msg.Msg = chatContent;
                        msg.Name = name;
                        msg.ChatTime = DateTime.Now;
                        client.SendMsg(msg);
                    }
                    input_chatText.Text = "";
                    input_chatText.Select(0, 0);
                    chat_text.SelectionAlignment = HorizontalAlignment.Right;
                    chat_text.SelectionColor = Color.DeepSkyBlue;
                    chat_text.AppendText(name + " " + DateTime.Now.ToString() + Environment.NewLine + chatContent + Environment.NewLine);
                    break;
                default:break;
            }          
        }

        /// <summary>
        /// 获取消息并显示文本
        /// </summary>
        private void getMsgThread()
        {
            while (toRun)
            {
                if (client.HistoryMsg.Count > 0)
                {
                    //历史消息
                    Message msg = client.HistoryMsg.Dequeue();
                    if (client.ChatList.Count > 0)
                    {
                        client.ChatList.Dequeue();
                    }
                    addChatToForm(msg);

                }
                else if (client.ChatList.Count > 0)
                {
                    //Message msg = client.MsgList.Dequeue();
                    //实时聊天消息
                    Message msg = client.ChatList.Dequeue();
                    addChatToForm(msg);
                }
                Thread.Sleep(10);
            }
        }

        /// <summary>
        /// 添加消息到窗体
        /// </summary>
        /// <param name="msg"></param>
        private void addChatToForm(Message msg)
        {
            this.Invoke(new EventHandler(delegate
            {
                if (msg.Name.Equals(name))
                {
                    chat_text.SelectionAlignment = HorizontalAlignment.Right;
                    chat_text.SelectionColor = Color.DeepSkyBlue;
                }
                else
                {
                    chat_text.SelectionAlignment = HorizontalAlignment.Left;
                    chat_text.SelectionColor = Color.ForestGreen;
                }
                chat_text.AppendText(msg.Name + "  " + msg.ChatTime + Environment.NewLine + msg.Msg + Environment.NewLine);
            }));

        }

        private void ChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            toRun = false;
            //发送更新指令，表示已查看消息
            Message updateMsg = new Message(7);
            updateMsg.Name = friendname;
            updateMsg.ObjName = name;
            client.SendMsg(updateMsg);
        }
    }
}
