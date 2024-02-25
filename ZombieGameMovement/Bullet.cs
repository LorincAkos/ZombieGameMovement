﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using System.Drawing;
using System.Windows.Forms;

namespace ZombieGameMovement
{
    internal class Bullet
    {
        public string direction;
        public int bulletLeft;
        public int bulletTop;

        private int speed = 20;
        private PictureBox bullet = new();
        private System.Windows.Forms.Timer bulletTimer = new();

        public void MakeBullet(Form form)
        {
            bullet.BackColor = Color.White;
            bullet.Size = new Size(5, 5);
            bullet.Tag = "bullet";
            bullet.Left = bulletLeft;
            bullet.Top = bulletTop;
            bullet.BringToFront();

            form.Controls.Add(bullet);

            bulletTimer.Interval = speed;
            bulletTimer.Tick += new EventHandler(BulletTimerEvent);
            bulletTimer.Start();
        }

        private void BulletTimerEvent(object? sender, EventArgs e)
        {
            if (direction.Equals("left"))
            {
                bullet.Left -= speed;
            }

            if (direction.Equals("right"))
            {
                bullet.Left += speed;
            }

            if (direction.Equals("up")) 
            {
                bullet.Top -= speed;
            }

            if(direction.Equals("down"))
            {
                bullet.Top += speed;
            }

            if(bullet.Left < 10 || bullet.Left > 860 || bullet.Top < 10 || bullet.Top > 600)
            {
                bulletTimer.Stop();
                bulletTimer.Dispose();
                bullet.Dispose();
                bulletTimer = null;
                bullet = null;
            }
        }
    }
}