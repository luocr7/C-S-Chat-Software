using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSChat
{
    public partial class Register : Form
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
        private Point windowPoint;


        public Register(ref Client client)
        {
            InitializeComponent();
            SetClassLong(this.Handle, GCL_STYLE, GetClassLong(this.Handle, GCL_STYLE) | CS_SHADOW);
            this.client = client;
        }

        private void Register_Load(object sender, EventArgs e)
        {
            accountBox.Height = 30;
            pwdBox.Height = 30;
            nameBox.Height = 30;
        }

        private void Register_Paint(object sender, PaintEventArgs e)
        {
            Rectangle upRectangle = new Rectangle(new Point(0, 0), new Size(556, 60));
            e.Graphics.FillRectangle(new SolidBrush(Color.Turquoise), upRectangle);
            Pen pen = new Pen(Color.Gray);
            e.Graphics.DrawLine(pen, new Point(110, 120), new Point(300, 120));
            e.Graphics.DrawLine(pen, new Point(110, 185), new Point(300, 185));
            e.Graphics.DrawLine(pen, new Point(110, 245), new Point(300, 245));
        }


        private void closeBtn_MouseEnter(object sender, EventArgs e)
        {
            closeBtn.BackColor = Color.Red;
        }

        private void closeBtn_MouseLeave(object sender, EventArgs e)
        {
            closeBtn.BackColor = Color.Turquoise;
        }

        private void Register_MouseDown(object sender, MouseEventArgs e)
        {
            //鼠标点击时记录鼠标位置
            windowPoint = new Point(-e.X, -e.Y);
        }

        private void Register_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //鼠标位置
                Point p2 = MousePosition;
                p2.Offset(windowPoint);
                DesktopLocation = p2;
            }
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
           // Login loginForm = new Login();
            this.Hide();
           // loginForm.ShowDialog();
            Application.ExitThread();
        }

        private void registerBtn_Click(object sender, EventArgs e)
        {
            String pattern = "^[0-9]*$";
            Regex rx = new Regex(pattern);
            Message msg = new Message(2);
            msg.Account = accountBox.Text.ToString();
            msg.Pwd = pwdBox.Text.ToString();
            msg.Name = nameBox.Text.ToString();
            client.SendMsg(msg);
            client.ReceiveBackMsg();
            //规则检查
            if (accountBox.Text.Length == 0)
            {
                MessageBox.Show("账号不能为空!");
            }else if (!rx.IsMatch(accountBox.Text.ToString())||accountBox.Text.Length>9||accountBox.Text.Length<9)
            {
                MessageBox.Show("账号不符合规则!");
            }
            else if (pwdBox.Text.Length == 0)
            {
                MessageBox.Show("密码不能为空!");
            }else if(pwdBox.Text.Length<6||pwdBox.Text.Length>20||pwdBox.Text.ToString().IndexOf(" ") >= 0)
            {
                MessageBox.Show("密码不符合规则!");
            }
            else if (nameBox.Text.Length == 0)
            {
                MessageBox.Show("网名不能为空!");
            }       
            else if (nameBox.Text.Length < 1 || nameBox.Text.Length > 20)
            {
                MessageBox.Show("网名长度不符!");
            }
            else if (client.ReturnMsg!=null&&client.ReturnMsg.ReturnMsg.Equals("AccountExist"))
            {
                MessageBox.Show("账号已存在!");
            }else if (client.ReturnMsg!=null&&client.ReturnMsg.ReturnMsg.Equals("NameExist"))
            {
                MessageBox.Show("网名已存在!");
            }
            else if(client.ReturnMsg!=null&&client.ReturnMsg.ReturnMsg.Equals("registerSuccess"))
            {
                MessageBox.Show("注册成功!");
                Login loginForm = new Login(ref client);
                this.Hide();
                loginForm.ShowDialog();
                Application.ExitThread();
            }
        }


    }
}
