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
            this.browserMain = new System.Windows.Forms.WebBrowser();
            this.btnStart = new System.Windows.Forms.Button();
            this.listResult = new System.Windows.Forms.ListBox();
            this.btnResult = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
            this.pictureBox1.Size = new System.Drawing.Size(55, 55);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // browserMain
            // 
            this.browserMain.Location = new System.Drawing.Point(335, 7);
            this.browserMain.MinimumSize = new System.Drawing.Size(20, 20);
            this.browserMain.Name = "browserMain";
            this.browserMain.ScriptErrorsSuppressed = true;
            this.browserMain.Size = new System.Drawing.Size(332, 283);
            this.browserMain.TabIndex = 6;
            this.browserMain.Url = new System.Uri("http://sweet.snsapp.qq.com", System.UriKind.Absolute);
            this.browserMain.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // btnStart
            // 
            this.btnStart.Enabled = false;
            this.btnStart.Location = new System.Drawing.Point(88, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(229, 47);
            this.btnStart.TabIndex = 7;
            this.btnStart.Text = "请在右侧浏览器中登陆您的情侣空间 →";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // listResult
            // 
            this.listResult.BackColor = System.Drawing.Color.Black;
            this.listResult.ForeColor = System.Drawing.Color.Green;
            this.listResult.FormattingEnabled = true;
            this.listResult.HorizontalScrollbar = true;
            this.listResult.ItemHeight = 12;
            this.listResult.Items.AddRange(new object[] {
            "监听已启动!请在右侧浏览器中登陆您的情侣空间."});
            this.listResult.Location = new System.Drawing.Point(12, 64);
            this.listResult.Name = "listResult";
            this.listResult.Size = new System.Drawing.Size(305, 244);
            this.listResult.TabIndex = 8;
            // 
            // btnResult
            // 
            this.btnResult.Enabled = false;
            this.btnResult.Location = new System.Drawing.Point(88, 12);
            this.btnResult.Name = "btnResult";
            this.btnResult.Size = new System.Drawing.Size(229, 47);
            this.btnResult.TabIndex = 9;
            this.btnResult.Text = "打开导出文件夹";
            this.btnResult.UseVisualStyleBackColor = true;
            this.btnResult.Visible = false;
            this.btnResult.Click += new System.EventHandler(this.btnResult_Click);
            // 
            // LoveSaveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 320);
            this.Controls.Add(this.btnResult);
            this.Controls.Add(this.listResult);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.browserMain);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "LoveSaveForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LoveSave";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LoveSaveForm_FormClosing);
            this.Load += new System.EventHandler(this.LoveSaveForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog ofdHtml;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.WebBrowser browserMain;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ListBox listResult;
        private System.Windows.Forms.Button btnResult;
    }
}