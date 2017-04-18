using System;
using System.Collections.Generic;
using System.Text;
using Quget_Engine_One.Renderables;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace Quget_Engine_One.GameObjects
{
    class GameObject:IDisposable
    {
        public Renderable render;
        public Vector4 position;
        public Vector4 rotation;
        public Vector3 scale;
        protected Matrix4 modelView;
        public GameObject(Renderable render,Vector4 position,Vector4 rotation)
        {
            this.render = render;
            this.position = position;
            this.rotation = rotation;
            scale = Vector3.One;
        }
        public void SetRotation(float angleX,float angleY,float angleZ,float angleW)
        {
            rotation.X = angleX * ((float)Math.PI / 180f);
            rotation.Y = angleY * ((float)Math.PI / 180f);
            rotation.Z = angleZ * ((float)Math.PI / 180f);
            rotation.W = angleW * ((float)Math.PI / 180f);
        }
        public void SetScale(Vector3 scale)
        {
            this.scale = scale;
        }
        public virtual void Update(double time)
        {
            //position += direction * (velocity * (float)delta);
            //rotation += new Vector4(0, 0, 1 * (float)time, 0);
        }
        int count = 0;
        int current = 0;
        public virtual void Render()
        {
            render.Bind();
            var t2 = Matrix4.CreateTranslation(position.X, position.Y, position.Z);
            var r1 = Matrix4.CreateRotationX(rotation.X);
            var r2 = Matrix4.CreateRotationY(rotation.Y);
            var r3 = Matrix4.CreateRotationZ(rotation.Z);
            var s = Matrix4.CreateScale(scale);
            modelView = r1 * r2 * r3 * s * t2;
            GL.UniformMatrix4(21, false, ref modelView);

            if(count == 10)
            {
                current++;
                if (current == 3)
                    current = 0;

                count = 0;
            }
            Vector3 AnimatedUV = new Vector3();
            AnimatedUV.X = 4;
            AnimatedUV.Y = current;// 1;
            AnimatedUV.Z = 4;
            GL.Uniform3(22, AnimatedUV);
           

            count++;
            //32
            render.Render();
        }
        public void Dispose()
        {
            render.Dispose();
        }
    }
}
