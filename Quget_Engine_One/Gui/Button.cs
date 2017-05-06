using Quget_Engine_One.GameObjects;
using System;
using System.Collections.Generic;
using System.Text;
using Quget_Engine_One.Camera;
using OpenTK;
using Quget_Engine_One.Renderables;
using OpenTK.Input;
using OpenTK.Graphics.OpenGL4;

namespace Quget_Engine_One.Gui
{
    class Button : QuiObject
    {
        private Text.Label renderText;
        private bool clicked = false;
        private int offsetIndex = 0;
        public Button(TexturedRenderObject render, Vector4 position, Vector4 rotation, string name, Text.Label renderText,bool fixedOnCam) : base(render, position, rotation, name,fixedOnCam)
        {
            this.fixedOnCam = fixedOnCam;
            this.renderText = renderText;
        }
        protected override void OnMouseUp(MouseButtonEventArgs mouse)
        {
            if (clicked)
            {
                clicked = false;
                offsetIndex = 0;
                onButtonClick?.Invoke(this, mouse);
            }
            base.OnMouseUp(mouse);
        }
        protected override void OnMouseDown(MouseButtonEventArgs mouse)
        {
            if(!clicked)
            {
                clicked = true;
                offsetIndex = 2;
            }
            base.OnMouseDown(mouse);
        }
        public override void Update(double time)
        {
            base.Update(time);
        }
        public override void Render(ICamera camera)
        {
            float indexX = (float)Math.Floor(offsetIndex % 4.0f);
            float indexY = (float)Math.Floor(offsetIndex / 4.0f);
            float offsetX = 0.25f * indexX;
            float offsetY = 0.25f * indexY;

            Vector2 offset = new Vector2(offsetX, offsetY);
            GL.VertexAttrib2(3, offset);

            base.Render(camera);
            renderText.Render(camera);

        }

        public delegate void OnButtonClick(Button sender, MouseButtonEventArgs e);
        public event OnButtonClick onButtonClick;
    }
}
