using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Shapes;
using System.Device.Location;
using Windows.Devices.Geolocation;
using System.Windows.Media;
using Microsoft.Phone.Maps.Controls;
using System.ComponentModel;

namespace PhoneSever
{
    public partial class ChoosePosition : PhoneApplicationPage
    {
        double origMyLatitude; // 最初的经度 ;
        double origMyLongitude; // 最初的纬度 ;
        private BackgroundWorker backgroundworker; // 后台任务

        public ChoosePosition()
        {
            InitializeComponent();
            origMyLatitude = BasicInfo.myLatitude;
            origMyLongitude = BasicInfo.myLongitude;
            BasicInfo.chooseLocationLatitude = origMyLatitude;
            BasicInfo.chooseLocationLongitude = origMyLongitude;
            ShowMyLocationOnTheMap();
        }

        private void btnUp_Click(object sender, RoutedEventArgs e)
        {
            BasicInfo.chooseLocationLatitude += 0.0001;
            ShowMyLocationOnTheMap();
        }

        private void btnDown_Click(object sender, RoutedEventArgs e)
        {
            BasicInfo.chooseLocationLatitude -= 0.0001;
            ShowMyLocationOnTheMap();
        }

        private void btnLeft_Click(object sender, RoutedEventArgs e)
        {
            BasicInfo.chooseLocationLongitude -= 0.0001;
            ShowMyLocationOnTheMap();
        }

        private void btnRight_Click(object sender, RoutedEventArgs e)
        {
            BasicInfo.chooseLocationLongitude += 0.0001;
            ShowMyLocationOnTheMap();
        }

        private void ShowMyLocationOnTheMap()
        {
            // Make my current location the center of the Map.
            this.mapWithMyLocation.Center = new GeoCoordinate(origMyLatitude, origMyLongitude);
            this.mapWithMyLocation.ZoomLevel = 15;
            // 绘制圆形为当前的位置
            Grid myGrid = new Grid();
            Ellipse myCircle1 = new Ellipse();
            myCircle1.Fill = new SolidColorBrush(Colors.Black);
            myCircle1.Height = 50;
            myCircle1.Width = 50;
            myCircle1.Opacity = 80;
            Ellipse myCircle2 = new Ellipse();
            myCircle2.Fill = new SolidColorBrush(Colors.White);
            myCircle2.Height = 40;
            myCircle2.Width = 40;
            myCircle2.Opacity = 80;
            Ellipse myCircle3 = new Ellipse();
            myCircle3.Fill = new SolidColorBrush(Colors.Orange);
            myCircle3.Height = 30;
            myCircle3.Width = 30;
            myCircle3.Opacity = 80;
            myGrid.Children.Add(myCircle1);
            myGrid.Children.Add(myCircle2);
            myGrid.Children.Add(myCircle3);
            // 创建一个包含圆形的MapOverlay
            MapOverlay myLocationOverlay = new MapOverlay();
            myLocationOverlay.Content = myGrid;
            myLocationOverlay.PositionOrigin = new Point(0, 0);
            myLocationOverlay.GeoCoordinate = new GeoCoordinate(BasicInfo.chooseLocationLatitude, BasicInfo.chooseLocationLongitude);
            // 创建一个MapLayer包含MapOverlay
            MapLayer myLocationLayer = new MapLayer();
            myLocationLayer.Add(myLocationOverlay);
            mapWithMyLocation.Layers.Clear();
            // 将MapLayer加入到地图中
            mapWithMyLocation.Layers.Add(myLocationLayer);
        }

        private void btnReturnToOraginPosition_Click(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ShowMyLocationOnTheMap();
        }

        private void btnOK_Click(object sender, System.Windows.Input.GestureEventArgs e)
        {
            MessageBox.Show(BasicInfo.chooseLocationLatitude.ToString());
            this.NavigationService.GoBack();
        }

    }
}