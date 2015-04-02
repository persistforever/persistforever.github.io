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
using System.Windows.Media.Imaging;
using System.IO.IsolatedStorage;

namespace PhoneSever
{
    public partial class Settings : PhoneApplicationPage
    {
        public string myPhoto ;

        public Settings()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (IsolatedStorageSettings.ApplicationSettings.Contains("Weight"))
            {
                string temp = IsolatedStorageSettings.ApplicationSettings["Weight"] as string;
                if(temp != null)
                    BasicInfo.myWeight = double.Parse(temp);
            } if (IsolatedStorageSettings.ApplicationSettings.Contains("Height"))
            {
                string temp = IsolatedStorageSettings.ApplicationSettings["Height"] as string;
                if(temp != null)
                    BasicInfo.myHeight = double.Parse(temp);
            } if (IsolatedStorageSettings.ApplicationSettings.Contains("Sex"))
            {
                string temp = IsolatedStorageSettings.ApplicationSettings["Sex"] as string;
                if (temp != null)
                    BasicInfo.mySex = bool.Parse(temp);
            }
            tbxHeight.Text = BasicInfo.myHeight.ToString();
            tbxWeight.Text = BasicInfo.myWeight.ToString();
            rdbMale.IsChecked = BasicInfo.mySex;
            rdbFemale.IsChecked = !BasicInfo.mySex;
            BitmapImage ibImage = new BitmapImage(new Uri("photo/photo" + BasicInfo.myPhoto.ToString() + ".jpg"
                , UriKind.Relative));
            ibPhoto.Source = ibImage;
        }

        private void btnPhoto_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MyPhoto.xaml", UriKind.Relative));
        }

        private void rdbMale_Click(object sender, RoutedEventArgs e)
        {
            BasicInfo.mySex = true;
        }

        private void rdbFamale_Click(object sender, RoutedEventArgs e)
        {
            BasicInfo.mySex = false;
        }

        private void btnEnsure_Click(object sender, EventArgs e)
        {
            if (double.Parse(tbxHeight.Text) > 300 || double.Parse(tbxHeight.Text) < 100)
            {
                MessageBox.Show("身高必须在100~300厘米之间");
                return;
            }
            else
            {
                BasicInfo.myHeight = double.Parse(tbxHeight.Text);
            }
            if (double.Parse(tbxWeight.Text) > 200 || double.Parse(tbxWeight.Text) < 30)
            {
                MessageBox.Show("体重必须在30~300千克之间");
                return;
            }
            else
            {
                BasicInfo.myWeight = double.Parse(tbxWeight.Text);
            }
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            if (!settings.Contains("Weight"))
            {
                settings.Add("Weight", BasicInfo.myWeight);
            }
            else
            {
                settings["Weight"] = BasicInfo.myWeight.ToString();
            }
            if (!settings.Contains("Height"))
            {
                settings.Add("Height", BasicInfo.myHeight);
            }
            else
            {
                settings["Height"] = BasicInfo.myHeight.ToString();
            }
            if (!settings.Contains("Sex"))
            {
                settings.Add("Sex", BasicInfo.mySex);
            }
            else
            {
                settings["Sex"] = BasicInfo.mySex.ToString();
            }
            settings.Save();// 确认之后存储设置
            this.NavigationService.GoBack();
        } 

    }
}