using System;
using System.Windows.Forms;

namespace CSChat
{
    static class Program
    {
        static Client client;

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            client = new Client();
            client.start();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login(ref client));
        }
    }
}
