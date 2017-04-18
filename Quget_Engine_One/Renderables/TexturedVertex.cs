using OpenTK;
using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quget_Engine_One.Renderables
{
    public struct TexturedVertex
    {
        public const int Size = (4 + 2 + 4) * 4; // size of struct in bytes

        private readonly Vector4 position;
        private readonly Vector2 textureCoordinate;
        private readonly Color4 color;
        public TexturedVertex(Vector4 position, Vector2 textureCoordinate, Color4 color)
        {
            this.position = position;
            this.textureCoordinate = textureCoordinate;
            this.color = color;
        }
    }
}
