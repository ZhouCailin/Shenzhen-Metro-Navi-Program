namespace Trip
{
    partial class RegisterForm
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
            this.textPassword = new System.Windows.Forms.TextBox();
            this.textUserName = new System.Windows.Forms.TextBox();
            this.password = new System.Windows.Forms.Label();
            this.userName = new System.Windows.Forms.Label();
            this.confirmPassword = new System.Windows.Forms.Label();
            this.textComfirmPassword = new System.Windows.Forms.TextBox();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_confirm = new System.Windows.Forms.Button();
            this.userID = new System.Windows.Forms.Label();
            this.textUserID = new System.Windows.Forms.TextBox();
            this.userSex = new System.Windows.Forms.Label();
            this.radBtnMen = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radBtnWomen = new System.Windows.Forms.RadioButton();
            this.textUserAge = new System.Windows.Forms.TextBox();
            this.userAge = new System.Windows.Forms.Label();
            this.userCity = new System.Windows.Forms.Label();
            this.textUserCity = new System.Windows.Forms.TextBox();
            this.userSumName = new System.Windows.Forms.Label();
            this.textUserSumName = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textPassword
            // 
            this.textPassword.Location = new System.Drawing.Point(237, 270);
            this.textPassword.Name = "textPassword";
            this.textPassword.Size = new System.Drawing.Size(172, 25);
            this.textPassword.TabIndex = 7;
            // 
            // textUserName
            // 
            this.textUserName.Location = new System.Drawing.Point(236, 58);
            this.textUserName.Name = "textUserName";
            this.textUserName.Size = new System.Drawing.Size(172, 25);
            this.textUserName.TabIndex = 6;
            // 
            // password
            // 
            this.password.AutoSize = true;
            this.password.Font = new System.Drawing.Font("宋体", 12F);
            this.password.Location = new System.Drawing.Point(107, 275);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(69, 20);
            this.password.TabIndex = 5;
            this.password.Text = "密码：";
            // 
            // userName
            // 
            this.userName.AutoSize = true;
            this.userName.Font = new System.Drawing.Font("宋体", 12F);
            this.userName.Location = new System.Drawing.Point(97, 63);
            this.userName.Name = "userName";
            this.userName.Size = new System.Drawing.Size(89, 20);
            this.userName.TabIndex = 4;
            this.userName.Text = "用户名：";
            // 
            // confirmPassword
            // 
            this.confirmPassword.AutoSize = true;
            this.confirmPassword.Font = new System.Drawing.Font("宋体", 12F);
            this.confirmPassword.Location = new System.Drawing.Point(66, 320);
            this.confirmPassword.Name = "confirmPassword";
            this.confirmPassword.Size = new System.Drawing.Size(169, 20);
            this.confirmPassword.TabIndex = 8;
            this.confirmPassword.Text = "请确认你的密码：";
            // 
            // textComfirmPassword
            // 
            this.textComfirmPassword.Location = new System.Drawing.Point(237, 315);
            this.textComfirmPassword.Name = "textComfirmPassword";
            this.textComfirmPassword.Size = new System.Drawing.Size(172, 25);
            this.textComfirmPassword.TabIndex = 9;
            // 
            // btn_cancel
            // 
            this.btn_cancel.Font = new System.Drawing.Font("宋体", 12F);
            this.btn_cancel.Location = new System.Drawing.Point(314, 382);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(85, 29);
            this.btn_cancel.TabIndex = 10;
            this.btn_cancel.Text = "取消";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.Btn_cancel_Click);
            // 
            // btn_confirm
            // 
            this.btn_confirm.Font = new System.Drawing.Font("宋体", 12F);
            this.btn_confirm.Location = new System.Drawing.Point(101, 382);
            this.btn_confirm.Name = "btn_confirm";
            this.btn_confirm.Size = new System.Drawing.Size(85, 29);
            this.btn_confirm.TabIndex = 11;
            this.btn_confirm.Text = "确定";
            this.btn_confirm.UseVisualStyleBackColor = true;
            this.btn_confirm.Click += new System.EventHandler(this.Btn_Confirm_Click);
            // 
            // userID
            // 
            this.userID.AutoSize = true;
            this.userID.Font = new System.Drawing.Font("宋体", 12F);
            this.userID.Location = new System.Drawing.Point(83, 106);
            this.userID.Name = "userID";
            this.userID.Size = new System.Drawing.Size(129, 20);
            this.userID.TabIndex = 12;
            this.userID.Text = "用户身份证：";
            // 
            // textUserID
            // 
            this.textUserID.Location = new System.Drawing.Point(236, 101);
            this.textUserID.Name = "textUserID";
            this.textUserID.Size = new System.Drawing.Size(172, 25);
            this.textUserID.TabIndex = 13;
            // 
            // userSex
            // 
            this.userSex.AutoSize = true;
            this.userSex.Font = new System.Drawing.Font("宋体", 12F);
            this.userSex.Location = new System.Drawing.Point(97, 145);
            this.userSex.Name = "userSex";
            this.userSex.Size = new System.Drawing.Size(69, 20);
            this.userSex.TabIndex = 14;
            this.userSex.Text = "性别：";
            // 
            // radBtnMen
            // 
            this.radBtnMen.AutoSize = true;
            this.radBtnMen.Location = new System.Drawing.Point(2, 12);
            this.radBtnMen.Name = "radBtnMen";
            this.radBtnMen.Size = new System.Drawing.Size(43, 19);
            this.radBtnMen.TabIndex = 15;
            this.radBtnMen.TabStop = true;
            this.radBtnMen.Text = "男";
            this.radBtnMen.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radBtnWomen);
            this.groupBox1.Controls.Add(this.radBtnMen);
            this.groupBox1.Location = new System.Drawing.Point(235, 134);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(173, 31);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            // 
            // radBtnWomen
            // 
            this.radBtnWomen.AutoSize = true;
            this.radBtnWomen.Location = new System.Drawing.Point(79, 12);
            this.radBtnWomen.Name = "radBtnWomen";
            this.radBtnWomen.Size = new System.Drawing.Size(43, 19);
            this.radBtnWomen.TabIndex = 16;
            this.radBtnWomen.TabStop = true;
            this.radBtnWomen.Text = "女";
            this.radBtnWomen.UseVisualStyleBackColor = true;
            // 
            // textUserAge
            // 
            this.textUserAge.Location = new System.Drawing.Point(143, 179);
            this.textUserAge.Name = "textUserAge";
            this.textUserAge.Size = new System.Drawing.Size(57, 25);
            this.textUserAge.TabIndex = 18;
            // 
            // userAge
            // 
            this.userAge.AutoSize = true;
            this.userAge.Font = new System.Drawing.Font("宋体", 12F);
            this.userAge.Location = new System.Drawing.Point(84, 184);
            this.userAge.Name = "userAge";
            this.userAge.Size = new System.Drawing.Size(69, 20);
            this.userAge.TabIndex = 17;
            this.userAge.Text = "年龄：";
            // 
            // userCity
            // 
            this.userCity.AutoSize = true;
            this.userCity.Font = new System.Drawing.Font("宋体", 12F);
            this.userCity.Location = new System.Drawing.Point(221, 184);
            this.userCity.Name = "userCity";
            this.userCity.Size = new System.Drawing.Size(89, 20);
            this.userCity.TabIndex = 19;
            this.userCity.Text = "居住地：";
            // 
            // textUserCity
            // 
            this.textUserCity.Location = new System.Drawing.Point(324, 179);
            this.textUserCity.Name = "textUserCity";
            this.textUserCity.Size = new System.Drawing.Size(85, 25);
            this.textUserCity.TabIndex = 20;
            // 
            // userSumName
            // 
            this.userSumName.AutoSize = true;
            this.userSumName.Font = new System.Drawing.Font("宋体", 12F);
            this.userSumName.Location = new System.Drawing.Point(89, 232);
            this.userSumName.Name = "userSumName";
            this.userSumName.Size = new System.Drawing.Size(109, 20);
            this.userSumName.TabIndex = 21;
            this.userSumName.Text = "备注昵称：";
            // 
            // textUserSumName
            // 
            this.textUserSumName.Location = new System.Drawing.Point(234, 227);
            this.textUserSumName.Name = "textUserSumName";
            this.textUserSumName.Size = new System.Drawing.Size(174, 25);
            this.textUserSumName.TabIndex = 22;
            // 
            // RegisterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 442);
            this.Controls.Add(this.textUserSumName);
            this.Controls.Add(this.userSumName);
            this.Controls.Add(this.textUserCity);
            this.Controls.Add(this.userCity);
            this.Controls.Add(this.textUserAge);
            this.Controls.Add(this.userAge);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.userSex);
            this.Controls.Add(this.textUserID);
            this.Controls.Add(this.userID);
            this.Controls.Add(this.btn_confirm);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.textComfirmPassword);
            this.Controls.Add(this.confirmPassword);
            this.Controls.Add(this.textPassword);
            this.Controls.Add(this.textUserName);
            this.Controls.Add(this.password);
            this.Controls.Add(this.userName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "RegisterForm";
            this.Text = "用户注册";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textPassword;
        private System.Windows.Forms.TextBox textUserName;
        private System.Windows.Forms.Label password;
        private System.Windows.Forms.Label userName;
        private System.Windows.Forms.Label confirmPassword;
        private System.Windows.Forms.TextBox textComfirmPassword;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_confirm;
        private System.Windows.Forms.Label userID;
        private System.Windows.Forms.TextBox textUserID;
        private System.Windows.Forms.Label userSex;
        private System.Windows.Forms.RadioButton radBtnMen;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radBtnWomen;
        private System.Windows.Forms.TextBox textUserAge;
        private System.Windows.Forms.Label userAge;
        private System.Windows.Forms.Label userCity;
        private System.Windows.Forms.TextBox textUserCity;
        private System.Windows.Forms.Label userSumName;
        private System.Windows.Forms.TextBox textUserSumName;
    }
}