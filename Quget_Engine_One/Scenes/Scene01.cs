using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;
using Quget_Engine_One.GameObjects;
using Quget_Engine_One.CollisionMap;
using QConfig;
using Quget_Engine_One.Renderables;
using OpenTK.Graphics;
using Quget_Engine_One.Gui;
using Quget_Engine_One.Camera;
using Quget_Engine_One.Sound;
using OpenTK.Input;

namespace Quget_Engine_One.Scenes
{
    class Scene01 : Scene
    {
        private AnimatedGameObject testPlayer;
        private int speed = 90;
        private GameObject chestGo;
        private Map map;

        public Scene01(GameWindow gameWindow) : base(gameWindow)
        {

        }
        public override void OnLoad()
        {
            base.OnLoad();
            ShaderProgram program = GetShaderProgram("default");
            int size = 32;

            map = new Map(23, 40);
            TexturedRenderObject grass = new TexturedRenderObject(ObjectFactory.CreateTexturedQuad(size, size, 1, 1, Color4.White), program.id, "Content/Textures/grass.png");
            TexturedRenderObject noMove = new TexturedRenderObject(ObjectFactory.CreateTexturedQuad(size, size, 1, 1, Color4.Red), program.id, "Content/Textures/noMove.png");
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
                        GameObject go = new GameObject(noMove, new Vector4((i * size), (j * size), -3, 0), new Vector4(0, 0, 0, 0), "Tile" + i * j);
                        AddGameObject(go);

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
                        GameObject go = new GameObject(grass, new Vector4((i * size), (j * size), -3, 0), new Vector4(0, 0, 0, 0), "Tile" + i * j);
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
            //Writer.Write("Content/Levels/moveTile.qgt", moveTiles);
            LoadMoveTiles();


            TexturedRenderObject chest = new TexturedRenderObject(ObjectFactory.CreateTexturedQuad(64, 32, 1, 1, Color4.Gold), program.id, "Content/Textures/chest.png");
            chestGo = new GameObject(chest, new Vector4(512, 128, -2, 0), new Vector4(0, 0, 0, 0), "Chest");
            AddGameObject(chestGo);
            map.UpdateTile(16, 4, Tile.Movement.NO_MOVE);

            ShaderProgram animatedProgram = GetShaderProgram("animated");
            TexturedRenderObject playerQuad = new TexturedRenderObject(ObjectFactory.CreateTexturedQuad(32, 32, 4, 4, Color4.DarkViolet), animatedProgram.id, "Content/Textures/character.png");
            testPlayer = new AnimatedGameObject(playerQuad, new Vector4(128, 128, -2, 0), new Vector4(0, 0, 0, 0), "Player");
            AddGameObject(testPlayer);

            qui.CreateText("Hello World", new Vector2(20, 24), "Segoe UI Black", 48, Color4.Red, true);

            qui.CreateText("Hello World", new Vector2(20, 72), "Comic Sans MS", 48, Color4.Black, false);

            qui.CreateText("Hello World", new Vector2(20, 120), "Segoe Script", 48, Color4.Blue, true);

            Button button = qui.CreateButton("Dance", new Vector2(100, 300), new Vector2(128, 32), "Comic Sans MS", 16, Color4.OrangeRed, Color4.Black, true);
            button.onButtonClick += Button_onButtonClick;


            Button button2 = qui.CreateButton("Dance2", new Vector2(100, 300), new Vector2(128, 32), "Comic Sans MS", 16, Color4.OrangeRed, Color4.Black, false);
            button2.onButtonClick += Button_onButtonClick;

            testPlayer.AddAnimation("dance", new Animation(4, 11, 2));
            testPlayer.AddAnimation("walk", new Animation(0, 3, 1));
            testPlayer.PlayAnimation("idle");

            SetCamera(new FollowCamera(testPlayer, gameWindow));
            //gameWindow.SetCamera(new StaticCamera( new Vector3(gameWindow.Width/ 2, gameWindow.Height/ 2,0)));

            QSound.SetVolume(QSound.SoundType.Music, 80);
        }
        private void Button_onButtonClick(Button sender, MouseButtonEventArgs e)
        {
            testPlayer.StopAllAnimations();
            testPlayer.PlayAnimation("dance");

            if (!QSound.IsPlaying("Dance"))
                QSound.PlayFFmpeg("Content/Audio/test.wav", "Dance", QSound.SoundType.Music);
            Console.WriteLine("Button Clicked");
        }

        private void WalkAnimation()
        {
            if (!testPlayer.IsPlaying("walk"))
            {
                testPlayer.StopAllAnimations();
                testPlayer.PlayAnimation("walk");
            }
            if (!QSound.IsPlaying("Walk"))
                QSound.PlayFFmpeg("Content/Audio/walk.wav", "Walk", QSound.SoundType.Effect);
        }
        public override void OnUpdateFrame(FrameEventArgs e)
        {
            if (QKeyboard.Instance.GetKeyDown(Key.W))
            {
                int x = (int)testPlayer.position.X / 32;
                int y = (int)testPlayer.position.Y / 32;

                Tile tile = map.GetTile(x, y - 1);
                Console.WriteLine("\n{0}:{1}->{2}", x, y, tile.movement);
                testPlayer.SetRotation(0, 0, -90, 0);
                if (tile.movement != Tile.Movement.NO_MOVE)
                {
                    testPlayer.MoveTo(testPlayer.position.X, (y - 1) * 32, speed);
                    WalkAnimation();
                }
            }
            if (QKeyboard.Instance.GetKeyDown(Key.S))
            {
                int x = (int)testPlayer.position.X / 32;
                int y = (int)testPlayer.position.Y / 32;

                Tile tile = map.GetTile(x, y + 1);
                Console.WriteLine("\n{0}:{1}->{2}", x, y, tile.movement);
                testPlayer.SetRotation(0, 0, 90, 0);
                if (tile.movement != Tile.Movement.NO_MOVE)
                {
                    testPlayer.MoveTo(testPlayer.position.X, (y + 1) * 32, speed);
                    WalkAnimation();
                }
            }
            if (QKeyboard.Instance.GetKeyDown(Key.A))
            {

                int x = (int)(testPlayer.position.X / 32);
                int y = (int)testPlayer.position.Y / 32;

                Tile tile = map.GetTile(x - 1, y);
                Console.WriteLine("\n{0}:{1}->{2}", x, y, tile.movement);
                testPlayer.SetRotation(0, 0, -180, 0);
                if (tile.movement != Tile.Movement.NO_MOVE)
                {
                    testPlayer.MoveTo((x - 1) * 32, testPlayer.position.Y, speed);
                    WalkAnimation();
                }
            }
            if (QKeyboard.Instance.GetKeyDown(Key.D))
            {


                int x = (int)testPlayer.position.X / 32;
                int y = (int)testPlayer.position.Y / 32;

                Tile tile = map.GetTile(x + 1, y);
                Console.WriteLine("\n{0}:{1}->{2}", x, y, tile.movement);
                testPlayer.SetRotation(0, 0, 0, 0);
                if (tile.movement != Tile.Movement.NO_MOVE)
                {
                    testPlayer.MoveTo((x + 1) * 32, testPlayer.position.Y, speed);
                    // map.UpdateTile(x, y, Tile.Movement.NORMAL);
                    //map.UpdateTile(x + 1, y, Tile.Movement.NO_MOVE);
                    WalkAnimation();
                }

            }
            if (QKeyboard.Instance.GetKeyDown(Key.E))
            {
                testPlayer.StopAllAnimations();
                testPlayer.PlayAnimation("dance");

                if (!QSound.IsPlaying("Dance"))
                    QSound.PlayFFmpeg("Content/Audio/test.wav", "Dance", QSound.SoundType.Music);
            }
            if(QKeyboard.Instance.GetKeyDown(Key.R))
            {
                LoadScene(1);
            }
            if (QKeyboard.Instance.GetKeyDown(Key.Escape))
            {
                gameWindow.Exit();
            }

            base.OnUpdateFrame(e);
        }
        private void LoadMoveTiles()
        {
            List<QObject> moveTiles = Reader.LoadFile("Content/Levels/moveTile.qgt");
            for (int i = 0; i < moveTiles.Count; i++)
            {
                string[] position = moveTiles[i].GetValue("Position").Split(',');
                string[] size = moveTiles[i].GetValue("Size").Split(',');
                string movement = moveTiles[i].GetValue("Movement");

                int x = int.Parse(position[0]);
                int y = int.Parse(position[1]);
                int width = int.Parse(size[0]);
                int height = int.Parse(size[1]);
                Tile.Movement move = (Tile.Movement)int.Parse(movement);
                map.AddTile(width, height, x, y, move);
                // map.AddTile(32, 32, i, j, Tile.Movement.NO_MOVE);
            }
        }
    }
}
