using Quget_Engine_One.GameObjects;
using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;
using Quget_Engine_One.Renderables;
using Quget_Engine_One.Camera;
using OpenTK.Graphics.OpenGL4;

namespace Quget_Engine_One.Gui
{
    class QuiObject : GameObject
    {
        public bool fixedOnCam { protected set; get; }
        public QuiObject(TexturedRenderObject render, Vector4 position, Vector4 rotation, string name, bool fixedOnCam) : base(render, position, rotation, name)
        {

        }
        public override void Render(ICamera camera)
        {
            if(fixedOnCam)
            {
                render.Bind();
                var t2 = Matrix4.CreateTranslation(position.X, position.Y, position.Z);
                var r1 = Matrix4.CreateRotationX(rotation.X);
                var r2 = Matrix4.CreateRotationY(rotation.Y);
                var r3 = Matrix4.CreateRotationZ(rotation.Z);
                var s = Matrix4.CreateScale(scale);
                modelView = r1 * r2 * r3 * s * t2;
                GL.UniformMatrix4(21, false, ref modelView);

                positionRelCam = new Vector3(position);
                render.Render();
            }
            else
            {
                base.Render(camera);
            }

            //base.Render(camera);
        }
    }
}
