namespace Trip
{
    partial class CommentForm
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
            this.textStationName = new System.Windows.Forms.TextBox();
            this.stationName = new System.Windows.Forms.Label();
            this.textCommentContent = new System.Windows.Forms.TextBox();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_comment = new System.Windows.Forms.Button();
            this.textLineName = new System.Windows.Forms.TextBox();
            this.lineName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textStationName
            // 
            this.textStationName.Location = new System.Drawing.Point(296, 40);
            this.textStationName.Name = "textStationName";
            this.textStationName.Size = new System.Drawing.Size(122, 25);
            this.textStationName.TabIndex = 6;
            // 
            // stationName
            // 
            this.stationName.AutoSize = true;
            this.stationName.Font = new System.Drawing.Font("宋体", 12F);
            this.stationName.Location = new System.Drawing.Point(201, 45);
            this.stationName.Name = "stationName";
            this.stationName.Size = new System.Drawing.Size(89, 20);
            this.stationName.TabIndex = 4;
            this.stationName.Text = "地铁站名";
            // 
            // textCommentContent
            // 
            this.textCommentContent.Font = new System.Drawing.Font("宋体", 10F);
            this.textCommentContent.Location = new System.Drawing.Point(27, 90);
            this.textCommentContent.Multiline = true;
            this.textCommentContent.Name = "textCommentContent";
            this.textCommentContent.Size = new System.Drawing.Size(391, 285);
            this.textCommentContent.TabIndex = 8;
            // 
            // btn_cancel
            // 
            this.btn_cancel.Font = new System.Drawing.Font("宋体", 12F);
            this.btn_cancel.Location = new System.Drawing.Point(333, 404);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(85, 29);
            this.btn_cancel.TabIndex = 9;
            this.btn_cancel.Text = "取消";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.Btn_cancel_Click);
            // 
            // btn_comment
            // 
            this.btn_comment.Font = new System.Drawing.Font("宋体", 12F);
            this.btn_comment.Location = new System.Drawing.Point(27, 404);
            this.btn_comment.Name = "btn_comment";
            this.btn_comment.Size = new System.Drawing.Size(108, 29);
            this.btn_comment.TabIndex = 10;
            this.btn_comment.Text = "发表评论";
            this.btn_comment.UseVisualStyleBackColor = true;
            this.btn_comment.Click += new System.EventHandler(this.Btn_comment_Click);
            // 
            // textLineName
            // 
            this.textLineName.Location = new System.Drawing.Point(97, 40);
            this.textLineName.Name = "textLineName";
            this.textLineName.Size = new System.Drawing.Size(85, 25);
            this.textLineName.TabIndex = 12;
            // 
            // lineName
            // 
            this.lineName.AutoSize = true;
            this.lineName.Font = new System.Drawing.Font("宋体", 12F);
            this.lineName.Location = new System.Drawing.Point(23, 45);
            this.lineName.Name = "lineName";
            this.lineName.Size = new System.Drawing.Size(69, 20);
            this.lineName.TabIndex = 11;
            this.lineName.Text = "线路名";
            // 
            // CommentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 445);
            this.Controls.Add(this.textLineName);
            this.Controls.Add(this.lineName);
            this.Controls.Add(this.btn_comment);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.textCommentContent);
            this.Controls.Add(this.textStationName);
            this.Controls.Add(this.stationName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "CommentForm";
            this.Text = "发表评论";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textStationName;
        private System.Windows.Forms.Label stationName;
        private System.Windows.Forms.TextBox textCommentContent;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_comment;
        private System.Windows.Forms.TextBox textLineName;
        private System.Windows.Forms.Label lineName;
    }
}