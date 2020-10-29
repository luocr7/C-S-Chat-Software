namespace CSChat
{
    partial class FriendList
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FriendList));
            this.closeBtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.headPic = new System.Windows.Forms.PictureBox();
            this.myfriendBtn = new System.Windows.Forms.Button();
            this.myFriendPanel = new System.Windows.Forms.Panel();
            this.groupChatBtn = new System.Windows.Forms.Label();
            this.extendIcon = new System.Windows.Forms.PictureBox();
            this.userName = new System.Windows.Forms.Label();
            this.msgBtn = new System.Windows.Forms.Button();
            this.relationBtn = new System.Windows.Forms.Button();
            this.addFriendBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.headPic)).BeginInit();
            this.myFriendPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.extendIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // closeBtn
            // 
            this.closeBtn.BackColor = System.Drawing.Color.Transparent;
            this.closeBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("closeBtn.BackgroundImage")));
            this.closeBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.closeBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.closeBtn.FlatAppearance.BorderSize = 0;
            this.closeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeBtn.Location = new System.Drawing.Point(306, 12);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(40, 31);
            this.closeBtn.TabIndex = 0;
            this.closeBtn.UseVisualStyleBackColor = false;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            this.closeBtn.MouseEnter += new System.EventHandler(this.closeBtn_MouseEnter);
            this.closeBtn.MouseLeave += new System.EventHandler(this.closeBtn_MouseLeave);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(263, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(37, 31);
            this.button1.TabIndex = 1;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // headPic
            // 
            this.headPic.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("headPic.BackgroundImage")));
            this.headPic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.headPic.Location = new System.Drawing.Point(27, 23);
            this.headPic.Name = "headPic";
            this.headPic.Size = new System.Drawing.Size(73, 61);
            this.headPic.TabIndex = 4;
            this.headPic.TabStop = false;
            // 
            // myfriendBtn
            // 
            this.myfriendBtn.FlatAppearance.BorderSize = 0;
            this.myfriendBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.myfriendBtn.Font = new System.Drawing.Font("Thonburi", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.myfriendBtn.Location = new System.Drawing.Point(38, 0);
            this.myfriendBtn.Name = "myfriendBtn";
            this.myfriendBtn.Size = new System.Drawing.Size(324, 36);
            this.myfriendBtn.TabIndex = 5;
            this.myfriendBtn.Text = "我的好友";
            this.myfriendBtn.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.myfriendBtn.UseVisualStyleBackColor = true;
            this.myfriendBtn.Click += new System.EventHandler(this.myfriendBtn_Click);
            // 
            // myFriendPanel
            // 
            this.myFriendPanel.AutoScroll = true;
            this.myFriendPanel.AutoSize = true;
            this.myFriendPanel.Controls.Add(this.groupChatBtn);
            this.myFriendPanel.Controls.Add(this.extendIcon);
            this.myFriendPanel.Controls.Add(this.myfriendBtn);
            this.myFriendPanel.Location = new System.Drawing.Point(0, 168);
            this.myFriendPanel.Name = "myFriendPanel";
            this.myFriendPanel.Size = new System.Drawing.Size(365, 512);
            this.myFriendPanel.TabIndex = 8;
            // 
            // groupChatBtn
            // 
            this.groupChatBtn.AutoSize = true;
            this.groupChatBtn.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupChatBtn.Location = new System.Drawing.Point(3, 333);
            this.groupChatBtn.Name = "groupChatBtn";
            this.groupChatBtn.Size = new System.Drawing.Size(49, 20);
            this.groupChatBtn.TabIndex = 9;
            this.groupChatBtn.Text = "群聊";
            // 
            // extendIcon
            // 
            this.extendIcon.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("extendIcon.BackgroundImage")));
            this.extendIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.extendIcon.Cursor = System.Windows.Forms.Cursors.Default;
            this.extendIcon.ErrorImage = null;
            this.extendIcon.Location = new System.Drawing.Point(4, 0);
            this.extendIcon.Name = "extendIcon";
            this.extendIcon.Size = new System.Drawing.Size(41, 35);
            this.extendIcon.TabIndex = 8;
            this.extendIcon.TabStop = false;
            // 
            // userName
            // 
            this.userName.AutoSize = true;
            this.userName.BackColor = System.Drawing.Color.Transparent;
            this.userName.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.userName.ForeColor = System.Drawing.Color.White;
            this.userName.Location = new System.Drawing.Point(119, 23);
            this.userName.Name = "userName";
            this.userName.Size = new System.Drawing.Size(0, 27);
            this.userName.TabIndex = 9;
            // 
            // msgBtn
            // 
            this.msgBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.msgBtn.FlatAppearance.BorderSize = 0;
            this.msgBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.msgBtn.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.msgBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.msgBtn.Location = new System.Drawing.Point(0, 125);
            this.msgBtn.Name = "msgBtn";
            this.msgBtn.Size = new System.Drawing.Size(171, 37);
            this.msgBtn.TabIndex = 10;
            this.msgBtn.Text = "消 息";
            this.msgBtn.UseVisualStyleBackColor = true;
            this.msgBtn.Click += new System.EventHandler(this.msgBtn_Click);
            // 
            // relationBtn
            // 
            this.relationBtn.FlatAppearance.BorderSize = 0;
            this.relationBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.relationBtn.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.relationBtn.Location = new System.Drawing.Point(178, 125);
            this.relationBtn.Name = "relationBtn";
            this.relationBtn.Size = new System.Drawing.Size(184, 37);
            this.relationBtn.TabIndex = 11;
            this.relationBtn.Text = "联系人";
            this.relationBtn.UseVisualStyleBackColor = true;
            this.relationBtn.Click += new System.EventHandler(this.relationBtn_Click);
            // 
            // addFriendBtn
            // 
            this.addFriendBtn.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.addFriendBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addFriendBtn.Font = new System.Drawing.Font("微软雅黑", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.addFriendBtn.ForeColor = System.Drawing.Color.Transparent;
            this.addFriendBtn.Location = new System.Drawing.Point(0, 677);
            this.addFriendBtn.Name = "addFriendBtn";
            this.addFriendBtn.Size = new System.Drawing.Size(75, 40);
            this.addFriendBtn.TabIndex = 12;
            this.addFriendBtn.Text = "添加好友";
            this.addFriendBtn.UseVisualStyleBackColor = false;
            this.addFriendBtn.Click += new System.EventHandler(this.addFriendBtn_Click);
            // 
            // FriendList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(362, 720);
            this.Controls.Add(this.addFriendBtn);
            this.Controls.Add(this.relationBtn);
            this.Controls.Add(this.msgBtn);
            this.Controls.Add(this.userName);
            this.Controls.Add(this.myFriendPanel);
            this.Controls.Add(this.headPic);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.closeBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FriendList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FriendList";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FriendList_FormClosing);
            this.Load += new System.EventHandler(this.FriendList_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FriendList_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FriendList_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FriendList_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.headPic)).EndInit();
            this.myFriendPanel.ResumeLayout(false);
            this.myFriendPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.extendIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox headPic;
        private System.Windows.Forms.Button myfriendBtn;
        private System.Windows.Forms.Panel myFriendPanel;
        private System.Windows.Forms.PictureBox extendIcon;
        private System.Windows.Forms.Label userName;
        private System.Windows.Forms.Button msgBtn;
        private System.Windows.Forms.Button relationBtn;
        private System.Windows.Forms.Button addFriendBtn;
        private System.Windows.Forms.Label groupChatBtn;
    }
}