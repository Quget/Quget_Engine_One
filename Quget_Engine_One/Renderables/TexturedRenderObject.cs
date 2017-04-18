using System;
using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using Quget_Engine_One.Renderables;
using OpenTK;

namespace Quget_Engine_One.Renderables
{
    class TexturedRenderObject :Renderable
    {
        private int texture;
        public TexturedRenderObject(TexturedVertex[] vertices, int program, string fileName):base(program,vertices.Length)
        {
            // create first buffer: vertex
            GL.NamedBufferStorage(
                buffer,
                TexturedVertex.Size * vertices.Length,        // the size needed by this buffer
                vertices,                           // data to initialize with
                BufferStorageFlags.MapWriteBit);    // at this point we will only write to the buffer

            int offset = 0;
            GL.VertexArrayAttribBinding(vertexArray, 0, 0);
            GL.EnableVertexArrayAttrib(vertexArray, 0);
            GL.VertexArrayAttribFormat(
                vertexArray,
                0,                      // attribute index, from the shader location = 0
                4,                      // size of attribute, vec4
                VertexAttribType.Float, // contains floats
                false,                  // does not need to be normalized as it is already, floats ignore this flag anyway
                0);                     // relative offset, first item, in bytes

            offset += Vector4.SizeInBytes;
            GL.VertexArrayAttribBinding(vertexArray, 1, 0);
            GL.EnableVertexArrayAttrib(vertexArray, 1);
            GL.VertexArrayAttribFormat(
                vertexArray,
                1,                      // attribute index, from the shader location = 1
                2,                      // size of attribute, vec2
                VertexAttribType.Float, // contains floats
                false,                  // does not need to be normalized as it is already, floats ignore this flag anyway
                offset);                     // relative offset after a vec4, in bytes

            offset += Vector2.SizeInBytes;
            GL.VertexArrayAttribBinding(vertexArray, 2, 0);
            GL.EnableVertexArrayAttrib(vertexArray, 2);
            GL.VertexArrayAttribFormat(
                vertexArray,
                2,                      // attribute index, from the shader location = 1
                4,                      // size of attribute, vec4
                VertexAttribType.Float, // contains floats
                false,                  // does not need to be normalized as it is already, floats ignore this flag anyway
                offset);                     // relative offset after a vec2, in bytes
            
            // link the vertex array and buffer and provide the stride as size of Vertex
            GL.VertexArrayVertexBuffer(vertexArray, 0, buffer, IntPtr.Zero, TexturedVertex.Size);

            texture = InitTextures(fileName);
        }
        private int InitTextures(string fileName)
        {
            int width;
            int height;
            float[] data = LoadTexture(fileName, out width, out height);
            int texture;
            GL.CreateTextures(TextureTarget.Texture2D, 1, out texture);
            GL.TextureStorage2D(texture, 1, SizedInternalFormat.Rgba32f, width, height);
            GL.BindTexture(TextureTarget.Texture2D, texture);
            GL.TextureSubImage2D(texture, 0, 0, 0, width, height, OpenTK.Graphics.OpenGL4.PixelFormat.Rgba, PixelType.Float, data);
            return texture;
        }
        private float[] LoadTexture(string fileName, out int width, out int height)
        {
            float[] colours;
            System.Drawing.Bitmap bitmap = (System.Drawing.Bitmap)System.Drawing.Image.FromFile(fileName);
            width = bitmap.Width;
            height = bitmap.Height;
            colours = new float[width * height * 4];
            int i = 0;
            
            for( int y = 0; y < height; y++)
            {
                for(int x = 0; x < width; x++)
                {
                    System.Drawing.Color pixel = bitmap.GetPixel(x, y);
                    colours[i++] = pixel.R / 255f;
                    colours[i++] = pixel.G / 255f;
                    colours[i++] = pixel.B / 255f;
                    colours[i++] = pixel.A / 255f;
                }
            }
            
            return colours;
        }
        public override void Bind()
        {
            base.Bind();
            GL.BindTexture(TextureTarget.Texture2D, texture);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                GL.DeleteTexture(texture);
            }
            base.Dispose(disposing);
        }
    }
}
