using System;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL;

namespace Cubux.Engine
{
    class Texture
    {
        public static int LoadTexture(string textureName, bool repeat, bool rotate)
        {
            if (textureName != null)
            {
                int id = GL.GenTexture();

                Bitmap imagen = new Bitmap(textureName);
                if (rotate)
                    imagen.RotateFlip(RotateFlipType.Rotate180FlipY);
                BitmapData bitmapdata = imagen.LockBits(new Rectangle(0, 0, imagen.Width, imagen.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                GL.BindTexture(TextureTarget.Texture2D, id);
                if (repeat)
                {
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
                }
                else
                {
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
                }

                GL.TexEnv(TextureEnvTarget.TextureEnv, TextureEnvParameter.TextureEnvMode, (float)TextureEnvMode.Modulate);

                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.GenerateMipmap, 1);
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, imagen.Width, imagen.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgr, PixelType.UnsignedByte, bitmapdata.Scan0);

                imagen.UnlockBits(bitmapdata);
                imagen.Dispose();

                return id;
            }
            return 0;
        }
    }
}
