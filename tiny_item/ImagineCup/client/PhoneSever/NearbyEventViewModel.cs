using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSever.ViewModel
{
    public class NearbyEventViewModel : INotifyPropertyChanged
    {
        // 定义用户列表的集合
        private ObservableCollection<EventInfo> _allNearbyEventInfo;
        // 为集合作ViewModel的属性
        public ObservableCollection<EventInfo> AllNearbyEventInfo
        {
            get
            {
                if (_allNearbyEventInfo == null)
                    _allNearbyEventInfo = new ObservableCollection<EventInfo>();
                return _allNearbyEventInfo;
            }
            set
            {
                if (_allNearbyEventInfo != value)
                {
                    _allNearbyEventInfo = value;
                    NotifyPropertyChanged("AllNearbyEventInfo");
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

        public NearbyEventViewModel()
        {
            try
            {
                for (int i = 0; i < BasicInfo.nearbyEventList.Count; i++)
                {
                    EventInfo temp = new EventInfo()
                    {
                        myName = BasicInfo.nearbyEventList[i].myName,
                        myDistance = BasicInfo.nearbyEventList[i].myDistance,
                        myIntroduction = BasicInfo.nearbyEventList[i].myIntroduction
                    };
                    AllNearbyEventInfo.Add(temp);
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Exception:" + e.Message);
            }
        }
    }
}
