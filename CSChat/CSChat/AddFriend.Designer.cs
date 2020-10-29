namespace CSChat
{
    partial class AddFriend
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
            this.label1 = new System.Windows.Forms.Label();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.queryBtn = new System.Windows.Forms.Button();
            this.queryResultBox = new System.Windows.Forms.TextBox();
            this.addBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(165, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "网名:";
            // 
            // nameBox
            // 
            this.nameBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nameBox.Location = new System.Drawing.Point(229, 82);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(142, 27);
            this.nameBox.TabIndex = 1;
            // 
            // queryBtn
            // 
            this.queryBtn.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.queryBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.queryBtn.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.queryBtn.ForeColor = System.Drawing.Color.Transparent;
            this.queryBtn.Location = new System.Drawing.Point(415, 77);
            this.queryBtn.Name = "queryBtn";
            this.queryBtn.Size = new System.Drawing.Size(75, 34);
            this.queryBtn.TabIndex = 2;
            this.queryBtn.Text = "搜 索";
            this.queryBtn.UseVisualStyleBackColor = false;
            this.queryBtn.Click += new System.EventHandler(this.queryBtn_Click);
            // 
            // queryResultBox
            // 
            this.queryResultBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.queryResultBox.Location = new System.Drawing.Point(170, 183);
            this.queryResultBox.Name = "queryResultBox";
            this.queryResultBox.ReadOnly = true;
            this.queryResultBox.Size = new System.Drawing.Size(205, 27);
            this.queryResultBox.TabIndex = 3;
            this.queryResultBox.Visible = false;
            // 
            // addBtn
            // 
            this.addBtn.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.addBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addBtn.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.addBtn.ForeColor = System.Drawing.Color.Transparent;
            this.addBtn.Location = new System.Drawing.Point(415, 175);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(75, 35);
            this.addBtn.TabIndex = 4;
            this.addBtn.Text = "添加";
            this.addBtn.UseVisualStyleBackColor = false;
            this.addBtn.Visible = false;
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // AddFriend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 378);
            this.Controls.Add(this.addBtn);
            this.Controls.Add(this.queryResultBox);
            this.Controls.Add(this.queryBtn);
            this.Controls.Add(this.nameBox);
            this.Controls.Add(this.label1);
            this.Name = "AddFriend";
            this.Text = "AddFriend";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AddFriend_FormClosing);
            this.Load += new System.EventHandler(this.AddFriend_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.Button queryBtn;
        private System.Windows.Forms.TextBox queryResultBox;
        private System.Windows.Forms.Button addBtn;
    }
}