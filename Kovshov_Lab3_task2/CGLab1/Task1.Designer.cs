namespace CGLab1
{
    partial class Task1
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.rangeLeftNumeric = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rangeRightNumeric = new System.Windows.Forms.NumericUpDown();
            this.funcComboBox = new System.Windows.Forms.ComboBox();
            this.drawFunc = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rangeLeftNumeric)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rangeRightNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(12, 66);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(578, 366);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.SizeChanged += new System.EventHandler(this.pictureBox1_SizeChanged);
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // rangeLeftNumeric
            // 
            this.rangeLeftNumeric.DecimalPlaces = 10;
            this.rangeLeftNumeric.Location = new System.Drawing.Point(15, 19);
            this.rangeLeftNumeric.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.rangeLeftNumeric.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.rangeLeftNumeric.Name = "rangeLeftNumeric";
            this.rangeLeftNumeric.Size = new System.Drawing.Size(127, 20);
            this.rangeLeftNumeric.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.rangeRightNumeric);
            this.groupBox1.Controls.Add(this.rangeLeftNumeric);
            this.groupBox1.Location = new System.Drawing.Point(139, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(311, 47);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Диапазон";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(148, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "--";
            // 
            // rangeRightNumeric
            // 
            this.rangeRightNumeric.DecimalPlaces = 10;
            this.rangeRightNumeric.Location = new System.Drawing.Point(167, 19);
            this.rangeRightNumeric.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.rangeRightNumeric.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.rangeRightNumeric.Name = "rangeRightNumeric";
            this.rangeRightNumeric.Size = new System.Drawing.Size(127, 20);
            this.rangeRightNumeric.TabIndex = 2;
            // 
            // funcComboBox
            // 
            this.funcComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.funcComboBox.FormattingEnabled = true;
            this.funcComboBox.Location = new System.Drawing.Point(12, 25);
            this.funcComboBox.Name = "funcComboBox";
            this.funcComboBox.Size = new System.Drawing.Size(121, 21);
            this.funcComboBox.TabIndex = 3;
            // 
            // drawFunc
            // 
            this.drawFunc.Location = new System.Drawing.Point(477, 28);
            this.drawFunc.Name = "drawFunc";
            this.drawFunc.Size = new System.Drawing.Size(75, 23);
            this.drawFunc.TabIndex = 4;
            this.drawFunc.Text = "Построить";
            this.drawFunc.UseVisualStyleBackColor = true;
            this.drawFunc.Click += new System.EventHandler(this.drawFunc_Click);
            // 
            // Task1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 444);
            this.Controls.Add(this.drawFunc);
            this.Controls.Add(this.funcComboBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Task1";
            this.Text = "Task1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Task1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rangeLeftNumeric)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rangeRightNumeric)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.NumericUpDown rangeLeftNumeric;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown rangeRightNumeric;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox funcComboBox;
        private System.Windows.Forms.Button drawFunc;
    }
}