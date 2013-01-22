using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace GACCA
{
    class MainCore //程序核心类
    {
        public int RuningWaitTime = 50;//运行时检测路由表等待时间
        public int DisRuningWaitTime = 1000;//不运行时检测是否连接等待时间
        static string XunLeiName = "XLacc";//迅雷加速器进程的名字。
        static int Ip_after_Keyword = 98;//ipconfig 返回信息中ip在"GameAcc VPN"关键词后的位数
        static string KeyWord = "GameAcc VPN";//迅雷网游加速器的关键词
        static string ConnectIp = "116.255.216.22";//需要连接到的ip地址


        public string Ip;//检测到的ip
        public int IntNumber;//检测到的int号码
        public Thread MainThread;//主要线程，用于保持route路由表的更新。
        public bool IfThreadRun;//线程状态
        public string CmdOutput;//Cmd返回信息

        public MainCore()//构造函数
        {
            IfThreadRun = false;//初始化未连接
            CmdOutput = "";
            MainThread = new Thread(ThreadLoop); 
            MainThread.IsBackground = false;
            MainThread.Start();
        }

        bool CheckIf_XunLei_Open()//检测迅雷网游加速器是否运行
        {
            Process[] pp = Process.GetProcessesByName(XunLeiName);//检测迅雷加速器进程是否打开。
            if (pp.Length > 0) return true;
            MessageBox.Show("请打开迅雷网游加速器并保持打开", "Error");
            IfThreadRun=false;
            return false;
        }

        public bool GetIp()//获取迅雷网游加速器给出的ip
        {
            if (!CheckIf_XunLei_Open()) return false;//迅雷未开
            ///<Part>
            ///从cmd获取需要信息
            Process cmd = new Process();
            cmd.StartInfo.FileName = "ipconfig.exe";
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.StartInfo.CreateNoWindow = true;//后台方式打开cmd
            cmd.Start();
            string info = cmd.StandardOutput.ReadToEnd();//得到ipconfig.exe给出的信息。
            cmd.WaitForExit();
            cmd.Close();
            if (info.Length == 0)//ipconfig未返回信息
            {
                MessageBox.Show("获取Ip信息失败", "Error");
                IfThreadRun=false;
                return false;
            }
            ///</Part>
            int IpLocation = SearchKeyWord(info);//搜索关键词GameAcc VPN，并得到关键词所在位置
            if (IpLocation == -1) return false;
            ///<Part>
            ///获取IP
            Ip = "";
            for (int i = IpLocation + Ip_after_Keyword; info[i] != '\r'; i++)
            {
                Ip = Ip + info[i];
            }
            return true;//成功
            ///</Part>
        }

        int SearchKeyWord(string info)//寻找关键词的位置
        {
            string strSearch = KeyWord;
            char c = strSearch[0];
            for (int i = 0; i < info.Length; i++)
            {
                if (info[i].Equals(c) == false) continue;
                int Length = 1;
                for (int j = 1; j < strSearch.Length; j++)
                {
                    if ((i + j) >= info.Length) break;
                    if (info[i + j].Equals(strSearch[j]) == false) break;
                    Length++;
                }
                if (Length == strSearch.Length)
                {
                    return i;
                }
            }
            IfThreadRun=false;
            MessageBox.Show("查找关键词失败，请检查是否以模式二加速", "Error");
            return -1;
        }

        public bool GetIntNumber()//获取int number
        {
            if (!CheckIf_XunLei_Open()) return false;//迅雷未开
            ///<Part>
            ///从cmd获取需要信息
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardError = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.Start();
            cmd.StandardInput.AutoFlush = true;
            cmd.StandardInput.WriteLine("netsh int ip show int");
            cmd.StandardInput.WriteLine("exit");
            string info = cmd.StandardOutput.ReadToEnd();
            cmd.WaitForExit();
            cmd.Close();
            ///</Part>
            int IntLocation = SearchKeyWord(info);//获取关键词位置
            if (IntLocation == -1) return false;
            string linshi = "";
            int k = 0;
            for (k = IntLocation; info[k] != '\n'; k--) ;
            for (int j = 0; j < 10; j++)
            {
                if (info[j + k + 1].Equals(" ") && j > 1) break;
                linshi = linshi + info[k + j + 1];
            }
            int.TryParse(linshi, out IntNumber);
            return true;
        }

        public bool Connect()
        {
            string commend = "C:\\WINDOWS\\system32\\route.exe ADD " + ConnectIp + " MASK 255.255.255.255 " + Ip;//添加路由表命令
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardError = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.Start();
            cmd.StandardInput.AutoFlush = true;
            cmd.StandardInput.WriteLine(commend);
            cmd.StandardInput.WriteLine("exit");
            string info = cmd.StandardOutput.ReadToEnd();
            cmd.WaitForExit();
            cmd.Close();
            CmdOutput = info;//给出cmd的输出.
            return true;
        }//连接

        public bool Disconnect()
        {
            string commend = "C:\\WINDOWS\\system32\\route.exe DELETE " + ConnectIp + " MASK 255.255.255.255";//删除路由表命令
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardError = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.Start();
            cmd.StandardInput.AutoFlush = true;
            cmd.StandardInput.WriteLine(commend);
            cmd.StandardInput.WriteLine("exit");
            string info = cmd.StandardOutput.ReadToEnd();
            cmd.WaitForExit();
            cmd.Close();
            CmdOutput = info;//给出cmd的输出.
            return true;
        }//停止连接

        bool Check_if_Connected()
        {
            ///<Part>
            ///获取路由表信息
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardError = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.Start();
            cmd.StandardInput.AutoFlush = true;
            cmd.StandardInput.WriteLine("netsh int ip show route");
            cmd.StandardInput.WriteLine("exit");
            string info = cmd.StandardOutput.ReadToEnd();
            cmd.WaitForExit();
            cmd.Close();
            if (info.Length == 0)//未成功获取路由表信息
            {
                MessageBox.Show("获取路由表信息失败","Error");
                IfThreadRun=false;
                return false;
            }
            ///</Part>
            ///<Part>
            ///检测是否有对应的路由表信息
            string strSearch = ConnectIp+"/32";
            char c = strSearch[0];            
            for (int i = 0; i < info.Length; i++)
            {
                if (info[i].Equals(c) == false) continue;
                int Length = 1;
                for (int j = 1; j < strSearch.Length; j++)
                {
                    if ((i + j) >= info.Length) break;
                    if (info[i + j].Equals(strSearch[j]) == false) break;
                    Length++;
                }
                if (Length == strSearch.Length)
                {
                    return true;//找到路由表信息
                }
            }
            return false;//没有找到路由表信息
            ///</Part>
        }//检测连接状况

        void ThreadLoop()//线程内容
        {
            while (true)
            {
                if (IfThreadRun)
                {
                    Thread.Sleep(RuningWaitTime);
                    if (!Check_if_Connected())//检测是否需要重新连接
                    {
                        if(GetIntNumber())//得到Int数码
                        if(GetIp())//得到ip
                        Connect();//连接
                    }
                }
                else
                {
                    Thread.Sleep(DisRuningWaitTime);
                }
            }
        }

    }

    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
