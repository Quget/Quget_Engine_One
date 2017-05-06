using System;
using System.Collections.Generic;
using System.Text;

namespace Quget_Engine_One
{
    class QMouse
    {
        private static QMouse instance;
        public static QMouse Instance
        {
            get
            {
                if (instance == null)
                {
                    throw new Exception("Keyboard not instance created");
                }
                return instance;
            }
        }

        public QMouse(GameWindow gameWindow)
        {
            gameWindow.MouseDown += GameWindow_MouseDown;
            gameWindow.MouseUp += GameWindow_MouseUp;
            gameWindow.MouseWheel += GameWindow_MouseWheel;
        }

        private void GameWindow_MouseWheel(object sender, OpenTK.Input.MouseWheelEventArgs e)
        {

        }

        private void GameWindow_MouseUp(object sender, OpenTK.Input.MouseButtonEventArgs e)
        {
            onMouseUp?.Invoke(this, e);
        }

        private void GameWindow_MouseDown(object sender, OpenTK.Input.MouseButtonEventArgs e)
        {
            onMouseDown?.Invoke(this, e);
        }

        public static void Create(GameWindow gameWindow)
        {
            if (instance == null)
                instance = new QMouse(gameWindow);
        }


        public delegate void OnMouseDown(QMouse sender, OpenTK.Input.MouseButtonEventArgs e);
        public event OnMouseDown onMouseDown;

        public delegate void OnMouseUp(QMouse sender, OpenTK.Input.MouseButtonEventArgs e);
        public event OnMouseUp onMouseUp;
    }
}
