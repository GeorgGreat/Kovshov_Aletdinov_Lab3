namespace CGLab2
{
    partial class task_2_1
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.fillRadioButton = new System.Windows.Forms.RadioButton();
            this.paintingRadioButton = new System.Windows.Forms.RadioButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.pictureFfRadioButton = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.paintWind)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(12, 105);
            this.panel1.Size = new System.Drawing.Size(1046, 410);
            // 
            // paintWind
            // 
            this.paintWind.MouseDown += new System.Windows.Forms.MouseEventHandler(this.paintWind_MouseDown);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pictureFfRadioButton);
            this.groupBox2.Controls.Add(this.fillRadioButton);
            this.groupBox2.Controls.Add(this.paintingRadioButton);
            this.groupBox2.Location = new System.Drawing.Point(786, 32);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(272, 67);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Тип кисти";
            // 
            // fillRadioButton
            // 
            this.fillRadioButton.AutoSize = true;
            this.fillRadioButton.Location = new System.Drawing.Point(6, 42);
            this.fillRadioButton.Name = "fillRadioButton";
            this.fillRadioButton.Size = new System.Drawing.Size(108, 17);
            this.fillRadioButton.TabIndex = 1;
            this.fillRadioButton.TabStop = true;
            this.fillRadioButton.Text = "Заливка цветом";
            this.fillRadioButton.UseVisualStyleBackColor = true;
            this.fillRadioButton.CheckedChanged += new System.EventHandler(this.fillRadioButton_CheckedChanged);
            // 
            // paintingRadioButton
            // 
            this.paintingRadioButton.AutoSize = true;
            this.paintingRadioButton.Checked = true;
            this.paintingRadioButton.Location = new System.Drawing.Point(6, 19);
            this.paintingRadioButton.Name = "paintingRadioButton";
            this.paintingRadioButton.Size = new System.Drawing.Size(80, 17);
            this.paintingRadioButton.TabIndex = 0;
            this.paintingRadioButton.TabStop = true;
            this.paintingRadioButton.Text = "Рисование";
            this.paintingRadioButton.UseVisualStyleBackColor = true;
            this.paintingRadioButton.CheckedChanged += new System.EventHandler(this.paintingRadioButton_CheckedChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // pictureFfRadioButton
            // 
            this.pictureFfRadioButton.AutoSize = true;
            this.pictureFfRadioButton.Location = new System.Drawing.Point(142, 19);
            this.pictureFfRadioButton.Name = "pictureFfRadioButton";
            this.pictureFfRadioButton.Size = new System.Drawing.Size(124, 17);
            this.pictureFfRadioButton.TabIndex = 2;
            this.pictureFfRadioButton.TabStop = true;
            this.pictureFfRadioButton.Text = "Заливка картинкой";
            this.pictureFfRadioButton.UseVisualStyleBackColor = true;
            this.pictureFfRadioButton.CheckedChanged += new System.EventHandler(this.pictureFfRadioButton_CheckedChanged);
            // 
            // PaintFill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 527);
            this.Controls.Add(this.groupBox2);
            this.Name = "PaintFill";
            this.Text = "PaintFill";
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.paintWind)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton fillRadioButton;
        private System.Windows.Forms.RadioButton paintingRadioButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.RadioButton pictureFfRadioButton;
    }
}