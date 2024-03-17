using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Audio;

namespace ZombieGameMovement
{
    public static class RandNumGenerator
    {
        public static Random random = new();

        public static void GetZomCoordinate(out int x, out int y)
        {
            int tmp = random.Next(0,4);

            switch (tmp)
            {
                case 0:
                    x = 0;
                    y = random.Next(0,1920);
                    break;
                case 1:
                    x = 1080;
                    y = random.Next(0, 1920);
                    break;
                case 2:
                    x = random.Next(0, 1080);
                    y = 0;
                    break;
                case 3:
                    x = random.Next(0,1080);
                    y = 1920;
                    break;
                default:
                    x = 0;
                    y = 0;
                    break;
            }

        }
    }
}
