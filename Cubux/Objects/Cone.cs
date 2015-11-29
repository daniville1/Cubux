using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Cubux.Objects
{
    class Cone
    {
        public double radio;
        public double height;
        public double[] position = {0, 0, 0};
        public Color color;

        public Cone(double radio, double height, Color color, double positionX, double positionY, double positionZ)
        {
            this.radio = radio;
            this.height = height;
            this.color = color;
            position[0] = positionX;
            position[1] = positionY;
            position[2] = positionZ;
        }
    }
}
