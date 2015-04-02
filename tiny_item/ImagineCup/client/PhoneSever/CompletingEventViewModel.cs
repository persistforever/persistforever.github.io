using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using PhoneSever;

namespace PhoneSever.ViewModel
{
    public class CompletingEventViewModel : INotifyPropertyChanged
    {
        // 定义用户列表的集合
        private ObservableCollection<EventInfo> _allCompletingEventInfo;
        // 为集合作ViewModel的属性
        public ObservableCollection<EventInfo> AllCompletingEventInfo
        {
            get
            {
                if (_allCompletingEventInfo == null)
                    _allCompletingEventInfo = new ObservableCollection<EventInfo>();
                return _allCompletingEventInfo;
            }
            set
            {
                if (_allCompletingEventInfo != value)
                {
                    _allCompletingEventInfo = value;
                    NotifyPropertyChanged("AllCompletingEventInfo");
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

        public CompletingEventViewModel()
        {
            UpdateCompletingEvent();
        }

        public void UpdateCompletingEvent()
        {
            try
            {
                for (int i = 0; i < BasicInfo.completingEventList.Count; i++)
                {
                    EventInfo temp = new EventInfo()
                    {
                        myName = BasicInfo.completingEventList[i].myName,
                        myTime = BasicInfo.completingEventList[i].myTime,
                        myIntroduction = BasicInfo.completingEventList[i].myIntroduction
                    };
                    AllCompletingEventInfo.Add(temp);
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Exception:" + e.Message);
            }
        }
    }
}
