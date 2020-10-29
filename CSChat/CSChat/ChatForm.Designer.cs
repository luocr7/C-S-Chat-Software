namespace CSChat
{
    partial class ChatForm
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
            this.chat_text = new System.Windows.Forms.RichTextBox();
            this.input_chatText = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // chat_text
            // 
            this.chat_text.BackColor = System.Drawing.SystemColors.Control;
            this.chat_text.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chat_text.Location = new System.Drawing.Point(0, 0);
            this.chat_text.Name = "chat_text";
            this.chat_text.ReadOnly = true;
            this.chat_text.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.chat_text.Size = new System.Drawing.Size(664, 366);
            this.chat_text.TabIndex = 2;
            this.chat_text.Text = "";
            // 
            // input_chatText
            // 
            this.input_chatText.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.input_chatText.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.input_chatText.Location = new System.Drawing.Point(0, 363);
            this.input_chatText.Name = "input_chatText";
            this.input_chatText.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.input_chatText.Size = new System.Drawing.Size(664, 176);
            this.input_chatText.TabIndex = 3;
            this.input_chatText.Text = "";
            this.input_chatText.WordWrap = false;
            this.input_chatText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.input_chatText_KeyDown);
            // 
            // ChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(666, 540);
            this.Controls.Add(this.input_chatText);
            this.Controls.Add(this.chat_text);
            this.Name = "ChatForm";
            this.Text = "ChatForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChatForm_FormClosing);
            this.Load += new System.EventHandler(this.ChatForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RichTextBox chat_text;
        private System.Windows.Forms.RichTextBox input_chatText;
    }
}