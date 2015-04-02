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
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.Phone.Maps.Controls;
using System.ComponentModel;

namespace PhoneSever
{
    public partial class UserNearby : PhoneApplicationPage
    {
        public double positionLatitude;
        public double positionLongitude;
        private BackgroundWorker backgroundworker; // 后台任务

        public UserNearby()
        {
            InitializeComponent();
        }

        private void ShowMyLocationOnTheMap()
        {
            // Make my current location the center of the Map.
            this.mapWithMyLocation.Center = new GeoCoordinate(positionLatitude, positionLongitude);
            this.mapWithMyLocation.ZoomLevel = 12;
            // 绘制圆形为当前的位置
            Grid myGrid = new Grid();
            Ellipse myCircle1 = new Ellipse();
            myCircle1.Fill = new SolidColorBrush(Colors.Black);
            myCircle1.Height = 40;
            myCircle1.Width = 40;
            myCircle1.Opacity = 80;
            Ellipse myCircle2 = new Ellipse();
            myCircle2.Fill = new SolidColorBrush(Colors.White);
            myCircle2.Height = 30;
            myCircle2.Width = 30;
            myCircle2.Opacity = 80;
            Ellipse myCircle3 = new Ellipse();
            myCircle3.Fill = new SolidColorBrush(Colors.Orange);
            myCircle3.Height = 20;
            myCircle3.Width = 20;
            myCircle3.Opacity = 80;
            myGrid.Children.Add(myCircle1);
            myGrid.Children.Add(myCircle2);
            myGrid.Children.Add(myCircle3);
            // 创建一个包含圆形的MapOverlay
            MapOverlay myLocationOverlay = new MapOverlay();
            myLocationOverlay.Content = myGrid;
            myLocationOverlay.PositionOrigin = new Point(0, 0);
            myLocationOverlay.GeoCoordinate = new GeoCoordinate(positionLatitude, positionLongitude);
            // 创建grid
            MapLayer myLocationLayer = new MapLayer();
            myLocationLayer.Add(myLocationOverlay);
            for (int i = 0; i < BasicInfo.nearbyUserList.Count; i++)
            {
                Grid tempGrid = new Grid();
                tempGrid.RowDefinitions.Add(new RowDefinition());
                tempGrid.RowDefinitions.Add(new RowDefinition());
                tempGrid.Background = new SolidColorBrush(Colors.Transparent);
                //Creating a Rectangle
                Rectangle MyRectangle = new Rectangle();
                MyRectangle.Fill = new SolidColorBrush(Colors.Orange);
                MyRectangle.Height = 30;
                MyRectangle.Width = 30;
                MyRectangle.SetValue(Grid.RowProperty, 0);
                MyRectangle.SetValue(Grid.ColumnProperty, 0);
                tempGrid.Children.Add(MyRectangle);

                //Creating a Polygon
                Polygon MyPolygon = new Polygon();
                MyPolygon.Points.Add(new Point(0, 0));
                MyPolygon.Points.Add(new Point(30, 0));
                MyPolygon.Points.Add(new Point(0, 40));
                MyPolygon.Stroke = new SolidColorBrush(Colors.Black);
                MyPolygon.Fill = new SolidColorBrush(Colors.Black);
                MyPolygon.SetValue(Grid.RowProperty, 1);
                MyPolygon.SetValue(Grid.ColumnProperty, 0);
                tempGrid.Children.Add(MyPolygon);

                TextBlock MyTextBlock = new TextBlock();
                MyTextBlock.Margin = new Thickness(10, 0, 0, 0);
                MyTextBlock.Text = (i+1).ToString();
                MyTextBlock.FontSize = 20;
                MyTextBlock.Foreground = new SolidColorBrush(Colors.Black);
                MyTextBlock.SetValue(Grid.RowProperty, 0);
                MyTextBlock.SetValue(Grid.ColumnProperty, 0);
                tempGrid.Children.Add(MyTextBlock);
                tempGrid.Tap += MyGrid_Tap;

                MapOverlay tempLocationOverlay = new MapOverlay();
                tempLocationOverlay.Content = tempGrid;
                tempLocationOverlay.PositionOrigin = new Point(0, 0);
                tempLocationOverlay.GeoCoordinate = new GeoCoordinate(BasicInfo.nearbyUserList[i].myLatitude,
                    BasicInfo.nearbyUserList[i].myLongitude);
                myLocationLayer.Add(tempLocationOverlay);
            }
            // 将MapLayer加入到地图中
            mapWithMyLocation.Layers.Clear();
            mapWithMyLocation.Layers.Add(myLocationLayer);
        }

        private void MyGrid_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Grid MyGrid = (Grid)sender;
            int index = int.Parse(((TextBlock)(MyGrid.Children[2])).Text);
            BasicInfo.searchUserName = BasicInfo.nearbyUserList[index - 1].myName;
            NavigationService.Navigate(new Uri("/UserInfomation.xaml", UriKind.Relative));
        }

        private void btnReturnToOraginPosition_Click(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ShowMyLocationOnTheMap();
        }

        private void gridNearbyUserInfo_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            int index = lbNearbyUserList.SelectedIndex;
            BasicInfo.searchUserName = BasicInfo.nearbyUserList[index].myName;
            NavigationService.Navigate(new Uri("/UserInfomation.xaml", UriKind.Relative));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            positionLatitude = BasicInfo.myLatitude;
            positionLongitude = BasicInfo.myLongitude;
            ShowMyLocationOnTheMap();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            BasicInfo.nearbyUserList.Clear();
        }
    }
}