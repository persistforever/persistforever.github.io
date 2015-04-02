using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Net.Sockets;
using System.Text;
using System.Device.Location;
using System.Windows.Shapes;
using System.Windows.Media;
using Microsoft.Phone.Maps.Controls;
using System.Windows.Media.Imaging;
using System.ComponentModel;

namespace PhoneSever
{
    public partial class UserInfomation : PhoneApplicationPage
    {
        const string HOST = BasicInfo.HOST;
        const string PORT = "8888";
        public string msgLongReceive = ""; // 接收来自服务器端的实际表达的信息
        public string msgLongSend = ""; // 发送给服务器端的实际表达的信息
        public string msgShortReceive = ""; // 接收来自服务器端的一个数据包的信息
        public string msgShortSend = ""; // 发送给服务器端一个数据包的信息
        public double positionLatitude; // 位置的经度
        public double positionLongitude; // 位置的纬度
        private BackgroundWorker backgroundworker; // 后台任务

        public UserInfomation()
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
                case SocketAsyncOperation.Connect:
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
                case SocketAsyncOperation.Send:
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
                case SocketAsyncOperation.Receive:
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
                    for (int i = msgLongSend.Length; i < 4; i++)
                    {
                        msgShortSend += " ";
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
            if (msgLongReceive[0] == '3')
            {
                tbState.Visibility = Visibility.Collapsed;
                progressBar.Visibility = Visibility.Collapsed;
                gridBackground.Visibility = Visibility.Collapsed;
                msgLongReceive = msgLongReceive.Substring(1, msgLongReceive.Length - 1);
                MessageBox.Show(msgLongReceive);
                string[] friendInfo = msgLongReceive.Split(new char[] { '|' });
                tbName.Text = BasicInfo.searchUserName;
                tbScore.Text = (200).ToString();
                tbSpeed.Text = friendInfo[2];
                tbDistance.Text = friendInfo[3];
                string [] futureActivity = friendInfo[4].Split(new char[] { '&' });
                string [] finishActivity = friendInfo[5].Split(new char[] { '&' });
                positionLatitude = double.Parse(friendInfo[0]);
                positionLongitude = double.Parse(friendInfo[1]);
                BitmapImage ibImage = new BitmapImage(new Uri("photo/photo" + BasicInfo.myPhoto.ToString() + ".jpg"
                    , UriKind.Relative));
                ibPhoto.Source = ibImage;
                ShowMyLocationOnTheMap();
                msgLongReceive = "";
            }
            else if (msgLongReceive[0] == '8')
            {
                tbState.Visibility = Visibility.Collapsed;
                progressBar.Visibility = Visibility.Collapsed;
                gridBackground.Visibility = Visibility.Collapsed;
                msgLongReceive = msgLongReceive.Substring(1, msgLongReceive.Length - 1);
                MessageBox.Show(msgLongReceive);
                msgLongReceive = "";
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            msgLongSend = "3|" + BasicInfo.searchUserName;
            SendLongDataToServer();
        }

        private void ShowMyLocationOnTheMap()
        {
            // Make my current location the center of the Map.
            this.mapWithMyLocation.Center = new GeoCoordinate(positionLatitude, positionLongitude);
            this.mapWithMyLocation.ZoomLevel = 12;
            // 绘制圆形为当前的位置
            Ellipse myCircle = new Ellipse();
            myCircle.Fill = new SolidColorBrush(Colors.Red);
            myCircle.Height = 20;
            myCircle.Width = 20;
            myCircle.Opacity = 50;
            // 创建一个包含圆形的MapOverlay
            MapOverlay myLocationOverlay = new MapOverlay();
            myLocationOverlay.Content = myCircle;
            myLocationOverlay.PositionOrigin = new Point(0, 0);
            MessageBox.Show(BasicInfo.BrowseEventInfoLocationLatitude.ToString());
            myLocationOverlay.GeoCoordinate = new GeoCoordinate(positionLatitude, positionLongitude);
            // 创建一个MapLayer包含MapOverlay
            MapLayer myLocationLayer = new MapLayer();
            myLocationLayer.Add(myLocationOverlay);
            mapWithMyLocation.Layers.Clear();
            // 将MapLayer加入到地图中
            mapWithMyLocation.Layers.Add(myLocationLayer);
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

        private void btnBecomeFriend_Click(object sender, EventArgs e)
        {
            msgLongSend = "8|" + tbName.Text + "|" + BasicInfo.myName;
            SendLongDataToServer();
        }
    }
}