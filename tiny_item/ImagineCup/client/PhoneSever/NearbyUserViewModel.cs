using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSever.ViewModel
{
    public class NearbyUserViewModel : INotifyPropertyChanged
    {
        // 定义用户列表的集合
        private ObservableCollection<UserInfo> _allNearbyUserInfo;
        // 为集合作ViewModel的属性
        public ObservableCollection<UserInfo> AllNearbyUserInfo
        {
            get
            {
                if (_allNearbyUserInfo == null)
                    _allNearbyUserInfo = new ObservableCollection<UserInfo>();
                return _allNearbyUserInfo;
            }
            set
            {
                if (_allNearbyUserInfo != value)
                {
                    _allNearbyUserInfo = value;
                    NotifyPropertyChanged("AllNearbyUserInfo");
                }
            }
        }
        // 定义属性变换时间
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string p)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(p));
            }
        }

        public NearbyUserViewModel()
        {
            try
            {
                for (int i = 0; i < BasicInfo.nearbyUserList.Count; i++)
                {
                    UserInfo temp = new UserInfo()
                    {
                        myName = BasicInfo.nearbyUserList[i].myName,
                        myPhoto = BasicInfo.nearbyUserList[i].myPhoto,
                        myDistance = BasicInfo.nearbyUserList[i].myDistance,
                    };
                    AllNearbyUserInfo.Add(temp);
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Exception:" + e.Message);
            }
        }
    }
}
