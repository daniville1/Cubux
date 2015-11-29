using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL;
using OpenTK;
using System.IO;
using Glu = OpenTK.Graphics.Glu;
using System.Drawing.Imaging;

namespace Cubux
{
    public partial class Cubux : Form
    {
        Engine.Camera camare;
        Engine.Light ligth;
        Engine.Maps map;
        int level;
        Point pointer_current, pointer_previous;
        List<Int32> GList;
        Objects.Plane template;
        bool isTransparency = false;
        bool loaded = false;
        Random r;

        public Cubux()
        {
            // Pantalla completa
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Width = Screen.PrimaryScreen.Bounds.Width;
            this.Height = Screen.PrimaryScreen.Bounds.Height;
            //--


            // Mouse
            Cursor.Hide();
            //--


            InitializeComponent();
        }

        private void gl_Paint(object sender, PaintEventArgs e)
        {
            // Preconfiguracion
            if (!loaded) 
                return;
            if (GList == null)
                return;
            loaded = false;
            //--


            // Camara
            GL.LoadIdentity();
            Glu.LookAt(camare.position[0], camare.position[1], camare.position[2], camare.view[0], camare.view[1], camare.view[2], 0, 1, 0);
            //--


            // Luz
            ligth.positionLigth[0] = (float) camare.position[0];
            ligth.positionLigth[1] = (float)camare.position[1];
            ligth.positionLigth[2] = (float)camare.position[2];
            ligth.directionLigth[0] = (float)(camare.view[0] - camare.position[0]);
            ligth.directionLigth[1] = (float)(camare.view[1] - camare.position[1]);
            ligth.directionLigth[2] = (float)(camare.view[2] - camare.position[2]);
            ligth.directionLigth = Engine.OperationMatrixVector.normalize(ligth.directionLigth);

            GL.Light(LightName.Light0, LightParameter.Position, ligth.positionLigth);
            GL.Light(LightName.Light0, LightParameter.SpotDirection, ligth.directionLigth);

            if(ligth.turnOn)
                GL.Enable(EnableCap.Lighting);
            else
                GL.Disable(EnableCap.Lighting);
            //--


            // Profundidad
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            //--


            // Truco de transparencia
            if (isTransparency)
                GL.Enable(EnableCap.Blend);
            else
                GL.Disable(EnableCap.Blend);
            //--


            // Objetos
            GL.PushMatrix();
            for (int i = 0; i < GList.Count; i++)
            {
                GL.CallList(GList[i]);
            }
            GL.PopMatrix();
            GL.Finish();
            //--


            // Posconfiguracion
            gl.SwapBuffers();
            loaded = true;
            //--
        }

        private void gl_Load(object sender, EventArgs e)
        {
            // Perspectivas
            GL.Viewport(0, 0, gl.Size.Width - 1, gl.Size.Height - 1);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            Glu.Perspective(90, (gl.Size.Width / gl.Size.Height), 0.1f, 10000f);
            GL.MatrixMode(MatrixMode.Modelview);
            //--


            // Profundidad
            GL.ClearColor(Color.Black);
            GL.ClearDepth(1.0);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Lequal);
            //--


            // Tipo de Material
            float[] mat_especular = { 1.0f, 1.0f, 1.0f, 1.0f };
            float[] mat_brillo = { 50.0f };

            GL.ShadeModel(ShadingModel.Smooth);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Specular, mat_especular);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Shininess, mat_brillo);
            GL.Enable(EnableCap.ColorMaterial);
            //--


            // Textura
            GL.Enable(EnableCap.Texture2D);
            //--


            // Camara
            camare = new Engine.Camera(0, 0, -10, 0, 0, -1, 4, 2.0, 0.2, 0.1, 0.5);
            //--


            // Luz
            float[] white = { 1.0f, 1.0f, 1.0f, 1.0f };
            ligth = new Engine.Light();

            GL.Light(LightName.Light0, LightParameter.Ambient, white);
            GL.Light(LightName.Light0, LightParameter.Diffuse, white);
            GL.Light(LightName.Light0, LightParameter.Specular, white);
            GL.Light(LightName.Light0, LightParameter.SpotCutoff, 10);
            GL.Light(LightName.Light0, LightParameter.ConstantAttenuation, 2);
            GL.Enable(EnableCap.Light0);
            //--


            // Trucos
            GL.BlendFunc(BlendingFactorSrc.One, BlendingFactorDest.One);
            //--


            // Preconfiguracion de presentacion y nivel
            level = -1;
            template = Engine.Template.levelCompleted(-2);
            ligth.turnOn = false;
            drawTemplate();
            camare.levelCompleted = true;
            //--


            // Iniciar sonido
            Engine.Sound.play("init.wav");
            //--
        }

        private void tmrRedraw_Tick(object sender, EventArgs e)
        {
            gl_Paint(gl, null);
            mouse();
        }

        public void startLevel()
        {
            // Siguiente nivel
            level++;
            //--


            // Inicio mapa
            map = new Engine.Maps(level);
            //--

            
            // Preconfigura la posición inicial del personaje y vuelvo por defecto nivelCompletado
            camare.position[0] = map.position[0];
            camare.position[1] = map.position[1];
            camare.position[2] = map.position[2];
            camare.view[0] = map.view[0];
            camare.view[1] = map.view[1];
            camare.view[2] = map.view[2];
            camare.levelCompleted = false;
            ligth.turnOn = true;
            //--


            // Dibujo mapa
            drawMap();
            //--


            // Iniciar sonido
            Engine.Sound.play("music" + level + ".wav");
            //--
        }

        public void drawMap()
        {
            GList = new List<Int32>();
            GList.Add(0);

            foreach (Objects.Plane plane in map.planes)
            {
                GL.NewList(GList.Count, ListMode.Compile);
                GL.BindTexture(TextureTarget.Texture2D, Engine.Texture.LoadTexture(plane.textureName, plane.repeat, plane.rotate));
                GL.Color3(plane.color);
                foreach (Objects.Rectangle rectangle in plane.rectangles)
                {
                    GL.Begin(BeginMode.Polygon);
                    for (int i = 0; i < rectangle.points.Count; i++)
                    {
                        if (i == 0)
                            GL.TexCoord2(0, 0);
                        if (i == 1)
                            GL.TexCoord2(1, 0);
                        if (i == 2)
                            GL.TexCoord2(1, 1);
                        if (i == 3)
                            GL.TexCoord2(0, 1);

                        GL.Vertex3(rectangle.points[i][0], rectangle.points[i][1], rectangle.points[i][2]);
                    }
                    GL.End();
                }
                GL.EndList();
                GList.Add(GList.Count);
            }

            foreach (Objects.Sphere sphere in map.spheres)
            {
                GL.NewList(GList.Count, ListMode.Compile);
                GL.PushMatrix();
                GL.Color3(sphere.color);
                IntPtr q = Glu.NewQuadric();

                GL.Translate(sphere.position[0], sphere.position[1], sphere.position[2]);
                Glu.Sphere(q, sphere.radio, 100, 100);
                GL.Translate(-sphere.position[0], -sphere.position[1], -sphere.position[2]);

                GL.PopMatrix();
                GL.EndList();
                GList.Add(GList.Count);
            }

            foreach (Objects.Cone cone in map.cones)
            {
                GL.NewList(GList.Count, ListMode.Compile);
                GL.PushMatrix();
                GL.Color3(cone.color);
                IntPtr q = Glu.NewQuadric();

                GL.Translate(cone.position[0], cone.position[1], cone.position[2] - (cone.height / 2));
                Glu.Cylinder(q, 0, cone.radio, cone.height, Engine.Constant.QUALITY, Engine.Constant.QUALITY);
                GL.Translate(-cone.position[0], -cone.position[1], -cone.position[2] + (cone.height / 2));

                GL.PopMatrix();
                GL.EndList();
                GList.Add(GList.Count);
            }

            gl_Paint(gl, null);
            loaded = true;
        }

        public void drawTemplate()
        {
            GList = new List<Int32>();
            GList.Add(0);

            GL.NewList(GList.Count, ListMode.Compile);
            GL.BindTexture(TextureTarget.Texture2D, Engine.Texture.LoadTexture(template.textureName, template.repeat, template.rotate));
            GL.Color3(template.color);
            foreach (Objects.Rectangle rectangle in template.rectangles)
            {
                GL.Begin(BeginMode.Polygon);
                for (int i = 0; i < rectangle.points.Count; i++)
                {
                    if (i == 0)
                        GL.TexCoord2(0, 0);
                    if (i == 1)
                        GL.TexCoord2(1, 0);
                    if (i == 2)
                        GL.TexCoord2(1, 1);
                    if (i == 3)
                        GL.TexCoord2(0, 1);

                    GL.Vertex3(rectangle.points[i][0], rectangle.points[i][1], rectangle.points[i][2]);
                }
                GL.End();
            }
            GL.EndList();
            GList.Add(GList.Count);

            gl_Paint(gl, null);
            loaded = true;
        }

        private void mouse()
        {
            pointer_current = System.Windows.Forms.Cursor.Position;

            if (!camare.levelCompleted)
            {
                if (pointer_previous.X > pointer_current.X)
                    camare.lookLeftMouse(pointer_previous.X - pointer_current.X);
                else if (pointer_previous.X < pointer_current.X)
                    camare.lookRightMouse(pointer_current.X - pointer_previous.X);
                if (pointer_previous.Y > pointer_current.Y)
                    camare.lookUpMouse(pointer_previous.Y - pointer_current.Y);
                else if (pointer_previous.Y < pointer_current.Y)
                    camare.lookDownMouse(pointer_current.Y - pointer_previous.Y);
            }

            System.Windows.Forms.Cursor.Position = new Point(gl.Bounds.Width / 2, gl.Bounds.Height / 2);
            pointer_previous = System.Windows.Forms.Cursor.Position;
        }

        private void keyboard(object sender, PreviewKeyDownEventArgs e)
        {
            if (!camare.levelCompleted)
            {
                // Trucos
                if (e.KeyData == (Keys.Alt | Keys.C))
                    camare.isCollision = !camare.isCollision;
                else if (e.KeyData == (Keys.Alt | Keys.L))
                    ligth.turnOn = !ligth.turnOn;
                else if (e.KeyData == (Keys.Alt | Keys.T))
                {
                    isTransparency = !isTransparency;
                    ligth.turnOn = true;
                }
                //--


                else if (e.KeyData == Engine.Control.moveForward)
                    camare.moveForward(map.collisions);
                else if (e.KeyData == Engine.Control.moveBack)
                    camare.moveBack(map.collisions);
                else if (e.KeyData == Engine.Control.moveLeft)
                    camare.moveLeft(map.collisions);
                else if (e.KeyData == Engine.Control.moveRight)
                    camare.moveRight(map.collisions);
                else if (e.KeyData == Engine.Control.lookUp)
                    camare.lookUpKey();
                else if (e.KeyData == Engine.Control.lookDown)
                    camare.lookDownKey();
                else if (e.KeyData == Engine.Control.lookLeft)
                    camare.lookLeftKey();
                else if (e.KeyData == Engine.Control.lookRight)
                    camare.lookRightKey();
                else if (e.KeyData == Engine.Control.restart)
                {
                    camare.position[0] = map.position[0];
                    camare.position[1] = map.position[1];
                    camare.position[2] = map.position[2];
                    camare.view[0] = map.view[0];
                    camare.view[1] = map.view[1];
                    camare.view[2] = map.view[2];
                }
            }
            else if (e.KeyData == Engine.Control.go)
            {
                if (level == -2)
                {
                    template = Engine.Template.levelCompleted(-2);
                    ligth.turnOn = false;
                    drawTemplate();
                    level = -1;
                }
                else if (level == -1)
                {
                    template = Engine.Template.levelCompleted(-1);
                    ligth.turnOn = false;
                    drawTemplate();
                    level = 0;
                }
                else
                    startLevel();
            }            

            if (camare.levelCompleted)
            {
                camare.position[0] = 0;
                camare.position[1] = 0;
                camare.position[2] = -10;
                camare.view[0] = 0;
                camare.view[1] = 0;
                camare.view[2] = -1;

                if (level >= 1 && level <= 3)
                {
                    // Poner sonido Nivel Completado
                    Engine.Sound.play("levelCompleted.wav");
                    //--
                    template = Engine.Template.levelCompleted(level);
                    ligth.turnOn = false;
                    drawTemplate();
                }
                else if (level == 4)
                {
                    // Poner sonido final
                    Engine.Sound.play("end.wav");
                    //--
                    template = Engine.Template.levelCompleted(5);
                    ligth.turnOn = false;
                    drawTemplate();
                    System.Threading.Thread.Sleep(Engine.Constant.DELAY);

                    template = Engine.Template.levelCompleted(6);
                    ligth.turnOn = false;
                    drawTemplate();

                    level = -2;
                }    
            }

            if (e.KeyData == Engine.Control.exit)
                this.Close();
        }
    }
}