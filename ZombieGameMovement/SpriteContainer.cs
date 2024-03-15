﻿using System.Reflection;

namespace ZombieGameMovement
{
    static internal class SpriteContainer
    {
        static public readonly Image[] walkRight =
        {
            GetImageFromExecutingAssembly("Kuno_walk1.png"),
            GetImageFromExecutingAssembly("Kuno_walk2.png"),
            GetImageFromExecutingAssembly("Kuno_walk3.png"),
            GetImageFromExecutingAssembly("Kuno_walk4.png"),
        };

        public static Image GetImageFromExecutingAssembly(string fileName)
        {
            using (Stream? stream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"ZombieGameMovement.Sprites.{fileName}"))
            {
                if (stream is not null)
                {
                    return Image.FromStream(stream);
                }
                else
                {
                    throw new Exception();
                }
            }
        }
    }
}
