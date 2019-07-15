namespace FillFlex
{
    partial class RefillWind
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RefillWind));
            this.RefOn = new System.Windows.Forms.Button();
            this.RefOff = new System.Windows.Forms.Button();
            this.label83 = new System.Windows.Forms.Label();
            this.refact = new System.Windows.Forms.PictureBox();
            this.updtimer = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.refact)).BeginInit();
            this.SuspendLayout();
            // 
            // RefOn
            // 
            this.RefOn.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RefOn.Location = new System.Drawing.Point(22, 64);
            this.RefOn.Name = "RefOn";
            this.RefOn.Size = new System.Drawing.Size(179, 40);
            this.RefOn.TabIndex = 0;
            this.RefOn.Text = "Включити долив";
            this.RefOn.UseVisualStyleBackColor = true;
            this.RefOn.Click += new System.EventHandler(this.RefOn_Click);
            // 
            // RefOff
            // 
            this.RefOff.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RefOff.Location = new System.Drawing.Point(22, 118);
            this.RefOff.Name = "RefOff";
            this.RefOff.Size = new System.Drawing.Size(179, 40);
            this.RefOff.TabIndex = 1;
            this.RefOff.Text = "Виключити долив";
            this.RefOff.UseVisualStyleBackColor = true;
            this.RefOff.Click += new System.EventHandler(this.RefOff_Click);
            // 
            // label83
            // 
            this.label83.AutoSize = true;
            this.label83.BackColor = System.Drawing.Color.Transparent;
            this.label83.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label83.Location = new System.Drawing.Point(53, 25);
            this.label83.Name = "label83";
            this.label83.Size = new System.Drawing.Size(134, 21);
            this.label83.TabIndex = 87;
            this.label83.Text = "Долив активний";
            // 
            // refact
            // 
            this.refact.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.refact.Image = ((System.Drawing.Image)(resources.GetObject("refact.Image")));
            this.refact.InitialImage = null;
            this.refact.Location = new System.Drawing.Point(23, 23);
            this.refact.Name = "refact";
            this.refact.Size = new System.Drawing.Size(24, 24);
            this.refact.TabIndex = 86;
            this.refact.TabStop = false;
            // 
            // updtimer
            // 
            this.updtimer.Interval = 500;
            this.updtimer.Tick += new System.EventHandler(this.updtimer_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(19, 171);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 26);
            this.label1.TabIndex = 88;
            this.label1.Text = "* вимкнеться автоматично при\r\n   досягненні завдання";
            // 
            // RefillWind
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(223, 210);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label83);
            this.Controls.Add(this.refact);
            this.Controls.Add(this.RefOff);
            this.Controls.Add(this.RefOn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RefillWind";
            this.Text = "Долив";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RefillFormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.refact)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button RefOn;
        private System.Windows.Forms.Button RefOff;
        private System.Windows.Forms.Label label83;
        private System.Windows.Forms.PictureBox refact;
        private System.Windows.Forms.Timer updtimer;
        private System.Windows.Forms.Label label1;
    }
}