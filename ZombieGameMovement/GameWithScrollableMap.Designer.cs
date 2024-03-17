namespace ZombieGameMovement
{
    partial class GameWithScrollableMap
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameWithScrollableMap));
            GameTImer = new System.Windows.Forms.Timer(components);
            txtammo = new Label();
            txtscore = new Label();
            txthealth = new Label();
            healthBar = new ProgressBar();
            map = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)map).BeginInit();
            SuspendLayout();
            // 
            // GameTImer
            // 
            GameTImer.Enabled = true;
            GameTImer.Interval = 20;
            GameTImer.Tick += MainTimerEvent;
            // 
            // txtammo
            // 
            txtammo.AutoSize = true;
            txtammo.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold);
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
            txtscore.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold);
            txtscore.ForeColor = Color.White;
            txtscore.Location = new Point(403, 8);
            txtscore.Name = "txtscore";
            txtscore.Size = new Size(68, 25);
            txtscore.TabIndex = 1;
            txtscore.Text = "Kills: 0";
            // 
            // txthealth
            // 
            txthealth.AutoSize = true;
            txthealth.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold);
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
            // map
            // 
            map.BackColor = Color.Transparent;
            map.Image = (Image)resources.GetObject("map.Image");
            map.Location = new Point(-3, 42);
            map.Margin = new Padding(3, 2, 3, 2);
            map.Name = "map";
            map.Size = new Size(7680, 4320);
            map.SizeMode = PictureBoxSizeMode.AutoSize;
            map.TabIndex = 4;
            map.TabStop = false;
            map.Paint += CharacterPaintEvent;
            // 
            // GameWithScrollableMap
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(1904, 1041);
            Controls.Add(healthBar);
            Controls.Add(txthealth);
            Controls.Add(txtscore);
            Controls.Add(txtammo);
            Controls.Add(map);
            Name = "GameWithScrollableMap";
            Text = "Form1";
            KeyDown += KeyIsDown;
            KeyUp += KeyIsUp;
            ((System.ComponentModel.ISupportInitialize)map).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Timer GameTImer;
        private Label txtammo;
        private Label txtscore;
        private Label txthealth;
        private ProgressBar healthBar;
        private PictureBox map;
    }
}