using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quget_Engine_One
{
    /// <summary>
    /// Listen to keyboard inputs.
    /// </summary>
    class QKeyboard
    {
        private static QKeyboard instance;
        public static QKeyboard Instance
        {
            get
            {
                if(instance == null)
                {
                    throw new Exception("Keyboard not instance created");
                }
                return instance;
            }
        }
        private Dictionary<Key, bool> keyDown = new Dictionary<Key, bool>();

        public QKeyboard(GameWindow gameWindow)
        {
            gameWindow.KeyDown += Keyboard_KeyDown;
            gameWindow.KeyUp += Keyboard_KeyUp;
        }
        public static void Create(GameWindow gameWindow)
        {
            if(instance == null)
                instance = new QKeyboard(gameWindow);
        }
        private void Keyboard_KeyUp(object sender, KeyboardKeyEventArgs e)
        {
            if (keyDown.ContainsKey(e.Key))
            {
                keyDown[e.Key] = false;
            }
            else
            {
                keyDown.Add(e.Key, false);
            }
        }

        private void Keyboard_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            if (keyDown.ContainsKey(e.Key))
            {
                keyDown[e.Key] = true;
            }
            else
            {
                keyDown.Add(e.Key, true);
            }
        }

        public bool GetKeyDown(Key key)
        {
            if (keyDown.ContainsKey(key))
            {
                return keyDown[key];
            }
            else
            {
                return false;
            }
        }
    }

}
