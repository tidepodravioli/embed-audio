namespace audiopuke
{
    partial class Form1
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
        	this.trackBar1 = new System.Windows.Forms.TrackBar();
        	this.tracklbl = new System.Windows.Forms.Label();
        	this.albumlbl = new System.Windows.Forms.Label();
        	this.artistlbl = new System.Windows.Forms.Label();
        	this.yearlbl = new System.Windows.Forms.Label();
        	this.lblPosition = new System.Windows.Forms.Label();
        	this.comboBox1 = new System.Windows.Forms.ComboBox();
        	this.trackBarVolume = new System.Windows.Forms.TrackBar();
        	this.timer1 = new System.Windows.Forms.Timer(this.components);
        	this.button6 = new System.Windows.Forms.Button();
        	this.button5 = new System.Windows.Forms.Button();
        	this.pictureBox1 = new System.Windows.Forms.PictureBox();
        	this.button4 = new System.Windows.Forms.Button();
        	this.button3 = new System.Windows.Forms.Button();
        	this.button2 = new System.Windows.Forms.Button();
        	this.button1 = new System.Windows.Forms.Button();
        	((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
        	((System.ComponentModel.ISupportInitialize)(this.trackBarVolume)).BeginInit();
        	((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
        	this.SuspendLayout();
        	// 
        	// trackBar1
        	// 
        	this.trackBar1.Location = new System.Drawing.Point(19, 237);
        	this.trackBar1.Maximum = 1000;
        	this.trackBar1.Name = "trackBar1";
        	this.trackBar1.Size = new System.Drawing.Size(316, 45);
        	this.trackBar1.TabIndex = 4;
        	this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
        	this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
        	this.trackBar1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.trackBar1_MouseDown);
        	this.trackBar1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trackBar1_MouseUp);
        	// 
        	// tracklbl
        	// 
        	this.tracklbl.AutoSize = true;
        	this.tracklbl.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        	this.tracklbl.Location = new System.Drawing.Point(198, 12);
        	this.tracklbl.MaximumSize = new System.Drawing.Size(241, 0);
        	this.tracklbl.Name = "tracklbl";
        	this.tracklbl.Size = new System.Drawing.Size(87, 22);
        	this.tracklbl.TabIndex = 6;
        	this.tracklbl.Text = "No track";
        	// 
        	// albumlbl
        	// 
        	this.albumlbl.AutoSize = true;
        	this.albumlbl.Location = new System.Drawing.Point(199, 111);
        	this.albumlbl.MaximumSize = new System.Drawing.Size(240, 0);
        	this.albumlbl.Name = "albumlbl";
        	this.albumlbl.Size = new System.Drawing.Size(36, 13);
        	this.albumlbl.TabIndex = 7;
        	this.albumlbl.Text = "Album";
        	// 
        	// artistlbl
        	// 
        	this.artistlbl.AutoSize = true;
        	this.artistlbl.Location = new System.Drawing.Point(199, 86);
        	this.artistlbl.MaximumSize = new System.Drawing.Size(240, 0);
        	this.artistlbl.Name = "artistlbl";
        	this.artistlbl.Size = new System.Drawing.Size(30, 13);
        	this.artistlbl.TabIndex = 8;
        	this.artistlbl.Text = "Artist";
        	// 
        	// yearlbl
        	// 
        	this.yearlbl.AutoSize = true;
        	this.yearlbl.Location = new System.Drawing.Point(199, 169);
        	this.yearlbl.MaximumSize = new System.Drawing.Size(240, 0);
        	this.yearlbl.Name = "yearlbl";
        	this.yearlbl.Size = new System.Drawing.Size(29, 13);
        	this.yearlbl.TabIndex = 9;
        	this.yearlbl.Text = "Year";
        	this.yearlbl.Click += new System.EventHandler(this.YearlblClick);
        	// 
        	// lblPosition
        	// 
        	this.lblPosition.AutoSize = true;
        	this.lblPosition.Location = new System.Drawing.Point(16, 269);
        	this.lblPosition.Name = "lblPosition";
        	this.lblPosition.Size = new System.Drawing.Size(66, 13);
        	this.lblPosition.TabIndex = 10;
        	this.lblPosition.Text = "00:00/00:00";
        	// 
        	// comboBox1
        	// 
        	this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        	this.comboBox1.FormattingEnabled = true;
        	this.comboBox1.Location = new System.Drawing.Point(12, 298);
        	this.comboBox1.Name = "comboBox1";
        	this.comboBox1.Size = new System.Drawing.Size(323, 21);
        	this.comboBox1.TabIndex = 11;
        	// 
        	// trackBarVolume
        	// 
        	this.trackBarVolume.Location = new System.Drawing.Point(12, 331);
        	this.trackBarVolume.Maximum = 100;
        	this.trackBarVolume.Name = "trackBarVolume";
        	this.trackBarVolume.Size = new System.Drawing.Size(188, 45);
        	this.trackBarVolume.TabIndex = 12;
        	this.trackBarVolume.TickStyle = System.Windows.Forms.TickStyle.Both;
        	this.trackBarVolume.ValueChanged += new System.EventHandler(this.trackbarVolume_ValueChanged);
        	// 
        	// timer1
        	// 
        	this.timer1.Enabled = true;
        	this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
        	// 
        	// button6
        	// 
        	this.button6.Image = global::audiopuke.Properties.Resources.PreviousFrame_16x;
        	this.button6.Location = new System.Drawing.Point(17, 208);
        	this.button6.Name = "button6";
        	this.button6.Size = new System.Drawing.Size(75, 23);
        	this.button6.TabIndex = 14;
        	this.button6.UseVisualStyleBackColor = true;
        	this.button6.Click += new System.EventHandler(this.button6_Click);
        	// 
        	// button5
        	// 
        	this.button5.Image = global::audiopuke.Properties.Resources.NextFrameArrow_16x;
        	this.button5.Location = new System.Drawing.Point(341, 208);
        	this.button5.Name = "button5";
        	this.button5.Size = new System.Drawing.Size(75, 23);
        	this.button5.TabIndex = 13;
        	this.button5.UseVisualStyleBackColor = true;
        	this.button5.Click += new System.EventHandler(this.button5_Click);
        	// 
        	// pictureBox1
        	// 
        	this.pictureBox1.Location = new System.Drawing.Point(12, 12);
        	this.pictureBox1.Name = "pictureBox1";
        	this.pictureBox1.Size = new System.Drawing.Size(170, 170);
        	this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
        	this.pictureBox1.TabIndex = 5;
        	this.pictureBox1.TabStop = false;
        	// 
        	// button4
        	// 
        	this.button4.Image = global::audiopuke.Properties.Resources.Stop_grey_16x;
        	this.button4.Location = new System.Drawing.Point(260, 208);
        	this.button4.Name = "button4";
        	this.button4.Size = new System.Drawing.Size(75, 23);
        	this.button4.TabIndex = 3;
        	this.button4.UseVisualStyleBackColor = true;
        	this.button4.Click += new System.EventHandler(this.button4_Click);
        	// 
        	// button3
        	// 
        	this.button3.Image = global::audiopuke.Properties.Resources.PlayVideo_16x;
        	this.button3.Location = new System.Drawing.Point(179, 208);
        	this.button3.Name = "button3";
        	this.button3.Size = new System.Drawing.Size(75, 23);
        	this.button3.TabIndex = 2;
        	this.button3.UseVisualStyleBackColor = true;
        	this.button3.Click += new System.EventHandler(this.button3_Click);
        	// 
        	// button2
        	// 
        	this.button2.Image = global::audiopuke.Properties.Resources.Pause_16x;
        	this.button2.Location = new System.Drawing.Point(98, 208);
        	this.button2.Name = "button2";
        	this.button2.Size = new System.Drawing.Size(75, 23);
        	this.button2.TabIndex = 1;
        	this.button2.UseVisualStyleBackColor = true;
        	this.button2.Click += new System.EventHandler(this.button2_Click);
        	// 
        	// button1
        	// 
        	this.button1.Image = global::audiopuke.Properties.Resources.Open_16x;
        	this.button1.Location = new System.Drawing.Point(341, 259);
        	this.button1.Name = "button1";
        	this.button1.Size = new System.Drawing.Size(75, 23);
        	this.button1.TabIndex = 0;
        	this.button1.UseVisualStyleBackColor = true;
        	this.button1.Click += new System.EventHandler(this.button1_Click);
        	// 
        	// Form1
        	// 
        	this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        	this.ClientSize = new System.Drawing.Size(441, 388);
        	this.Controls.Add(this.button6);
        	this.Controls.Add(this.button5);
        	this.Controls.Add(this.trackBarVolume);
        	this.Controls.Add(this.comboBox1);
        	this.Controls.Add(this.lblPosition);
        	this.Controls.Add(this.yearlbl);
        	this.Controls.Add(this.artistlbl);
        	this.Controls.Add(this.albumlbl);
        	this.Controls.Add(this.tracklbl);
        	this.Controls.Add(this.pictureBox1);
        	this.Controls.Add(this.trackBar1);
        	this.Controls.Add(this.button4);
        	this.Controls.Add(this.button3);
        	this.Controls.Add(this.button2);
        	this.Controls.Add(this.button1);
        	this.MaximizeBox = false;
        	this.MaximumSize = new System.Drawing.Size(457, 427);
        	this.MinimumSize = new System.Drawing.Size(457, 427);
        	this.Name = "Form1";
        	this.Text = "Audiopuke";
        	this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
        	this.Load += new System.EventHandler(this.Form1_Load);
        	((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
        	((System.ComponentModel.ISupportInitialize)(this.trackBarVolume)).EndInit();
        	((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
        	this.ResumeLayout(false);
        	this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label tracklbl;
        private System.Windows.Forms.Label albumlbl;
        private System.Windows.Forms.Label artistlbl;
        private System.Windows.Forms.Label yearlbl;
        private System.Windows.Forms.Label lblPosition;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TrackBar trackBarVolume;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
    }
}

