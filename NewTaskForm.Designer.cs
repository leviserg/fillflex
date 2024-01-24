namespace FillFlex
{
    partial class NewTaskForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewTaskForm));
            this.SetpVal = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.Submit = new System.Windows.Forms.Button();
            this.setptooltip = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.IdLabel = new System.Windows.Forms.TextBox();
            this.tankIdLabel = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.label6 = new System.Windows.Forms.Label();
            this.transpField = new System.Windows.Forms.TextBox();
            this.driverField = new System.Windows.Forms.TextBox();
            this.customerField = new System.Windows.Forms.TextBox();
            this.SourceTankBox = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.SetpVal)).BeginInit();
            this.SuspendLayout();
            // 
            // SetpVal
            // 
            this.SetpVal.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SetpVal.Location = new System.Drawing.Point(149, 109);
            this.SetpVal.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.SetpVal.Name = "SetpVal";
            this.SetpVal.Size = new System.Drawing.Size(125, 27);
            this.SetpVal.TabIndex = 0;
            this.SetpVal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(23, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Завдання, кг *";
            // 
            // Submit
            // 
            this.Submit.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Submit.Location = new System.Drawing.Point(12, 270);
            this.Submit.Name = "Submit";
            this.Submit.Size = new System.Drawing.Size(579, 46);
            this.Submit.TabIndex = 2;
            this.Submit.Text = "Задати";
            this.Submit.UseVisualStyleBackColor = true;
            this.Submit.Click += new System.EventHandler(this.Submit_Click);
            // 
            // setptooltip
            // 
            this.setptooltip.AutoSize = true;
            this.setptooltip.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.setptooltip.Location = new System.Drawing.Point(177, 142);
            this.setptooltip.Name = "setptooltip";
            this.setptooltip.Size = new System.Drawing.Size(66, 13);
            this.setptooltip.TabIndex = 3;
            this.setptooltip.Text = "0...99999 кг";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(21, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(252, 21);
            this.label3.TabIndex = 4;
            this.label3.Text = "Ідентифікаційний код наливу *";
            // 
            // IdLabel
            // 
            this.IdLabel.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.IdLabel.Location = new System.Drawing.Point(12, 61);
            this.IdLabel.Name = "IdLabel";
            this.IdLabel.Size = new System.Drawing.Size(265, 27);
            this.IdLabel.TabIndex = 5;
            this.IdLabel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tankIdLabel
            // 
            this.tankIdLabel.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tankIdLabel.Location = new System.Drawing.Point(315, 61);
            this.tankIdLabel.Name = "tankIdLabel";
            this.tankIdLabel.Size = new System.Drawing.Size(265, 27);
            this.tankIdLabel.TabIndex = 7;
            this.tankIdLabel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(332, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(233, 21);
            this.label4.TabIndex = 6;
            this.label4.Text = "Ідентифік. код флексітанку*";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label5.Location = new System.Drawing.Point(12, 244);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(138, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "* - обов\'язково заповнити";
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(603, 330);
            this.shapeContainer1.TabIndex = 9;
            this.shapeContainer1.TabStop = false;
            // 
            // lineShape1
            // 
            this.lineShape1.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.X1 = 294;
            this.lineShape1.X2 = 294;
            this.lineShape1.Y1 = 10;
            this.lineShape1.Y2 = 257;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(372, 98);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(147, 21);
            this.label6.TabIndex = 10;
            this.label6.Text = "Дані транспорту:";
            // 
            // transpField
            // 
            this.transpField.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.transpField.ForeColor = System.Drawing.Color.Gray;
            this.transpField.Location = new System.Drawing.Point(315, 133);
            this.transpField.Name = "transpField";
            this.transpField.Size = new System.Drawing.Size(265, 27);
            this.transpField.TabIndex = 11;
            this.transpField.Text = "введіть номер транспорту";
            this.transpField.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.transpField.Click += new System.EventHandler(this.TranspEnter);
            this.transpField.Leave += new System.EventHandler(this.TranspLeave);
            // 
            // driverField
            // 
            this.driverField.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.driverField.ForeColor = System.Drawing.Color.Gray;
            this.driverField.Location = new System.Drawing.Point(315, 181);
            this.driverField.Name = "driverField";
            this.driverField.Size = new System.Drawing.Size(265, 27);
            this.driverField.TabIndex = 12;
            this.driverField.Text = "введіть П.І.Б водія";
            this.driverField.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.driverField.Click += new System.EventHandler(this.driverEnter);
            this.driverField.Leave += new System.EventHandler(this.driverLeave);
            // 
            // customerField
            // 
            this.customerField.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.customerField.ForeColor = System.Drawing.Color.Gray;
            this.customerField.Location = new System.Drawing.Point(315, 226);
            this.customerField.Name = "customerField";
            this.customerField.Size = new System.Drawing.Size(265, 27);
            this.customerField.TabIndex = 13;
            this.customerField.Text = "введіть назву Замовника";
            this.customerField.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.customerField.Click += new System.EventHandler(this.customerEnter);
            this.customerField.Leave += new System.EventHandler(this.customerLeave);
            // 
            // SourceTankBox
            // 
            this.SourceTankBox.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SourceTankBox.FormattingEnabled = true;
            this.SourceTankBox.Location = new System.Drawing.Point(149, 184);
            this.SourceTankBox.Name = "SourceTankBox";
            this.SourceTankBox.Size = new System.Drawing.Size(124, 29);
            this.SourceTankBox.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(21, 187);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 21);
            this.label7.TabIndex = 15;
            this.label7.Text = "Бак № *";
            // 
            // NewTaskForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 330);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.SourceTankBox);
            this.Controls.Add(this.customerField);
            this.Controls.Add(this.driverField);
            this.Controls.Add(this.transpField);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tankIdLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.IdLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.setptooltip);
            this.Controls.Add(this.Submit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SetpVal);
            this.Controls.Add(this.shapeContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NewTaskForm";
            this.Text = "Нове завдання";
            ((System.ComponentModel.ISupportInitialize)(this.SetpVal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown SetpVal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Submit;
        private System.Windows.Forms.Label setptooltip;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox IdLabel;
        private System.Windows.Forms.TextBox tankIdLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox transpField;
        private System.Windows.Forms.TextBox driverField;
        private System.Windows.Forms.TextBox customerField;
        private System.Windows.Forms.ComboBox SourceTankBox;
        private System.Windows.Forms.Label label7;
    }
}