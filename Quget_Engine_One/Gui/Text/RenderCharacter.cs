using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using Quget_Engine_One.Camera;
using Quget_Engine_One.GameObjects;
using Quget_Engine_One.Gui;
using Quget_Engine_One.Renderables;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quget_Engine_One.Gui.Text
{
    class RenderCharacter : QuiObject
    {
        private float _offset;
        // public RenderCharacter(Renderable model, Vector4 position, float charOffset): base(model, position, Vector4.Zero, Vector4.Zero, 0)
        public RenderCharacter(TexturedRenderObject render, Vector4 position, Vector4 rotation,float charOffset, bool fixedOnCam) : base(render, position, rotation,"Char",fixedOnCam)
        {
            this.fixedOnCam = fixedOnCam;
            _offset = charOffset;
           // SetScale(0.2f,0.2f,0.2f);
        }

        public void SetChar(float charOffset)
        {
            _offset = charOffset;
        }

        
        public override void Render(ICamera camera)
        {
            GL.VertexAttrib2(3, new Vector2(_offset, 0));
            base.Render(camera);
        }
    }
}
