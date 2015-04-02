using Microsoft.Phone.Controls;
using System;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Windows.Devices.Geolocation;

namespace PhoneSever
{
    public partial class SearchUser : PhoneApplicationPage
    {
        private BackgroundWorker backgroundworker;
        Storyboard storyboard = new Storyboard();
        DoubleAnimation anima = new DoubleAnimation();
        const string HOST = BasicInfo.HOST;
        const string PORT = "8888";
        public string msgLongReceive = ""; // 接收来自服务器端的实际表达的信息
        public string msgLongSend = ""; // 发送给服务器端的实际表达的信息
        public string msgShortReceive = ""; // 接收来自服务器端的一个数据包的信息
        public string msgShortSend = ""; // 发送给服务器端一个数据包的信息

        public SearchUser()
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
            MessageBox.Show(msgLongReceive);
            msgLongReceive = msgLongReceive.Substring(1, msgLongReceive.Length - 2);
            string[] fList = msgLongReceive.Split(new char[] { '&', '/' }); 
            for (int i = 0; i < fList.Length / 5; i++)
            {
                if (fList[i * 5 + 1] == BasicInfo.myName)
                    continue;
                BasicInfo.nearbyUserList.Add(new UserInfo(fList[i * 5 + 1],
                    "photo/photo3.jpg",
                    double.Parse(fList[i * 5 + 2]), double.Parse(fList[i * 5 + 3].Substring(0,4)),
                    double.Parse(fList[i * 5 + 4].Substring(0,4))));
            }
            msgLongReceive = "";
            storyboard.Stop();
            NavigationService.Navigate(new Uri("/UserNearby.xaml", UriKind.Relative));
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            btn.IsEnabled = false;
            BitmapImage i = new BitmapImage(new Uri("switcher.png", UriKind.Relative));
            image.Source = i;
            //Rectangle btn = sender as Rectangle;
            RotateTransform rotateTransform = ellipes.RenderTransform as RotateTransform;
            anima.Duration = new Duration(TimeSpan.FromSeconds(10));
            backgroundworker = new BackgroundWorker();
            Storyboard.SetTarget(anima, rotateTransform);
            Storyboard.SetTargetProperty(anima, new PropertyPath(RotateTransform.AngleProperty));
            storyboard.Children.Add(anima);
            storyboard.Begin();
            backgroundworker.DoWork += new DoWorkEventHandler(backgroundworker_Dowork);
            backgroundworker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundworker_Runworkercompleted);
            anima.From = 0;
            anima.To = 1000;
            // anima.Duration = new Duration(TimeSpan.FromSeconds(5));
            backgroundworker.RunWorkerAsync();
        }

        public async void Start()
        {
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
                msgLongSend = "7|" + BasicInfo.myLatitude.ToString("0.000000") + "|" +
                    BasicInfo.myLongitude.ToString("0.000000") + "|" + "1000" ;
                SendLongDataToServer();
            }
            catch (Exception ex)
            {
            }
        }

        void backgroundworker_Runworkercompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Dispatcher.BeginInvoke(() =>
            {
            });
        }

        void backgroundworker_Dowork(object sender, DoWorkEventArgs e)
        {
            Start();
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            storyboard.Children.Clear();
            base.OnNavigatedFrom(e);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            BitmapImage i = new BitmapImage(new Uri("switch.png", UriKind.Relative));
            image.Source = i;
        }
    }
}