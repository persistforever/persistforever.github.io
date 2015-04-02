using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace PhoneSever
{
    public partial class ChooseStartEvent : PhoneApplicationPage
    {
        public ChooseStartEvent()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            int index = lbCompletingEventRanking.SelectedIndex;
            MessageBox.Show(index.ToString());
            DateTime dateTime = DateTime.Now;
            string[] time = BasicInfo.completingEventList[index].myTime.Split(new char[]{'/'});
            if (dateTime.Year == int.Parse(time[0]) && dateTime.Month == int.Parse(time[1])
                && dateTime.Day == int.Parse(time[2]) && dateTime.Hour == int.Parse(time[3]))
            {
                BasicInfo.startEventName = BasicInfo.completingEventList[index].myName;
                NavigationService.Navigate(new Uri("/StartEvent.xaml", UriKind.Relative));
            }
            else
            {
                MessageBox.Show("The event doesn't start");
                return;
            }
        }
    }
}