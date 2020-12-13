namespace Trip
{
    partial class CommentViewForm
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
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_CommentSearch = new System.Windows.Forms.Button();
            this.dGV_commentView = new System.Windows.Forms.DataGridView();
            this.textComment = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dGV_commentView)).BeginInit();
            this.SuspendLayout();
            // 
            // textStationName
            // 
            this.textStationName.Location = new System.Drawing.Point(118, 27);
            this.textStationName.Name = "textStationName";
            this.textStationName.Size = new System.Drawing.Size(231, 25);
            this.textStationName.TabIndex = 8;
            // 
            // stationName
            // 
            this.stationName.AutoSize = true;
            this.stationName.Font = new System.Drawing.Font("宋体", 12F);
            this.stationName.Location = new System.Drawing.Point(12, 32);
            this.stationName.Name = "stationName";
            this.stationName.Size = new System.Drawing.Size(89, 20);
            this.stationName.TabIndex = 7;
            this.stationName.Text = "地铁站名";
            // 
            // btn_cancel
            // 
            this.btn_cancel.Font = new System.Drawing.Font("宋体", 12F);
            this.btn_cancel.Location = new System.Drawing.Point(421, 441);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(85, 29);
            this.btn_cancel.TabIndex = 10;
            this.btn_cancel.Text = "取消";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.Btn_cancel_Click);
            // 
            // btn_CommentSearch
            // 
            this.btn_CommentSearch.Font = new System.Drawing.Font("宋体", 12F);
            this.btn_CommentSearch.Location = new System.Drawing.Point(228, 441);
            this.btn_CommentSearch.Name = "btn_CommentSearch";
            this.btn_CommentSearch.Size = new System.Drawing.Size(108, 29);
            this.btn_CommentSearch.TabIndex = 11;
            this.btn_CommentSearch.Text = "查询评论";
            this.btn_CommentSearch.UseVisualStyleBackColor = true;
            this.btn_CommentSearch.Click += new System.EventHandler(this.Btn_CommentSearch_Click);
            // 
            // dGV_commentView
            // 
            this.dGV_commentView.AllowUserToAddRows = false;
            this.dGV_commentView.AllowUserToDeleteRows = false;
            this.dGV_commentView.BackgroundColor = System.Drawing.SystemColors.Info;
            this.dGV_commentView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dGV_commentView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dGV_commentView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGV_commentView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dGV_commentView.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.dGV_commentView.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.dGV_commentView.Location = new System.Drawing.Point(12, 63);
            this.dGV_commentView.Name = "dGV_commentView";
            this.dGV_commentView.RowHeadersWidth = 4;
            this.dGV_commentView.RowTemplate.Height = 27;
            this.dGV_commentView.Size = new System.Drawing.Size(98, 337);
            this.dGV_commentView.TabIndex = 12;
            this.dGV_commentView.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DGV_CommentView_CellMouseClick);
            // 
            // textComment
            // 
            this.textComment.BackColor = System.Drawing.SystemColors.Info;
            this.textComment.Location = new System.Drawing.Point(132, 63);
            this.textComment.Multiline = true;
            this.textComment.Name = "textComment";
            this.textComment.Size = new System.Drawing.Size(374, 337);
            this.textComment.TabIndex = 14;
            // 
            // CommentViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 482);
            this.Controls.Add(this.textComment);
            this.Controls.Add(this.dGV_commentView);
            this.Controls.Add(this.btn_CommentSearch);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.textStationName);
            this.Controls.Add(this.stationName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "CommentViewForm";
            this.Text = "评论查询";
            ((System.ComponentModel.ISupportInitialize)(this.dGV_commentView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textStationName;
        private System.Windows.Forms.Label stationName;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_CommentSearch;
        private System.Windows.Forms.DataGridView dGV_commentView;
        private System.Windows.Forms.TextBox textComment;
    }
}