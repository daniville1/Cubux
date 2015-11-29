using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cubux.Objects
{
    class Rectangle
    {
        public List<double[]> points = new List<double[]>();
        public double[,] matrix = Engine.FactoryMatrix.getIdentity();

        public Rectangle()
        {
            points.Add(new double[4]);
            points.Add(new double[4]);
            points.Add(new double[4]);
            points.Add(new double[4]);

            points[0][3] = 1;
            points[1][3] = 1;
            points[2][3] = 1;
            points[3][3] = 1;
        }
    }
}

