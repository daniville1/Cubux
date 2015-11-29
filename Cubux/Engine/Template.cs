using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cubux.Engine
{
    class Template
    {
        public static Objects.Plane levelCompleted(int level)
        {
            switch (level)
            {
                case -2: return plane(@"Img\Templates\startGame.jpg");
                case -1: return plane(@"Img\Templates\instructionsGame.jpg");
                case 1: return plane(@"Img\Templates\levelCompleted1.jpg");
                case 2: return plane(@"Img\Templates\levelCompleted2.jpg");
                case 3: return plane(@"Img\Templates\levelCompleted3.jpg");
                case 4: return plane(@"Img\Templates\levelCompleted4.jpg");
                case 5: return plane(@"Img\Templates\completeGame.jpg");
                case 6: return plane(@"Img\Templates\credits.jpg");
            }
            return null;
        }

        private static Objects.Plane plane(string textureName)
        {
            Objects.Plane p = new Objects.Plane();
            Objects.Rectangle r = new Objects.Rectangle();

            p.textureName = textureName;
            p.repeat = false;

            r.points[0][0] = 10;
            r.points[0][1] = 10;
            r.points[0][2] = 0;

            r.points[1][0] = -10;
            r.points[1][1] = 10;
            r.points[1][2] = 0;

            r.points[2][0] = -10;
            r.points[2][1] = -10;
            r.points[2][2] = 0;

            r.points[3][0] = 10;
            r.points[3][1] = -10;
            r.points[3][2] = 0;
            p.rectangles.Add(r);

            return p;
        }
    }
}
