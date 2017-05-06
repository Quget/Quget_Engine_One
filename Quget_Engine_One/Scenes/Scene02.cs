using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;

namespace Quget_Engine_One.Scenes
{
    class Scene02 : Scene
    {
        public Scene02(GameWindow gameWindow) : base(gameWindow)
        {
        }
        public override void OnLoad()
        {
            qui.CreateText("Hello World This is scene2", new Vector2(20, 24), "Segoe UI Black", 48, Color4.Red, true);
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
            base.OnUpdateFrame(e);
        }
    }
}
