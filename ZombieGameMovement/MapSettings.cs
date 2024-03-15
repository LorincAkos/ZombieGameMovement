using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieGameMovement
{
    internal class MapSettings
    {
        public int MapSpeed {  get; set; }
        public int MapStartOnXAxis {  get; set; }
        public int MapStartOnYAxis {  get; set; }
        public int MapEndOnXAxis {  get; set; }
        public int MapEndOnYAxis {  get; set; }
        public int MapStartOnXAxisCriticalZone {  get; set; }
        public int MapStartOnYAxisCriticalZone {  get; set; }
        public int MapEndOnXAxisCriticalZone { get; set; }
        public int MapEndOnYAxisCriticalZone { get; set; }

        public MapSettings(int mapSpeed, int formHeight, int formWidth, int mapHeight, int mapWidth)
        {
            MapSpeed = mapSpeed;
            MapStartOnXAxis = 0;
            MapStartOnYAxis = 0;
            MapEndOnXAxis = (mapWidth - formWidth) * -1;
            MapEndOnYAxis = (mapHeight - formHeight) * -1;

            MapStartOnXAxisCriticalZone = MapStartOnXAxis - formWidth / 2 * -1;
            MapStartOnYAxisCriticalZone = MapStartOnYAxis - formHeight / 2 * -1;
            MapEndOnXAxisCriticalZone = (mapWidth - formWidth / 2);
            MapEndOnYAxisCriticalZone = (mapHeight - formHeight / 2);
        }
    }
}
