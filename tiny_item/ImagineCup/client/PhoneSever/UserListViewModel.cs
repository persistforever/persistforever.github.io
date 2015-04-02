using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using PhoneSever;

namespace PhoneSever.ViewModel
{
    public class UserListViewModel : INotifyPropertyChanged
    {
        // 定义用户列表的集合
        private ObservableCollection<UserInfo> _allUserInfo;
        // 为集合作ViewModel的属性
        public ObservableCollection<UserInfo> AllUserInfo
        {
            get
            {
                if (_allUserInfo == null)
                    _allUserInfo = new ObservableCollection<UserInfo>();
                return _allUserInfo;
            }
            set
            {
                if (_allUserInfo != value)
                {
                    _allUserInfo = value;
                    NotifyPropertyChanged("AllUserInfo");
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

        public UserListViewModel()
        {
            try
            {
                for (int i = 0; i < BasicInfo.rankingUserList.Count; i++)
                {
                    UserInfo temp = new UserInfo() {
                        myRank = BasicInfo.rankingUserList[i].myRank,
                        myName = BasicInfo.rankingUserList[i].myName,
                        myPhoto = BasicInfo.rankingUserList[i].myPhoto,
                        myScore = BasicInfo.rankingUserList[i].myScore,
                        mySpeed = BasicInfo.rankingUserList[i].mySpeed,
                        myDistance = BasicInfo.rankingUserList[i].myDistance,
                    };
                    AllUserInfo.Add(temp);
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Exception:" + e.Message);
            }
        }
    }
}
