namespace LoveSave
{
    partial class LoveSaveForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoveSaveForm));
            this.ofdHtml = new System.Windows.Forms.OpenFileDialog();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnDiary = new System.Windows.Forms.Button();
            this.lblResult = new System.Windows.Forms.Label();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.subtabItem = new System.Windows.Forms.TabPage();
            this.subtabChat = new System.Windows.Forms.TabPage();
            this.btnChat = new System.Windows.Forms.Button();
            this.subtabComemorate = new System.Windows.Forms.TabPage();
            this.btnMemos = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabMain.SuspendLayout();
            this.subtabItem.SuspendLayout();
            this.subtabChat.SuspendLayout();
            this.subtabComemorate.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ofdHtml
            // 
            this.ofdHtml.DefaultExt = "htm";
            this.ofdHtml.Title = "请选择要解析的文件";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::LoveSave.Properties.Resources._7995e601;
            this.pictureBox1.Location = new System.Drawing.Point(12, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(70, 70);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnDiary
            // 
            this.btnDiary.Location = new System.Drawing.Point(48, 10);
            this.btnDiary.Name = "btnDiary";
            this.btnDiary.Size = new System.Drawing.Size(114, 23);
            this.btnDiary.TabIndex = 2;
            this.btnDiary.Text = "选择要解析的文件";
            this.btnDiary.UseVisualStyleBackColor = true;
            this.btnDiary.Click += new System.EventHandler(this.btnDiary_Click);
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblResult.Location = new System.Drawing.Point(3, 9);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(16, 15);
            this.lblResult.TabIndex = 3;
            this.lblResult.Text = "...";
            // 
            // tabMain
            // 
            this.tabMain.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabMain.Controls.Add(this.subtabItem);
            this.tabMain.Controls.Add(this.subtabChat);
            this.tabMain.Controls.Add(this.subtabComemorate);
            this.tabMain.Location = new System.Drawing.Point(87, 10);
            this.tabMain.Margin = new System.Windows.Forms.Padding(2);
            this.tabMain.Multiline = true;
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(224, 68);
            this.tabMain.TabIndex = 4;
            // 
            // subtabItem
            // 
            this.subtabItem.Controls.Add(this.btnDiary);
            this.subtabItem.Location = new System.Drawing.Point(4, 25);
            this.subtabItem.Margin = new System.Windows.Forms.Padding(2);
            this.subtabItem.Name = "subtabItem";
            this.subtabItem.Padding = new System.Windows.Forms.Padding(2);
            this.subtabItem.Size = new System.Drawing.Size(216, 39);
            this.subtabItem.TabIndex = 0;
            this.subtabItem.Text = "解析日志";
            this.subtabItem.UseVisualStyleBackColor = true;
            // 
            // subtabChat
            // 
            this.subtabChat.Controls.Add(this.btnChat);
            this.subtabChat.Location = new System.Drawing.Point(4, 25);
            this.subtabChat.Margin = new System.Windows.Forms.Padding(2);
            this.subtabChat.Name = "subtabChat";
            this.subtabChat.Padding = new System.Windows.Forms.Padding(2);
            this.subtabChat.Size = new System.Drawing.Size(216, 39);
            this.subtabChat.TabIndex = 1;
            this.subtabChat.Text = "解析密聊";
            this.subtabChat.UseVisualStyleBackColor = true;
            // 
            // btnChat
            // 
            this.btnChat.Location = new System.Drawing.Point(52, 10);
            this.btnChat.Name = "btnChat";
            this.btnChat.Size = new System.Drawing.Size(114, 23);
            this.btnChat.TabIndex = 3;
            this.btnChat.Text = "选择要解析的文件";
            this.btnChat.UseVisualStyleBackColor = true;
            this.btnChat.Click += new System.EventHandler(this.btnChat_Click);
            // 
            // subtabComemorate
            // 
            this.subtabComemorate.Controls.Add(this.btnMemos);
            this.subtabComemorate.Location = new System.Drawing.Point(4, 25);
            this.subtabComemorate.Margin = new System.Windows.Forms.Padding(2);
            this.subtabComemorate.Name = "subtabComemorate";
            this.subtabComemorate.Padding = new System.Windows.Forms.Padding(2);
            this.subtabComemorate.Size = new System.Drawing.Size(216, 39);
            this.subtabComemorate.TabIndex = 2;
            this.subtabComemorate.Text = "解析纪念";
            this.subtabComemorate.UseVisualStyleBackColor = true;
            // 
            // btnMemos
            // 
            this.btnMemos.Location = new System.Drawing.Point(52, 10);
            this.btnMemos.Name = "btnMemos";
            this.btnMemos.Size = new System.Drawing.Size(114, 23);
            this.btnMemos.TabIndex = 4;
            this.btnMemos.Text = "选择要解析的文件";
            this.btnMemos.UseVisualStyleBackColor = true;
            this.btnMemos.Click += new System.EventHandler(this.btnMemos_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.lblResult);
            this.panel1.Location = new System.Drawing.Point(12, 83);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(300, 45);
            this.panel1.TabIndex = 5;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(335, 7);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.Size = new System.Drawing.Size(332, 283);
            this.webBrowser1.TabIndex = 6;
            this.webBrowser1.Url = new System.Uri("http://sweet.snsapp.qq.com", System.UriKind.Absolute);
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(43, 150);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(196, 96);
            this.button1.TabIndex = 7;
            this.button1.Text = "关键参数已捕获,点我开始解析";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // LoveSaveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 320);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoveSaveForm";
            this.Text = "LoveSave";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LoveSaveForm_FormClosing);
            this.Load += new System.EventHandler(this.LoveSaveForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabMain.ResumeLayout(false);
            this.subtabItem.ResumeLayout(false);
            this.subtabChat.ResumeLayout(false);
            this.subtabComemorate.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog ofdHtml;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnDiary;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage subtabItem;
        private System.Windows.Forms.TabPage subtabChat;
        private System.Windows.Forms.Button btnChat;
        private System.Windows.Forms.TabPage subtabComemorate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnMemos;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Button button1;
    }
}