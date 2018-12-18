using System;
using System.Collections.Generic;
using System.Text;
using Quget_Engine_One.GameObjects;
using Quget_Engine_One.Renderables;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using Quget_Engine_One.Camera;
using Quget_Engine_One.Gui;

namespace Quget_Engine_One.Gui.Text
{
    /// <summary>
    /// A text label. Object
    /// </summary>
    class Label : QuiObject
    {
        public const string Characters = @"qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM0123456789µ§½!""#¤%&/()=?^*@£€${[]}\~¨'-_.:,;<>|°©®±¥";
        private static readonly Dictionary<char, int> Lookup;
        public static readonly float CharacterWidthNormalized;
        // 21x48 per char, 
        public readonly List<RenderCharacter> text;
        public override float width
        {
            get
            {
                return render.width * (text.Count);
            }
        }
        public override float height
        {
            get
            {
                return render.height;
            }
        }
        static Label()
        {
            Lookup = new Dictionary<char, int>();
            for (int i = 0; i < Characters.Length; i++)
            {
                if (!Lookup.ContainsKey(Characters[i]))
                    Lookup.Add(Characters[i], i);
            }
            CharacterWidthNormalized = 1f / Characters.Length;
        }

        //public RenderText(Renderable model, Vector4 position, Color4 color, string value): base(model, position, Vector4.Zero, Vector4.Zero, 0)
        public Label(TexturedRenderObject render, Vector4 position, Vector4 rotation, string value, bool fixedOnCam) : base(render, position, rotation,"Text",fixedOnCam)
        {
            this.fixedOnCam = fixedOnCam;
            text = new List<RenderCharacter>(value.Length);
            SetText(value);
        }

        public void SetText(string value)
        {
            text.Clear();
            for (int i = 0; i < value.Length; i++)
            {
                int offset;
                if (Lookup.TryGetValue(value[i], out offset))
                {
                    /*
                    var c = new RenderCharacter(Model,
                        new Vector4(_position.X + (i * 0.015f),
                            _position.Y,
                            _position.Z,
                            _position.W),

                        (offset * CharacterWidthNormalized));*/
                    float spacing = render.width;
                    RenderCharacter c = new RenderCharacter(render,
                        new Vector4(position.X + (i * spacing), position.Y, position.Z, position.W),
                        Vector4.Zero,
                        (offset * CharacterWidthNormalized),fixedOnCam);
                    c.SetScale(scale);
                    text.Add(c);
                }
            }
        }
        
        public override void Render(ICamera camera)
        {
           // render.Bind();
            //GL.VertexAttrib4(3, _color);
            for (int i = 0; i < text.Count; i++)
            {
                text[i].Render(camera);
            }
        }
    }
}
