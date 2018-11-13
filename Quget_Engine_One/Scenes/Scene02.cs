using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
using Quget_Engine_One.GameObjects;
using Quget_Engine_One.Renderables;

namespace Quget_Engine_One.Scenes
{
    class Scene02 : Scene
    {
        private GameObject rotatingCube;

        private GameObject zMovingCube;
        public Scene02(GameWindow gameWindow) : base(gameWindow)
        {

           
        }

        public override void OnLoad()
        {
            gameWindow.ToPerspective();
            ShaderProgram program = GetShaderProgram("default");

            TexturedRenderObject cube = new TexturedRenderObject(ObjectFactory.CreateTexturedCube(4, 4, 4, Color4.Azure), program.id, "Content/Textures/noMove.png");

            rotatingCube = new GameObject(cube, new Vector4(0,0,-25, 0), new Vector4(20,20,0,0), "Cube");
            AddGameObject(rotatingCube);

            //TexturedRenderObject cube = new TexturedRenderObject(ObjectFactory.CreateTexturedCube(5, 5, 5, Color4.Azure), program.id, "Content/Textures/noMove.png");
            zMovingCube = new GameObject(cube, new Vector4(0, 0, -30, 0), new Vector4(0, 0, 0, 0), "Cube2");
            AddGameObject(zMovingCube);

            qui.CreateText("Hello World This is scene2", new Vector3(-1, 0.5f, -1), "Segoe UI Black", 12, 0.05f, -0.05f, Color4.Red, true);
            Camera.StaticCamera camera = new Camera.StaticCamera(new Vector3(0,0,0),gameWindow);
            SetCamera(camera);
            base.OnLoad();
        }

        public override void OnUpdateFrame(FrameEventArgs e)
        {
            if (QKeyboard.Instance.GetKeyDown(Key.T))
            {
                LoadScene(0);
            }

            if (QKeyboard.Instance.GetKeyDown(Key.Escape))
            {
                gameWindow.Exit();
            }

            if (QKeyboard.Instance.GetKeyDown(Key.W))
            {
                zMovingCube.SetPosition(zMovingCube.position.X,
                    zMovingCube.position.Y,
                    zMovingCube.position.Z + (10 * (float)e.Time),
                    zMovingCube.position.W);
            }

            if (QKeyboard.Instance.GetKeyDown(Key.S))
            {
                zMovingCube.SetPosition(zMovingCube.position.X,
                    zMovingCube.position.Y,
                    zMovingCube.position.Z - (10 * (float)e.Time),
                    zMovingCube.position.W);
            }


            if (QKeyboard.Instance.GetKeyDown(Key.A))
            {
                zMovingCube.SetPosition(zMovingCube.position.X - (10 * (float)e.Time),
                    zMovingCube.position.Y,
                    zMovingCube.position.Z ,
                    zMovingCube.position.W);
            }

            if (QKeyboard.Instance.GetKeyDown(Key.D))
            {
                zMovingCube.SetPosition(zMovingCube.position.X + (10 * (float)e.Time),
                    zMovingCube.position.Y,
                    zMovingCube.position.Z,
                    zMovingCube.position.W);
            }


            rotatingCube.rotation = new Vector4(rotatingCube.rotation.X + (5.0f * (float)e.Time), 
                rotatingCube.rotation.Y + (5.0f * (float)e.Time), 
                rotatingCube.rotation.Z, 
                rotatingCube.rotation.W);

            base.OnUpdateFrame(e);
        }
    }
}
