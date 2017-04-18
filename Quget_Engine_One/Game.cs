using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
using Quget_Engine_One.GameObjects;
using Quget_Engine_One.Renderables;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quget_Engine_One
{
    class Game
    {
        private GameObject testPlayer;
        private GameWindow gameWindow;
        private int speed = 90;
        public Game()
        {
            gameWindow = new GameWindow();
            gameWindow.onUpdate += GameWindow_onUpdate;
            gameWindow.onWindowLoad += GameWindow_onWindowLoad;
            gameWindow.Run(60, 60);
        }

        private void GameWindow_onWindowLoad(GameWindow sender, EventArgs e)
        {
            ShaderProgram program = gameWindow.GetShaderProgram("default");
            int size = 32;
            TexturedRenderObject quad = new TexturedRenderObject(ObjectFactory.CreateTexturedQuad(size, 32, 32, Color4.White), program.id, "Content/Textures/grass.png");
            for (int j = 0; j < 23; j++)
            {
                for (int i = 0; i < 40; i++)
                {
                    GameObject go = new GameObject(quad, new Vector4((size / 2) + (i * size), (size / 2) + (j * size), -2, 0), new Vector4(0, 0, 0, 0));
                    gameWindow.AddGameObject(go);
                }
            }
            ShaderProgram animatedProgram = gameWindow.GetShaderProgram("animated");
            TexturedRenderObject playerQuad = new TexturedRenderObject(ObjectFactory.CreateTexturedQuad(32, 32, 32, Color4.Red), animatedProgram.id, "Content/Textures/character.png");
            testPlayer = new GameObject(playerQuad, new Vector4(32 / 2, 32 / 2, -1, 0), new Vector4(0, 0, 0, 0));
            gameWindow.AddGameObject(testPlayer);

        }
        private void GameWindow_onUpdate(GameWindow sender, OpenTK.FrameEventArgs e)
        {
            if (QKeyboard.Instance.GetKeyDown(Key.W))
            {
                testPlayer.position = new Vector4(testPlayer.position.X, testPlayer.position.Y - (speed * (float)e.Time), testPlayer.position.Z, testPlayer.position.W);
                testPlayer.SetRotation(0, 0, -90, 0);
            }
            if (QKeyboard.Instance.GetKeyDown(Key.S))
            {
                testPlayer.position = new Vector4(testPlayer.position.X, testPlayer.position.Y + (speed * (float)e.Time), testPlayer.position.Z, testPlayer.position.W);
                testPlayer.SetRotation(0, 0, 90, 0);
            }
            if (QKeyboard.Instance.GetKeyDown(Key.A))
            {
                testPlayer.position = new Vector4(testPlayer.position.X - (speed * (float)e.Time), testPlayer.position.Y, testPlayer.position.Z, testPlayer.position.W);
                testPlayer.SetRotation(0, 0, -180, 0);
            }
            if (QKeyboard.Instance.GetKeyDown(Key.D))
            {
                testPlayer.position = new Vector4(testPlayer.position.X + (speed * (float)e.Time), testPlayer.position.Y, testPlayer.position.Z, testPlayer.position.W);
                testPlayer.SetRotation(0, 0, 0, 0);
            }
            if (QKeyboard.Instance.GetKeyDown(Key.Escape))
            {
                gameWindow.Exit();
            }
        }
    }
}
