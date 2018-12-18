using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;
using OpenTK.Input;
using Quget_Engine_One.Renderables;

namespace Quget_Engine_One.GameObjects
{
    class Player : AnimatedGameObject
    {
        private Vector4 startPos;

        public float Score { get; private set; }

        public Player(TexturedRenderObject render, Vector4 position, Vector4 rotation, string name) : base(render, position, rotation, name)
        {
            startPos = position;
        }

        protected override void OnCollision(GameObject other)
        {
            if (other.disposed)
                return;

            Pickup pickup = other as Pickup;
            if (pickup != null && !pickup.disposed)
            {
                Score++;
                other.Remove();
                other = null;
                return;
            }
            SetPosition(startPos.X, startPos.Y, startPos.Z, startPos.W);
            base.OnCollision(other);
        }

        protected override void OnMouseDown(MouseButtonEventArgs mouse)
        {
            base.OnMouseDown(mouse);
        }

        protected override void OnMouseUp(MouseButtonEventArgs mouse)
        {
            base.OnMouseUp(mouse);
        }
    }
}
