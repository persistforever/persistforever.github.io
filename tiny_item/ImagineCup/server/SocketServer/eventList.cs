using System;
using System.IO;

namespace SocketServer
{
    public class ActivityList  //活动链表
    {
        private class Activity  //活动类
        {
            public string name, locationInfo, sponsor, longitude, latitude, joinUser, time; //数据域
            public Activity next; //指针域
            public Activity(string name, string locationInfo, string sponsor, string longitude, string latitude,
                string joinUser, string time) //构造函数
            {
                this.name = name;
                this.locationInfo = locationInfo;
                this.sponsor = sponsor;
                this.longitude = longitude;
                this.latitude = latitude;
                this.joinUser = joinUser;
                this.time = time;
            }
        }

        private int count;
        public int Count //计数
        {
            get { return this.count; }
        }

        private Activity head; //头指针

        public void AddActivity(string name, string locationInfo, string sponsor, string longitude, string latitude,
                string joinUser, string time)  //向链表中加入一个活动
        {
            Activity newNode = new Activity(name, locationInfo, sponsor, longitude, latitude, joinUser, time);
            if (head == null)
            {
                head = newNode;
                head.next = null;
            }
            else
            {
                Activity temp = head;
                while (temp.next != null)
                {
                    temp = temp.next;
                };
                temp.next = newNode;
            }
            count++;
        }

        public string PostEvent(string result) //发起活动
        {
            //切割数据
            string[] answer = result.Split(new char[] { '|' });
            string name = answer[1];
            Activity temp = head;
            while (temp != null)
            {
                if (temp.name == name)
                    return "this event exists";
                temp = temp.next;
            }
            int i = 1;
            AddActivity(answer[i++], answer[i++], answer[i++], answer[i++], answer[i++], "", answer[i++]);
            UpdateList(name);
            UpdateInfo(name);
            return "Create event successful";
        }


        public string searchEvent(string result) //查询周围活动
        {
            string[] location = result.Split(new char[] { '|' });
            string longitude = location[1], latitude = location[2], range = location[3], searchResult = "";
            Activity temp = head;
            double x1 = System.Double.Parse(longitude), y1 = System.Double.Parse(latitude), drange = System.Double.Parse(range);
            while (temp != null)
            {
                double x2 = System.Double.Parse(temp.longitude);
                double y2 = System.Double.Parse(temp.latitude);
                double a = (x1 - x2) / 2;
                double aa = Math.Sin(a) * Math.Sin(a);
                double b = (y1 - y2) / 2;
                double bb = Math.Cos(x1) * Math.Cos(x2) * Math.Sin(b) * Math.Sin(b);
                double distance = 2 * Math.Asin(Math.Sqrt(aa + bb)) * 6378.137;
                if (distance < drange)
                {
                    searchResult = searchResult + temp.name + '|' + temp.locationInfo + '|' + temp.sponsor + '|' + temp.time
                        + '|' + distance + '|' + temp.joinUser + '|' + temp.longitude + '|' + temp.latitude+'|';
                }
                temp = temp.next;
            }
            string[] activity = searchResult.Split(new char[] { '|' });
            for (int i = 0; i < activity.Length / 8; i++)
                for (int j = i + 1; j < activity.Length / 8; j++)
                {
                    if (Double.Parse(activity[8 * j + 4]) < Double.Parse(activity[8 * i + 4]))
                    {
                        string strTmp = null;
                        for (int k = 0; k < 8; k++)
                        { strTmp = activity[8 * j + k]; activity[8 * j + k] = activity[8 * i + k]; activity[8 * i + k] = strTmp; }
                    }
                }
            result = null;
            for (int i = 0; i < activity.Length / 8; i++)
            {
                for (int j = 0; j < 8; j++)
                    result = result + activity[8 * i + j] + '|';
            }
            return result;
        }

        public string JoinActivity(string result) //加入活动
        {
            string[] answer = result.Split(new char[] { '|' });
            string activityName = answer[1], userName = answer[2];
            Activity temp = head;
            while (temp != null)
            {
                if (temp.name == activityName)
                {
                    break;
                }
                temp = temp.next;
            }
            if (temp == null)
            {
                return "Join in failure";
            }
            string[] activity = temp.joinUser.Split(new char[] { '&' });
            for (int i = 0; i < activity.Length; i++)
            {
                if (activity[i] == userName)
                    return "You have joined it!";
            }
            temp.joinUser = temp.joinUser + userName + "&";
            UpdateInfo(activityName);
            return "Join in successful!";
        }
        public string activityInfo(string result)
        {
            string[] answer = result.Split(new char[] { '|' });
            string activityName = answer[1];
            Activity temp = head;
            while (temp != null)
            {
                if (temp.name == activityName)
                    break;
                temp = temp.next;
            }
            return temp.name + '|' + temp.locationInfo + '|' + temp.sponsor + '|' + temp.time
                        + '|' + temp.joinUser + '|' + temp.longitude + '|' + temp.latitude;
        }

        public string findMyActivity(string result) //我的活动
        {
            if (result == "") return "";
            string[] activity = result.Split(new char[] { '|' });
            string futureActivity = activity[0];
            string[] partFuture = futureActivity.Split(new char[] { '&' });
            string finishActivity = activity[1];
            string[] parFinish = finishActivity.Split(new char[] { '&' });
            string back = "";

            for (int i = 0; i < partFuture.Length; i++)
            {
            Activity temp = head;
                while (temp != null)
                {
                    if (temp.name == partFuture[i])
                    {
                        back = back + temp.name + '&' + temp.time + '&' + temp.locationInfo + '&';
                        break;
                    }
                    temp = temp.next;
                }
            }
            back += '|';
            for (int i = 0; i < parFinish.Length; i++)
            {
                Activity temp = head;
                while (temp != null)
                {
                    if (temp.name == parFinish[i])
                    {
                        back = back + temp.name + '&' + temp.time + '&' + temp.locationInfo + '&';
                        break;
                    }
                    temp = temp.next;
                }
            }
            return back;
        }
        public void UpdateInfo(string ActivityName) //更新活动信息
        {
            Activity temp = head;
            while (temp != null)
            {
                if (temp.name == ActivityName)
                    break;
                temp = temp.next;
            }
            string result = temp.name + '|' + temp.locationInfo + '|' + temp.sponsor + '|'
                + temp.longitude + '|' + temp.latitude + '|' + temp.joinUser + '|' + temp.time + '|';
            string strname = "data\\"+ ActivityName + "Activity.txt";
            FileStream myFs1 = new FileStream(strname, FileMode.Create);
            StreamWriter mySw1 = new StreamWriter(myFs1);
            mySw1.Write(result);
            mySw1.Close();
            myFs1.Close();
        }
        public void UpdateList(string activityName) //更新链表信息
        {
            string strList = "data\\ActivityList.txt";
            if (File.Exists(strList))
            {
                FileStream myFs = new FileStream(strList, FileMode.Append);
                StreamWriter mySw = new StreamWriter(myFs);
                mySw.Write(activityName + "|");
                mySw.Close();
                myFs.Close();
            }
            else
            {
                FileStream myFs = new FileStream(strList, FileMode.Create);
                StreamWriter mySw = new StreamWriter(myFs);
                mySw.Write(activityName + "|");
                mySw.Close();
                myFs.Close();
            }
        }
    }
}

