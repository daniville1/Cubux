using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Cubux.Engine
{
    class Maps
    {
        public double[] position = new double[4];
        public double[] view = new double[4];
        public string path;
        public List<Objects.Plane> planes = new List<Objects.Plane>();
        public List<Objects.Sphere> spheres = new List<Objects.Sphere>();
        public List<Objects.Cone> cones = new List<Objects.Cone>();
        public List<Engine.Collision> collisions = new List<Engine.Collision>();

        public Maps(int level)
        {
            switch (level)
            {
                case 1:
                    path = @"Img\Texture\Level1\";
                    environment();
                    room(true);
                    level1();
                    break;
                case 2:
                    path = @"Img\Texture\Level2\";
                    environment();
                    room(false);
                    level2();
                    break;
                case 3:
                    path = @"Img\Texture\Level3\";
                    environment();
                    room(true);
                    level3();
                    break;
                case 4:
                    path = @"Img\Texture\Level4\";
                    environment();
                    room(false);
                    level4();
                    break;
            }
        }

        public void environment()
        {
            double height = 2000;
            double width = 5000;
            Objects.Plane p;
            Objects.Rectangle r;

            p = new Objects.Plane();
            p.textureName = path + "panoramic1.jpg";
            r = new Objects.Rectangle();
            r.points[0][0] = width;
            r.points[0][1] = height;
            r.points[0][2] = -width;
            r.points[1][0] = -width;
            r.points[1][1] = height;
            r.points[1][2] = -width;
            r.points[2][0] = -width;
            r.points[2][1] = -1;
            r.points[2][2] = -width;
            r.points[3][0] = width;
            r.points[3][1] = -1;
            r.points[3][2] = -width;
            p.rectangles.Add(r);
            p.rotate = true;
            planes.Add(p);

            p = new Objects.Plane();
            p.textureName = path + "panoramic2.jpg";
            r = new Objects.Rectangle();
            r.points[0][0] = width;
            r.points[0][1] = height;
            r.points[0][2] = -width;
            r.points[1][0] = width;
            r.points[1][1] = height;
            r.points[1][2] = width;
            r.points[2][0] = width;
            r.points[2][1] = -1;
            r.points[2][2] = width;
            r.points[3][0] = width;
            r.points[3][1] = -1;
            r.points[3][2] = -width;
            p.rectangles.Add(r);
            planes.Add(p);

            p = new Objects.Plane();
            p.textureName = path + "panoramic3.jpg";
            r = new Objects.Rectangle();
            r.points[0][0] = -width;
            r.points[0][1] = height;
            r.points[0][2] = width;
            r.points[1][0] = width;
            r.points[1][1] = height;
            r.points[1][2] = width;
            r.points[2][0] = width;
            r.points[2][1] = -1;
            r.points[2][2] = width;
            r.points[3][0] = -width;
            r.points[3][1] = -1;
            r.points[3][2] = width;
            p.rectangles.Add(r);
            p.rotate = true;
            planes.Add(p);

            p = new Objects.Plane();
            p.textureName = path + "panoramic4.jpg";
            r = new Objects.Rectangle();
            r.points[0][0] = -width;
            r.points[0][1] = height;
            r.points[0][2] = width;
            r.points[1][0] = -width;
            r.points[1][1] = height;
            r.points[1][2] = -width;
            r.points[2][0] = -width;
            r.points[2][1] = -1;
            r.points[2][2] = -width;
            r.points[3][0] = -width;
            r.points[3][1] = -1;
            r.points[3][2] = width;
            p.rectangles.Add(r);
            planes.Add(p);

            p = new Objects.Plane();
            p.textureName = path + "panoramic5.jpg";
            r = new Objects.Rectangle();
            r.points[0][0] = -width;
            r.points[0][1] = height;
            r.points[0][2] = width;
            r.points[1][0] = width;
            r.points[1][1] = height;
            r.points[1][2] = width;
            r.points[2][0] = width;
            r.points[2][1] = height;
            r.points[2][2] = -width;
            r.points[3][0] = -width;
            r.points[3][1] = height;
            r.points[3][2] = -width;
            p.rectangles.Add(r);
            p.rotate = true;
            planes.Add(p);

            p = new Objects.Plane();
            p.textureName = path + "panoramic6.jpg";
            r = new Objects.Rectangle();
            r.points[0][0] = width;
            r.points[0][1] = -1;
            r.points[0][2] = width;
            r.points[1][0] = width;
            r.points[1][1] = -1;
            r.points[1][2] = -width;
            r.points[2][0] = -width;
            r.points[2][1] = -1;
            r.points[2][2] = -width;
            r.points[3][0] = -width;
            r.points[3][1] = -1;
            r.points[3][2] = width;
            p.rectangles.Add(r);
            planes.Add(p);
        }

        public void room(bool isRoof)
        {
            double width = 100;
            double height = 12;
            double quality = 2;
            Objects.Plane p;
            Objects.Rectangle r;

            p = new Objects.Plane();
            p.textureName = path + "soil.jpg";
            for (double i = 0; i < width; i += quality)
            {
                for (double j = 0; j < width; j += quality)
                {
                    r = new Objects.Rectangle();

                    r.points[0][0] = j;
                    r.points[0][2] = i;
                    r.points[1][0] = j;
                    r.points[1][2] = i + quality;
                    r.points[2][0] = j + quality;
                    r.points[2][2] = i + quality;
                    r.points[3][0] = j + quality;
                    r.points[3][2] = i;
                    p.rectangles.Add(r);
                }
            }
            planes.Add(p);

            if (isRoof)
            {
                p = new Objects.Plane();
                p.textureName = path + "roof.jpg";
                for (double i = 0; i < width; i += quality)
                {
                    for (double j = 0; j < width; j += quality)
                    {
                        r = new Objects.Rectangle();

                        r.points[0][0] = j;
                        r.points[0][1] = height;
                        r.points[0][2] = i;
                        r.points[1][0] = j;
                        r.points[1][1] = height;
                        r.points[1][2] = i + quality;
                        r.points[2][0] = j + quality;
                        r.points[2][1] = height;
                        r.points[2][2] = i + quality;
                        r.points[3][0] = j + quality;
                        r.points[3][1] = height;
                        r.points[3][2] = i;
                        p.rectangles.Add(r);
                    }
                }
                planes.Add(p);
            }

            wallX(0, width, 0, height, path + "wall1.jpg", quality, false, false);
            wallX(0, width, width, height, path + "wall2.jpg", quality, false, false);
            wallZ(0, width, 0, height, path + "wall3.jpg", quality, false, false);
            wallZ(0, width, width, height, path + "wall4.jpg", quality, false, false);
        }

        public void level1()
        {
            double height = 12;
            double quality = 2;
            string textureName = path + "wall1.jpg";

            // Posicion inicial del personaje
            position[0] = 1;
            position[1] = 4;
            position[2] = 1;
            view[0] = 100;
            view[1] = 4;
            view[2] = 100;
            //--


            // Construccion de paredes X
            wallX(10, 90, 10, height, textureName, quality, true, true);
            wallX(20, 80, 20, height, textureName, quality, true, true);
            wallX(30, 70, 30, height, textureName, quality, true, true);
            wallX(30, 80, 40, height, textureName, quality, true, true);
            wallX(20, 40, 50, height, textureName, quality, true, true);
            wallX(50, 80, 50, height, textureName, quality, true, true);
            wallX(30, 40, 70, height, textureName, quality, true, true);
            wallX(50, 80, 70, height, textureName, quality, true, true);
            wallX(30, 90, 80, height, textureName, quality, true, true);
            wallX(20, 80, 90, height, textureName, quality, true, true);
            wallX(90, 100, 90, height, textureName, quality, true, true);
            //--


            // Construccion de paredes Z
            wallZ(10, 90, 10, height, textureName, quality, true, true);
            wallZ(20, 90, 20, height, textureName, quality, true, true);
            wallZ(30, 40, 30, height, textureName, quality, true, true);
            wallZ(60, 70, 30, height, textureName, quality, true, true);
            wallZ(50, 70, 40, height, textureName, quality, true, true);
            wallZ(50, 70, 50, height, textureName, quality, true, true);
            wallZ(20, 40, 80, height, textureName, quality, true, true);
            wallZ(50, 80, 80, height, textureName, quality, true, true);
            wallZ(90, 100, 80, height, textureName, quality, true, true);
            wallZ(10, 90, 90, height, textureName, quality, true, true);
            //--


            // Construccion de esferas
            sphere(3, Color.Green, 50, 0, 5);
            sphere(3, Color.Gainsboro, 40, 0, 15);
            sphere(1, Color.HotPink, 70, 5, 25);
            sphere(1, Color.Yellow, 95, 1, 55);
            sphere(3, Color.Red, 5, 0, 50);
            sphere(2, Color.Blue, 30, 3, 55);
            sphere(3, Color.YellowGreen, 85, 2, 70);
            sphere(2, Color.PeachPuff, 50, 5, 85);
            sphere(1, Color.Gold, 70, 5, 95);
            //--


            // Meta final
            cone(4, 5, Color.Blue, 95, 5, 95);
            //--
        }

        public void level2()
        {
            double height = 12;
            double quality = 2;
            string textureName = path + "wall2.jpg";

            // Posicion inicial del personaje
            position[0] = 1;
            position[1] = 4;
            position[2] = 1;
            view[0] = 100;
            view[1] = 4;
            view[2] = 100;
            //--


            // Construccion de paredes X
            wallX(30, 50, 10, height, textureName, quality, true, true);
            wallX(80, 90, 20, height, textureName, quality, true, true);
            wallX(10, 20, 30, height, textureName, quality, true, true);
            wallX(0, 30, 40, height, textureName, quality, true, true);
            wallX(30, 50, 50, height, textureName, quality, true, true);
            wallX(10, 70, 60, height, textureName, quality, true, true);
            wallX(10, 80, 70, height, textureName, quality, true, true);
            wallX(0, 90, 80, height, textureName, quality, true, true);
            wallX(10, 100, 90, height, textureName, quality, true, true);
            //--


            // Construccion de paredes Z
            wallZ(0, 30, 10, height, textureName, quality, true, true);
            wallZ(60, 70, 10, height, textureName, quality, true, true);
            wallZ(0, 20, 20, height, textureName, quality, true, true);
            wallZ(10, 50, 30, height, textureName, quality, true, true);
            wallZ(10, 50, 50, height, textureName, quality, true, true);
            wallZ(0, 40, 60, height, textureName, quality, true, true);
            wallZ(10, 60, 70, height, textureName, quality, true, true);
            wallZ(20, 70, 80, height, textureName, quality, true, true);
            wallZ(20, 80, 90, height, textureName, quality, true, true);
            //--


            // Construccion de esferas
            sphere(3, Color.Blue, 15, 4, 5);
            sphere(3, Color.Pink, 45, 2, 5);
            sphere(4, Color.Red, 85, 5, 10);
            sphere(2, Color.RosyBrown, 85, 1, 25);
            sphere(5, Color.SeaGreen, 20, 0, 55);
            sphere(1, Color.Silver, 95, 0, 15);
            sphere(2, Color.SlateBlue, 15, 0, 65);
            sphere(3, Color.Tan, 5, 2, 85);
            sphere(3, Color.Thistle, 55, 0, 85);
            sphere(1, Color.Turquoise, 5, 1, 95);
            //--


            // Meta final
            cone(4, 5, Color.Blue, 95, 5, 95);
            //--
        }

        public void level3()
        {
            double height = 12;
            double quality = 2;
            string textureName = path + "wall3.jpg";

            // Posicion inicial del personaje
            position[0] = 95;
            position[1] = 4;
            position[2] = 5;
            view[0] = 100;
            view[1] = 4;
            view[2] = 100;
            //--


            // Construccion de paredes X
            wallX(0, 30, 10, height, textureName, quality, true, true);
            wallX(60, 80, 10, height, textureName, quality, true, true);
            wallX(20, 30, 20, height, textureName, quality, true, true);
            wallX(0, 20, 60, height, textureName, quality, true, true);
            wallX(50, 70, 70, height, textureName, quality, true, true);
            wallX(30, 80, 80, height, textureName, quality, true, true);
            wallX(0, 70, 90, height, textureName, quality, true, true);
            wallX(80, 100, 90, height, textureName, quality, true, true);
            //-


            // Construccion de paredes Z
            wallZ(20, 40, 20, height, textureName, quality, true, true);
            wallZ(20, 80, 30, height, textureName, quality, true, true);
            wallZ(0, 60, 40, height, textureName, quality, true, true);
            wallZ(0, 70, 50, height, textureName, quality, true, true);
            wallZ(10, 50, 60, height, textureName, quality, true, true);
            wallZ(20, 70, 70, height, textureName, quality, true, true);
            wallZ(10, 90, 80, height, textureName, quality, true, true);
            wallZ(0, 80, 90, height, textureName, quality, true, true);
            //--


            // Construccion de esferas
            sphere(3, Color.Navy, 15, 0, 5);
            sphere(3, Color.Navy, 25, 0, 5);
            sphere(3, Color.Navy, 35, 0, 5);
            sphere(3, Color.Navy, 85, 0, 5);

            sphere(3, Color.MintCream, 10, 0, 15);
            sphere(3, Color.MintCream, 35, 0, 15);
            sphere(3, Color.MintCream, 45, 0, 15);
            sphere(3, Color.MintCream, 55, 0, 15);
            sphere(3, Color.MintCream, 75, 0, 15);

            sphere(3, Color.LightGray, 65, 0, 25);
            sphere(3, Color.LightGray, 85, 0, 25);
            sphere(3, Color.LightGray, 95, 0, 25);

            sphere(3, Color.Indigo, 5, 0, 35);
            sphere(3, Color.Indigo, 25, 0, 35);
            sphere(3, Color.Indigo, 35, 0, 35);
            sphere(3, Color.Indigo, 45, 0, 35);
            sphere(3, Color.Indigo, 55, 0, 35);
            sphere(3, Color.Indigo, 75, 0, 35);
            sphere(3, Color.Indigo, 85, 0, 35);


            sphere(3, Color.DeepPink, 65, 0, 45);
            sphere(3, Color.DeepPink, 95, 0, 45);

            sphere(3, Color.Cyan, 15, 0, 55);
            sphere(3, Color.Cyan, 35, 0, 55);
            sphere(3, Color.Cyan, 45, 0, 55);
            sphere(3, Color.Cyan, 55, 0, 55);
            sphere(3, Color.Cyan, 75, 0, 55);
            sphere(3, Color.Cyan, 85, 0, 55);

            sphere(3, Color.Coral, 25, 0, 65);
            sphere(3, Color.Coral, 95, 0, 65);

            sphere(3, Color.BurlyWood, 15, 0, 75);
            sphere(3, Color.BurlyWood, 35, 0, 75);
            sphere(3, Color.BurlyWood, 55, 0, 75);
            sphere(3, Color.BurlyWood, 75, 0, 75);
            sphere(3, Color.BurlyWood, 85, 0, 75);


            sphere(3, Color.Aqua, 15, 0, 85);
            sphere(3, Color.Aqua, 35, 0, 85);
            sphere(3, Color.Aqua, 55, 0, 85);
            sphere(3, Color.Aqua, 75, 0, 85);
            sphere(3, Color.Aqua, 95, 0, 85);

            sphere(2, Color.Green, 15, 0, 92);
            sphere(2, Color.Green, 20, 0, 95);
            sphere(2, Color.Green, 25, 0, 98);
            sphere(2, Color.Green, 35, 0, 92);
            sphere(2, Color.Green, 40, 0, 95);
            sphere(2, Color.Green, 45, 0, 98);
            sphere(3, Color.Green, 75, 0, 95);
            //--


            // Meta final
            cone(4, 5, Color.Blue, 5, 5, 95);
            //--
        }

        public void level4()
        {
            double height = 12;
            double quality = 2;
            string textureName = path + "wall3.jpg";

            // Posicion inicial del personaje
            position[0] = 98;
            position[1] = 4;
            position[2] = 5;
            view[0] = 100;
            view[1] = 4;
            view[2] = 100;
            //--


            // Construccion de paredes X
            wallX(0, 30, 10, height, textureName, quality, true, true);
            wallX(60, 100, 10, height, textureName, quality, true, true);
            wallX(0, 10, 20, height, textureName, quality, true, true);
            wallX(30, 90, 20, height, textureName, quality, true, true);
            wallX(40, 100, 30, height, textureName, quality, true, true);
            wallX(60, 80, 40, height, textureName, quality, true, true);
            wallX(20, 40, 60, height, textureName, quality, true, true);
            wallX(70, 90, 60, height, textureName, quality, true, true);
            wallX(10, 90, 70, height, textureName, quality, true, true);
            wallX(20, 50, 80, height, textureName, quality, true, true);
            wallX(70, 100, 80, height, textureName, quality, true, true);
            wallX(0, 20, 90, height, textureName, quality, true, true);
            wallX(60, 90, 90, height, textureName, quality, true, true);
            //-


            // Construccion de paredes Z
            wallZ(20, 70, 10, height, textureName, quality, true, true);
            wallZ(80, 90, 10, height, textureName, quality, true, true);
            wallZ(20, 60, 20, height, textureName, quality, true, true);
            wallZ(10, 50, 30, height, textureName, quality, true, true);
            wallZ(80, 100, 30, height, textureName, quality, true, true);
            wallZ(30, 60, 40, height, textureName, quality, true, true);
            wallZ(0, 20, 50, height, textureName, quality, true, true);
            wallZ(80, 100, 50, height, textureName, quality, true, true);
            wallZ(40, 90, 60, height, textureName, quality, true, true);
            wallZ(50, 60, 70, height, textureName, quality, true, true);
            wallZ(40, 50, 80, height, textureName, quality, true, true);
            wallZ(30, 60, 90, height, textureName, quality, true, true);
            //--


            // Construccion de esferas
            sphere(3, Color.Navy, 15, 0, 5);
            sphere(3, Color.Navy, 25, 0, 5);
            sphere(3, Color.Navy, 35, 0, 5);
            sphere(3, Color.Navy, 85, 0, 5);

            sphere(3, Color.MintCream, 10, 0, 15);
            sphere(3, Color.MintCream, 35, 0, 15);
            sphere(3, Color.MintCream, 45, 0, 15);
            sphere(3, Color.MintCream, 55, 0, 15);
            sphere(3, Color.MintCream, 75, 0, 15);

            sphere(3, Color.LightGray, 65, 0, 25);
            sphere(3, Color.LightGray, 85, 0, 25);
            sphere(3, Color.LightGray, 95, 0, 25);

            sphere(3, Color.Indigo, 5, 0, 35);
            sphere(3, Color.Indigo, 25, 0, 35);
            sphere(3, Color.Indigo, 35, 0, 35);
            sphere(3, Color.Indigo, 45, 0, 35);
            sphere(3, Color.Indigo, 55, 0, 35);
            sphere(3, Color.Indigo, 75, 0, 35);
            sphere(3, Color.Indigo, 85, 0, 35);


            sphere(3, Color.DeepPink, 65, 0, 45);
            sphere(3, Color.DeepPink, 95, 0, 45);

            sphere(3, Color.Cyan, 15, 0, 55);
            sphere(3, Color.Cyan, 35, 0, 55);
            sphere(3, Color.Cyan, 45, 0, 55);
            sphere(3, Color.Cyan, 55, 0, 55);
            sphere(3, Color.Cyan, 75, 0, 55);
            sphere(3, Color.Cyan, 85, 0, 55);

            sphere(3, Color.Coral, 25, 0, 65);
            sphere(3, Color.Coral, 95, 0, 65);

            sphere(3, Color.BurlyWood, 15, 0, 75);
            sphere(3, Color.BurlyWood, 35, 0, 75);
            sphere(3, Color.BurlyWood, 55, 0, 75);
            sphere(3, Color.BurlyWood, 75, 0, 75);
            sphere(3, Color.BurlyWood, 85, 0, 75);


            sphere(2, Color.Aqua, 15, 0, 83);
            sphere(2, Color.Aqua, 20, 0, 85);
            sphere(2, Color.Aqua, 25, 0, 87);
            sphere(3, Color.Aqua, 55, 0, 85);
            sphere(3, Color.Aqua, 75, 0, 85);
            sphere(3, Color.Aqua, 95, 0, 85);

            sphere(2, Color.Green, 15, 0, 92);
            sphere(2, Color.Green, 20, 0, 95);
            sphere(2, Color.Green, 25, 0, 98);
            sphere(3, Color.Green, 75, 0, 95);
            //--


            // Meta final
            cone(4, 5, Color.Blue, 5, 5, 95);
            //--
        }

        public void wallX(double x1, double x2, double z, double height, string textureName, double quality, bool hasColumnsInit, bool hasColumnsEnd)
        {
            Objects.Plane p;
            Objects.Rectangle r;
            Engine.Collision collision;

            collision = new Engine.Collision(x1, x2, z - 0.5, z + 0.5, 1);
            collisions.Add(collision);
            p = new Objects.Plane();
            p.textureName = textureName;
            for (double i = x1; i < x2; i += quality)
            {
                for (double j = 0; j < height; j += quality)
                {
                    r = new Objects.Rectangle();

                    r.points[0][0] = i;
                    r.points[0][1] = j;
                    r.points[0][2] = z;
                    r.points[1][0] = i + quality;
                    r.points[1][1] = j;
                    r.points[1][2] = z;
                    r.points[2][0] = i + quality;
                    r.points[2][1] = j + quality;
                    r.points[2][2] = z;
                    r.points[3][0] = i;
                    r.points[3][1] = j + quality;
                    r.points[3][2] = z;
                    p.rectangles.Add(r);
                }
            }
            if (hasColumnsInit)
            {
                for (double j = 0; j < height; j += quality)
                {
                    r = new Objects.Rectangle();

                    r.points[0][0] = x1;
                    r.points[0][1] = j;
                    r.points[0][2] = z - 0.2;
                    r.points[1][0] = x1;
                    r.points[1][1] = j;
                    r.points[1][2] = z + 0.2;
                    r.points[2][0] = x1;
                    r.points[2][1] = j + quality;
                    r.points[2][2] = z + 0.2;
                    r.points[3][0] = x1;
                    r.points[3][1] = j + quality;
                    r.points[3][2] = z - 0.2;
                    p.rectangles.Add(r);
                }
            }
            if (hasColumnsEnd)
            {
                for (double j = 0; j < height; j += quality)
                {
                    r = new Objects.Rectangle();
                    r.points[0][0] = x2;
                    r.points[0][1] = j;
                    r.points[0][2] = z - 0.2;
                    r.points[1][0] = x2;
                    r.points[1][1] = j;
                    r.points[1][2] = z + 0.2;
                    r.points[2][0] = x2;
                    r.points[2][1] = j + quality;
                    r.points[2][2] = z + 0.2;
                    r.points[3][0] = x2;
                    r.points[3][1] = j + quality;
                    r.points[3][2] = z - 0.2;
                    p.rectangles.Add(r);
                }
            }
            planes.Add(p);
        }

        public void wallZ(double z1, double z2, double x, double height, string textureName, double quality, bool hasColumnsInit, bool hasColumnsEnd)
        {
            Objects.Plane p;
            Objects.Rectangle r;
            Engine.Collision collision;

            collision = new Engine.Collision(x - 0.5, x + 0.5, z1, z2, 2);
            collisions.Add(collision);
            p = new Objects.Plane();
            p.textureName = textureName;
            for (double i = z1; i < z2; i += quality)
            {
                for (double j = 0; j < height; j += quality)
                {
                    r = new Objects.Rectangle();

                    r.points[0][0] = x;
                    r.points[0][1] = j;
                    r.points[0][2] = i;
                    r.points[1][0] = x;
                    r.points[1][1] = j;
                    r.points[1][2] = i + quality;
                    r.points[2][0] = x;
                    r.points[2][1] = j + quality;
                    r.points[2][2] = i + quality;
                    r.points[3][0] = x;
                    r.points[3][1] = j + quality;
                    r.points[3][2] = i;
                    p.rectangles.Add(r);
                }
            }
            if (hasColumnsInit)
            {
                for (double j = 0; j < height; j += quality)
                {
                    r = new Objects.Rectangle();

                    r.points[0][0] = x - 0.2;
                    r.points[0][1] = j;
                    r.points[0][2] = z1;
                    r.points[1][0] = x + 0.2;
                    r.points[1][1] = j;
                    r.points[1][2] = z1;
                    r.points[2][0] = x + 0.2;
                    r.points[2][1] = j + quality;
                    r.points[2][2] = z1;
                    r.points[3][0] = x - 0.2;
                    r.points[3][1] = j + quality;
                    r.points[3][2] = z1;
                    p.rectangles.Add(r);
                }
            }
            if (hasColumnsEnd)
            {
                for (double j = 0; j < height; j += quality)
                {
                    r = new Objects.Rectangle();

                    r.points[0][0] = x - 0.2;
                    r.points[0][1] = j;
                    r.points[0][2] = z2;
                    r.points[1][0] = x + 0.2;
                    r.points[1][1] = j;
                    r.points[1][2] = z2;
                    r.points[2][0] = x + 0.2;
                    r.points[2][1] = j + quality;
                    r.points[2][2] = z2;
                    r.points[3][0] = x - 0.2;
                    r.points[3][1] = j + quality;
                    r.points[3][2] = z2;
                    p.rectangles.Add(r);
                }
            }
            planes.Add(p);
        }

        public void sphere(double radio, Color color, double positionX, double positionY, double positionZ)
        {
            Objects.Sphere s;
            Engine.Collision collision;

            s = new Objects.Sphere(radio, color, positionX, positionY, positionZ);
            spheres.Add(s);

            collision = new Engine.Collision(positionX - radio, positionX + radio, positionZ - radio, positionZ + radio, 3);
            collisions.Add(collision);
        }

        public void cone(double radio, double height, Color color, double positionX, double positionY, double positionZ)
        {
            Objects.Cone c;
            Engine.Collision collision;

            c = new Objects.Cone(radio, height, color, positionX, positionY, positionZ);
            cones.Add(c);

            collision = new Engine.Collision(positionX - radio, positionX + radio, positionZ - (height / 2), positionZ + (height / 2), 4);
            collisions.Add(collision);
        }
    }
}
