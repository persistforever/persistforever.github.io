using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Windows.Devices.Geolocation;
using PhoneSever;

namespace PhoneSever
{
    public partial class RegistLogin : PhoneApplicationPage
    {
        const string HOST = BasicInfo.HOST ;
        const string PORT = "8888";
        public string msgLongReceive="" ; // 接收来自服务器端的实际表达的信息
        public string msgLongSend="" ; // 发送给服务器端的实际表达的信息
        public string msgShortReceive="" ; // 接收来自服务器端的一个数据包的信息
        public string msgShortSend = ""; // 发送给服务器端一个数据包的信息
        private BackgroundWorker backgroundworker; // 后台任务

        // 构造函数
        public RegistLogin()
        {
            InitializeComponent();
        }

        private void SocketAsyncEventArgs_Completed(object sender, SocketAsyncEventArgs e)
        {
            if (e.SocketError != SocketError.Success)
            {
                if (e.SocketError == SocketError.ConnectionAborted)
                {
                    Dispatcher.BeginInvoke(() => MessageBox.Show("连接超时!" + e.SocketError));
                }
                else if (e.SocketError == SocketError.ConnectionRefused)
                {
                    Dispatcher.BeginInvoke(() => MessageBox.Show("服务器启动" + e.SocketError));
                }
                else
                {
                    Dispatcher.BeginInvoke(() => MessageBox.Show("出错了" + e.SocketError));
                }
                if (e.UserToken != null)
                {
                    Socket sock = e.UserToken as Socket;
                    sock.Shutdown(SocketShutdown.Both);
                    sock.Close();
                }
                return;
            }
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.Connect :
                    if (e.UserToken != null)
                    {
                        Socket sock = e.UserToken as Socket;
                        bool completesAsynchronously = sock.SendAsync(e);
                        if (!completesAsynchronously)
                        {
                            SocketAsyncEventArgs_Completed(e.UserToken, e);
                        }
                    }
                    break;
                case SocketAsyncOperation.Send :
                    if (e.UserToken != null)
                    {
                        Socket sock = e.UserToken as Socket;
                        bool completesAsynchronously = sock.ReceiveAsync(e);
                        if (!completesAsynchronously)
                        {
                            SocketAsyncEventArgs_Completed(e.UserToken, e);
                        }
                    }
                    break;
                case SocketAsyncOperation.Receive :
                    if (e.UserToken != null)
                    {
                        msgShortReceive = Encoding.UTF8.GetString(e.Buffer, 0, e.BytesTransferred);
                        Socket sock = e.UserToken as Socket;
                        Dispatcher.BeginInvoke(() =>
                            {
                                HandleShortDataFromServer();
                            });
                    }
                    break;
            }
        }

        private void SendLongDataToServer()
        {
            if (msgLongSend == "")
            {
                msgShortSend = "&&&&";
            }
            else
            {
                if (msgLongSend.Length > 4)
                {
                    msgShortSend = msgLongSend.Substring(0, 4);
                    msgLongSend = msgLongSend.Substring(4, msgLongSend.Length - 4);
                }
                else
                {
                    msgShortSend = msgLongSend;
                    for (int i = msgLongSend.Length ; i < 4; i++)
                    {
                        msgShortSend += " " ;
                    }
                    msgLongSend = "";
                }
            }
            SendShortDataToServer();
        }

        private void HandleShortDataFromServer() // 处理来自服务器的一个数据报的信息
        {
            if (msgShortReceive == "||||") // 收到继续发送的信息
            {
                msgShortReceive = "";
                SendLongDataToServer();
            }
            else if (msgShortReceive == "&&&&") // 收到结束发送的信息
            {
                HandleLongDataFromServer();
            }
            else
            {
                msgLongReceive += msgShortReceive;
                msgShortReceive = "";
                msgLongSend = "||||";
                SendLongDataToServer();
            }
        }

        private void SendShortDataToServer() // 发送给服务器一个数据包的信息
        {
            string host = HOST.Trim();
            int port = Convert.ToInt32(PORT.Trim());
            DnsEndPoint hostEntry = new DnsEndPoint(host, port);
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            SocketAsyncEventArgs socketEventArg = new SocketAsyncEventArgs();
            byte[] buffer = Encoding.UTF8.GetBytes(msgShortSend);
            socketEventArg.SetBuffer(buffer, 0, buffer.Length);
            socketEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(SocketAsyncEventArgs_Completed);
            socketEventArg.RemoteEndPoint = hostEntry;
            socketEventArg.UserToken = sock;
            try
            {
                sock.ConnectAsync(socketEventArg);
                
            }
            catch (SocketException ex)
            {
                throw new SocketException((int)ex.ErrorCode);
            }
        }

        private void HandleLongDataFromServer()
        {
            msgLongReceive = msgLongReceive.Trim();
            if (msgLongReceive[0] == '1') // 如果是注册
            {
                MessageBox.Show(msgLongReceive.Substring(1, msgLongReceive.Length - 1));
                tbState.Visibility = Visibility.Collapsed;
                progressBar.Visibility = Visibility.Collapsed;
                gridBackground.Visibility = Visibility.Collapsed;
            }
            else if(msgLongReceive[0] == '2') // 如果是登陆
            { 
                if (msgLongReceive.Length == 1) // 如果没有好友
                {
                    MessageBox.Show("Login successful");
                    BasicInfo.myName = tbUser2.Text;
                    BasicInfo.myPassword = tbPasswd2.Password;
                    msgLongSend = "4|" + tbUser2.Text;
                    SendLongDataToServer();
                }
                else // 如果有好友
                {
                    if (msgLongReceive[1] == '1')
                    {
                        MessageBox.Show(msgLongReceive.Substring(2, msgLongReceive.Length - 2));
                        tbState.Visibility = Visibility.Collapsed;
                        progressBar.Visibility = Visibility.Collapsed;
                        gridBackground.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        UpdataUserList(msgLongReceive.Substring(1, msgLongReceive.Length - 1));
                        BasicInfo.myName = tbUser2.Text;
                        BasicInfo.myPassword = tbPasswd2.Password;
                        msgLongSend = "4|" + tbUser2.Text;
                        SendLongDataToServer();
                    }
                }
            }
            else if (msgLongReceive[0] == '4') // 如果是活动列表
            {
                UpdataEventList(msgLongReceive.Substring(1, msgLongReceive.Length - 1));
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            }
            msgLongReceive = "";
        }

        private void UpdataEventList(string msgLongReceive) // 更新活动列表
        {
            tbState.Text = "正在载入活动列表";
            string[] type = msgLongReceive.Split(new char[] { '|' });
            int index = type.Length;
            if (index == 1)
            {
                string[] activity = type[0].Split(new char[] { '&' });
                for (int i = 0; i < activity.Length / 3; i++)
                {
                    BasicInfo.completingEventList.Add(new EventInfo(activity[3 * i + 0],
                        activity[3 * i + 1], activity[3 * i + 2]));
                }
            }
            else if (index == 2)
            {
                string[] completingActivity = type[0].Split(new char[] { '&' });
                for (int i = 0; i < completingActivity.Length / 3; i++)
                {
                    BasicInfo.completingEventList.Add(new EventInfo(completingActivity[3 * i + 0],
                        completingActivity[3 * i + 1], completingActivity[3 * i + 2]));
                }
                string[] completedActivity = type[1].Split(new char[] { '&' });
                for (int i = 0; i < completedActivity.Length / 3; i++)
                {
                    BasicInfo.completedEventList.Add(new EventInfo(completedActivity[3 * i + 0],
                        completedActivity[3 * i + 1], completedActivity[3 * i + 2]));
                }
            }
        } 

        private void UpdataUserList(string fList) // 更新用户列表 
        {
            tbState.Text = "正在载入用户列表";
            string[] friend =fList.Split(new char[] { '&', '/' });
            for (int i = 0; i < friend.Length / 5; i++)
            {
                BasicInfo.rankingUserList.Add(new UserInfo(friend[5*i+1], double.Parse(friend[5*i+4]),
                    "photo/photo"+friend[5*i]+".jpg", double.Parse(friend[5*i+3]), 
                    double.Parse(friend[5*i+4]), i+1));
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            tbState.Visibility = Visibility.Collapsed;
            progressBar.Visibility = Visibility.Collapsed;
            gridBackground.Visibility = Visibility.Collapsed;
        }

        void backgroundworker_Runworkercompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Dispatcher.BeginInvoke(() =>
            {
            });
        }

        void backgroundworker_Dowork(object sender, DoWorkEventArgs e)
        {
            SendLongDataToServer();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            tbState.Visibility = Visibility.Visible;
            tbState.Text = "正在处理信息...";
            gridBackground.Visibility = Visibility.Visible;
            progressBar.Visibility = System.Windows.Visibility.Visible;
            progressBar.IsIndeterminate = true;
            Geolocator geolocator = new Geolocator();
            geolocator.DesiredAccuracyInMeters = 50;
            try
            {
                Geoposition geoposition = await geolocator.GetGeopositionAsync(
                    maximumAge: TimeSpan.FromMinutes(5),
                    timeout: TimeSpan.FromSeconds(10)
                    );
                BasicInfo.myLatitude = geoposition.Coordinate.Latitude;
                BasicInfo.myLongitude = geoposition.Coordinate.Longitude;
            }
            catch (Exception ex)
            {
                if ((uint)ex.HResult == 0x80004004)
                { }
            }
            // 判断输入是否为空
            if (tbUser2.Text == "" || tbPasswd2.Password == "")
            {
                MessageBox.Show("输入的信息不能为空");
                tbState.Visibility = Visibility.Collapsed;
                progressBar.Visibility = Visibility.Collapsed;
                gridBackground.Visibility = Visibility.Collapsed;
                return;
            }
            else
            {
                msgLongSend = "2|" + tbUser2.Text + "|" + tbPasswd2.Password + "|" +
                         BasicInfo.myLatitude.ToString("0.000000") + "|" +
                         BasicInfo.myLongitude.ToString("0.000000");
                //创建后台处理对象
                backgroundworker = new BackgroundWorker();
                backgroundworker.DoWork += new DoWorkEventHandler(backgroundworker_Dowork);
                backgroundworker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundworker_Runworkercompleted);
                backgroundworker.RunWorkerAsync();
            }
        }

        private async void btnRegist_Click(object sender, EventArgs e)
        {
            tbState.Visibility = Visibility.Visible;
            tbState.Text = "正在处理信息...";
            gridBackground.Visibility = Visibility.Visible;
            progressBar.Visibility = System.Windows.Visibility.Visible;
            progressBar.IsIndeterminate = true;
            Geolocator geolocator = new Geolocator();
            geolocator.DesiredAccuracyInMeters = 50;
            try
            {
                Geoposition geoposition = await geolocator.GetGeopositionAsync(
                    maximumAge: TimeSpan.FromMinutes(5),
                    timeout: TimeSpan.FromSeconds(10)
                    );

                BasicInfo.myLatitude = geoposition.Coordinate.Latitude;
                BasicInfo.myLongitude = geoposition.Coordinate.Longitude;
            }
            catch (Exception ex)
            {
            }
            // 判断输入是否为空
            if (tbUser1.Text == "" || tbPasswd1.ToString() == "" || tbEnsurepasswd.ToString() == "")
            {
                tbState.Visibility = Visibility.Collapsed;
                progressBar.Visibility = Visibility.Collapsed;
                gridBackground.Visibility = Visibility.Collapsed;
                MessageBox.Show("输入的信息不能为空");
            }
            else // 判断密码是否相同
            {
                if (tbPasswd1.Password != tbEnsurepasswd.Password)
                {
                    tbState.Visibility = Visibility.Collapsed;
                    progressBar.Visibility = Visibility.Collapsed;
                    gridBackground.Visibility = Visibility.Collapsed;
                    MessageBox.Show("两次输入的密码不一致");
                }
                else
                {
                    msgLongSend = "1|" + tbUser1.Text + "|" + tbPasswd1.Password + "|" +
                    BasicInfo.myLatitude.ToString("0.000000") + "|" +
                    BasicInfo.myLongitude.ToString("0.000000");
                    //创建后台处理对象
                    backgroundworker = new BackgroundWorker();
                    backgroundworker.DoWork += new DoWorkEventHandler(backgroundworker_Dowork);
                    backgroundworker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundworker_Runworkercompleted);
                    backgroundworker.RunWorkerAsync();
                }
            }
        }
    }
}