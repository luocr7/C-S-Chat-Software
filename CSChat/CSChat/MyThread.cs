using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSChat
{
    public abstract class MyThread
    {
        ///<summary>
        ///线程
        /// </summary>
        private Thread thread=null;

        public abstract void run();

        ///<summary>
        ///开启线程
        /// </summary>
        public void start()
        {
            if (thread == null)
            {
                thread = new Thread(run);
            }
            thread.Start();
        }
    }
}
