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
            player = new PictureBox();
            GameTImer = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)player).BeginInit();
            SuspendLayout();
            // 
            // txtammo
            // 
            txtammo.AutoSize = true;
            txtammo.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            txtammo.ForeColor = Color.White;
            txtammo.Location = new Point(9, 9);
            txtammo.Name = "txtammo";
            txtammo.Size = new Size(92, 25);
            txtammo.TabIndex = 0;
            txtammo.Text = "Ammo: 0";
            // 
            // txtscore
            // 
            txtscore.AutoSize = true;
            txtscore.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            txtscore.ForeColor = Color.White;
            txtscore.Location = new Point(367, 9);
            txtscore.Name = "txtscore";
            txtscore.Size = new Size(68, 25);
            txtscore.TabIndex = 1;
            txtscore.Text = "Kills: 0";
            // 
            // txthealth
            // 
            txthealth.AutoSize = true;
            txthealth.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            txthealth.ForeColor = Color.White;
            txthealth.Location = new Point(664, 9);
            txthealth.Name = "txthealth";
            txthealth.Size = new Size(70, 25);
            txthealth.TabIndex = 2;
            txthealth.Text = "Health";
            // 
            // healthBar
            // 
            healthBar.Location = new Point(743, 14);
            healthBar.Name = "healthBar";
            healthBar.Size = new Size(169, 23);
            healthBar.TabIndex = 3;
            healthBar.Value = 100;
            // 
            // player
            // 
            player.Image = Properties.Resources.up;
            player.Location = new Point(414, 287);
            player.Name = "player";
            player.Size = new Size(71, 100);
            player.SizeMode = PictureBoxSizeMode.AutoSize;
            player.TabIndex = 4;
            player.TabStop = false;
            // 
            // GameTImer
            // 
            GameTImer.Enabled = true;
            GameTImer.Interval = 20;
            GameTImer.Tick += MainTimerEvent;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(924, 661);
            Controls.Add(player);
            Controls.Add(healthBar);
            Controls.Add(txthealth);
            Controls.Add(txtscore);
            Controls.Add(txtammo);
            Name = "Form1";
            Text = "Form1";
            KeyDown += KeyIsDown;
            KeyUp += KeyIsUp;
            ((System.ComponentModel.ISupportInitialize)player).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label txtammo;
        private Label txtscore;
        private Label txthealth;
        private ProgressBar healthBar;
        private PictureBox player;
        private System.Windows.Forms.Timer GameTImer;
    }
}