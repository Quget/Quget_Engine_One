using OpenTK;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quget_Engine_One.Camera
{
    public interface ICamera
    {
        Matrix4 positionMatrix { get; }
        Vector3 position { get;}
        void Update(double time);
    }
}
