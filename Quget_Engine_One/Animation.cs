using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quget_Engine_One
{
    class Animation
    {
        private float frameTime = 0.33f;
        private float animationTime = 0;
        private int animationStart = 0;
        private int animationCurrent = 0;
        private int animationEnd = 0;
        private int animationCounter = 0;
        private int animationCount = -1;//looping
        private bool isRunning = false;
        public Animation(int start, int end, int count = 1)
        {
            animationStart = start;
            animationCurrent = start;
            animationEnd = end;
            animationCount = count;
        }
        public bool IsPlaying()
        {
            return isRunning;
        }
        public void Start()
        {
            isRunning = true;
        }
        public void Stop()
        {
            isRunning = false;
        }
        public void Reset()
        {
            animationCounter = 0;
            animationCurrent = animationStart;
            isRunning = false;
        }
        public int UpdateAnimation(double time)
        {
            if (isRunning)
            {
                animationTime += (float)time;
                if (animationTime > frameTime)
                {
                    animationCurrent++;
                    if (animationCurrent == animationEnd)
                    {
                        animationCurrent = animationStart;
                        if (animationCount != -1)
                        {
                            animationCounter++;
                            if (animationCounter >= animationCount)
                            {
                                isRunning = false;
                                animationCounter = 0;
                                //animationCurrent = 0;
                            }   
                        }
                    }
                    
                    /*
                    Vector3 AnimatedUV = new Vector3();
                    AnimatedUV.X = 4;
                    AnimatedUV.Y = animationCurrent;// 1;
                    AnimatedUV.Z = 4;
                    */
                    animationTime = 0;//reset
                    return animationCurrent;//AnimatedUV;

                }
            }
            return -1;//Vector3.Zero;
        }
    }
}
