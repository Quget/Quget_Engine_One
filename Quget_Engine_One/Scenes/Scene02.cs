using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
using Quget_Engine_One.Camera;
using Quget_Engine_One.GameObjects;
using Quget_Engine_One.Renderables;

namespace Quget_Engine_One.Scenes
{
    class Scene02 : Scene
    {
        private List<GameObject> rotatingCubes = new List<GameObject>();

        private Player playerCube;

        public Scene02(GameWindow gameWindow) : base(gameWindow)
        {

        }

        public override void OnLoad()
        {
            gameWindow.ToPerspective();
            base.OnLoad();

            ShaderProgram program = GetShaderProgram("default");

            TexturedRenderObject cube = new TexturedRenderObject(ObjectFactory.CreateTexturedCube(2, 2, 2, Color4.Azure), program.id, "Content/Textures/noMove.png");

            int size = 2;
            //TexturedRenderObject grass = new TexturedRenderObject(ObjectFactory.CreateTexturedQuad(size, size, 1, 1, Color4.White), program.id, "Content/Textures/grass.png");
            //TexturedRenderObject noMove = new TexturedRenderObject(ObjectFactory.CreateTexturedQuad(size, size, 1, 1, Color4.Red), program.id, "Content/Textures/noMove.png");
            TexturedRenderObject grass = new TexturedRenderObject(ObjectFactory.CreateTexturedCube(2, 2, 2, Color4.Azure), program.id, "Content/Textures/grass.png");
            TexturedRenderObject noMove = new TexturedRenderObject(ObjectFactory.CreateTexturedCube(2, 2, 2, Color4.Azure), program.id, "Content/Textures/noMove.png");
            //TexturedRenderObject noMove = new TexturedRenderObject(ObjectFactory.CreateTexturedCube(size, 1, 1, Color4.Red), program.id, "Content/Textures/noMove.png");
            int y = 23;
            int x = 40;
            //List<QObject> moveTiles = new List<QObject>();
            int count = 0;
            for (int j = 0; j < y; j++)
            {
                for (int i = 0; i < x; i++)
                {
                    // GameObject go = new GameObject(quad, new Vector4((size / 2) + (i * size), (size / 2) + (j * size), -3, 0), new Vector4(0, 0, 0, 0),"Tile" + i *j);

                    if (j == 0 || i == 0 || j == y - 1 || i == x - 1)
                    {
                        GameObject go = new GameObject(noMove, new Vector4((i * size), (j * size), -19, 0), new Vector4(0, 0, 0, 0), "Tile" + i * j);
                        AddGameObject(go);
                        rotatingCubes.Add(go);

                        // map.AddTile(size, size, i, j, Tile.Movement.NO_MOVE);
                        /*
                        QObject qObject = new QObject("movetile" + count);
                        qObject.Add("Position", i + "," + j);
                        qObject.Add("Size", 32 + "," + 32);
                        qObject.Add("Movement", ((int)Tile.Movement.NO_MOVE).ToString());
                        moveTiles.Add(qObject);*/
                    }
                    else
                    {
                        GameObject go = new GameObject(grass, new Vector4((i * size), (j * size), -20, 0), new Vector4(0, 0, 0, 0), "Tile" + i * j);
                        AddGameObject(go);

                        //  map.AddTile(size, size, i, j, Tile.Movement.NORMAL);
                        /*
                        QObject qObject = new QObject("movetile" + count);
                        qObject.Add("Position", i + "," + j);
                        qObject.Add("Size", 32 + "," + 32);
                        qObject.Add("Movement", ((int)Tile.Movement.NORMAL).ToString());
                        moveTiles.Add(qObject);*/
                    }
                    count++;

                }
            }

            /*
            Random random = new Random();
            for(int i = 0; i < 10000; i++)
            {
                Vector4 position = new Vector4(random.Next(-100, 100), random.Next(-100, 100), random.Next(-100, -10), 0);
                GameObject rotatingCube = new GameObject(cube, position, new Vector4(20, 20, 0, 0), "Cube");
                AddGameObject(rotatingCube);
                rotatingCubes.Add(rotatingCube);
            }
            */


            //TexturedRenderObject cube = new TexturedRenderObject(ObjectFactory.CreateTexturedCube(5, 5, 5, Color4.Azure), program.id, "Content/Textures/noMove.png");

            ShaderProgram animatedProgram = GetShaderProgram("animated");
            cube = new TexturedRenderObject(ObjectFactory.CreateTexturedCube(2, 2, 2, Color4.Azure), animatedProgram.id, "Content/Textures/default.png");
            playerCube = new Player(cube, new Vector4(20, 20, -18f, 0), new Vector4(0, 0, 0, 0), "Cube2");
            AddGameObject(playerCube);
            playerCube.AddAnimation("dance", new Animation(4, 11, 2));
            playerCube.AddAnimation("walk", new Animation(0, 3, 1));
            playerCube.PlayAnimation("idle");


            qui.CreateText("Hello World This is scene2", new Vector3(-1, 0.5f, -1), "Segoe UI Black", 12, 0.05f, -0.05f, Color4.Red, true);

            FollowCamera followCamera = new FollowCamera(playerCube,new Vector3(0,0,10),true,gameWindow);

            //ThirdPersonCamera thirdPersonCamera = new ThirdPersonCamera(playerCube, new Vector3(0, 0, 0));

            SetCamera(followCamera);

            //SetCamera(new ThirdPersonCamera(zMovingCube,new Vector3(0,0,0)));
            //SetCamera(new StaticCamera(new Vector3(-20,-20,10),true,gameWindow));
            //SetCamera(new FollowCamera(zMovingCube, gameWindow));

        }

        public override void OnUpdateFrame(FrameEventArgs e)
        {
            for (int i = 0; i < rotatingCubes.Count; i++)
            {
                playerCube.Collision(rotatingCubes[i]);
            }


            if (QKeyboard.Instance.GetKeyDown(Key.Q))
            {
                ThirdPersonCamera thirdPersonCamera = new ThirdPersonCamera(playerCube, camera.position);
                Console.WriteLine(camera.position);
                SetCamera(thirdPersonCamera);
            }

            if (QKeyboard.Instance.GetKeyDown(Key.E))
            {
                FollowCamera followCamera = new FollowCamera(playerCube, new Vector3(0, 0, 10), true, gameWindow);
                SetCamera(followCamera);
            }

            if (QKeyboard.Instance.GetKeyDown(Key.T))
            {
                LoadScene(0);
            }

            if (QKeyboard.Instance.GetKeyDown(Key.Escape))
            {
                gameWindow.Exit();
            }

            if (QKeyboard.Instance.GetKeyDown(Key.A))
            {
                playerCube.SetPosition(playerCube.position.X - (10 * (float)e.Time),
                    playerCube.position.Y,
                    playerCube.position.Z ,
                    playerCube.position.W);
            }

            if (QKeyboard.Instance.GetKeyDown(Key.D))
            {
                playerCube.SetPosition(playerCube.position.X + (10 * (float)e.Time),
                    playerCube.position.Y,
                    playerCube.position.Z,
                    playerCube.position.W);
            }


            if (QKeyboard.Instance.GetKeyDown(Key.W))
            {
                playerCube.SetPosition(playerCube.position.X ,
                    playerCube.position.Y - (10 * (float)e.Time),
                    playerCube.position.Z,
                    playerCube.position.W);
            }

            if (QKeyboard.Instance.GetKeyDown(Key.S))
            {
                playerCube.SetPosition(playerCube.position.X ,
                    playerCube.position.Y + (10 * (float)e.Time),
                    playerCube.position.Z,
                    playerCube.position.W);
            }

            base.OnUpdateFrame(e);
        }
    }
}
