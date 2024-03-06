namespace ZombieGameMovement
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
            components = new System.ComponentModel.Container();
            txtammo = new Label();
            txtscore = new Label();
            txthealth = new Label();
            healthBar = new ProgressBar();
            GameTImer = new System.Windows.Forms.Timer(components);
            map = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)map).BeginInit();
            SuspendLayout();
            // 
            // txtammo
            // 
            txtammo.AutoSize = true;
            txtammo.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            txtammo.ForeColor = Color.White;
            txtammo.Location = new Point(10, 12);
            txtammo.Name = "txtammo";
            txtammo.Size = new Size(118, 32);
            txtammo.TabIndex = 0;
            txtammo.Text = "Ammo: 0";
            // 
            // txtscore
            // 
            txtscore.AutoSize = true;
            txtscore.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            txtscore.ForeColor = Color.White;
            txtscore.Location = new Point(461, 11);
            txtscore.Name = "txtscore";
            txtscore.Size = new Size(90, 32);
            txtscore.TabIndex = 1;
            txtscore.Text = "Kills: 0";
            // 
            // txthealth
            // 
            txthealth.AutoSize = true;
            txthealth.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            txthealth.ForeColor = Color.White;
            txthealth.Location = new Point(759, 12);
            txthealth.Name = "txthealth";
            txthealth.Size = new Size(88, 32);
            txthealth.TabIndex = 2;
            txthealth.Text = "Health";
            // 
            // healthBar
            // 
            healthBar.Location = new Point(849, 19);
            healthBar.Margin = new Padding(3, 4, 3, 4);
            healthBar.Name = "healthBar";
            healthBar.Size = new Size(193, 31);
            healthBar.TabIndex = 3;
            healthBar.Value = 100;
            // 
            // GameTImer
            // 
            GameTImer.Enabled = true;
            GameTImer.Interval = 20;
            GameTImer.Tick += MainTimerEvent;
            // 
            // map
            // 
            map.BackColor = Color.Transparent;
            map.Image = Properties.Resources.TestMap1;
            map.Location = new Point(14, 56);
            map.Name = "map";
            map.Size = new Size(5714, 3749);
            map.TabIndex = 4;
            map.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(1054, 879);
            Controls.Add(healthBar);
            Controls.Add(txthealth);
            Controls.Add(txtscore);
            Controls.Add(txtammo);
            Controls.Add(map);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            Text = "Form1";
            KeyDown += KeyIsDown;
            KeyUp += KeyIsUp;
            ((System.ComponentModel.ISupportInitialize)map).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label txtammo;
        private Label txtscore;
        private Label txthealth;
        private ProgressBar healthBar;
        private System.Windows.Forms.Timer GameTImer;
        private PictureBox map;
    }
}