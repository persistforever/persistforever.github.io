using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using PhoneSever;

namespace PhoneSever.ViewModel
{
    public class UserActivityListViewModel : INotifyPropertyChanged
    {
        // 定义用户列表的集合
        private ObservableCollection<EventInfo> _allUserActivityInfo;
        // 为集合作ViewModel的属性
        public ObservableCollection<EventInfo> AllUserActivityInfo
        {
            get
            {
                if (_allUserActivityInfo == null)
                    _allUserActivityInfo = new ObservableCollection<EventInfo>();
                return _allUserActivityInfo;
            }
            set
            {
                if (_allUserActivityInfo != value)
                {
                    _allUserActivityInfo = value;
                    NotifyPropertyChanged("AllUserActivityInfo");
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

        public UserActivityListViewModel()
        {
            try
            {
                for (int i = 0; i < BasicInfo.userActivityList.Count; i++)
                {
                    EventInfo temp = new EventInfo()
                    {
                        myName = BasicInfo.userActivityList[i].myName,
                    };
                    AllUserActivityInfo.Add(temp);
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Exception:" + e.Message);
            }
        }
    }
}
