using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using System.IO;
using OpenTK.Input;
using System.Diagnostics;
using Quget_Engine_One.Renderables;
using Quget_Engine_One.GameObjects;
using System.Threading;

namespace Quget_Engine_One
{
    class GameWindow : OpenTK.GameWindow
    {

        //private ShaderProgram texProgram;
        //private ShaderProgram colorProgram;
        //private List<Renderable> renderObjects = new List<Renderable>();
        private Dictionary<String, ShaderProgram> programs = new Dictionary<string, ShaderProgram>();
        private List<GameObject> gameObjects = new List<GameObject>();
        private Matrix4 projectionMatrix;
        public GameWindow():base(1280,720,GraphicsMode.Default,"Quget Engine One",GameWindowFlags.Default,
                            DisplayDevice.Default,4,5,GraphicsContextFlags.ForwardCompatible)
        {
            QKeyboard.Create(this);
            string version = GL.GetString(StringName.Version);
            Console.WriteLine("gl version:{0}", version);
        }
        public ShaderProgram GetShaderProgram(string name)
        {
            if(programs.ContainsKey(name))
            {
                return programs[name];
            }
            else
            {
                return programs["default"];
            }
        }
        public void AddGameObject(GameObject gameObject)
        {
            gameObjects.Add(gameObject);
        }
        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, this.Width, this.Height);
            CreateProjection();
        }
        private void LinkShaders()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo("Content/Shaders");
            FileInfo[] files = directoryInfo.GetFiles("*.vs");
            for(int i = 0; i < files.Length; i++)
            {
                string name = files[i].Name.Substring(0, files[i].Name.Length - 3);
                ShaderProgram program = new ShaderProgram();
                program.AddShader(ShaderType.VertexShader, "Content/Shaders/" + name + ".vs");
                program.AddShader(ShaderType.FragmentShader, "Content/Shaders/" + name + ".fs");
                program.Link();

                programs.Add(name,program);
            }

        }
        protected override void OnLoad(EventArgs e)
        {
            CreateProjection();
            LinkShaders();

            onWindowLoad?.Invoke(this, e);

            CursorVisible = true;

            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            GL.PatchParameter(PatchParameterInt.PatchVertices, 3);

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
           // GL.Enable(EnableCap.DepthTest);
            //GL.Enable(EnableCap.CullFace);
            Closed += GameWindow_Closed;
        }

        private void GameWindow_Closed(object sender, EventArgs e)
        {
            Exit();
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            onUpdate?.Invoke(this, e);

            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Update(e.Time);
            }
        }
        private void CreateProjection()
        {

            var aspectRatio = (float)Width / Height;
            /*
            projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(
                60 * ((float)Math.PI / 180f), // field of view angle, in radians
                aspectRatio,                // current window aspect ratio
                0.1f,                       // near plane
                4000f);                     // far plane
                */
            projectionMatrix = Matrix4.CreateOrthographicOffCenter(0, this.Width, this.Height, 0, 0.1f, 1000f);
        }
        public override void Exit()
        {
            Debug.WriteLine("Exit has been called. Exiting...");
            for(int i = 0; i < gameObjects.Count; i ++)
            {
                gameObjects[i].Dispose();
            }
            foreach(KeyValuePair<String,ShaderProgram> program in programs)
            {
                program.Value.Dispose();
            }

            base.Exit();
        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            Console.Write("\rVsync: {0} FPS:{1:0}", VSync, 1f / e.Time);
            GL.ClearColor(Color4.Purple);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            int lastShaderProgram = -1;
            for(int i = 0; i < gameObjects.Count; i++)
            {
                int program = gameObjects[i].render.GetProgram();
                if(lastShaderProgram != program)
                {
                    GL.UniformMatrix4(20, false, ref projectionMatrix);
                }
                lastShaderProgram = program;
                gameObjects[i].Render();
            }
            SwapBuffers();
        }

        public delegate void OnFrame(GameWindow sender, FrameEventArgs e);
        public event OnFrame onUpdate;

        public delegate void OnWindowLoad(GameWindow sender, EventArgs e);
        public event OnWindowLoad onWindowLoad;
    }
}
