using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using Quget_Engine_One.Renderables;
using Quget_Engine_One.Camera;

namespace Quget_Engine_One.GameObjects
{
    /// <summary>
    /// A game object but with a sprite animation.
    /// Sprite animation is using the shader.
    /// </summary>
    class AnimatedGameObject : GameObject
    {
        private Dictionary<string,Animation> animations = new Dictionary<string, Animation>();
        //private Vector2 offset = Vector2.Zero;
        protected int offsetIndex = 0;
        public AnimatedGameObject(TexturedRenderObject render, Vector4 position, Vector4 rotation, string name) : base(render, position, rotation,name)
        {
            
        }

        public bool IsPlaying(string name)
        {
            if (!animations.ContainsKey(name))
                return false;

            return animations[name].IsPlaying();
        }

        public void AddAnimation(string name, Animation animation)
        {
            animations.Add(name, animation);
        }

        public void ResetAnimation(string name)
        {
            animations[name].Reset();
        }

        public void StopAllAnimations()
        {
            foreach (KeyValuePair<string, Animation> animation in animations)
            {
                animation.Value.Stop();
            }
        }

        public void PlayAnimation(string name)
        {
            if (!animations.ContainsKey(name))
                return;

            animations[name].Start();
        }
        public void StopAnimation(string name)
        {
            if (!animations.ContainsKey(name))
                return;

            animations[name].Stop();
        }

        public override void Update(double time)
        {
            foreach(KeyValuePair<string,Animation> animation in animations)
            {
                // Vector3 animatedUV = animation.Value.UpdateAnimation(time);
                int index = animation.Value.UpdateAnimation(time);
                if (index != -1)
                {
                    offsetIndex = index;
                    //GL.Uniform3(22, animatedUV);
                }
            }
            base.Update(time);
        }

        public override void Render(ICamera camera)
        {
            //offsetIndex
            float indexX = (float)Math.Floor(offsetIndex % 4.0f);
            float indexY = (float)Math.Floor(offsetIndex / 4.0f);
            float offsetX = 0.25f * indexX;
            float offsetY = 0.25f * indexY;
            //float offsetX = 4 + ((1 / 4) * indexX);
            // float offsetY = 4 + ((1 / 4) * indexY);
            //vec2 newCoords = vec2(vs_textureCoordinate.x / countX + ((1 / countX) * indexX), vs_textureCoordinate.y / countY + ((1 / countY) * indexY));
            Vector2 offset = new Vector2(offsetX, offsetY);
            GL.VertexAttrib2(3, offset);
            base.Render(camera);
        }
    }
}
