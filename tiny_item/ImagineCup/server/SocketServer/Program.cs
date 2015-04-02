using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SocketServer
{
    class Program
    {
        private static AutoResetEvent _flipFlop = new AutoResetEvent(false);
        // 建立数据库
        public static userList _databaseUserList = new userList();
        public static ActivityList _databaseActivityList = new ActivityList();
        // 操作的判断Flag
        public static bool flagRigest = false; // 注册判断
        public static bool flagLogin = false; // 登陆判断
        public static bool flagCreateEvent = false; // 发起活动判断
        public static string msgLongReceive; // 接收来自客户端的实际表达的信息
        public static string msgLongSend; // 发送给客户端的实际表达的信息
        public static string msgShortReceive; // 接收来自客户端的一个数据包的信息
        public static string msgShortSend; // 发送给客户端一个数据包的信息

        static void Main(string[] args)
        {

            _databaseUserList = UserLoading(); // 载入用户数据库
            _databaseActivityList = ActivityLoading(); // 载入活动数据库

            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPEndPoint localEP = new IPEndPoint(ipHostInfo.AddressList[0], 8888);
            Console.WriteLine("本地IP地址和端口号为:{0}", localEP);
            try
            {
                listener.Bind(localEP);
                listener.Listen(10);
                while (true)
                {
                    //Console.WriteLine("等待客户端连接...");
                    listener.BeginAccept(AcceptCallBack, listener);
                    _flipFlop.WaitOne();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void AcceptCallBack(IAsyncResult ar)
        {
            Socket listener = (Socket)ar.AsyncState;
            Socket socket = listener.EndAccept(ar);
            //Console.WriteLine("连接到客户端.");
            _flipFlop.Set();
            var state = new StateObject();
            state.Socket = socket;
            socket.BeginReceive(state.Buffer, 0, StateObject.BufferSize, 0, ReceiveCallBack, state);
        }

        private static void ReceiveCallBack(IAsyncResult ar)
        {
            StateObject state = (StateObject)ar.AsyncState;
            Socket socket = state.Socket;
            int read = socket.EndReceive(ar);
            if (read > 0)
            {
                string chunk = Encoding.UTF8.GetString(state.Buffer, 0, read);
                state.StringBuilder.Append(chunk);
                if (state.StringBuilder.Length > 0)
                {
                    string result = state.StringBuilder.ToString();
                    //Console.WriteLine("收到客户端发来的信息:{0}", result); //接收来自客户端的数据
                    msgShortReceive = result;
                    HandleShortDataFromClient(); // 处理来自客户端的一个数据包的信息
                    SendLongDataToClient(socket); // 服务器发送实际表达的信息给客户端
                }
            }
        }

        private static void SendLongDataToClient(Socket handler)
        {
            if (msgLongSend.Length == 0) // 如果发送给客户端的信息发送完了
            {
                msgShortSend = "&&&&";
            }
            else
            {
                if (msgLongSend.Length > 4) // 如果需要拆分数据包
                {
                    msgShortSend = msgLongSend.Substring(0, 4);
                    msgLongSend = msgLongSend.Substring(4, msgLongSend.Length - 4);
                }
                else
                {
                    msgShortSend = msgLongSend;
                    for (int i = msgLongSend.Length; i < 4; i++)
                    {
                        msgShortSend += " ";
                    }
                    msgLongSend = "";
                }
            }
            SendShortDataToClient(handler);
            msgShortSend = "";
        }

        private static void SendShortDataToClient(Socket handler)
        {
            byte[] byteData = Encoding.UTF8.GetBytes(msgShortSend);
            handler.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallBack), handler);
        }

        private static void SendCallBack(IAsyncResult ar)
        {
            try
            {
                Socket handler = (Socket)ar.AsyncState;
                int bytesSent = handler.EndSend(ar);
                if (bytesSent > 0)
                {
                    //Console.WriteLine("发送成功!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void HandleShortDataFromClient() // 处理从客户端发来的一个数据报的信息
        {
            if (msgShortReceive == "||||") // 收到继续发送的信息
            {
                msgShortReceive = "";
            }
            else if (msgShortReceive == "&&&&") // 收到结束发送的信息
            {
                msgShortReceive = "";
                HandleLongDataFromClient();
            }
            else // 收到的不是结束也不是继续，那么就是客户端发送的中间数据包
            {
                msgLongReceive += msgShortReceive;
                msgShortReceive = "";
                msgLongSend = "||||";
            }
        }

        static void PostToMain(string result)
        {
            msgLongSend = result;
            Console.WriteLine("发送给客户的信息是"+ result+"end");
        }

        private static void HandleLongDataFromClient() // 将客户端传来的数据报组织后获得的长字符串
        {
            msgLongReceive = msgLongReceive.Trim();
            Console.WriteLine("客户端传来的数据是" + msgLongReceive + "end");
            string result = msgLongReceive;
            ActivityList alst = ActivityLoading();
            userList ulst = UserLoading();
            char index = result[0];
            switch (index)
            {
                case '1':
                    PostToMain(index + ulst.Rigest(result));
                    break;
                case '2':
                    PostToMain(index + ulst.Login(result));
                    break;
                case '3':
                    PostToMain(index + ulst.findFriend(result));
                    break;
                case '4':
                    string res = ulst.findMyActivity(result);
                    PostToMain(index + alst.findMyActivity(res));
                    break;
                case '5':
                    ulst.PostEvent(result);
                    PostToMain(index + alst.PostEvent(result));
                    break;
                case '6':
                    PostToMain(index + alst.searchEvent(result));
                    break;
                case '7':
                    PostToMain(index + ulst.searchFriend(result));
                    break;
                case '8':
                    PostToMain(index + ulst.Addfriend(result));
                    break;
                case '9':
                    PostToMain(index + ulst.finishActivity(result));
                    break;
                case '0':
                    result = alst.JoinActivity(result);
                    PostToMain(index + ulst.JoinActivity(result));
                    break;
                case 'a':
                    PostToMain(index + alst.activityInfo(result));
                    break;
            };
            msgLongReceive = "";
        }

        static userList UserLoading()  //载入用户信息
        {
            string strList = "data\\namelist.txt";
            userList ust = new userList();
            if (File.Exists(strList))
            {
                FileStream myFs = new FileStream(strList, FileMode.Open);
                StreamReader mySr = new StreamReader(myFs);
                string nameList = null, strtmp;
                while ((strtmp = mySr.ReadLine()) != null)
                {
                    nameList += strtmp;
                }
                mySr.Close();
                myFs.Close();
                string[] strtemp = nameList.Split(new char[] { '|' });
                for (int i = 0; i < strtemp.Length - 1; i++)
                {
                    string result = null, strname = "data\\" + strtemp[i] + ".txt";
                    FileStream myFs1 = new FileStream(strname, FileMode.Open);
                    StreamReader mySr1 = new StreamReader(myFs1);
                    while ((strtmp = mySr1.ReadLine()) != null)
                    {
                        result += strtmp;
                        string[] nList = result.Split(new char[] { '|' });
                        int j = 0;
                        ust.Add(nList[j++], nList[j++], nList[j++], nList[j++], nList[j++], nList[j++], nList[j++], nList[j++], nList[j++], nList[j++], nList[j++]);
                    }
                    mySr1.Close();
                    myFs1.Close();
                }
                return ust;
            }
            else
            {
                return ust;
            }
        }

        static ActivityList ActivityLoading() //载入活动信息
        {
            string strList = "data\\ActivityList.txt";
            ActivityList ust = new ActivityList();
            if (File.Exists(strList))
            {
                FileStream myFs = new FileStream(strList, FileMode.Open);
                StreamReader mySr = new StreamReader(myFs);
                string nameList = null, strtemp;
                while ((strtemp = mySr.ReadLine()) != null)
                {
                    nameList += strtemp;
                }
                mySr.Close();
                myFs.Close();
                string[] strtmp = nameList.Split(new char[] { '|' });
                for (int i = 0; i < strtmp.Length - 1; i++)
                {
                    string result = null, strname = "data\\" + strtmp[i] + "Activity.txt";
                    FileStream myFs1 = new FileStream(strname, FileMode.Open);
                    StreamReader mySr1 = new StreamReader(myFs1);
                    while ((strtemp = mySr1.ReadLine()) != null)
                    {
                        result += strtemp;
                        string[] nList = result.Split(new char[] { '|' });
                        int j = 0;
                        ust.AddActivity(nList[j++], nList[j++], nList[j++], nList[j++], nList[j++], nList[j++], nList[j++]);
                    }
                    mySr1.Close();
                    myFs1.Close();
                }
                return ust;
            }
            else
            {
                return ust;
            }
        }

        public class StateObject
        {
            public Socket Socket;
            public StringBuilder StringBuilder = new StringBuilder();
            public const int BufferSize = 10;
            public byte[] Buffer = new byte[BufferSize];
            public int TotalSize;
        }
    }
}
