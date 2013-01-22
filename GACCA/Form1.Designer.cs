namespace GACCA
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.Refresh = new System.Windows.Forms.Button();
            this.Disconnect = new System.Windows.Forms.Button();
            this.Connect = new System.Windows.Forms.Button();
            this.Repair = new System.Windows.Forms.Button();
            this.Quit = new System.Windows.Forms.Button();
            this.MainText = new System.Windows.Forms.RichTextBox();
            this.MainNotify = new System.Windows.Forms.NotifyIcon(this.components);
            this.MainMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuConnect = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuDisconnect = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // Refresh
            // 
            this.Refresh.Location = new System.Drawing.Point(297, 12);
            this.Refresh.Name = "Refresh";
            this.Refresh.Size = new System.Drawing.Size(75, 23);
            this.Refresh.TabIndex = 0;
            this.Refresh.Text = "Refresh";
            this.Refresh.UseVisualStyleBackColor = true;
            this.Refresh.Click += new System.EventHandler(this.Refresh_Click);
            // 
            // Disconnect
            // 
            this.Disconnect.Location = new System.Drawing.Point(297, 42);
            this.Disconnect.Name = "Disconnect";
            this.Disconnect.Size = new System.Drawing.Size(75, 23);
            this.Disconnect.TabIndex = 1;
            this.Disconnect.Text = "Disconnect";
            this.Disconnect.UseVisualStyleBackColor = true;
            this.Disconnect.Click += new System.EventHandler(this.Disconnect_Click);
            // 
            // Connect
            // 
            this.Connect.Location = new System.Drawing.Point(297, 72);
            this.Connect.Name = "Connect";
            this.Connect.Size = new System.Drawing.Size(75, 23);
            this.Connect.TabIndex = 2;
            this.Connect.Text = "Connect";
            this.Connect.UseVisualStyleBackColor = true;
            this.Connect.Click += new System.EventHandler(this.Connect_Click);
            // 
            // Repair
            // 
            this.Repair.Location = new System.Drawing.Point(297, 102);
            this.Repair.Name = "Repair";
            this.Repair.Size = new System.Drawing.Size(75, 23);
            this.Repair.TabIndex = 3;
            this.Repair.Text = "Repair";
            this.Repair.UseVisualStyleBackColor = true;
            this.Repair.Click += new System.EventHandler(this.Repair_Click);
            // 
            // Quit
            // 
            this.Quit.Location = new System.Drawing.Point(297, 226);
            this.Quit.Name = "Quit";
            this.Quit.Size = new System.Drawing.Size(75, 23);
            this.Quit.TabIndex = 4;
            this.Quit.Text = "Quit";
            this.Quit.UseVisualStyleBackColor = true;
            this.Quit.Click += new System.EventHandler(this.Quit_Click);
            // 
            // MainText
            // 
            this.MainText.Location = new System.Drawing.Point(13, 12);
            this.MainText.Name = "MainText";
            this.MainText.Size = new System.Drawing.Size(278, 237);
            this.MainText.TabIndex = 5;
            this.MainText.Text = "Made by Catofes\nCompany : PKU\nVersion: 1.0.0.0";
            // 
            // MainNotify
            // 
            this.MainNotify.ContextMenuStrip = this.MainMenu;
            this.MainNotify.Icon = ((System.Drawing.Icon)(resources.GetObject("MainNotify.Icon")));
            this.MainNotify.Text = "GACCA";
            this.MainNotify.Visible = true;
            this.MainNotify.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuConnect,
            this.MenuDisconnect,
            this.MenuExit});
            this.MainMenu.Name = "contextMenuStrip1";
            this.MainMenu.Size = new System.Drawing.Size(138, 70);
            // 
            // MenuConnect
            // 
            this.MenuConnect.Name = "MenuConnect";
            this.MenuConnect.Size = new System.Drawing.Size(137, 22);
            this.MenuConnect.Text = "Connect";
            this.MenuConnect.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // MenuDisconnect
            // 
            this.MenuDisconnect.Name = "MenuDisconnect";
            this.MenuDisconnect.Size = new System.Drawing.Size(137, 22);
            this.MenuDisconnect.Text = "Disconnect";
            this.MenuDisconnect.Click += new System.EventHandler(this.MenuDisconnect_Click);
            // 
            // MenuExit
            // 
            this.MenuExit.Name = "MenuExit";
            this.MenuExit.Size = new System.Drawing.Size(137, 22);
            this.MenuExit.Text = "Exit";
            this.MenuExit.Click += new System.EventHandler(this.MenuExit_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 261);
            this.Controls.Add(this.MainText);
            this.Controls.Add(this.Quit);
            this.Controls.Add(this.Repair);
            this.Controls.Add(this.Connect);
            this.Controls.Add(this.Disconnect);
            this.Controls.Add(this.Refresh);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(400, 300);
            this.MinimumSize = new System.Drawing.Size(400, 300);
            this.Name = "Form1";
            this.Text = "GACCA";
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.MainMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Refresh;
        private System.Windows.Forms.Button Disconnect;
        private System.Windows.Forms.Button Connect;
        private System.Windows.Forms.Button Repair;
        private System.Windows.Forms.Button Quit;
        private System.Windows.Forms.RichTextBox MainText;
        private System.Windows.Forms.NotifyIcon MainNotify;
        private System.Windows.Forms.ContextMenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem MenuConnect;
        private System.Windows.Forms.ToolStripMenuItem MenuDisconnect;
        private System.Windows.Forms.ToolStripMenuItem MenuExit;
    }
}

