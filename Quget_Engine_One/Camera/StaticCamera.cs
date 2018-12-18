using OpenTK;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quget_Engine_One.Camera
{
    /// <summary>
    /// Just a static camera.
    /// </summary>
    class StaticCamera : ICamera
    {
        public Matrix4 positionMatrix { get; }
        public Vector3 position { get; set; }
        public StaticCamera()
        {
            Vector3 position;
            position.X = 0;
            position.Y = 0;
            position.Z = 0;
            positionMatrix = Matrix4.LookAt(position, -Vector3.UnitZ, Vector3.UnitY);
            this.position = position;
        }
        public StaticCamera(Vector3 position, GameWindow gameWindow)
        {
            Vector3 translation = new Vector3();
            translation.X = (gameWindow.Width / 2) - position.X;
            translation.Y = (gameWindow.Height / 2) - position.Y;
            translation.Z = 0;
            positionMatrix = Matrix4.CreateTranslation(translation);
            position = translation;
        }

        public StaticCamera(Vector3 position, bool threeD,GameWindow gameWindow)
        {
            Vector3 translation = new Vector3();
            if(threeD)
            {
                translation.X =  position.X;
                translation.Y =  position.Y;
                translation.Z = position.Z;
            }
            else
            {
                translation.X = (gameWindow.Width / 2) - position.X;
                translation.Y = (gameWindow.Height / 2) - position.Y;
                translation.Z = 0;
            }

            positionMatrix = Matrix4.CreateTranslation(translation);
            position = translation;
        }
        public void Update(double time)
        { }
    }
}
