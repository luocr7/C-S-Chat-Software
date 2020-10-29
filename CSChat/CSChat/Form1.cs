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
    public partial class Form1 : Form
    {
        Client client;
        bool toRun = true;

        public Form1(ref Client client)
        {
            InitializeComponent();
            this.client = client;
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            Thread t = new Thread(run);
            t.IsBackground = true;
            t.Start();
        }

        private void run()
        {
            while (toRun)
            {
                if (client.MsgList.Count>0)
                {
                    show();
                    Thread.Sleep(10);
                }
                Thread.Sleep(10);
            }
        }

        private void show()
        {
            this.Invoke(new EventHandler(delegate
            {
                if (client != null)
                {
                   // String msg = client.MsgList.Dequeue();
                   // ChatBox.AppendText(client.GetMsg.Name + "给您发来了消息：" + msg + Environment.NewLine);
                }
            }));
        }

        private void EditChat_KeyDown(object sender, KeyEventArgs e)
       {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    //禁止回车后换行
                    e.SuppressKeyPress = true;
                    Message msg = new Message(0);
                    msg.Name = client.Name;
                    msg.Msg = EditChat.Text.ToString();
                    msg.ObjName = "余悸";
                    msg.Type = 0;
                    client.SendMsg(msg);
                    EditChat.Text = "";
                    break;
                default:
                    break;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            toRun = false;
            client.stop();         
        }
    }
}
