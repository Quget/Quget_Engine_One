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
        public Vector3 position { get; set; }

        private bool threeD = false;

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

        public FollowCamera(GameObject target, Vector3 offset,bool threeD, GameWindow gameWindow)
        {
            this.target = target;
            this.offset = offset;
            this.threeD = threeD;
            this.gameWindow = gameWindow;
        }

        public void Update(double time)
        {
            Vector3 translation = new Vector3();

            if(!threeD)
            {
                translation.X = (gameWindow.Width / 2) - target.position.X;
                translation.Y = (gameWindow.Height / 2) - target.position.Y;
                //translation.Z = 10;
            }
            else
            {
                translation.X = -(target.position.X - offset.X);
                translation.Y = -(target.position.Y - offset.Y);
                translation.Z = (target.position.Z - offset.Z);
            }

            positionMatrix = Matrix4.CreateTranslation(translation);
            position = translation;
        }
    }
}
