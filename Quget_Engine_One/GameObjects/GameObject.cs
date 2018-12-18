using System;
using System.Collections.Generic;
using System.Text;
using Quget_Engine_One.Renderables;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using Quget_Engine_One.Camera;

namespace Quget_Engine_One.GameObjects
{
    /// <summary>
    /// A GameObject. This object can move, rotate, scale and is visible on the screen.
    /// </summary>
    class GameObject : IDisposable
    {
        public TexturedRenderObject render { private set; get; }
        public Vector4 position { private set; get; }
        public Vector4 rotation { set; get; }
        public Vector3 scale { private set; get; }
        public Vector4 direction { private set; get; }
        public string name { private set; get; }
        public bool disposed { private set; get; }

        private Vector4 targetPosition;
        private float moveToSpeed;
        private bool goalReached = true;

        protected Matrix4 modelView;

        private Vector4 oldPos;
        public Vector3 positionRelCam { protected set; get; }
        public virtual float width
        {
            get
            {
                return render.width;
            }
        }
        public virtual float height
        {
            get
            {
                return render.height;
            }
        }

        /// <summary>
        /// Creates a given game object with a rendertextureobject, position,rotation and name.
        /// </summary>
        /// <param name="render"></param>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <param name="name"></param>
        public GameObject(TexturedRenderObject render,Vector4 position,Vector4 rotation,string name)
        {
            this.render = render;
            this.position = position;
            this.rotation = rotation;
            scale = Vector3.One;
            this.name = name;
            QMouse.Instance.onMouseDown += Instance_onMouseDown;
            QMouse.Instance.onMouseUp += Instance_onMouseUp;
            positionRelCam = new Vector3(position);
        }

        private void Instance_onMouseUp(QMouse sender, OpenTK.Input.MouseButtonEventArgs e)
        {
            OnMouseUp(e);
        }

        /// <summary>
        /// User pressed on the game object(Orthographic tested only)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Instance_onMouseDown(QMouse sender, OpenTK.Input.MouseButtonEventArgs e)
        {
            float x = positionRelCam.X - (width / 2);
            float y = positionRelCam.Y - (height / 2);

            if (e.X > x && e.X < x + width &&
                e.Y > y && e.Y < y + height)
            {
                OnMouseDown(e);
            }
        }

        /// <summary>
        /// There was a collision, this event is called when there is one
        /// </summary>
        /// <param name="other"></param>
        protected virtual void OnCollision(GameObject other)
        {
            //other.Remove();
            Console.WriteLine("It Works!");
        }

        protected virtual void OnMouseUp(OpenTK.Input.MouseButtonEventArgs mouse)
        {

        }

        protected virtual void OnMouseDown(OpenTK.Input.MouseButtonEventArgs mouse)
        {

        }
        /// <summary>
        /// OnUpdate method, for now used to see of the object has reached its destination
        /// This was a test.
        /// </summary>
        /// <param name="time"></param>
        public virtual void Update(double time)
        {

            if(!goalReached)
            {
                Vector4 movement = targetPosition - position;
                movement.Normalize();
                float x = position.X + ((movement.X * moveToSpeed) * (float)time);
                float y = position.Y + ((movement.Y * moveToSpeed) * (float)time);
                SetPosition(x, y,position.Z,position.W);

                Vector2 distance = new Vector2(targetPosition) - new Vector2(position);
                if (distance.Length < 1)
                {
                    SetPosition(targetPosition.X, targetPosition.Y, position.Z, position.W);
                    goalReached = true;
                }
            }
            
        }

        /// <summary>
        /// Check if there is a collision between this game object and another.
        /// </summary>
        /// <param name="other"></param>
        public void Collision(GameObject other)
        {
            if (other == null || other.render == null)
                return;
            else
            {
                if(other.disposed || this.disposed)
                {
                    return;
                }
            }
            float x = positionRelCam.X - (width / 2);
            float y = positionRelCam.Y - (height / 2);
            float otherX = other.positionRelCam.X - (other.width / 2);
            float otherY = other.positionRelCam.Y - (other.height / 2);

            if (x < otherX + other.width &&
               x + width > otherX &&
               y < otherY + other.height &&
               height + y > otherY)
            {
                SetPosition(oldPos.X, oldPos.Y, oldPos.Z);
                OnCollision(other);
            }
        }
        /// <summary>
        /// Move to a target position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="speed"></param>
        /// <param name="z"></param>
        /// <param name="w"></param>
        public void MoveTo(float x, float y, float speed, float z = 0, float w = 0)
        {
            if (!goalReached)
                return;

            targetPosition = new Vector4(x, y, z, w);
            moveToSpeed = speed;
            goalReached = false;
        }
        /// <summary>
        /// set the position of this game object
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="w"></param>
        public void SetPosition(float x, float y, float z = 0,float w = 0)
        {
            oldPos = position;
            position = new Vector4(x, y, z, 0);
        }
        /// <summary>
        /// Set the rotation of this game object
        /// </summary>
        /// <param name="angleX"></param>
        /// <param name="angleY"></param>
        /// <param name="angleZ"></param>
        /// <param name="angleW"></param>
        public void SetRotationAngle(float angleX,float angleY,float angleZ,float angleW)
        {
            rotation = new Vector4(
                angleX * ((float)Math.PI / 180f),
                angleY * ((float)Math.PI / 180f),
                angleZ * ((float)Math.PI / 180f),
                angleW * ((float)Math.PI / 180f)
            );

            var d = new Vector4(rotation.X, rotation.Y, 0, 0);
            d.Normalize();
            direction = d;
        }
        /// <summary>
        /// Scale the game object
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void SetScale(float x, float y, float z)
        {
            scale = new Vector3(x, y, z);
        }
        
        public void SetScale(Vector3 scale)
        {
            this.scale = scale;
        }

        /// <summary>
        /// render the game object with given camera using it's position,rotation and scale.
        /// </summary>
        /// <param name="camera"></param>
        public virtual void Render(ICamera camera)
        {
            render.Bind();
            var t2 = Matrix4.CreateTranslation(position.X, position.Y, position.Z);
            var r1 = Matrix4.CreateRotationX(rotation.X);
            var r2 = Matrix4.CreateRotationY(rotation.Y);
            var r3 = Matrix4.CreateRotationZ(rotation.Z);
            var s = Matrix4.CreateScale(scale);
            modelView = r1 * r2 * r3 * s * t2 * camera.positionMatrix;
            GL.UniformMatrix4(21, false, ref modelView);

            positionRelCam = new Vector3(position) + camera.position;
            render.Render();
        }

        public void Dispose()
        {
            render.Dispose();
            disposed = true;
        }

        public void Remove()
        {
            QMouse.Instance.onMouseDown -= Instance_onMouseDown;
            QMouse.Instance.onMouseUp -= Instance_onMouseUp;
            Dispose();
            onRemove?.Invoke(this);
        }

        public delegate void OnRemove(GameObject sender);
        public event OnRemove onRemove;
    }
}
