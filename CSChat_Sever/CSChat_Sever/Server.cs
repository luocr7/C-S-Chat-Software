using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;

namespace CSChat_Sever
{
    public class Server:MyThread
    {
        ///<summary>
        ///控制线程循环参数
        /// </summary>
        private bool toRun = false;

        /// <summary>
        /// 监听socket
        /// </summary>
        public Socket socketMonitor;

        /// <summary>
        /// 服务器socket
        /// </summary>
        public Socket serverSocket;

        /// <summary>
        /// 服务端收到的消息，用队列处理
        /// </summary>
        Queue<Message> msgList=new Queue<Message>();

        /// <summary>
        /// 字节数组，用于接收已经编码的数据
        /// </summary>
        static byte[] buffer = new byte[4096];

        ///<summary>
        ///客户端集合(泛型)，存储已连接的客户端及客户端名字
        /// </summary>
        Dictionary<String,Socket> clientTable=new Dictionary<String,Socket>(); 

        /// <summary>
        /// 消息队列setter
        /// </summary>
        internal Queue<Message> MsgList { get => msgList; set => msgList = value; }



        /// <summary>
        /// 消息服务实例
        /// </summary>
        MsgService msgService = new MsgService();

        /// <summary>
        /// 构造函数
        /// </summary>
        public Server()
        {
            toRun = true;
        }

        ///<summary>
        ///监听IP和端口号和socket
        /// </summary>
        public void Monitor()
        {
            socketMonitor = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socketMonitor.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            IPAddress ip = IPAddress.Parse("192.168.43.63");
            IPEndPoint endPoint = new IPEndPoint(ip, 8080);
            socketMonitor.Bind(endPoint);
            socketMonitor.Listen(10);
            while (toRun)
            {
                socketMonitor.BeginAccept(new AsyncCallback(AcceptClientCallBack), socketMonitor);
                Thread.Sleep(10); 
            }
        }

        ///<summary>
        ///接收客户端请求回调函数
        ///<paramref name="async"/>
        /// </summary>
        public void AcceptClientCallBack(IAsyncResult async)
        {
            //Socket实例
            var socket = async.AsyncState as Socket;
            if (socket != null)
            {
                Socket client = (Socket)async.AsyncState;
                Socket handler = client.EndAccept(async);
                handler.BeginReceive(buffer, 0, buffer.Length, 0, new AsyncCallback(ReceiveMsgCallBack), handler);
            }
        }

        ///<summary>
        ///获取信息回调函数
        ///<paramref name="async"/>
        /// </summary>
        public void ReceiveMsgCallBack(IAsyncResult async)
        {
            try
            {
                Socket client = (Socket)async.AsyncState;
                //读取消息
                int bytes = client.EndReceive(async);
                if (bytes > 0)
                {
                    String message = Encoding.UTF8.GetString(buffer, 0, bytes);
                    Message m = JsonConvert.DeserializeObject<Message>(message);
                    //Message msgs = new Message();
                    MsgList.Enqueue(m);
                    Message r_msg=new Message();
                    if (m.Type == 1)
                    {
                        r_msg = msgService.loginJudge(m);
                        if (r_msg.ReturnMsg.Equals("loginSuccess"))
                        {
                            clientTable.Add(r_msg.Name, client);
                        }
                        SendMsg(client, r_msg);
                    }
                    else if (m.Type == 2)
                    {
                        r_msg = msgService.registerUser(m);
                        SendMsg(client, r_msg);
                    }
                    else if (m.Type == 3)
                    {
                        r_msg = msgService.queryUserInfo(m);
                        SendMsg(client, r_msg);
                    }
                    else if (m.Type == 4)
                    {
                        msgService.AddChatRecord(m);
                        m.HasRead = "False";
                        if (clientTable.ContainsKey(m.ObjName))
                        {
                            //if (clientTable[m.ObjName].Available > 0)
                            //{
                            SendMsg(clientTable[m.ObjName], m);
                            Thread.Sleep(10);
                            //}
                            //else
                            //{
                            //    //clientTable.Remove(m.ObjName);
                            //}
                        }
                    }
                    else if (m.Type == 5)
                    {
                        Queue<Message> chatList = msgService.QueryChat(m);
                        //for(int i = 0; i < chatList.Count; i++)
                        // {
                        while (chatList.Count > 0)
                        {
                            Message msg = chatList.Dequeue();
                            if (clientTable.ContainsKey(m.Name))
                            {
                                SendMsg(clientTable[m.Name], msg);
                                Thread.Sleep(10);
                            }
                        }
                        // }
                    }
                    else if (m.Type == 6)
                    {
                        Queue<Message> historyChat = msgService.QueryHistoryChat(m);
                        while (historyChat.Count > 0)
                        {
                            Message msg = historyChat.Dequeue();
                            if (clientTable.ContainsKey(m.Name))
                            {
                                SendMsg(clientTable[m.Name], msg);
                                Thread.Sleep(10);
                            }
                        }
                    }
                    else if (m.Type == 7)
                    {
                        msgService.UpdateHasRead(m);
                    }
                    else if (m.Type == 8)
                    {
                        Message msg = msgService.addFriend(m);
                        SendMsg(client, msg);
                    }
                    else if (m.Type == 9)
                    {
                        Message msg = msgService.queryFriendByName(m);
                        SendMsg(client, msg);
                    }else if (m.Type == 10)
                    {
                        Queue<Message> chatList = msgService.QueryGroupChat(m);
                        while (chatList.Count > 0)
                        {
                            Message msg = chatList.Dequeue();
                            if (clientTable.ContainsKey(m.Name))
                            {
                                SendMsg(clientTable[m.Name], msg);
                                Thread.Sleep(10);
                            }
                        }
                    }else if (m.Type == 11)
                    {
                        msgService.AddChatRecord(m);
                        m.HasRead = "False";
                        foreach(Socket clients in clientTable.Values)
                        {
                            if (clients != client)
                            {
                                SendMsg(clients, m);
                                Thread.Sleep(10);
                            }
                        }
                    }
        
                    client.BeginReceive(buffer, 0, buffer.Length, 0, new AsyncCallback(ReceiveMsgCallBack),client);
                }
            }
            catch (SocketException)
            {
                MessageBox.Show("客户端断开连接");
            }
        }
        

        ///<summary>
        ///发送消息
        /// </summary>
        public void SendMsg(Socket socket, Message msg)
        {
            String jsonMsg = JsonConvert.SerializeObject(msg);
            byte[] msgByte = Encoding.UTF8.GetBytes(jsonMsg);
            socket.BeginSend(msgByte, 0, msgByte.Length, 0, new AsyncCallback(SendCallBack), socket);
        }

        public void SendChatMsg(Socket socket,Queue<Message> msgList)
        {
            String jsonMsg = JsonConvert.SerializeObject(msgList);
            byte[] msgByte = Encoding.UTF8.GetBytes(jsonMsg);
            socket.BeginSend(msgByte, 0, msgByte.Length, 0, new AsyncCallback(SendCallBack), socket);
        }

        ///<summary>
        ///异步发送消息回调函数
        ///<paramref name="async"/>
        /// </summary>
        public void SendCallBack(IAsyncResult async)
        {
            Socket sendSocket = (Socket)async.AsyncState;
            int bytes = sendSocket.EndSend(async);
        }

        ///<summary>
        ///关闭socket
        /// </summary>
        public void CloseSocket()
        {
            if (socketMonitor != null && socketMonitor.Connected)
            {
                //禁止套接字上的接收和发送
                socketMonitor.Shutdown(SocketShutdown.Both);
                Thread.Sleep(20);
                socketMonitor.Close();
            }
            if (serverSocket != null && serverSocket.Connected)
            {
                serverSocket.Shutdown(SocketShutdown.Both);
                Thread.Sleep(20);
                serverSocket.Close();
            }            
        }

        ///<summary>
        ///运行线程
        /// </summary>
        public override void run()
        {
            Monitor();
        }

        ///<summary>
        ///关闭线程
        /// </summary>
        public void stop()
        {
            toRun = false;
            CloseSocket();    
        }
    }
}
