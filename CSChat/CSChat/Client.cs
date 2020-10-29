using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Newtonsoft.Json;

namespace CSChat
{
    public class Client:MyThread
    {
        ///<summary>
        ///控制线程循环参数
        /// </summary>
        private bool toRun = false;

        ///<summary>
        ///控制异步接收线程循环
        /// </summary>
        private bool syncRun = true;

        ///<summary>
        ///客户端socket
        /// </summary>
        public Socket clientSocket;

        ///<summary>
        ///反馈消息
        /// </summary>
        private Message returnMsg=new Message(1);

        /// <summary>
        /// 客户端收到的消息对象
        /// </summary>
        private Message getMsg;

        /// <summary>
        /// 收到的消息，加入队列中
        /// </summary>
        Queue<Message> msgList = new Queue<Message>();

        /// <summary>
        /// 聊天信息
        /// </summary>
        Queue<Message> chatList = new Queue<Message>();

        /// <summary>
        /// 供好友列表里消息提醒使用
        /// </summary>
        Queue<Message> msgTip = new Queue<Message>();

        /// <summary>
        /// 历史消息
        /// </summary>
        Queue<Message> historyMsg = new Queue<Message>();

        /// <summary>
        /// 添加好友消息
        /// </summary>
        Message addFriendMsg;

        Byte[] buffer = new Byte[4096*1000];

        /// <summary>
        /// 客户端名字
        /// </summary>
        private String name;

        public Message GetMsg { get => getMsg; set => getMsg = value; }
        public string Name { get => name; set => name = value; }
        public Message ReturnMsg { get => returnMsg; set => returnMsg = value; }
        public Queue<Message> MsgList { get => msgList; set => msgList = value; }
        public Queue<Message> ChatList { get => chatList; set => chatList = value; }
        public Queue<Message> MsgTip { get => msgTip; set => msgTip = value; }
        public Queue<Message> HistoryMsg { get => historyMsg; set => historyMsg = value; }
        public Message AddFriendMsg { get => addFriendMsg; set => addFriendMsg = value; }

        public Client()
        {
            toRun = true;
            Connect();
        }

        ///<summary>
        ///连接
        /// </summary>
        public void Connect()
        {
            IPAddress ip = IPAddress.Parse("192.168.43.63");
            IPEndPoint endPoint = new IPEndPoint(ip, 8080);
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(endPoint);
            //定时发送心跳包
            //Thread heartThread = new Thread(heart);
            //heartThread.Start();
           // Message msg = new Message(0);
            //msg.Msg = "";
            //msg.Name = "";
            //SendMsg(msg);
        }

        /// <summary>
        /// 心跳包
        /// </summary>
        //public void heart()
        //{
        //    while (toRun)
        //    {
        //        Message m = new Message(12);
        //        m.Msg = "live";
        //        SendMsg(m);
        //        Thread.Sleep(5000);
        //    }
        //}

        ///<summary>
        ///发送消息
        /// </summary>
        public void SendMsg(Message msg)
        {
            String jsonMsg = JsonConvert.SerializeObject(msg);
            byte[] msgByte = Encoding.UTF8.GetBytes(jsonMsg);
            clientSocket.Send(msgByte);
        }

        /// <summary>
        /// 线程控制异步接收实时聊天内容
        /// </summary>
        public void ReceiveThread()
        {
            Thread t = new Thread(ReceiveMsg);
            t.Start();
        }

        /// <summary>
        /// 接收聊天记录
        /// </summary>
        /// <returns></returns>
        public void ReceiveChatMsg()
        {
            Byte[] buffer = new Byte[4096];
            int bytes = clientSocket.Receive(buffer);
            String jsonMsg = Encoding.UTF8.GetString(buffer, 0, bytes);
            List<Message> msgList = JsonConvert.DeserializeObject<List<Message>>(jsonMsg);
            //this.ChatList=msgList;
        }

        ///<summary>
        ///接收反馈信息
        /// </summary>
        public void ReceiveBackMsg()
        {
            Byte[] buffer = new Byte[4096];
            int bytes = clientSocket.Receive(buffer);
            if (bytes > 0)
            {
                String jsonMsg = Encoding.UTF8.GetString(buffer, 0, bytes);
                Message m = JsonConvert.DeserializeObject<Message>(jsonMsg);
                ReturnMsg = m;
            }
            else
            {
                ReturnMsg.ReturnMsg="";
            }
        }

        ///<summary>
        ///异步接收消息
        /// </summary>
        public void ReceiveMsg()
        {
            while (syncRun)
            {
                //ReceiveMsgCallBack();
                clientSocket.BeginReceive(buffer, 0, buffer.Length, 0, new AsyncCallback(ReceiveMsgCallBack), clientSocket);
            Thread.Sleep(10);
        }
    }

        ///<summary>
        ///关闭异步接收线程
        /// </summary>
        public void closeReceiveThread()
        {
            syncRun = false;
        }

        ///<summary>
        ///接收消息回调函数
        /// </summary>
        public void ReceiveMsgCallBack(IAsyncResult async)
        {
            try
            {
                Socket sever = async.AsyncState as Socket;
                int bytes = sever.EndReceive(async);
                if (bytes > 0)
                {
                    String jsonMsg = Encoding.UTF8.GetString(buffer, 0, bytes);
                    Message m = JsonConvert.DeserializeObject<Message>(jsonMsg);
                    Message ms = JsonConvert.DeserializeObject<Message>(jsonMsg);
                    Message mg= JsonConvert.DeserializeObject<Message>(jsonMsg);
                    Message mc = JsonConvert.DeserializeObject<Message>(jsonMsg);
                    Message gm= JsonConvert.DeserializeObject<Message>(jsonMsg);
                    if (m.Type != 6&&m.Type!=5)
                    {
                        ChatList.Enqueue(m);
                    }
                    if (mg.Type == 5||mg.Type==10)
                    {
                        HistoryMsg.Enqueue(mg);
                    }
                    if (mc.Type == 9||mc.Type==8)
                    {
                        AddFriendMsg = mc;
                    }
                    MsgTip.Enqueue(ms);
                }
                if (syncRun)
                {
                    clientSocket.BeginReceive(buffer, 0, buffer.Length, 0, new AsyncCallback(ReceiveMsgCallBack), clientSocket);
                }
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
            //while (toRun)
            //{

            //    if (clientSocket.Poll(10, SelectMode.SelectRead))
            //    {
            //        Byte[] buffer = new Byte[4096];
            //        int bytes = clientSocket.Receive(buffer);//会被阻塞
            //        if (bytes > 0)
            //        {
            //            String jsonMsg = Encoding.UTF8.GetString(buffer, 0, bytes);
            //            Message m = JsonConvert.DeserializeObject<Message>(jsonMsg);
            //            chatList.Enqueue(m);
            //            msgTip.Enqueue(m);
            //        }
            //    }
            //    Thread.Sleep(10);
            //}
        }

        ///<summary>
        ///关闭socket
        /// </summary>
        public void CloseSocket()
        {
            if (clientSocket != null && clientSocket.Connected)
            {
                clientSocket.Shutdown(SocketShutdown.Both);
                Thread.Sleep(10);
                clientSocket.Close();
            }
        }

        ///<summary>
        ///运行线程
        /// </summary>
        public override void run()
        {
            //while (toRun)
            //{
            //    // Connect();
            //    //SendMsg();
            //    ReceiveMsg();
            //    Thread.Sleep(10);
            //}
    }

        ///<summary>
        ///关闭线程
        /// </summary>
        public void stop()
        {
            toRun = false;
            syncRun = false;
            Message m = new Message(12);
            m.Msg = "die";
            SendMsg(m);
            CloseSocket();
        }
    }
}
