using OpenTK;
using Quget_Engine_One.GameObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quget_Engine_One.Camera
{
    class FollowCamera : ICamera
    {
        public Matrix4 positionMatrix { get; private set; }
        public Vector3 position { get; private set; }

        private readonly GameObject target;
        private readonly Vector3 offset;
        private GameWindow gameWindow;
        public FollowCamera(GameObject target,GameWindow gameWindow)
        {
            this.target = target;
            this.offset = new Vector3(0,10,0);
            this.gameWindow = gameWindow;
        }
        public FollowCamera(GameObject target, Vector3 offset, GameWindow gameWindow)
        {
            this.target = target;
            this.offset = offset;
            this.gameWindow = gameWindow;
        }

        public void Update(double time)
        {
            Vector3 translation = new Vector3();
            translation.X = (gameWindow.Width / 2) - target.position.X;
            translation.Y = (gameWindow.Height / 2) - target.position.Y;
            positionMatrix = Matrix4.CreateTranslation(translation);
            position = translation;
        }
    }
}
