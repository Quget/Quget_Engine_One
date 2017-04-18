using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using Quget_Engine_One.Renderables;

namespace Quget_Engine_One.GameObjects
{
    class Player : GameObject
    {
        private Dictionary<string,Animation> animations = new Dictionary<string, Animation>();
        public Player(Renderable render, Vector4 position, Vector4 rotation) : base(render, position, rotation)
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
        public void PlayAnimation(string name)
        {
            if (!animations.ContainsKey(name))
                return;

            animations[name].Reset();
            animations[name].Start();
        }
        public void StopAnimation(string name)
        {
            if (!animations.ContainsKey(name))
                return;

            animations[name].Stop();
        }
        public override void Render()
        {
            //GL.Uniform3(22, AnimatedUV);
            base.Render();

        }
        public override void Update(double time)
        {
            foreach(KeyValuePair<string,Animation> animation in animations)
            {
                Vector3 animatedUV = animation.Value.UpdateAnimation(time);
                if(animatedUV != Vector3.Zero)
                {
                   // AnimatedUV = animatedUV;
                    GL.Uniform3(22, animatedUV);
                }
            }
            base.Update(time);
        }
    }
}
