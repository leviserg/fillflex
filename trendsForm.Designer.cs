namespace FillFlex
{
    partial class trendsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(trendsForm));
            this.cartChart = new LiveCharts.WinForms.CartesianChart();
            this.scrollChart = new LiveCharts.WinForms.CartesianChart();
            this.chckParam0 = new System.Windows.Forms.CheckBox();
            this.chckParam1 = new System.Windows.Forms.CheckBox();
            this.chckParam2 = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chckParam7 = new System.Windows.Forms.CheckBox();
            this.chckParam6 = new System.Windows.Forms.CheckBox();
            this.chckParam5 = new System.Windows.Forms.CheckBox();
            this.chckParam4 = new System.Windows.Forms.CheckBox();
            this.chckParam3 = new System.Windows.Forms.CheckBox();
            this.CopyBtn = new System.Windows.Forms.Button();
            this.SaveBtn = new System.Windows.Forms.Button();
            this.PrintBtn = new System.Windows.Forms.Button();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.printPage = new System.Drawing.Printing.PrintDocument();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cartChart
            // 
            this.cartChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cartChart.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cartChart.Location = new System.Drawing.Point(25, 97);
            this.cartChart.Name = "cartChart";
            this.cartChart.Size = new System.Drawing.Size(831, 406);
            this.cartChart.TabIndex = 10;
            this.cartChart.Text = "cartChart";
            // 
            // scrollChart
            // 
            this.scrollChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.scrollChart.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.scrollChart.Location = new System.Drawing.Point(41, 518);
            this.scrollChart.Name = "scrollChart";
            this.scrollChart.Size = new System.Drawing.Size(790, 112);
            this.scrollChart.TabIndex = 11;
            this.scrollChart.Text = "cartesianChart1";
            // 
            // chckParam0
            // 
            this.chckParam0.AutoSize = true;
            this.chckParam0.Checked = true;
            this.chckParam0.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chckParam0.ForeColor = System.Drawing.Color.Blue;
            this.chckParam0.Location = new System.Drawing.Point(17, 6);
            this.chckParam0.Name = "chckParam0";
            this.chckParam0.Size = new System.Drawing.Size(35, 17);
            this.chckParam0.TabIndex = 12;
            this.chckParam0.Text = "...";
            this.chckParam0.UseVisualStyleBackColor = true;
            this.chckParam0.CheckedChanged += new System.EventHandler(this.ToggleLine0Series);
            // 
            // chckParam1
            // 
            this.chckParam1.AutoSize = true;
            this.chckParam1.Checked = true;
            this.chckParam1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chckParam1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.chckParam1.Location = new System.Drawing.Point(121, 6);
            this.chckParam1.Name = "chckParam1";
            this.chckParam1.Size = new System.Drawing.Size(35, 17);
            this.chckParam1.TabIndex = 13;
            this.chckParam1.Text = "...";
            this.chckParam1.UseVisualStyleBackColor = true;
            this.chckParam1.CheckedChanged += new System.EventHandler(this.ToggleLine1Series);
            // 
            // chckParam2
            // 
            this.chckParam2.AutoSize = true;
            this.chckParam2.Checked = true;
            this.chckParam2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chckParam2.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.chckParam2.Location = new System.Drawing.Point(225, 6);
            this.chckParam2.Name = "chckParam2";
            this.chckParam2.Size = new System.Drawing.Size(35, 17);
            this.chckParam2.TabIndex = 14;
            this.chckParam2.Text = "...";
            this.chckParam2.UseVisualStyleBackColor = true;
            this.chckParam2.CheckedChanged += new System.EventHandler(this.ToggleLine2Series);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chckParam7);
            this.panel1.Controls.Add(this.chckParam6);
            this.panel1.Controls.Add(this.chckParam5);
            this.panel1.Controls.Add(this.chckParam4);
            this.panel1.Controls.Add(this.chckParam3);
            this.panel1.Controls.Add(this.chckParam2);
            this.panel1.Controls.Add(this.chckParam1);
            this.panel1.Controls.Add(this.chckParam0);
            this.panel1.Location = new System.Drawing.Point(436, 23);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(427, 51);
            this.panel1.TabIndex = 15;
            // 
            // chckParam7
            // 
            this.chckParam7.AutoSize = true;
            this.chckParam7.Checked = true;
            this.chckParam7.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chckParam7.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.chckParam7.Location = new System.Drawing.Point(329, 29);
            this.chckParam7.Name = "chckParam7";
            this.chckParam7.Size = new System.Drawing.Size(35, 17);
            this.chckParam7.TabIndex = 19;
            this.chckParam7.Text = "...";
            this.chckParam7.UseVisualStyleBackColor = true;
            this.chckParam7.CheckedChanged += new System.EventHandler(this.ToggleLine7Series);
            // 
            // chckParam6
            // 
            this.chckParam6.AutoSize = true;
            this.chckParam6.Checked = true;
            this.chckParam6.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chckParam6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.chckParam6.Location = new System.Drawing.Point(225, 29);
            this.chckParam6.Name = "chckParam6";
            this.chckParam6.Size = new System.Drawing.Size(35, 17);
            this.chckParam6.TabIndex = 18;
            this.chckParam6.Text = "...";
            this.chckParam6.UseVisualStyleBackColor = true;
            this.chckParam6.CheckedChanged += new System.EventHandler(this.ToggleLine6Series);
            // 
            // chckParam5
            // 
            this.chckParam5.AutoSize = true;
            this.chckParam5.Checked = true;
            this.chckParam5.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chckParam5.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.chckParam5.Location = new System.Drawing.Point(121, 29);
            this.chckParam5.Name = "chckParam5";
            this.chckParam5.Size = new System.Drawing.Size(35, 17);
            this.chckParam5.TabIndex = 17;
            this.chckParam5.Text = "...";
            this.chckParam5.UseVisualStyleBackColor = true;
            this.chckParam5.CheckedChanged += new System.EventHandler(this.ToggleLine5Series);
            // 
            // chckParam4
            // 
            this.chckParam4.AutoSize = true;
            this.chckParam4.Checked = true;
            this.chckParam4.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chckParam4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.chckParam4.Location = new System.Drawing.Point(17, 29);
            this.chckParam4.Name = "chckParam4";
            this.chckParam4.Size = new System.Drawing.Size(35, 17);
            this.chckParam4.TabIndex = 16;
            this.chckParam4.Text = "...";
            this.chckParam4.UseVisualStyleBackColor = true;
            this.chckParam4.CheckedChanged += new System.EventHandler(this.ToggleLine4Series);
            // 
            // chckParam3
            // 
            this.chckParam3.AutoSize = true;
            this.chckParam3.Checked = true;
            this.chckParam3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chckParam3.ForeColor = System.Drawing.Color.Blue;
            this.chckParam3.Location = new System.Drawing.Point(329, 6);
            this.chckParam3.Name = "chckParam3";
            this.chckParam3.Size = new System.Drawing.Size(35, 17);
            this.chckParam3.TabIndex = 15;
            this.chckParam3.Text = "...";
            this.chckParam3.UseVisualStyleBackColor = true;
            this.chckParam3.CheckedChanged += new System.EventHandler(this.ToggleLine3Series);
            // 
            // CopyBtn
            // 
            this.CopyBtn.Location = new System.Drawing.Point(307, 21);
            this.CopyBtn.Name = "CopyBtn";
            this.CopyBtn.Size = new System.Drawing.Size(122, 48);
            this.CopyBtn.TabIndex = 16;
            this.CopyBtn.Text = "Copy To\r\nClipboard";
            this.CopyBtn.UseVisualStyleBackColor = true;
            this.CopyBtn.Click += new System.EventHandler(this.CopyBtn_Click);
            // 
            // SaveBtn
            // 
            this.SaveBtn.Location = new System.Drawing.Point(166, 21);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(122, 48);
            this.SaveBtn.TabIndex = 17;
            this.SaveBtn.Text = "Save To\r\nFile";
            this.SaveBtn.UseVisualStyleBackColor = true;
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // PrintBtn
            // 
            this.PrintBtn.Location = new System.Drawing.Point(25, 21);
            this.PrintBtn.Name = "PrintBtn";
            this.PrintBtn.Size = new System.Drawing.Size(122, 48);
            this.PrintBtn.TabIndex = 18;
            this.PrintBtn.Text = "Print\r\nPage";
            this.PrintBtn.UseVisualStyleBackColor = true;
            this.PrintBtn.Click += new System.EventHandler(this.PrintBtn_Click);
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // printPage
            // 
            this.printPage.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printPage_PrintPage);
            // 
            // trendsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 664);
            this.Controls.Add(this.PrintBtn);
            this.Controls.Add(this.SaveBtn);
            this.Controls.Add(this.CopyBtn);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.scrollChart);
            this.Controls.Add(this.cartChart);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "trendsForm";
            this.Text = "Тренди";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private LiveCharts.WinForms.CartesianChart cartChart;
        private LiveCharts.WinForms.CartesianChart scrollChart;
        private System.Windows.Forms.CheckBox chckParam0;
        private System.Windows.Forms.CheckBox chckParam1;
        private System.Windows.Forms.CheckBox chckParam2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chckParam5;
        private System.Windows.Forms.CheckBox chckParam4;
        private System.Windows.Forms.CheckBox chckParam3;
        private System.Windows.Forms.CheckBox chckParam7;
        private System.Windows.Forms.CheckBox chckParam6;
        private System.Windows.Forms.Button CopyBtn;
        private System.Windows.Forms.Button SaveBtn;
        private System.Windows.Forms.Button PrintBtn;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Drawing.Printing.PrintDocument printPage;
    }
}