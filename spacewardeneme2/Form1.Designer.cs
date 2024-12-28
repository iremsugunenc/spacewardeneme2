namespace spacewardeneme2
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            pictureBoxGalaxy = new PictureBox();
            label1 = new Label();
            SpaceShip = new PictureBox();
            label5 = new Label();
            label6 = new Label();
            pictureBox5 = new PictureBox();
            startButton = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBoxGalaxy).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SpaceShip).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            SuspendLayout();
            // 
            // pictureBoxGalaxy
            // 
            pictureBoxGalaxy.Dock = DockStyle.Fill;
            pictureBoxGalaxy.Image = (Image)resources.GetObject("pictureBoxGalaxy.Image");
            pictureBoxGalaxy.Location = new Point(0, 0);
            pictureBoxGalaxy.Name = "pictureBoxGalaxy";
            pictureBoxGalaxy.Size = new Size(1809, 714);
            pictureBoxGalaxy.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxGalaxy.TabIndex = 0;
            pictureBoxGalaxy.TabStop = false;
            // 
            // label1
            // 
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Red;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(207, 34);
            label1.TabIndex = 2;
            label1.Text = "Oyuncu Sağlık:100";
            // 
            // SpaceShip
            // 
            SpaceShip.BackColor = Color.Transparent;
            SpaceShip.Image = (Image)resources.GetObject("SpaceShip.Image");
            SpaceShip.Location = new Point(28, 195);
            SpaceShip.Name = "SpaceShip";
            SpaceShip.Size = new Size(86, 64);
            SpaceShip.SizeMode = PictureBoxSizeMode.StretchImage;
            SpaceShip.TabIndex = 1;
            SpaceShip.TabStop = false;
            // 
            // label5
            // 
            label5.BackColor = Color.Transparent;
            label5.ForeColor = Color.Red;
            label5.Location = new Point(1227, 9);
            label5.Name = "label5";
            label5.Size = new Size(199, 26);
            label5.TabIndex = 6;
            label5.Text = "240229098 İrem Su Günenç";
            // 
            // label6
            // 
            label6.BackColor = Color.Transparent;
            label6.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.Red;
            label6.Location = new Point(12, 43);
            label6.Name = "label6";
            label6.Size = new Size(207, 33);
            label6.TabIndex = 7;
            label6.Text = "Düşman Sayısı";
            // 
            // pictureBox5
            // 
            pictureBox5.BackColor = Color.Transparent;
            pictureBox5.Image = (Image)resources.GetObject("pictureBox5.Image");
            pictureBox5.Location = new Point(179, 219);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(40, 26);
            pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox5.TabIndex = 12;
            pictureBox5.TabStop = false;
            // 
            // startButton
            // 
            startButton.Location = new Point(505, 278);
            startButton.Name = "startButton";
            startButton.Size = new Size(291, 137);
            startButton.TabIndex = 13;
            startButton.Text = "Başlat";
            startButton.UseVisualStyleBackColor = true;
            startButton.Click += startButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1809, 714);
            Controls.Add(startButton);
            Controls.Add(pictureBox5);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(SpaceShip);
            Controls.Add(label1);
            Controls.Add(pictureBoxGalaxy);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            KeyDown += Form1_KeyDown;
            KeyUp += Form1_KeyUp;
            ((System.ComponentModel.ISupportInitialize)pictureBoxGalaxy).EndInit();
            ((System.ComponentModel.ISupportInitialize)SpaceShip).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBoxGalaxy;
        private Label label1;
        private PictureBox SpaceShip;
        private Label label5;
        private Label label6;
        private PictureBox pictureBox5;
        private Button startButton;  // Başlat butonu tanımlandı
    }
}
