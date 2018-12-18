using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;
using Quget_Engine_One.Renderables;

namespace Quget_Engine_One.GameObjects
{
    class Pickup : GameObject
    {
        public Pickup(TexturedRenderObject render, Vector4 position, Vector4 rotation, string name) : base(render, position, rotation, name)
        {

        }

        protected override void OnCollision(GameObject other)
        {
            //base.OnCollision(other);
        }
    }
}
