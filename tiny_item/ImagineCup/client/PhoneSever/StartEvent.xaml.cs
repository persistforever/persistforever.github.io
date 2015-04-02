using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Windows.Devices.Geolocation;
using System.Device.Location;
using System.Windows.Shapes;
using System.Windows.Media;
using Microsoft.Phone.Maps.Controls;
using System.Windows.Threading;
using System.ComponentModel;
using System.Net.Sockets;
using System.Text;

namespace PhoneSever
{
    public partial class StartEvent : PhoneApplicationPage
    {
        const string HOST = BasicInfo.HOST;
        const string PORT = "8888";
        public string msgLongReceive = ""; // 接收来自服务器端的实际表达的信息
        public string msgLongSend = ""; // 发送给服务器端的实际表达的信息
        public string msgShortReceive = ""; // 接收来自服务器端的一个数据包的信息
        public string msgShortSend = ""; // 发送给服务器端一个数据包的信息
        private BackgroundWorker backgroundworker; // 后台任务
        Geolocator geolocator = null;
        bool tracking = false;
        int numPoint = -1; // 记录绘制的点的个数
        List<double> latitudeList = new List<double>(); // 纬度链表
        List<double> longitudeList = new List<double>(); // 经度链表
        DispatcherTimer dispatcherTimer = new DispatcherTimer(); // 定义计时器
        public int second = 0; // 秒
        public int minute = 0; // 分
        public int hour = 0; // 时
        public double distance = 0; // 距离
        public double speed = 0; // 速度
        
        // 构造函数
        public StartEvent()
        {
            InitializeComponent();
            // 用于本地化 ApplicationBar 的示例代码
            //BuildLocalizedApplicationBar();
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
            msgLongReceive = "";
        }
        
        private void TrackLocation_Click(object sender, RoutedEventArgs e)
        {
            if (!tracking)
            {
                dispatcherTimer.Start();
                geolocator = new Geolocator();
                geolocator.DesiredAccuracy = PositionAccuracy.High;
                geolocator.MovementThreshold = 40;
                geolocator.StatusChanged += geolocator_StatusChanged;
                geolocator.PositionChanged += geolocator_PositionChanged;
                tracking = true;
                stopRect.Visibility = Visibility.Visible ;
                startTrangle.Visibility = Visibility.Collapsed ;
            }
            else
            {
                dispatcherTimer.Stop();
                geolocator.PositionChanged -= geolocator_PositionChanged;
                geolocator.StatusChanged -= geolocator_StatusChanged;
                geolocator = null;
                tracking = false;
                startTrangle.Visibility = Visibility.Visible ;
                stopRect.Visibility = Visibility.Collapsed ;
            }
        }

        private void geolocator_PositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            distance += 0.04;
            double time = hour + minute / 60.0 + second / 3600.0;
            speed = distance / time;
            if (App.InBackground == false)
            {
                Dispatcher.BeginInvoke(() =>
                    {
                        numPoint++;
                        latitudeList.Add(args.Position.Coordinate.Latitude) ;
                        longitudeList.Add(args.Position.Coordinate.Longitude) ;
                        ShowMyLocationOnTheMap();
                    });
            }
        }

        private void geolocator_StatusChanged(Geolocator sender, StatusChangedEventArgs args)
        {
            string status = "";
            switch (args.Status)
            {
                case PositionStatus.Disabled :
                    status = "位置服务禁止";
                    break;
                case PositionStatus.Initializing :
                    status = "正在初始化";
                    break;
                case PositionStatus.NoData :
                    status = "没有数据";
                    break;
                case PositionStatus.Ready :
                    status = "准备中";
                    break;
                case PositionStatus.NotAvailable :
                    status = "不可用";
                    break;
                case PositionStatus.NotInitialized :
                    status = "未初始化";
                    break;
            }
            Dispatcher.BeginInvoke(() =>
                {
                });
        }

        private void ShowMyLocationOnTheMap()
        {
            this.mapWithMyLocation.Center = new GeoCoordinate(latitudeList[numPoint], longitudeList[numPoint]);
            this.mapWithMyLocation.ZoomLevel = 18;
            // 绘制圆形为当前的位置
            Ellipse myCircle = new Ellipse();
            myCircle.Fill = new SolidColorBrush(Colors.Red);
            myCircle.Height = 15;
            myCircle.Width = 15;
            myCircle.Opacity = 50;
            // 创建一个包含圆形的MapOverlay
            MapOverlay myLocationOverlay = new MapOverlay();
            myLocationOverlay.Content = myCircle;
            myLocationOverlay.PositionOrigin = new Point(0, 0);
            myLocationOverlay.GeoCoordinate = new GeoCoordinate(latitudeList[numPoint], longitudeList[numPoint]);            // 创建一个MapLayer包含MapOverlay
            MapLayer myLocationLayer = new MapLayer();
            myLocationLayer.Add(myLocationOverlay);
            // 将MapLayer加入到地图中
            mapWithMyLocation.Layers.Add(myLocationLayer);
        }

        private void btnReturnToOraginPosition_Click(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (numPoint >= 0)
            {
                this.mapWithMyLocation.Center = new GeoCoordinate(latitudeList[numPoint], longitudeList[numPoint]);
                this.mapWithMyLocation.ZoomLevel = 18;
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e) // 定时器跳动时分秒增加
        {
            second++;
            if (second >= 60)
            {
                second = 0;
                minute++;
                if (minute >= 60)
                {
                    minute = 0;
                    hour++;
                    if (hour >= 24)
                    {
                        second = 0;
                        minute = 0;
                        hour = 0;
                    }
                }
            }
            ShowTimeOnTxt();
        }

        public void ShowTimeOnTxt() // 显示时间
        {
            txtSec1.Text = (second % 10).ToString();
            txtSec2.Text = (second / 10).ToString();
            txtMin1.Text = (minute % 10).ToString();
            txtMin2.Text = (minute / 10).ToString();
            txtHour1.Text = (hour % 10).ToString();
            txtHour2.Text = (hour / 10).ToString();
            txtDistance.Text = distance.ToString("0.0");
            txtSpeed.Text = speed.ToString("0.0");
            txtEnergy.Text = (BasicInfo.myWeight * distance * 1.036).ToString("0.0");
            txtExplore.Text = (BasicInfo.myWeight * speed * 3.6 / 75.0).ToString("0.0");
            txtPersist.Text = (speed * distance).ToString("0.0");
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ShowTimeOnTxt();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            dispatcherTimer.Stop();
            if (tracking)
            {
                geolocator.PositionChanged -= geolocator_PositionChanged;
                geolocator.StatusChanged -= geolocator_StatusChanged;
                geolocator = null;
                tracking = false;
            }
            startTrangle.Visibility = Visibility.Visible;
            stopRect.Visibility = Visibility.Collapsed;
            msgLongSend = "9|" + BasicInfo.myName + "|" + BasicInfo.startEventName + "|" +
                speed.ToString() + "|" + distance.ToString() + "|" + "20|" + "12";
            SendLongDataToServer();
            second = 0;
            minute = 0;
            hour = 0;
            distance = 0;
            speed = 0;
            txtDistance.Text = "0.0"; txtEnergy.Text = "0.0"; txtHour1.Text = "0";
            txtHour2.Text = "0"; txtMin1.Text = "0"; txtMin2.Text = "0"; txtPersist.Text = "0.0";
            txtSec1.Text = "0"; txtSec2.Text = "0"; txtSpeed.Text = "0.0"; txtExplore.Text = "0.0";
        }
    }
}