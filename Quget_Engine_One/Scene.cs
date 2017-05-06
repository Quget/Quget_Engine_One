using OpenTK;
using OpenTK.Graphics.OpenGL4;
using Quget_Engine_One.Camera;
using Quget_Engine_One.GameObjects;
using Quget_Engine_One.Gui;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quget_Engine_One
{
    class Scene
    {
        private List<GameObject> gameObjects = new List<GameObject>();
        private ICamera camera;
        protected Qui qui { private set; get; }

        protected GameWindow gameWindow { private set; get; }
        public Scene(GameWindow gameWindow)
        {
            this.gameWindow = gameWindow;
            qui = new Qui(this);
        }
        public virtual void OnLoad()
        {
            camera = new StaticCamera();
        }
        public void LoadScene(int index)
        {
            gameWindow.LoadScene(index);
        }
        public virtual void OnUpdateFrame(FrameEventArgs e)
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Update(e.Time);
            }
            /*
            Parallel.ForEach(gameObjects, (gameObject) =>
            {
                 gameObject.Update(e.Time);
            });*/
            camera.Update(e.Time);
        }
        public void OnRenderFrame(FrameEventArgs e, ref Matrix4 projectionMatrix)
        {
            int lastShaderProgram = -1;

            int renderCount = 0;

            for (int i = 0; i < gameObjects.Count; i++)
            {
                if (CanRender(gameObjects[i]))
                {
                    int program = gameObjects[i].render.GetProgram();
                    if (lastShaderProgram != program)
                    {
                        GL.UniformMatrix4(20, false, ref projectionMatrix);
                    }
                    lastShaderProgram = program;
                    gameObjects[i].Render(camera);
                    renderCount++;
                }
            }
        }
        public ShaderProgram GetShaderProgram(string name)
        {
            return gameWindow.GetShaderProgram(name);
        }
        public void SetCamera(ICamera camera)
        {
            this.camera = camera;
        }
        public void AddGameObject(GameObject gameObject)
        {
            gameObject.onRemove += GameObject_onRemove;
            gameObjects.Add(gameObject);
        }

        private void GameObject_onRemove(GameObject sender)
        {
            sender.onRemove -= GameObject_onRemove;
            gameObjects.Remove(sender);
        }

        public void RemoveGameObject(GameObject gameObject)
        {
            gameObjects.Remove(gameObject);
            gameObject.Dispose();
        }
        public  void Exit()
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Dispose();
            }
        }
        private bool CanRender(GameObject gameObject)
        {
            if (gameObject is QuiObject)
            {
                QuiObject quiObject = (QuiObject)gameObject;

                if (quiObject.fixedOnCam)
                    return true;
            }

            float renderPosX = (gameObject.position.X + camera.position.X);
            float renderPosY = (gameObject.position.Y + camera.position.Y);
            //Position check
            if (renderPosX - gameObject.width > gameWindow.Width ||
                renderPosX + gameObject.width < 0 ||
                 renderPosY - gameObject.height > gameWindow.Height ||
                 renderPosY + gameObject.height < 0)
            {
                return false;
            }

            return true;
        }
    }
}
