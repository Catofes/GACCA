using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace GACCA
{
    public partial class Form1 : Form
    {

        MainCore main = new MainCore();//新建程序核心对象

        public Form1()
        {
            InitializeComponent();
            this.MainNotify.Visible = false;
        }

        private void Refresh_Click(object sender, EventArgs e)//显示ip地址和int地址信息
        {
            if(main.GetIntNumber())//更新int号码
                if (main.GetIp())//更新ip地址
                {
                    MainText.Clear();
                    string txt = "你的ip是： ";
                    txt += main.Ip;
                    txt += '\n';
                    txt += "你的虚拟网络号是： ";
                    txt += main.IntNumber.ToString();
                    MainText.AppendText(txt);//显示
                }
        }

        private void Disconnect_Click(object sender, EventArgs e)//断开连接
        {
            main.IfThreadRun = false;//线程进入不运行状态
            Thread.Sleep(2 * main.RuningWaitTime);//等待线程切换完毕
            MainText.Clear();
            main.Disconnect();//清除路由表
            MainText.AppendText(main.CmdOutput);//输出
        }

        private void Connect_Click(object sender, EventArgs e)//连接
        {
            if(main.GetIntNumber())
                if (main.GetIp())
                {
                    main.Connect();
                    MainText.Clear();
                    MainText.AppendText(main.CmdOutput);//输出
                    main.IfThreadRun = true;//线程进入运行状态
                }
        }

        private void Repair_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("修复路由表将清空整个路由表信息，确定要这样做吗?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                ///<Part>
                ///netsh reset 网络设置
                Process cmd = new Process();
                cmd.StartInfo.FileName = "cmd.exe";
                cmd.StartInfo.RedirectStandardOutput = true;
                cmd.StartInfo.RedirectStandardInput = true;
                cmd.StartInfo.RedirectStandardError = true;
                cmd.StartInfo.UseShellExecute = false;
                cmd.StartInfo.CreateNoWindow = true;
                cmd.Start();
                cmd.StandardInput.AutoFlush = true;
                cmd.StandardInput.WriteLine("netsh int ip reset");
                cmd.StandardInput.WriteLine("exit");
                string info = cmd.StandardOutput.ReadToEnd();
                cmd.WaitForExit();
                cmd.Close();
                MainText.AppendText(info);
                MessageBox.Show("重置完成，请重启电脑以完成设置。", "Message");
            }
        }//修复

        protected override void OnClosing(CancelEventArgs e)
        {
            main.Disconnect();
            main.MainThread.Abort();
            base.OnClosing(e);
        }//关闭时断开连接

        private void Quit_Click(object sender, EventArgs e)
        {
            this.Close();
        }//退出按钮

        private void Form1_SizeChanged(object sender, EventArgs e)//最小化动画。
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Visible = true;
                MainNotify.Visible = true;
                Thread.Sleep(150);
                Hide();
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)//双击托盘还原
        {   
            this.Visible=true;
            WindowState = FormWindowState.Normal;
            MainNotify.Visible = false;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)//菜单中的连接
        {
            this.Visible = true;
            WindowState = FormWindowState.Normal;
            MainNotify.Visible = false;
            this.Connect_Click(sender, e);
        }

        private void MenuDisconnect_Click(object sender, EventArgs e)//菜单中的断开连接
        {
            this.Visible = true;
            WindowState = FormWindowState.Normal;
            MainNotify.Visible = false;
            this.Disconnect_Click(sender, e);
        }

        private void MenuExit_Click(object sender, EventArgs e)//菜单中的退出
        {
            this.Quit_Click(sender, e);
        }

    }
}
