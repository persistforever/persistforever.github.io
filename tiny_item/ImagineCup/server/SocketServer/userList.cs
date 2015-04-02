using System;
using System.IO;

namespace SocketServer
{
    public class userList //活动链表
    {
        public class userInformation //活动类
        {
            public string name, password, photo, longitude, latitude, speed,
                          distance, score, futureActivityList, finishActivityList, friendList; //数据域
            public userList.userInformation next; //指针域
            public userInformation(string name, string password, string photo, string longitude, string latitude, string speed,
                          string distance, string score, string futureActivityList, string finishActivityList, string friendList)
            { //构造函数
                this.name = name;
                this.password = password;
                if (photo == "") this.photo = "0"; else this.photo = photo;
                if (longitude == "") this.longitude = "0"; else this.longitude = longitude;
                if (latitude == "") this.latitude = "0"; else this.latitude = latitude;
                if (speed == "") this.speed = "0"; else this.speed = speed;
                if (distance == "") this.distance = "0"; else this.distance = distance;
                if (score == "") this.score = "0"; else this.score = score;
                this.futureActivityList = futureActivityList;
                this.finishActivityList = finishActivityList;
                this.friendList = friendList;
            }
        }
        private int count;
        public int Count //计数器
        {
            get { return this.count; }
        }

        private userInformation head; //头指针

        public void Add(string name, string password, string photo, string longitude, string latitude, string speed,
                          string distance, string score, string futureActivityList, string finishActivityList, string friendList)
        { //向链表中加入用户
            userInformation newNode = new userInformation(name, password, photo, longitude, latitude, speed,
                          distance, score, futureActivityList, finishActivityList, friendList);
            if (head == null)
            {
                head = newNode;
                head.next = null;
            }
            else
            {
                userInformation temp = head;
                while (temp.next != null)
                {
                    temp = temp.next;
                };
                temp.next = newNode;
            }
            count++;
        }
        public string Rigest(string result) //注册
        {
            string[] answer = result.Split(new char[] { '|' });
            string name = answer[1], password = answer[2];

            //检测是否存在该用户名
            userInformation temp = head;
            while (temp != null)
            {
                if (temp.name == name)
                {
                    return "User existes!";
                }
                temp = temp.next;
            }
            Add(name, password, "", "", "", "", "", "", "", "", "");
            updateUserList(name);
            UpdateInfo(name);
            return "Regist successful!";
        }

        public string Login(string result) //登录
        {
            string[] answer = result.Split(new char[] { '|' });
            string name = answer[1], password = answer[2], longitude = answer[3], latitude = answer[4];
            userInformation temp = head;
            while (temp != null)
            {
                if (temp.name == name)
                {
                    if (temp.password == password)
                    {
                        temp.longitude = longitude;
                        temp.latitude = latitude;
                        UpdateInfo(temp.name);
                        Console.WriteLine("登录成功！");
                        return temp.friendList;
                    }
                    else
                    {
                        return "1Password is wrong, please reinput!";
                    }
                }
                temp = temp.next;
            }
            return "1User doesn't exist!";
        }

        public string Addfriend(string result) //加好友
        {
            string[] answer = result.Split(new char[] { '|' });
            string friendName = answer[1], userName = answer[2];
            userInformation userTemp = null, friendTemp = null, temp = head;
            int flag = 0;
            while (temp != null)
            {
                if (temp.name == userName)
                {
                    userTemp = temp;
                    flag = flag + 1;
                }
                if (temp.name == friendName)
                {
                    friendTemp = temp;
                    flag = flag + 2;
                }
                temp = temp.next;
            }
            if (flag == 0)
            {
                return "输入两个账号都错误！";
            }
            else if (flag == 1)
            {
                return "输入好友账号错误！";
            }
            else if (flag == 2)
            {
                return "输入自己账号错误！";
            }
            string[] friend = userTemp.friendList.Split(new char[] { '&', '/' });
            for (int i = 0; i < friend.Length / 2; i++)
            {
                if (friend[i] == friendName)
                    return "This user has been your friend!";
            }
            userTemp.friendList = userTemp.friendList + friendTemp.photo + '&' + friendTemp.name + '&' + friendTemp.score + '&' +
                friendTemp.speed + '&' + friendTemp.distance + '/';
            userTemp.friendList = UpdateFriendList(userTemp.friendList);
            UpdateInfo(userName);
            return "add friend successful";
        }

        public string UpdateFriendList(string friendList) //更新好友链表
        {
            string[] friend = friendList.Split(new char[] { '&', '/' });
            for (int i = 0; i < friend.Length / 5; i++)
                for (int j = i + 1; j < friend.Length / 5; j++)
                {
                    if (Double.Parse(friend[5 * j + 2]) > Double.Parse(friend[5 * i + 2]))
                    {
                        string temp = null;
                        temp = friend[5 * j + 0]; friend[5 * j + 0] = friend[5 * i + 0]; friend[5 * i + 0] = temp;
                        temp = friend[5 * j + 1]; friend[5 * j + 1] = friend[5 * i + 1]; friend[5 * i + 1] = temp;
                        temp = friend[5 * j + 2]; friend[5 * j + 2] = friend[5 * i + 2]; friend[5 * i + 2] = temp;
                        temp = friend[5 * j + 3]; friend[5 * j + 3] = friend[5 * i + 3]; friend[5 * i + 3] = temp;
                        temp = friend[5 * j + 4]; friend[5 * j + 4] = friend[5 * i + 4]; friend[5 * i + 4] = temp;
                    }
                }
            string result = null;
            for (int i = 0; i < friend.Length / 5; i++)
            {
                int j = 0;
                result = result + friend[5 * i + j++] + '&' + friend[5 * i + j++] + '&' + friend[5 * i + j++] + '&' + friend[5 * i + j++] + '&' + friend[5 * i + j++] + '/';
            }
            return result;
        }

        public string findFriend(string result) //好友详情
        {
            string[] fName = result.Split(new char[] { '|' });
            string friendName = fName[1];
            userInformation temp = head;
            while (temp != null)
            {
                if (temp.name == friendName)
                    break;
                temp = temp.next;
            }
            string back = temp.longitude + '|' + temp.latitude + '|' + temp.speed + '|' +
                temp.distance + '|' + temp.futureActivityList + '|' + temp.finishActivityList;
            return back;
        }

        public string searchFriend(string result) //查找好友
        {
            string[] location = result.Split(new char[] { '|' });
            string longitude = location[1], latitude = location[2], range = location[3], searchResult = "";
            userInformation temp = head;
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
                    searchResult = searchResult + temp.photo + '|' + temp.name + '|' + distance + '|' + temp.longitude + '|' +
                        temp.latitude + '|' ;
                }
                temp = temp.next;
            }
            string[] activity = searchResult.Split(new char[] { '|' });
            for (int i = 0; i < activity.Length / 5; i++)
                for (int j = i + 1; j < activity.Length / 5; j++)
                {
                    if (Double.Parse(activity[5 * j + 2]) < Double.Parse(activity[5 * i + 2]))
                    {
                        string strTmp = null;
                        strTmp = activity[5 * j + 0]; activity[5 * j + 0] = activity[5 * i + 0]; activity[5 * i + 0] = strTmp;
                        strTmp = activity[5 * j + 1]; activity[5 * j + 1] = activity[5 * i + 1]; activity[5 * i + 1] = strTmp;
                        strTmp = activity[5 * j + 2]; activity[5 * j + 2] = activity[5 * i + 2]; activity[5 * i + 2] = strTmp;
                        strTmp = activity[5 * j + 3]; activity[5 * j + 3] = activity[5 * i + 3]; activity[5 * i + 3] = strTmp;
                        strTmp = activity[5 * j + 4]; activity[5 * j + 4] = activity[5 * i + 4]; activity[5 * i + 4] = strTmp;
                    }
                }
            result = null;
            for (int i = 0; i < activity.Length / 5; i++)
            {
                result = result + activity[5 * i + 0] + '&' + activity[5 * i + 1] + '&' + activity[5 * i + 2] +
                    '&' + activity[5 * i + 3] + '&' + activity[5 * i + 4] + '/';
            }
            return result;
        }


        public string finishActivity(string result) //活动结束
        {
            string[] information = result.Split(new char[] { '|' });
            string name = information[1], activityName = information[2], speed = information[3], distance = information[4], count = information[5], sponsor = information[6];
            double score = double.Parse(speed) * double.Parse(distance) * double.Parse(count) * double.Parse(sponsor);
            userInformation temp = head;
            while (temp != null)
            {
                if (temp.name == name)
                    break;
                temp = temp.next;
            }
            if (temp.speed == "")
                temp.speed = speed.ToString();
            double dSpeed = (double.Parse(temp.speed) + double.Parse(speed)) / 2;
            temp.speed = "" + dSpeed.ToString();
            temp.distance = "" + (double.Parse(temp.distance) + double.Parse(distance)).ToString();
            temp.score = "" + (double.Parse(temp.score) + score).ToString();
            temp.finishActivityList = activityName + '&' + temp.finishActivityList;
            string[] futureActivity = temp.futureActivityList.Split(new char[] { '&' });
            temp.futureActivityList = "";
            for (int i = 0; i < futureActivity.Length / 2; i++)
            {
                if (futureActivity[i * 2] != activityName)
                    temp.futureActivityList = temp.futureActivityList + futureActivity[i * 2] + '&' + futureActivity[i * 2 + 1] + '&';
            }
            UpdateInfo(name);
            return "Successful!";
        }

        public string findMyActivity(string result) //我的活动
        {
            string[] information = result.Split(new char[] { '|' });
            string name = information[1];
            userInformation temp = head;
            while (temp != null)
            {
                if (temp.name == name)
                    break;
                temp = temp.next;
            }
            temp.futureActivityList = sort(temp.futureActivityList);
            return temp.futureActivityList +'|' +temp.finishActivityList;
        }

        public string sort(string futureActivity) //活动列表排序
        {
            string[] activity = futureActivity.Split(new char[] { '&' });
            DateTime dt = DateTime.Now;
            string ctime = dt.ToString();
            string[] date = ctime.Split(new char[] { '/', ' ', ':' });
            string year = date[0], month = date[1], day = date[2], hour = date[3];
            string Aactivity = "";
            for (int i = 0; i < activity.Length / 2; i++)
            {
                string[] atime = activity[i * 2 + 1].Split('/');
                double ayear = double.Parse(atime[0]), amonth = double.Parse(atime[1]), aday = double.Parse(atime[2]), ahour = double.Parse(atime[3]),
                    nyear = double.Parse(year), nmonth = double.Parse(month), nday = double.Parse(day), nhour = double.Parse(hour);
                if (ayear < nyear || amonth < nmonth || aday < nday)
                    continue;
                else
                {
                    double distance = (((ayear - nyear) * 365 + (amonth - nmonth)) * 30 + (aday - nday)) * 24 + (ahour - nhour);
                    Aactivity = Aactivity + activity[2 * i] + '|' + distance.ToString() + '|';
                }
            }
            string[] AAactivity = Aactivity.Split(new char[] { '|' });
            for (int i = 0; i < AAactivity.Length / 2; i++)
                for (int j = i + 1; j < AAactivity.Length / 2; j++)
                {
                    if (Double.Parse(AAactivity[2 * j + 1]) < Double.Parse(AAactivity[2 * i + 1]))
                    {
                        string temp = null;
                        temp = activity[2 * j + 0]; activity[2 * j + 0] = activity[2 * i + 0]; activity[2 * i + 0] = temp;
                        temp = activity[2 * j + 1]; activity[2 * j + 1] = activity[2 * i + 1]; activity[2 * i + 1] = temp;
                    }
                }
            string result = null;
            for (int i = 0; i < activity.Length - 1; i++)
            {
                if (i % 2 == 0)
                    result = result + activity[i] + '&';
            }
            return result;
        }

        public void PostEvent(string result) //发起活动，更新我的活动列表
        {
            string[] answer = result.Split(new char[] { '|' });
            string activityName = answer[1], userName = answer[3], time = answer[6];
            userInformation temp = head;
            while (temp != null)
            {
                if (temp.name == userName)
                    break;
                temp = temp.next;
            }
            temp.futureActivityList = temp.futureActivityList + activityName + '&' + time + '&';
            UpdateInfo(userName);
        }

        public string JoinActivity(string result) //加入活动，更新我的活动列表
        {
            if (result == "You have joined it!")
            {
                return "You have joined it!";
            }
            else if (result == "Join in successful!")
            {
                return "Join in successful!";
            }
            string[] answer = result.Split(new char[] { '|' });
            string activityName = answer[1], userName = answer[2], time = answer[3];
            DateTime dt = DateTime.Now;
            string ctime = dt.ToString();
            string[] date = ctime.Split(new char[] { '/', ' ', ':' });
            string year = date[0], month = date[1], day = date[2], hour = date[3];
            string[] atime = time.Split(new char[] { '/' });
            double ayear = double.Parse(atime[0]), amonth = double.Parse(atime[1]), aday = double.Parse(atime[2]), ahour = double.Parse(atime[3]),
                   nyear = double.Parse(year), nmonth = double.Parse(month), nday = double.Parse(day), nhour = double.Parse(hour);
            if (ayear < nyear || amonth < nmonth || aday < nday)
                return "this activity have finished";
            userInformation temp = head;
            while (temp != null)
            {
                if (temp.name == userName)
                    break;
                temp = temp.next;
            }
            temp.futureActivityList = temp.futureActivityList + activityName + '&' + time + '&';
            UpdateInfo(userName);
            return "join successful!";
        }



        public void UpdateInfo(string userName) //更新用户信息
        {
            userInformation temp = head;
            while (temp != null)
            {
                if (temp.name == userName)
                    break;
                temp = temp.next;
            }
            string result = temp.name + '|' + temp.password + '|' + temp.photo + '|' + temp.longitude + '|' + temp.latitude
                 + '|' + temp.speed + '|' + temp.distance + '|' + temp.score + '|' + temp.futureActivityList + '|' + temp.finishActivityList + '|' + temp.friendList;
            string strname = "data\\" + userName + ".txt";
            FileStream myFs1 = new FileStream(strname, FileMode.Create);
            StreamWriter mySw1 = new StreamWriter(myFs1);
            mySw1.Write(result);
            mySw1.Close();
            myFs1.Close();
        }



        public void updateUserList(string name) //更新用户链表
        {
            string strList = "data\\namelist.txt";
            if (File.Exists(strList))
            {
                FileStream myFs = new FileStream(strList, FileMode.Append);
                StreamWriter mySw = new StreamWriter(myFs);
                mySw.Write(name + "|");
                mySw.Close();
                myFs.Close();
            }
            else
            {
                FileStream myFs = new FileStream(strList, FileMode.Create);
                StreamWriter mySw = new StreamWriter(myFs);
                mySw.Write(name + "|");
                mySw.Close();
                myFs.Close();
            }
        }
    }
}
