using OpenTK;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quget_Engine_One.Camera
{
    class StaticCamera : ICamera
    {
        public Matrix4 positionMatrix { get; }
        public Vector3 position { get; private set; }
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
        public void Update(double time)
        { }
    }
}
