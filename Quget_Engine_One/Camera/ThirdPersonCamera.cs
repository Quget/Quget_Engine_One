using OpenTK;
using Quget_Engine_One.GameObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quget_Engine_One.Camera
{
    class ThirdPersonCamera : ICamera
    {
        public Matrix4 positionMatrix { get; private set; }
        public Vector3 position { get; set; }

        private readonly GameObject target;
        private readonly Vector3 offset;

        public ThirdPersonCamera(GameObject target,Vector3 position)
        {
            this.target = target;
            this.position = position;
        }

        public void Update(double time)
        {
            positionMatrix = Matrix4.LookAt(new Vector3(-position.X,position.Y,-position.Z),
                new Vector3(target.position.X, target.position.Y, target.position.Z),
                Vector3.UnitZ);
            //throw new NotImplementedException();
        }
    }
}
