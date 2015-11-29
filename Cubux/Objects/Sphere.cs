using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Cubux.Objects
{
    class Sphere
    {
        public double radio;
        public double[] position = { 0, 0, 0 };
        public Color color;

        public Sphere(double radio, Color color, double positionX, double positionY, double positionZ)
        {
            this.radio = radio;
            this.color = color;
            position[0] = positionX;
            position[1] = positionY;
            position[2] = positionZ;
        }
    }
}
