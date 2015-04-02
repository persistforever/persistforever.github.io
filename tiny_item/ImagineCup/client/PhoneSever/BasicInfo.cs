using PhoneSever.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSever
{
    public class BasicInfo
    {
        public const string HOST = "169.254.139.78"; // IP地址
        public static string myName = ""; // 姓名
        public static string myPassword = ""; //密码
        public static string searchUserName = ""; // 搜索的用户的姓名
        public static string searchEventName = ""; // 搜索的活动名
        public static string startEventName = ""; // 开始的活动名
        public static double myLatitude ; // 经度
        public static double myLongitude ; // 纬度
        public static double myHeight = 173 ; // 我的身高
        public static double myWeight = 55; // 我的体重
        public static bool mySex = true ; // 我的性别
        public static int myPhoto = 8 ; // 我的头像
        public static double chooseLocationLatitude; // 创建活动时选择的位置纬度
        public static double chooseLocationLongitude; // 创建活动时选择的位置经度
        public static double BrowseEventInfoLocationLatitude; // 浏览活动信息地图经度
        public static double BrowseEventInfoLocationLongitude; // 浏览活动信息地图纬度
        public static List<UserInfo> rankingUserList; // 前台绑定的好友排名列表
        public static List<UserInfo> nearbyUserList; // 前台绑定的附近好友列表
        public static List<EventInfo> completingEventList; // 前台绑定的未完成的活动列表
        public static List<EventInfo> completedEventList; // 前台绑定的完成的活动列表
        public static List<EventInfo> nearbyEventList; // 前台绑定的附近的活动列表
        public static List<EventInfo> userActivityList; // 用户参与的活动列表
        public static CompletedEventViewModel completedEventViewModel ;
        public static CompletingEventViewModel completingEventViewModel;

        static BasicInfo() // 构造函数
        {
            rankingUserList = new List<UserInfo>();
            completingEventList = new List<EventInfo>();
            completedEventList = new List<EventInfo>();
            nearbyEventList = new List<EventInfo>();
            nearbyUserList = new List<UserInfo>();
            userActivityList = new List<EventInfo>();
            completedEventViewModel = new CompletedEventViewModel();
            completingEventViewModel = new CompletingEventViewModel();
        }
    }

    public class UserInfo // 前台绑定需要的用户信息
    {
        public int myRank
        {
            get;
            set; 
        }
        public string myName // 姓名
        {   
            get;
            set;
        }
        public double myScore // 分数
        {
            get;
            set;
        }
        public string myPhoto // 头像
        {
            get;
            set;
        }
        public double mySpeed // 速度
        {
            get;
            set;
        }
        public double myDistance // 距离
        {
            get;
            set;
        }
        public double myLatitude // 经度
        {
            get;
            set;
        }
        public double myLongitude // 纬度
        {
            get;
            set;
        }
        public UserInfo(string name, double score, string photo, double distance, double speed, int rank)
        {
            myName = name;
            myScore = score;
            myPhoto = photo;
            myDistance = distance;
            mySpeed = speed;
            myRank = rank;
        }
        public UserInfo(string name, string photo, double distance, double latitude, double longitude)
        {
            myLatitude = latitude;
            myLongitude = longitude;
            myName = name;
            myPhoto = photo;
            myDistance = distance;
        }
        public UserInfo()
        { }
    }

    public class EventInfo // 前台绑定需要的活动信息
    {
        public string myName // 姓名
        {
            get;
            set;
        }
        public string myTime // 时间
        {
            get;
            set;
        }
        public string myIntroduction // 介绍
        {
            get;
            set;
        }
        public string myDistance // 距离
        {
            get;
            set;
        }
        public double myLatitude // 经度
        {
            get;
            set;
        }
        public double myLongitude // 纬度
        {
            get;
            set;
        }
        public EventInfo(string name, string time, string introduction)
        {
            myName = name ;
            myTime = time ;
            myIntroduction = introduction ;
        }
        public EventInfo(string name, string introduction, string distance, double latitude, double longitude)
        {
            myName = name;
            myIntroduction = introduction;
            myDistance = distance;
            myLatitude = latitude;
            myLongitude = longitude;
        }
        public EventInfo(string name)
        {
            myName = name;
        }
        public EventInfo()
        { }
    }
}