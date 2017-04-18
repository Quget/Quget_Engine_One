using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Quget_Engine_One
{
    class ShaderProgram : IDisposable
    {
        public int id { private set; get; }
        private List<int> shaders = new List<int>();
        public ShaderProgram()
        {
            id = GL.CreateProgram();
        }
        private int CompileShaders(ShaderType type, string path)
        {
            int shader = GL.CreateShader(type);
            string source = File.ReadAllText(path);
            GL.ShaderSource(shader, source);
            GL.CompileShader(shader);
            string info = GL.GetShaderInfoLog(shader);
            if(!string.IsNullOrWhiteSpace(info))
            {
                Debug.WriteLine("GL.CompileShader {0} had info log:{1}", type, info);
            }
            return shader;
        }


        public void Link()
        {
            //foreach (var shader in shaders)
                //GL.AttachShader(id, shader);

            for (int i = 0; i < shaders.Count; i++)
            {
                GL.AttachShader(id, shaders[i]);
            }

            GL.LinkProgram(id);
            string info = GL.GetProgramInfoLog(id);
            if (!string.IsNullOrWhiteSpace(info))
                Debug.WriteLine($"GL.LinkProgram had info log: {info}");

            for (int i = 0; i < shaders.Count; i++)
            {
                GL.DetachShader(id, shaders[i]);
                GL.DeleteShader(shaders[i]);
            }
        }

        public void AddShader(ShaderType type, string path)
        {
            int shader = GL.CreateShader(type);
            string src = File.ReadAllText(path);
            GL.ShaderSource(shader, src);
            GL.CompileShader(shader);
            string info = GL.GetShaderInfoLog(shader);
            if (!string.IsNullOrWhiteSpace(info))
                Debug.WriteLine($"GL.CompileShader [{type}] had info log: {info}");
            shaders.Add(shader);

        }

        /*
        private int CreateProgram()
        {
            int program = GL.CreateProgram();
            var shaders = new List<int>();
            shaders.Add(CompileShaders(ShaderType.VertexShader, "Content/Shaders/vertex-shader.vs"));
            shaders.Add(CompileShaders(ShaderType.FragmentShader, "Content/Shaders/fragment-shader.fs"));

            for (int i = 0; i < shaders.Count; i++)
            {
                GL.AttachShader(program, shaders[i]);
            }

            GL.LinkProgram(program);
            var info = GL.GetProgramInfoLog(program);
            if (!string.IsNullOrWhiteSpace(info))
            {
                Debug.WriteLine("GL.LinkProgram had info log:{0}", info);
            }

            for (int i = 0; i < shaders.Count; i++)
            {
                GL.DetachShader(program, shaders[i]);
                GL.DeleteShader(shaders[i]);
            }
            return program;
        }
        */
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                GL.DeleteProgram(id);
            }
        }
}
}
