using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CSChat
{
    public partial class Login : Form
    {
        private const int CS_SHADOW = 0x20000;
        private const int GCL_STYLE = (-26);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SetClassLong(IntPtr hwnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassLong(IntPtr hwnd, int nIndex);

        ///<summary>
        ///客户端实例
        /// </summary>
        Client client;

        /// <summary>
        /// 窗体位置
        /// </summary>
        Point windowPoint;

        public Login(ref Client client)
        {
            InitializeComponent();
            SetClassLong(this.Handle, GCL_STYLE, GetClassLong(this.Handle, GCL_STYLE) | CS_SHADOW);
            this.client = client;
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            if (accountBox.Text.Length == 0)
            {
                MessageBox.Show("请输入账号!");
            }else if (pwdBox.Text.Length == 0)
            {
                MessageBox.Show("请输入密码!");
            }
            else 
            {
                Message msg = new Message(1);
                msg.Account = accountBox.Text.ToString();
                msg.Pwd = pwdBox.Text.ToString();
                client.SendMsg(msg);
                client.ReceiveBackMsg();
                if (client.ReturnMsg!= null)
                {
                    if (client.ReturnMsg.ReturnMsg.Equals("AccountError"))
                    {
                        MessageBox.Show("账号不存在!");
                    }
                    else if (client.ReturnMsg.ReturnMsg.Equals("PwdError"))
                    {
                        MessageBox.Show("密码错误!");
                    }
                    else if (client.ReturnMsg.ReturnMsg.Equals("loginSuccess"))
                    {
                        FriendList friendList = new FriendList(ref client,client.ReturnMsg.Name);
                        this.Hide();
                        friendList.ShowDialog();
                        Application.ExitThread();
                    }
                }
            }
        }

        private void Login_Paint(object sender, PaintEventArgs e)
        {
            Rectangle upRectangle = new Rectangle(new Point(0, 0), new Size(581, 100));
            e.Graphics.FillRectangle(new SolidBrush(Color.Turquoise), upRectangle);
            Pen pen = new Pen(Color.Gray);
            e.Graphics.DrawLine(pen, new Point(110, 170), new Point(300, 170));
            e.Graphics.DrawLine(pen, new Point(110, 220), new Point(300, 220));
        }

        private void loginButton_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
            this.Dispose();
            this.Close();
        }

        private void closeButton_MouseLeave(object sender, EventArgs e)
        {
            closeButton.BackColor = Color.Turquoise;
        }

        private void closeButton_MouseEnter(object sender, EventArgs e)
        {
            closeButton.BackColor = Color.Red;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Login_MouseDown(object sender, MouseEventArgs e)
        {
            //鼠标点击时记录鼠标位置
            windowPoint = new Point(-e.X, - e.Y);
        }

        private void Login_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //鼠标位置
                Point p2 = MousePosition;
                p2.Offset(windowPoint);
                DesktopLocation = p2;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Register registerForm = new Register(ref client);
            this.Hide();
            registerForm.ShowDialog();
            Application.ExitThread();
        }
    }
}
