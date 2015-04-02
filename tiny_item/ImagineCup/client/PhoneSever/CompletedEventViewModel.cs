using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using PhoneSever;

namespace PhoneSever.ViewModel
{
    public class CompletedEventViewModel : INotifyPropertyChanged
    {
        // 定义用户列表的集合
        private ObservableCollection<EventInfo> _allCompletedEventInfo;
        // 为集合作ViewModel的属性
        public ObservableCollection<EventInfo> AllCompletedEventInfo
        {
            get
            {
                if (_allCompletedEventInfo == null)
                    _allCompletedEventInfo = new ObservableCollection<EventInfo>();
                return _allCompletedEventInfo;
            }
            set
            {
                if (_allCompletedEventInfo != value)
                {
                    _allCompletedEventInfo = value;
                    NotifyPropertyChanged("AllCompletedEventInfo");
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

        public CompletedEventViewModel()
        {
            UpdateCompletedEvent() ;
        }

        public void UpdateCompletedEvent()
        {
            try
            {
                for (int i = 0; i < BasicInfo.completedEventList.Count; i++)
                {
                    EventInfo temp = new EventInfo()
                    {
                        myName = BasicInfo.completedEventList[i].myName,
                        myTime = BasicInfo.completedEventList[i].myTime,
                        myIntroduction = BasicInfo.completedEventList[i].myIntroduction
                    };
                    AllCompletedEventInfo.Add(temp);
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Exception:" + e.Message);
            }
        }
    }
}
