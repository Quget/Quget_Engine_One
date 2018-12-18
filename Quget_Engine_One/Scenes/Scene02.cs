using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
using QConfig;
using Quget_Engine_One.Camera;
using Quget_Engine_One.GameObjects;
using Quget_Engine_One.Gui.Text;
using Quget_Engine_One.Renderables;

namespace Quget_Engine_One.Scenes
{
    class Scene02 : Scene
    {
        private List<GameObject> noMoveCubes = new List<GameObject>();

        private Player playerCube;

        private Label score;

        private float spawnTimer = 0;
        private float timeToSpawn = 10;

        private List<Pickup> pickUps = new List<Pickup>();
        public Scene02(GameWindow gameWindow) : base(gameWindow)
        {

        }

        public override void OnLoad()
        {
            gameWindow.ToPerspective();
            base.OnLoad();

            ShaderProgram program = GetShaderProgram("default");

            TexturedRenderObject cube = new TexturedRenderObject(ObjectFactory.CreateTexturedCube(2, 2, 2, Color4.Azure), program.id, "Content/Textures/noMove.png");

           
            TexturedRenderObject grass = new TexturedRenderObject(ObjectFactory.CreateTexturedCube(2, 2, 2, Color4.Azure), program.id, "Content/Textures/grass.png");
            TexturedRenderObject noMove = new TexturedRenderObject(ObjectFactory.CreateTexturedCube(2, 2, 2, Color4.Azure), program.id, "Content/Textures/noMove.png");
            
            int size = 2;
            int y = 23;
            int x = 40;
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
                        noMoveCubes.Add(go);

                    }
                    else
                    {
                        GameObject go = new GameObject(grass, new Vector4((i * size), (j * size), -20, 0), new Vector4(0, 0, 0, 0), "Tile" + i * j);
                        AddGameObject(go);
                    }
                    count++;

                }
            }
            
            ShaderProgram animatedProgram = GetShaderProgram("animated");
            cube = new TexturedRenderObject(ObjectFactory.CreateTexturedCube(2, 2, 2, Color4.Azure), animatedProgram.id, "Content/Textures/default.png");
            playerCube = new Player(cube, new Vector4(20, 20, -18f, 0), new Vector4(0, 0, 0, 0), "Player");
            AddGameObject(playerCube);
            playerCube.AddAnimation("dance", new Animation(4, 11, 2));
            playerCube.AddAnimation("walk", new Animation(0, 3, 1));
            playerCube.PlayAnimation("idle");


            score = qui.CreateText("0", new Vector3(-1, 0.5f, -1), "Segoe UI Black", 12, 0.05f, -0.05f, Color4.Red, true);

            FollowCamera followCamera = new FollowCamera(playerCube,new Vector3(0,0,10),true,gameWindow);

            SetCamera(followCamera);
            SpawnPickUp();
        }

        private void SpawnPickUp()
        {
            ShaderProgram program = GetShaderProgram("default");
            TexturedRenderObject cube = new TexturedRenderObject(ObjectFactory.CreateTexturedCube(2, 2, 2, Color4.OrangeRed), program.id, "Content/Textures/default.png");
            Random random = new Random();
            
            Pickup pickup = new Pickup(cube, new Vector4(random.Next(5, 50), random.Next(5, 50), -18f, 0), new Vector4(0, 0, 0, 0), "PickUp");
            AddGameObject(pickup);
            pickUps.Add(pickup);

            //playerCube = new Player(cube, new Vector4(20, 20, -18f, 0), new Vector4(0, 0, 0, 0), "Cube2");
        }

        public override void OnUpdateFrame(FrameEventArgs e)
        {
            spawnTimer += (float)e.Time;
            if(spawnTimer > timeToSpawn)
            {
                SpawnPickUp();
                spawnTimer = 0;
            }
            score.SetText("Score: " + playerCube.Score);

            for (int i = 0; i < pickUps.Count; i++)
            {
                if(pickUps[i] == null)
                {
                    pickUps.RemoveAt(0);
                    i = -1;
                    continue;
                }
                playerCube.Collision(pickUps[i]);
            }

            for (int i = 0; i < noMoveCubes.Count; i++)
            {
                playerCube.Collision(noMoveCubes[i]);
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
