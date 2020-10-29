using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSChat
{
    public partial class FriendList : Form
    {
        private const int CS_SHADOW = 0x20000;
        private const int GCL_STYLE = (-26);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SetClassLong(IntPtr hwnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassLong(IntPtr hwnd, int nIndex);
        /// <summary>
        /// 窗体位置
        /// </summary>
        Point windowPoint;

        ///<summary>
        ///客户端实例
        /// </summary>
        Client client;

        /// <summary>
        /// 是否展开好友列表
        /// </summary>
        bool extendFriend = false;

        ///<summary>
        ///是否展开群聊列表
        /// </summary>
        bool extendGroup = false;

        /// <summary>
        /// 头像集合
        /// </summary>
        List<PictureBox> headImgList = new List<PictureBox>();

        ///<summary>
        ///好友名字集合
        /// </summary>
        List<Label> friendNameList = new List<Label>();

        ///<summary>
        ///未展开图标
        /// </summary>
        Image notextendIcon = Image.FromFile("icons/qianjin.png");

        ///<summary>
        ///展开图标
        /// </summary>
        Image extendedIcon = Image.FromFile("icons/extend.png");

        /// <summary>
        /// 好友列表子控件
        /// </summary>
        Panel childPanel = new Panel();


        /// <summary>
        /// 消息列表子控件
        /// </summary>
        Panel msgChildPanel = new Panel();

        /// <summary>
        /// 好友列表子控件高度
        /// </summary>
        int friendPanelHeight = 0;

        ///<summy>
        ///好友信息
        /// </summy>
        List<String> friendMsg;

        /// <summary>
        /// 我的网名
        /// </summary>
        private String name;

        /// <summary>
        /// 消息提示标志图片
        /// </summary>
        PictureBox msgTip = new PictureBox();

        /// <summary>
        /// 历史消息列表
        /// </summary>
        List<Message> historyList = new List<Message>();

        /// <summary>
        /// 控制线程循环
        /// </summary>
        bool toRun = true;

        /// <summary>
        /// 消息窗口子控件高度
        /// </summary>
        int childHeight = 20;

        public FriendList(ref Client client,String name)
        {
            InitializeComponent();
            SetClassLong(this.Handle, GCL_STYLE, GetClassLong(this.Handle, GCL_STYLE) | CS_SHADOW);
            this.client = client;
            userName.Text = name;
            Message msg = new Message(3);
            msg.Name = name;
            this.name = name;
            //发送消息查询好友数据
            client.SendMsg(msg);
            client.ReceiveBackMsg();
            friendMsg = client.ReturnMsg.FriendList;
            client.ReceiveThread();
            //发送消息查看是否有历史消息
            Message histroyMsg = new Message(6);
            histroyMsg.Name = name;
            client.SendMsg(histroyMsg);
            //隐藏消息窗口
            msgChildPanel.Visible = false;
        }

        private void FriendList_Load(object sender, EventArgs e)
        {
            showFriend();
            childPanel.AutoScroll = true;
            msgChildPanel.AutoScroll = true;
            myFriendPanel.Controls.Add(childPanel);
            childPanel.Width = myFriendPanel.Width;
            myFriendPanel.Controls.Add(msgChildPanel);
            msgChildPanel.Width = myFriendPanel.Width;
            //消息提醒标志
            msgTip.BackgroundImage = Image.FromFile("icons/msgtip.png");
            msgTip.BackgroundImageLayout = ImageLayout.Stretch;
            msgTip.Width = 20;
            msgTip.Height = 20;
            msgTip.Location = new Point(20, 20);
            msgTip.BackColor = Color.Transparent;
            msgTip.Location = new Point(20, 5);
            msgTip.Visible = false;
            msgBtn.Controls.Add(msgTip);
            //监听实时消息
            MonitorNowChat();
            //群组双击事件注册
            groupChatBtn.DoubleClick += new EventHandler(Group_DoubleClick);
        }


        /// <summary>
        /// 显示好友列表
        /// </summary>
        private void showFriend()
        {
            childPanel.Controls.Clear();
            friendNameList.Clear();
            headImgList.Clear();
            friendPanelHeight = 0;
            //好友位置显示
            childPanel.Height = 0;
            int initLoc = myfriendBtn.Location.Y + 10;
            groupChatBtn.Location = new Point(0, myfriendBtn.Location.Y + 30);
            for (int i = 0; i < friendMsg.Count; i++)
            {
                PictureBox pic = new PictureBox();
                Label name = new Label();
                name.Text = friendMsg[i];
                name.Location = new Point(70, initLoc + 20);
                name.Font = new Font("Thonburi", 12);
                friendNameList.Add(name);
                if (i % 2 != 0)
                {
                    pic.BackgroundImage = Image.FromFile("timg.png");
                }
                else
                {
                    pic.BackgroundImage = Image.FromFile("icons/car.png");
                }
                pic.BackgroundImageLayout = ImageLayout.Stretch;
                pic.Width = 40; pic.Height = 30;
                pic.Location = new Point(20, initLoc + 20);
                friendPanelHeight += 70;
                headImgList.Add(pic);
                initLoc += 40;
            }
            for (int i = 0; i < friendMsg.Count; i++)
            {
                childPanel.Controls.Add(friendNameList[i]);
                childPanel.Controls.Add(headImgList[i]);
            }
            
            //绑定双击事件
            for (int i = 0; i < friendNameList.Count; i++)
            {
                friendNameList[i].DoubleClick += new EventHandler(Friend_DoubleClick);
                headImgList[i].DoubleClick += new EventHandler(Friend_DoubleClick);
            }
        }

        private void FriendList_MouseDown(object sender, MouseEventArgs e)
        {
            //鼠标点击时记录鼠标位置
            windowPoint = new Point(-e.X, -e.Y);
        }

        private void FriendList_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //鼠标位置
                Point p2 = MousePosition;
                p2.Offset(windowPoint);
                DesktopLocation = p2;
            }
        }

        /// <summary>
        /// 双击好友弹出聊天窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
       private void Friend_DoubleClick(object sender,EventArgs args)
        {
            //防止重复显示聊天记录
            while (client.ChatList.Count > 0)
            {
                client.ChatList.Dequeue();
            }
            Label friend_name = (Label)sender;
            msgTip.Visible = false;
            ChatForm chatForm = new ChatForm(ref client,friend_name.Text.ToString(),name);
            chatForm.Show();
        }

        /// <summary>
        /// 群组双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void Group_DoubleClick(object sender,EventArgs args)
        {
            while (client.ChatList.Count > 0)
            {
                client.ChatList.Dequeue();
            }
            Label friend_name = (Label)sender;
            msgTip.Visible = false;
            ChatForm chatForm = new ChatForm(ref client, friend_name.Text.ToString(), name);
            chatForm.Show();
        }


        /// <summary>
        /// 绘图事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FriendList_Paint(object sender, PaintEventArgs e)
        {
            //用渐变色填充上部矩形
            Rectangle rec = new Rectangle(new Point(0, 0), new Size(347,100));
            LinearGradientBrush brush = new LinearGradientBrush(rec,
            Color.Turquoise, Color.DeepSkyBlue, LinearGradientMode.Horizontal);
            e.Graphics.FillRectangle(brush, rec);
            Pen pen1 = new Pen(Color.DeepSkyBlue);
            Pen pen2 = new Pen(Color.Gray);
            this.CreateGraphics().DrawLine(pen1, new Point(130, 130), new Point(365, 130));
            this.CreateGraphics().DrawLine(pen2, new Point(0, 130), new Point(130, 130));
            relationBtn.ForeColor = Color.DeepSkyBlue;
            msgBtn.ForeColor = Color.Black;
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
            toRun = false;
            client.stop();
        }

        private void closeBtn_MouseEnter(object sender, EventArgs e)
        {
            closeBtn.BackColor = Color.Red;
        }

        /// <summary>
        /// 关闭窗口按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeBtn_MouseLeave(object sender, EventArgs e)
        {
            closeBtn.BackColor = Color.Transparent;
        }


        /// <summary>
        /// 最小化按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }


        /// <summary>
        /// 群组按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void groupChat_Click(object sender, EventArgs e)
        {
            //childPanel.AutoScroll = true;
            //childPanel.Location = new Point(0, groupChatBtn.Location.Y + 10);
            //extendGroup = !extendGroup;
            //if (extendGroup)
            //{
            //    childPanel.Height = 300;
            //    childPanel.Width = myFriendPanel.Width;

            //}
            //else
            //{
            //    childPanel.Height = 0;

            //}
        }


        /// <summary>
        /// 好友列表按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void myfriendBtn_Click(object sender, EventArgs e)
        {
            //是否展开
            extendFriend = !extendFriend;
            if (extendFriend)
            {
                childPanel.Refresh();
                extendIcon.BackgroundImage = extendedIcon;
                childPanel.Height = friendPanelHeight;
                groupChatBtn.Location = new Point(0, childPanel.Location.Y + childPanel.Height + 10);
            }
            else
            {
                extendIcon.BackgroundImage = notextendIcon;
                childPanel.Height = 0;
                groupChatBtn.Location = new Point(10, myfriendBtn.Location.Y +30);
            }
        }

        /// <summary>
        /// 消息按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void msgBtn_Click(object sender, EventArgs e)
        {
            msgChildPanel.Visible = true;
            //绘制焦点
            Pen pen1 = new Pen(Color.DeepSkyBlue);
            Pen pen2 = new Pen(Color.Gray);
            this.CreateGraphics().DrawLine(pen1, new Point(0, 130), new Point(130, 130));
            this.CreateGraphics().DrawLine(pen2, new Point(130, 130), new Point(365, 130));
            msgBtn.ForeColor = Color.DeepSkyBlue;
            relationBtn.ForeColor = Color.Black;
            //清除子控件
            //childPanel.Controls.Clear();
            childPanel.Visible = false;
            groupChatBtn.Visible = false;
            myfriendBtn.Visible = false;
            extendIcon.Visible = false;
            extendFriend = false;
        }

        /// <summary>
        /// 添加消息窗口控件
        /// </summary>
        private void addMsgControl(Message msg)
        {
            Label name = new Label();
            name.Location = new Point(120, childHeight);
            name.Font = new Font("Thonburi", 12);
            name.Text = msg.Name;
            name.DoubleClick += new EventHandler(Friend_DoubleClick);
            PictureBox pic = new PictureBox();
            pic.BackgroundImage = Image.FromFile("icons/car.png");
            pic.BackgroundImageLayout = ImageLayout.Stretch;
            pic.Width = 40; pic.Height = 30;
            pic.Location = new Point(70, childHeight);
            msgChildPanel.Controls.Add(name);
            msgChildPanel.Controls.Add(pic);
            childHeight += 40;
            msgChildPanel.Height += 70;
        }

        /// <summary>
        /// 检查是否有历史消息
        /// </summary>
        /// <param name="historyChat"></param>
        /// <returns></returns>
        private bool checkHasHistoryChat(Queue<Message> historyChat)
        {
            bool hasHistoryMsg = false;
            while (historyChat.Count>0) {
                Message msg = historyChat.Dequeue();
                if (msg.HasRead != null && msg.HasRead.Equals("False"))
                {
                    if (!checkMsgHasExist(historyList, msg)||(!checkMsgHasExist(historyList, msg)&&msg.Type==4))
                    {
                        historyList.Add(msg);
                        this.Invoke(new EventHandler(delegate
                        {
                            msgTip.Visible = true;
                            addMsgControl(msg);
                        }));
                    }
                    hasHistoryMsg = true;
                }
            }
            return hasHistoryMsg;
        }

        /// <summary>
        /// 检查历史消息列表中是否已存在消息，防止重复
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private bool checkMsgHasExist(List<Message> list,Message msg)
        {
            for(int i = 0; i < list.Count; i++)
            {
                if (list[i].Name.Equals(msg.Name))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 监听是否有消息传过来
        /// </summary>
        private void MonitorNowChat()
        {
            Thread nowmsgThread = new Thread(getNowMsg);
            nowmsgThread.Start();
        }

        /// <summary>
        /// 获取实时消息,从client里获取消息队列
        /// </summary>
        private void getNowMsg()
        {
            while (toRun)
            {
                checkHasHistoryChat(client.MsgTip);
                //添加好友
                if (client.AddFriendMsg != null)
                {
                    if (client.AddFriendMsg.ReturnMsg!=null)
                    {
                        if (client.AddFriendMsg.ReturnMsg.Equals("success"))
                        {
                            friendMsg.Add(client.AddFriendMsg.ObjName);
                            this.Invoke(new EventHandler(delegate
                            {
                                showFriend();
                            }));
                            client.AddFriendMsg.ReturnMsg = "";
                        }
                    }
                }
                Thread.Sleep(10);
            }
        }

        /// <summary>
        /// 好友列表点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void relationBtn_Click(object sender, EventArgs e)
        {
            msgChildPanel.Visible = false;
            //绘制焦点
            Pen pen1 = new Pen(Color.DeepSkyBlue);
            Pen pen2 = new Pen(Color.Gray);
            this.CreateGraphics().DrawLine(pen1, new Point(130, 130), new Point(365, 130));
            this.CreateGraphics().DrawLine(pen2, new Point(0, 130), new Point(130, 130));
            relationBtn.ForeColor = Color.DeepSkyBlue;
            msgBtn.ForeColor = Color.Black;
            groupChatBtn.Visible = true;
            myfriendBtn.Visible = true;
            extendIcon.Visible = true;
            extendFriend = true;
            //myFriendPanel.Controls.Remove(msgChildPanel);
            childPanel.Visible = true;
        }

        private void FriendList_FormClosing(object sender, FormClosingEventArgs e)
        {
            toRun = false;
            client.stop();
        }

        private void addFriendBtn_Click(object sender, EventArgs e)
        {
            AddFriend addFriend = new AddFriend(client, name);
            addFriend.Show();
        }

    }
}
