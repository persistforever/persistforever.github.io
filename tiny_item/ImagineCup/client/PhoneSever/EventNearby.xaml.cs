using Microsoft.Phone.Controls;
using Microsoft.Phone.Maps.Controls;
using System;
using System.ComponentModel;
using System.Device.Location;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Windows.Devices.Geolocation;

namespace PhoneSever
{
    public partial class EventNearby : PhoneApplicationPage
    {
        double origMyLatitude; // 最初的经度 ;
        double origMyLongitude; // 最初的纬度 ;
        private BackgroundWorker backgroundworker; // 后台任务

        public EventNearby()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            origMyLatitude = BasicInfo.myLatitude;
            origMyLongitude = BasicInfo.myLongitude;
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
            myLocationOverlay.GeoCoordinate = new GeoCoordinate(origMyLatitude, origMyLongitude);
            // 创建grid
            MapLayer myLocationLayer = new MapLayer();
            myLocationLayer.Add(myLocationOverlay);
            for (int i = 0; i < BasicInfo.nearbyEventList.Count; i++)
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
                MyTextBlock.Text = (i + 1).ToString();
                MyTextBlock.FontSize = 20;
                MyTextBlock.Foreground = new SolidColorBrush(Colors.Black);
                MyTextBlock.SetValue(Grid.RowProperty, 0);
                MyTextBlock.SetValue(Grid.ColumnProperty, 0);
                tempGrid.Children.Add(MyTextBlock);
                tempGrid.Tap += MyGrid_Tap;

                MapOverlay tempLocationOverlay = new MapOverlay();
                tempLocationOverlay.Content = tempGrid;
                tempLocationOverlay.PositionOrigin = new Point(0, 0);
                tempLocationOverlay.GeoCoordinate = new GeoCoordinate(BasicInfo.nearbyEventList[i].myLatitude,
                BasicInfo.nearbyEventList[i].myLongitude);
                myLocationLayer.Add(tempLocationOverlay);
            }
            // 将MapLayer加入到地图中
            mapWithMyLocation.Layers.Clear();
            mapWithMyLocation.Layers.Add(myLocationLayer);
        }
        void MyGrid_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Grid MyGrid = (Grid)sender ;
            int index = int.Parse(((TextBlock)(MyGrid.Children[2])).Text);
            BasicInfo.searchEventName = BasicInfo.nearbyEventList[index - 1].myName;
            NavigationService.Navigate(new Uri("/EventInfomation.xaml", UriKind.Relative));
        }

        private void gridNearbyEventInfo_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            int index = lbNearbyEventRanking.SelectedIndex ;
            BasicInfo.searchEventName = BasicInfo.nearbyEventList[index].myName;
            NavigationService.Navigate(new Uri("/EventInfomation.xaml", UriKind.Relative));
        }

        private void btnReturnToOraginPosition_Click(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ShowMyLocationOnTheMap();
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            BasicInfo.nearbyEventList.Clear();
        }
    }
}