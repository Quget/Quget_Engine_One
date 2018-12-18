using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
using Quget_Engine_One.Camera;
using Quget_Engine_One.GameObjects;
using Quget_Engine_One.Gui;
using Quget_Engine_One.Renderables;
using Quget_Engine_One.Sound;
using Quget_Engine_One.Gui.Text;
using System;
using System.Collections.Generic;
using System.Text;
using Quget_Engine_One.CollisionMap;
using QConfig;
using Quget_Engine_One.Scenes;

namespace Quget_Engine_One
{
    /// <summary>
    /// Holds the game Window and scenes.
    /// </summary>
    class Game
    {
        private GameWindow gameWindow;
        public Game()
        {
            
            gameWindow = new GameWindow();
            gameWindow.onWindowLoad += GameWindow_onWindowLoad;
            gameWindow.Run(60, 60);
            //gameWindow.Run(0, 0);


        }
        private void GameWindow_onWindowLoad(GameWindow sender, EventArgs e)
        {
            gameWindow.AddScene(new Scene01(gameWindow));
            gameWindow.AddScene(new Scene02(gameWindow));
            gameWindow.LoadScene(1);
        }
    }
}
