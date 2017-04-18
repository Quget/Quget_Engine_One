using OpenTK.Graphics.OpenGL4;

using System;
using System.Collections.Generic;
using System.Text;


namespace Quget_Engine_One.Renderables
{
    class Renderable : IDisposable
    {
        protected int vertexArray;
        protected int buffer;
        protected int verticesCount;
        protected int program;
        public Renderable(int program,int verticesCount)
        {
            this.program = program;
            this.verticesCount = verticesCount;
            //Create array and buffer
            vertexArray = GL.GenVertexArray();
            buffer = GL.GenBuffer();

            GL.BindVertexArray(vertexArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, buffer);
        }
        public int GetProgram()
        {
            return program;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public virtual void Bind()
        {
            GL.UseProgram(program);
            GL.BindVertexArray(vertexArray);
        }
        public void Render()
        {
            GL.DrawArrays(PrimitiveType.Triangles, 0, verticesCount);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                GL.DeleteVertexArray(vertexArray);
                GL.DeleteBuffer(buffer);
            }
        }
    }
}
