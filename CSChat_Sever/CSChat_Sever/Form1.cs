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

namespace CSChat_Sever
{
    public partial class Form1 : Form
    {
        Server server;
        Thread t;
        bool toRun = true;
        public Form1(ref Server server)
        {
            InitializeComponent();
            this.server = server;           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            t = new Thread(run);
            t.IsBackground = true;
            t.Start();
        }

        private void run()
        {
            while (toRun)
            {
                if (server.MsgList.Count!=0)
                {
                    show();
                }
                Thread.Sleep(10);
            }
        }

        private void show()
        {
            this.Invoke(new EventHandler(delegate
            {
                if (server.MsgList.Count > 0)
                {
                    Message msg = server.MsgList.Dequeue();
                    if (msg.Type == 1)
                    {
                        msgBox.AppendText("客户端" + msg.Name + "发来登录请求" + Environment.NewLine);
                    }else if (msg.Type == 2)
                    {
                        msgBox.AppendText("客户端" + msg.Name + "发来注册请求" +  Environment.NewLine);
                    }else if (msg.Type == 3)
                    {
                        msgBox.AppendText("客户端" + msg.Name + "发来查询请求"+ Environment.NewLine);
                    }
                    else if (msg.Type == 4)
                    {
                        msgBox.AppendText("客户端" + msg.Name + "发来消息："+msg.Msg + Environment.NewLine);
                    }
                    else if (msg.Type == 5)
                    {
                        msgBox.AppendText("客户端" + msg.Name + "发来查询聊天记录请求" + Environment.NewLine);
                    }
                    else if (msg.Type ==6)
                    {
                        msgBox.AppendText("客户端" + msg.Name + "发来查询历史消息请求" + Environment.NewLine);
                    }
                    else if (msg.Type == 7)
                    {
                        msgBox.AppendText("客户端" + msg.Name + "发来更新已读状态请求" + Environment.NewLine);
                    }
                    else if (msg.Type == 8)
                    {
                        msgBox.AppendText("客户端" + msg.Name + "发来添加好友请求" + Environment.NewLine);
                    }
                    else if (msg.Type == 9)
                    {
                        msgBox.AppendText("客户端" + msg.Name + "发来查询好友请求" + Environment.NewLine);
                    }
                    else if (msg.Type == 10)
                    {
                        msgBox.AppendText("客户端" + msg.Name + "发来查询群聊请求" + Environment.NewLine);
                    }
                    else if (msg.Type == 11)
                    {
                        msgBox.AppendText("客户端" + msg.Name + "发来消息："+msg.Msg + Environment.NewLine);
                    }
                }
            }));
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            server.stop();
            toRun = false;
        }
    }
}
