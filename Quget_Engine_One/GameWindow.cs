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
using Quget_Engine_One.Sound;
using Quget_Engine_One.Camera;
using System.Threading.Tasks;
using Quget_Engine_One.Gui;

namespace Quget_Engine_One
{
    /// <summary>
    /// Game Window
    /// </summary>
    class GameWindow : OpenTK.GameWindow
    {

       
        private List<Scene> scenes = new List<Scene>();
        private int selectedScene = -1;

        private Dictionary<string, ShaderProgram> programs = new Dictionary<string, ShaderProgram>();
        private Matrix4 projectionMatrix;

        /// <summary>
        /// Constructor of the game window, It's 1280x720, gives it a name, windowed mode, default display
        /// Open GL 4.5
        /// </summary>
        public GameWindow():base(1280,720,GraphicsMode.Default,"Quget Engine One",GameWindowFlags.Default,
                            DisplayDevice.Default,4,5,GraphicsContextFlags.Default)
        {
            //Listen to keyboard input
            QKeyboard.Create(this);
            //Listen to mouse input
            QMouse.Create(this);

            string version = UnicodeToUTF8(GL.GetString(StringName.Version));
            string shaderVersion = UnicodeToUTF8(GL.GetString(StringName.ShadingLanguageVersion));
            string renderer = UnicodeToUTF8(GL.GetString(StringName.Renderer));
            Console.WriteLine("OpenGL Version   :{0}", version);
            Console.WriteLine("Shader Version   :{0}", shaderVersion);
            Console.WriteLine("Render Hardware  :{0}", renderer);
            //VSync = VSyncMode.On;
        }

        //Convert text from unicode to utf8
        public string UnicodeToUTF8(string text)
        {
            byte[] toBytes = Encoding.Unicode.GetBytes(text);
            return Encoding.UTF8.GetString(toBytes);
        }
        /// <summary>
        /// Ask for a shader program, if it doesn't exsist then return a default shader
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Add a scene
        /// </summary>
        /// <param name="scene"></param>
        public void AddScene(Scene scene)
        {
            scenes.Add(scene);
        }

        /// <summary>
        /// Remove a scene
        /// </summary>
        /// <param name="scene"></param>
        public void RemoveScene(Scene scene)
        {
            scenes.Remove(scene);
        }

        /// <summary>
        /// Load a scene
        /// </summary>
        /// <param name="index"></param>
        public void LoadScene(int index)
        {
            if (index < 0 || index > scenes.Count)
                throw new Exception("Index of scene is below 0 or higher then the amount of scenes");

            if(selectedScene != -1)
                scenes[selectedScene].Exit();


            selectedScene = index;
            scenes[selectedScene].OnLoad();
        }

        /// <summary>
        /// Do something when the window is changing size.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, this.Width, this.Height);
            CreateProjection();
        }

        /// <summary>
        /// Load all the shaders from the folder Content/Shaders
        /// </summary>
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
        /// <summary>
        /// When the game window is loading or loaded
        /// Load the shaders,setup OpenGL variables
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            CreateProjection();
            LinkShaders();

            onWindowLoad?.Invoke(this, e);

            CursorVisible = true;

            
            GL.PolygonMode(MaterialFace.Front, PolygonMode.Fill);
            
            GL.PatchParameter(PatchParameterInt.PatchVertices, 3);

            GL.Enable(EnableCap.Blend);
            //GL.Disable(EnableCap.Blend);
            //GL.BlendEquation(BlendEquationMode.Max);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            GL.Enable(EnableCap.DepthTest);
            //GL.Enable(EnableCap.CullFace);
            Closed += GameWindow_Closed;
        }

        /// <summary>
        /// When de game window is closed, there will be things to clean up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameWindow_Closed(object sender, EventArgs e)
        {
            QSound.Dispose();
            Exit();
        }
        /// <summary>
        /// Call the Update method in the scene on every frame
        /// </summary>
        /// <param name="e"></param>
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            if(selectedScene != -1)
                scenes[selectedScene].OnUpdateFrame(e);
        }
        /// <summary>
        /// Create a projection. A camera. Default is Perspective
        /// </summary>
        private void CreateProjection()
        {

            //var aspectRatio = (float)Width / Height;
            /*
            projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(
                60 * ((float)Math.PI / 180f), // field of view angle, in radians
                aspectRatio,                // current window aspect ratio
                0.1f,                       // near plane
                4000f);                     // far plane
                */
            //projectionMatrix = Matrix4.CreateOrthographicOffCenter(0, this.Width, this.Height, 0, 0.1f, 10f);

            //projectionMatrix = Matrix4.CreateOrthographicOffCenter(0, this.Width, this.Height, 0, 0.1f, 10f);
            ToPerspective();
        }

        /// <summary>
        /// Create a perspective view
        /// </summary>
        public void ToPerspective()
        {
            var aspectRatio = (float)Width / Height;
            projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(
                60 * ((float)Math.PI / 180f), // field of view angle, in radians
                aspectRatio,                // current window aspect ratio
                0.1f,                       // near plane
                4000f);                     // far plane

        }

        /// <summary>
        /// Orthographic view
        /// </summary>
        public void ToOrthographic()
        {
            projectionMatrix = Matrix4.CreateOrthographicOffCenter(0, this.Width, this.Height, 0, 0.1f, 10f);
        }

        /// <summary>
        /// Clear up all the things
        /// </summary>
        public override void Exit()
        {
            if (selectedScene != -1)
                scenes[selectedScene].Exit();
            foreach(KeyValuePair<String,ShaderProgram> program in programs)
            {
                program.Value.Dispose();
            }

            base.Exit();
        }
        /// <summary>
        /// Every frame Render called before OnUpdateFrame.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderFrame(FrameEventArgs e)
        {

            GL.ClearColor(Color4.Purple);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            //ProcessEvents();

            if (selectedScene != -1)
            {
                scenes[selectedScene].OnRenderFrame(e, ref projectionMatrix);
            }

            this.Title = String.Format("Quget Engine One | \rVsync:{0} | FPS:{1:0000}", VSync, 1f / e.Time);
            Console.Write("\rVsync: {0} FPS:{1:0000000000}", VSync, 1f / e.Time);
            SwapBuffers();
            Thread.Sleep(1);//reduce cpu usage.
        }
/*
        public delegate void OnFrame(GameWindow sender, FrameEventArgs e);
        public event OnFrame onUpdate;
*/
        public delegate void OnWindowLoad(GameWindow sender, EventArgs e);
        public event OnWindowLoad onWindowLoad;

    }
}
