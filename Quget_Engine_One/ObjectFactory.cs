using OpenTK;
using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Quget_Engine_One.Renderables;
using Quget_Engine_One.Gui.Text;

namespace Quget_Engine_One
{
    class ObjectFactory
    {


        public static TexturedVertex[] CreateTexturedCube(float side, float textureWidth, float textureHeight, Color4 color)
        {
            float h = 1;
            float w = 1;

            side = side / 2f; // half side - and other half

            TexturedVertex[] vertices =
            {
                new TexturedVertex(new Vector4(-side, -side, -side, 1.0f),   new Vector2(0, 0), color),
                new TexturedVertex(new Vector4(-side, -side, side, 1.0f),    new Vector2(0, h), color),
                new TexturedVertex(new Vector4(-side, side, -side, 1.0f),    new Vector2(w, 0), color),
                new TexturedVertex(new Vector4(-side, side, -side, 1.0f),    new Vector2(w, 0), color),
                new TexturedVertex(new Vector4(-side, -side, side, 1.0f),    new Vector2(0, h), color),
                new TexturedVertex(new Vector4(-side, side, side, 1.0f),     new Vector2(w, h), color),

                new TexturedVertex(new Vector4(side, -side, -side, 1.0f),    new Vector2(0, 0), color),
                new TexturedVertex(new Vector4(side, side, -side, 1.0f),     new Vector2(w, 0), color),
                new TexturedVertex(new Vector4(side, -side, side, 1.0f),     new Vector2(0, h), color),
                new TexturedVertex(new Vector4(side, -side, side, 1.0f),     new Vector2(0, h), color),
                new TexturedVertex(new Vector4(side, side, -side, 1.0f),     new Vector2(w, 0), color),
                new TexturedVertex(new Vector4(side, side, side, 1.0f),      new Vector2(w, h), color),

                new TexturedVertex(new Vector4(-side, -side, -side, 1.0f),   new Vector2(0, 0), color),
                new TexturedVertex(new Vector4(side, -side, -side, 1.0f),    new Vector2(w, 0), color),
                new TexturedVertex(new Vector4(-side, -side, side, 1.0f),    new Vector2(0, h), color),
                new TexturedVertex(new Vector4(-side, -side, side, 1.0f),    new Vector2(0, h), color),
                new TexturedVertex(new Vector4(side, -side, -side, 1.0f),    new Vector2(w, 0), color),
                new TexturedVertex(new Vector4(side, -side, side, 1.0f),     new Vector2(w, h), color),

                new TexturedVertex(new Vector4(-side, side, -side, 1.0f),    new Vector2(0, 0), color),
                new TexturedVertex(new Vector4(-side, side, side, 1.0f),     new Vector2(0, h), color),
                new TexturedVertex(new Vector4(side, side, -side, 1.0f),     new Vector2(w, 0), color),
                new TexturedVertex(new Vector4(side, side, -side, 1.0f),     new Vector2(w, 0), color),
                new TexturedVertex(new Vector4(-side, side, side, 1.0f),     new Vector2(0, h), color),
                new TexturedVertex(new Vector4(side, side, side, 1.0f),      new Vector2(w, h), color),
                                                                                              
                new TexturedVertex(new Vector4(-side, -side, -side, 1.0f),   new Vector2(0, 0), color),
                new TexturedVertex(new Vector4(-side, side, -side, 1.0f),    new Vector2(0, h), color),
                new TexturedVertex(new Vector4(side, -side, -side, 1.0f),    new Vector2(w, 0), color),
                new TexturedVertex(new Vector4(side, -side, -side, 1.0f),    new Vector2(w, 0), color),
                new TexturedVertex(new Vector4(-side, side, -side, 1.0f),    new Vector2(0, h), color),
                new TexturedVertex(new Vector4(side, side, -side, 1.0f),     new Vector2(0, 0), color),
                                                                                              
                new TexturedVertex(new Vector4(-side, -side, side, 1.0f),    new Vector2(0, 0), color),
                new TexturedVertex(new Vector4(side, -side, side, 1.0f),     new Vector2(w, 0), color),
                new TexturedVertex(new Vector4(-side, side, side, 1.0f),     new Vector2(0, h), color),
                new TexturedVertex(new Vector4(-side, side, side, 1.0f),     new Vector2(0, h), color),
                new TexturedVertex(new Vector4(side, -side, side, 1.0f),     new Vector2(w, 0), color),
                new TexturedVertex(new Vector4(side, side, side, 1.0f),      new Vector2(w, h), color),
            };
            return vertices;
        }
        public static TexturedVertex[] CreateTexturedQuad(float width, float height,float spriteX,float spriteY, Color4 color)
        {
            //side = side / 2f; // half side - and other half
            width = width / 2f;
            height = height / 2f;
            float w = 1.0f / spriteX;
            float h = 1.0f / spriteY;
            //float h = 32;//textureHeight;
            //float w = 32;// textureWidth;
            TexturedVertex[] vertices =
            {
                 new TexturedVertex(new Vector4(-width, -height, 0, 1.0f),    new Vector2(0, 0), color),
                new TexturedVertex(new Vector4(width, -height, 0, 1.0f),     new Vector2(w, 0), color),
                new TexturedVertex(new Vector4(-width, height, 0, 1.0f),     new Vector2(0, h), color),
                new TexturedVertex(new Vector4(-width, height, 0, 1.0f),     new Vector2(0, h), color),
                new TexturedVertex(new Vector4(width, -height, 0, 1.0f),     new Vector2(w, 0), color),
                new TexturedVertex(new Vector4(width, height, 0, 1.0f),      new Vector2(w, h), color),
            };
            return vertices;
        }
        public static TexturedVertex[] CreateTexturedCharacter(float width, float height,Color4 color)
        {
            float h = 1;
            float w = Label.CharacterWidthNormalized;
            width = width / 2f;
            height = height / 2f;
            //width = side;
           // height = side;
            float depth = 0;
            TexturedVertex[] vertices =
            {
                new TexturedVertex(new Vector4(-width, -height, depth, 1.0f),    new Vector2(0, 0), color),
                new TexturedVertex(new Vector4(width, -height, depth, 1.0f),     new Vector2(w, 0), color),
                new TexturedVertex(new Vector4(-width, height, depth, 1.0f),     new Vector2(0, h), color),
                new TexturedVertex(new Vector4(-width, height, depth, 1.0f),     new Vector2(0, h), color),
                new TexturedVertex(new Vector4(width, -height, depth, 1.0f),     new Vector2(w, 0), color),
                new TexturedVertex(new Vector4(width, height, depth, 1.0f),      new Vector2(w, h), color),
            };
            return vertices;
        }
        public static TexturedVertex[] CreateTexturedQuad(float side, float textureWidth, float textureHeight, Color4 color)
        {
            side = side / 2f; // half side - and other half
            float h = 1;
            float w = 1;
            //float h = 32;//textureHeight;
            //float w = 32;// textureWidth;
            TexturedVertex[] vertices =
            {
                 new TexturedVertex(new Vector4(-side, -side, 0, 1.0f),    new Vector2(0, 0), color),
                new TexturedVertex(new Vector4(side, -side, 0, 1.0f),     new Vector2(w, 0), color),
                new TexturedVertex(new Vector4(-side, side, 0, 1.0f),     new Vector2(0, h), color),
                new TexturedVertex(new Vector4(-side, side, 0, 1.0f),     new Vector2(0, h), color),
                new TexturedVertex(new Vector4(side, -side, 0, 1.0f),     new Vector2(w, 0), color),
                new TexturedVertex(new Vector4(side, side, 0, 1.0f),      new Vector2(w, h), color),
                /*
                new TexturedVertex(new Vector4(-side, -side, 0, 1.0f),   new Vector2(0, h)),
                new TexturedVertex(new Vector4(-side, side, 0, 1.0f),    new Vector2(0, 0)),
                new TexturedVertex(new Vector4(side, side, 0, 1.0f),    new Vector2(w, 0)),
                new TexturedVertex(new Vector4(side, -side, 0, 1.0f),    new Vector2(w, h)),
                new TexturedVertex(new Vector4(-side, -side, 0, 1.0f),    new Vector2(0, h)),
                new TexturedVertex(new Vector4(side, side, 0, 1.0f),     new Vector2(w, 0)),*/
            };
            return vertices;
        }
    }
}
