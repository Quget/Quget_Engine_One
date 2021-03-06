﻿using OpenTK;
using OpenTK.Graphics;
using Quget_Engine_One.GameObjects;
using Quget_Engine_One.Renderables;
using Quget_Engine_One.Gui.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Quget_Engine_One.Gui
{
    /// <summary>
    /// A Qui. Yes it is a play on Quget and Gui.
    /// Holds methods to create buttons, text and in the future more.
    /// </summary>
    class Qui
    {
        private Scene scene;
        public Dictionary<string, string> pathFontDictionary = new Dictionary<string, string>();
        public Qui(Scene scene)
        {
            this.scene = scene;
        }

        private string GenerateFontImage(string fontName, int fontSize)
        {

            string name = fontName + "S" + fontSize;
            string savePath = "Content/Textures/" + name + ".png";
            if (pathFontDictionary.ContainsKey(name))
            {
                return pathFontDictionary[name];
            }
            else
            {
                if (File.Exists(savePath))
                {
                    pathFontDictionary.Add(name, savePath);
                    return pathFontDictionary[name];
                }
                else
                {
                    CharacterGenerator charGen = new CharacterGenerator();
                    System.Drawing.Size charSize;
                    System.Drawing.Bitmap bitmap = charGen.GenerateCharacters(fontSize, fontName, out charSize);
                    bitmap.Save(savePath, System.Drawing.Imaging.ImageFormat.Png);
                    pathFontDictionary.Add(name, savePath);
                }
                return pathFontDictionary[name];
            }

        }

        public Button CreateButton(string text, Vector2 position,Vector2 size, string fontName, int fontSize, Color4 color,Color4 textColor, bool fixedOnCam)
        {
            ShaderProgram animatedProgram = scene.GetShaderProgram("animated");

            string path = GenerateFontImage(fontName, fontSize);
            float textSize = (text.Length * fontSize) - fontSize;
            TexturedRenderObject textModel = new TexturedRenderObject(ObjectFactory.CreateTexturedCharacter(fontSize, fontSize, textColor), animatedProgram.id, path);
            Text.Label renderText = new Text.Label(textModel, new Vector4(position.X - (textSize / 2), position.Y, -0.9f, 0), new Vector4(0, 0, 0, 0), text, fixedOnCam);

            TexturedRenderObject buttonRender = new TexturedRenderObject(ObjectFactory.CreateTexturedQuad(size.X, size.Y, 2, 1, color), animatedProgram.id, "Content/Textures/button.png");
            Button button = new Button(buttonRender, new Vector4(position.X, position.Y, -1, 0), new Vector4(0, 0, 0, 0), text + "Button",renderText, fixedOnCam);
            scene.AddGameObject(button);
            return button;

            /*
            
            GameObject buttonObject = new GameObject(button, new Vector4(position.X, position.Y, -1, 0), new Vector4(0, 0, 0, 0), text + "Button");
            gameWindow.AddGameObject(buttonObject);

            float textSize = (text.Length * fontSize) - fontSize;
            CreateText(text, new Vector2(position.X - (textSize /2) , position.Y), fontName, fontSize, textColor);

            return buttonObject;
            */
        }
        public Label CreateText(string text, Vector2 position,string fontName, int fontSize, Color4 color, bool fixedOnCam)
        {
            return CreateText(text, new Vector3(position.X, position.Y, -1), fontName, fontSize,fontSize,fontSize, color, fixedOnCam);
           
        }

        public Label CreateText(string text, Vector3 position, string fontName, int fontSize,float width,float height, Color4 color, bool fixedOnCam)
        {
            string path = GenerateFontImage(fontName, fontSize);
            ShaderProgram animatedProgram = scene.GetShaderProgram("animated");
            TexturedRenderObject textModel = new TexturedRenderObject(ObjectFactory.CreateTexturedCharacter(width, height, color), animatedProgram.id, path);
            Label render = new Text.Label(textModel, new Vector4(position.X, position.Y, position.Z, 0), new Vector4(0, 0, 0, 0), text, fixedOnCam);
            //return render;
            scene.AddGameObject(render);
            return render;
        }

    }
}
