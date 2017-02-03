namespace LoveSaveDo
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnCheck = new System.Windows.Forms.Button();
            this.gb = new System.Windows.Forms.GroupBox();
            this.txtItemContent = new System.Windows.Forms.TextBox();
            this.txtItemTime = new System.Windows.Forms.TextBox();
            this.txtNickName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvComments = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.ofdOpen = new System.Windows.Forms.OpenFileDialog();
            this.gb.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComments)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(0, 0);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 0;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnCheck
            // 
            this.btnCheck.Location = new System.Drawing.Point(81, 0);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(75, 23);
            this.btnCheck.TabIndex = 1;
            this.btnCheck.Text = ">>>";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // gb
            // 
            this.gb.Controls.Add(this.txtItemContent);
            this.gb.Controls.Add(this.txtItemTime);
            this.gb.Controls.Add(this.txtNickName);
            this.gb.Controls.Add(this.label3);
            this.gb.Controls.Add(this.dgvComments);
            this.gb.Controls.Add(this.label2);
            this.gb.Controls.Add(this.label1);
            this.gb.Controls.Add(this.lblName);
            this.gb.Location = new System.Drawing.Point(12, 38);
            this.gb.Name = "gb";
            this.gb.Size = new System.Drawing.Size(598, 372);
            this.gb.TabIndex = 2;
            this.gb.TabStop = false;
            this.gb.Text = "Details";
            // 
            // txtItemContent
            // 
            this.txtItemContent.Location = new System.Drawing.Point(57, 58);
            this.txtItemContent.Multiline = true;
            this.txtItemContent.Name = "txtItemContent";
            this.txtItemContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtItemContent.Size = new System.Drawing.Size(253, 51);
            this.txtItemContent.TabIndex = 7;
            // 
            // txtItemTime
            // 
            this.txtItemTime.Location = new System.Drawing.Point(210, 28);
            this.txtItemTime.Name = "txtItemTime";
            this.txtItemTime.Size = new System.Drawing.Size(100, 21);
            this.txtItemTime.TabIndex = 6;
            // 
            // txtNickName
            // 
            this.txtNickName.Location = new System.Drawing.Point(57, 28);
            this.txtNickName.Name = "txtNickName";
            this.txtNickName.Size = new System.Drawing.Size(100, 21);
            this.txtNickName.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "评论: ";
            // 
            // dgvComments
            // 
            this.dgvComments.AllowUserToAddRows = false;
            this.dgvComments.AllowUserToDeleteRows = false;
            this.dgvComments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvComments.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.dgvComments.Location = new System.Drawing.Point(6, 138);
            this.dgvComments.Name = "dgvComments";
            this.dgvComments.ReadOnly = true;
            this.dgvComments.RowHeadersVisible = false;
            this.dgvComments.RowTemplate.Height = 23;
            this.dgvComments.Size = new System.Drawing.Size(586, 228);
            this.dgvComments.TabIndex = 3;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "角色";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "时间";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "内容";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "内容: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(163, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "时间: ";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(22, 31);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(41, 12);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "角色: ";
            // 
            // ofdOpen
            // 
            this.ofdOpen.Filter = "HTML|*.html|HTM|*.htm|TEXT|*.txt";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 474);
            this.Controls.Add(this.gb);
            this.Controls.Add(this.btnCheck);
            this.Controls.Add(this.btnOpen);
            this.Name = "Form1";
            this.Text = "LoveSave";
            this.gb.ResumeLayout(false);
            this.gb.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComments)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.GroupBox gb;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvComments;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.OpenFileDialog ofdOpen;
        private System.Windows.Forms.TextBox txtItemContent;
        private System.Windows.Forms.TextBox txtItemTime;
        private System.Windows.Forms.TextBox txtNickName;
    }
}

