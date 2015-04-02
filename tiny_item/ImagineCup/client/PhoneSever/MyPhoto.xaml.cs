using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;

namespace PhoneSever
{
    public partial class MyPhoto : PhoneApplicationPage
    {
        public MyPhoto()
        {
            InitializeComponent();
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            btn1.BorderBrush = new SolidColorBrush(Colors.Purple);
            btn2.BorderBrush = new SolidColorBrush(Colors.Gray);
            btn3.BorderBrush = new SolidColorBrush(Colors.Gray);
            btn4.BorderBrush = new SolidColorBrush(Colors.Gray);
            btn5.BorderBrush = new SolidColorBrush(Colors.Gray);
            btn6.BorderBrush = new SolidColorBrush(Colors.Gray);
            btn7.BorderBrush = new SolidColorBrush(Colors.Gray);
            btn8.BorderBrush = new SolidColorBrush(Colors.Gray);
            BasicInfo.myPhoto = 1;
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            btn1.BorderBrush = new SolidColorBrush(Colors.Gray);
            btn2.BorderBrush = new SolidColorBrush(Colors.Purple);
            btn3.BorderBrush = new SolidColorBrush(Colors.Gray);
            btn4.BorderBrush = new SolidColorBrush(Colors.Gray);
            btn5.BorderBrush = new SolidColorBrush(Colors.Gray);
            btn6.BorderBrush = new SolidColorBrush(Colors.Gray);
            btn7.BorderBrush = new SolidColorBrush(Colors.Gray);
            btn8.BorderBrush = new SolidColorBrush(Colors.Gray);
            BasicInfo.myPhoto = 2;
        }

        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            btn1.BorderBrush = new SolidColorBrush(Colors.Gray);
            btn2.BorderBrush = new SolidColorBrush(Colors.Gray);
            btn3.BorderBrush = new SolidColorBrush(Colors.Purple);
            btn4.BorderBrush = new SolidColorBrush(Colors.Gray);
            btn5.BorderBrush = new SolidColorBrush(Colors.Gray);
            btn6.BorderBrush = new SolidColorBrush(Colors.Gray);
            btn7.BorderBrush = new SolidColorBrush(Colors.Gray);
            btn8.BorderBrush = new SolidColorBrush(Colors.Gray);
            BasicInfo.myPhoto = 3;
        }

        private void btn4_Click(object sender, RoutedEventArgs e)
        {
            btn1.BorderBrush = new SolidColorBrush(Colors.Gray);
            btn2.BorderBrush = new SolidColorBrush(Colors.Gray);
            btn3.BorderBrush = new SolidColorBrush(Colors.Gray);
            btn4.BorderBrush = new SolidColorBrush(Colors.Purple);
            btn5.BorderBrush = new SolidColorBrush(Colors.Gray);
            btn6.BorderBrush = new SolidColorBrush(Colors.Gray);
            btn7.BorderBrush = new SolidColorBrush(Colors.Gray);
            btn8.BorderBrush = new SolidColorBrush(Colors.Gray);
            BasicInfo.myPhoto = 4;
        }

        private void btn5_Click(object sender, RoutedEventArgs e)
        {
            btn1.BorderBrush = new SolidColorBrush(Colors.Gray);
            btn2.BorderBrush = new SolidColorBrush(Colors.Gray);
            btn3.BorderBrush = new SolidColorBrush(Colors.Gray);
            btn4.BorderBrush = new SolidColorBrush(Colors.Gray);
            btn5.BorderBrush = new SolidColorBrush(Colors.Purple);
            btn6.BorderBrush = new SolidColorBrush(Colors.Gray);
            btn7.BorderBrush = new SolidColorBrush(Colors.Gray);
            btn8.BorderBrush = new SolidColorBrush(Colors.Gray);
            BasicInfo.myPhoto = 5;
        }

        private void btn6_Click(object sender, RoutedEventArgs e)
        {
            btn1.BorderBrush = new SolidColorBrush(Colors.Gray);
            btn2.BorderBrush = new SolidColorBrush(Colors.Gray);
            btn3.BorderBrush = new SolidColorBrush(Colors.Gray);
            btn4.BorderBrush = new SolidColorBrush(Colors.Gray);
            btn5.BorderBrush = new SolidColorBrush(Colors.Gray);
            btn6.BorderBrush = new SolidColorBrush(Colors.Purple);
            btn7.BorderBrush = new SolidColorBrush(Colors.Gray);
            btn8.BorderBrush = new SolidColorBrush(Colors.Gray);
            BasicInfo.myPhoto = 6;
        }

        private void btn7_Click(object sender, RoutedEventArgs e)
        {
            btn1.BorderBrush = new SolidColorBrush(Colors.Gray);
            btn2.BorderBrush = new SolidColorBrush(Colors.Gray);
            btn3.BorderBrush = new SolidColorBrush(Colors.Gray);
            btn4.BorderBrush = new SolidColorBrush(Colors.Gray);
            btn5.BorderBrush = new SolidColorBrush(Colors.Gray);
            btn6.BorderBrush = new SolidColorBrush(Colors.Gray);
            btn7.BorderBrush = new SolidColorBrush(Colors.Purple);
            btn8.BorderBrush = new SolidColorBrush(Colors.Gray);
            BasicInfo.myPhoto = 7;
        }

        private void btn8_Click(object sender, RoutedEventArgs e)
        {
            btn1.BorderBrush = new SolidColorBrush(Colors.Gray);
            btn2.BorderBrush = new SolidColorBrush(Colors.Gray);
            btn3.BorderBrush = new SolidColorBrush(Colors.Gray);
            btn4.BorderBrush = new SolidColorBrush(Colors.Gray);
            btn5.BorderBrush = new SolidColorBrush(Colors.Gray);
            btn6.BorderBrush = new SolidColorBrush(Colors.Gray);
            btn7.BorderBrush = new SolidColorBrush(Colors.Gray);
            btn8.BorderBrush = new SolidColorBrush(Colors.Purple);
            BasicInfo.myPhoto = 8;
        }
    }
}