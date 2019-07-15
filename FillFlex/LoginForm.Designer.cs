namespace FillFlex
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.submit = new System.Windows.Forms.Button();
            this.LoginText = new System.Windows.Forms.TextBox();
            this.PwdText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(22, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Логін";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(22, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "Пароль";
            // 
            // submit
            // 
            this.submit.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.submit.Location = new System.Drawing.Point(26, 124);
            this.submit.Name = "submit";
            this.submit.Size = new System.Drawing.Size(203, 33);
            this.submit.TabIndex = 2;
            this.submit.Text = "Увійти";
            this.submit.UseVisualStyleBackColor = true;
            this.submit.Click += new System.EventHandler(this.submit_Click);
            // 
            // LoginText
            // 
            this.LoginText.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LoginText.ForeColor = System.Drawing.Color.Gray;
            this.LoginText.Location = new System.Drawing.Point(102, 23);
            this.LoginText.MaxLength = 20;
            this.LoginText.Name = "LoginText";
            this.LoginText.Size = new System.Drawing.Size(127, 27);
            this.LoginText.TabIndex = 3;
            this.LoginText.Text = "Введіть ім\'я";
            this.LoginText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.LoginText.Click += new System.EventHandler(this.LoginFieldEnter);
            this.LoginText.Leave += new System.EventHandler(this.LoginFieldLeave);
            // 
            // PwdText
            // 
            this.PwdText.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PwdText.ForeColor = System.Drawing.Color.Gray;
            this.PwdText.Location = new System.Drawing.Point(102, 75);
            this.PwdText.Name = "PwdText";
            this.PwdText.PasswordChar = '*';
            this.PwdText.Size = new System.Drawing.Size(127, 27);
            this.PwdText.TabIndex = 4;
            this.PwdText.Text = "******";
            this.PwdText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.PwdText.Click += new System.EventHandler(this.PwdFieldEnter);
            this.PwdText.Leave += new System.EventHandler(this.PwdFieldLeave);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(255, 177);
            this.Controls.Add(this.PwdText);
            this.Controls.Add(this.LoginText);
            this.Controls.Add(this.submit);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoginForm";
            this.Text = "Вхід";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LoginFormClose);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button submit;
        private System.Windows.Forms.TextBox LoginText;
        private System.Windows.Forms.TextBox PwdText;
    }
}