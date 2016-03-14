using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1
{
    class Character
    {
        public string Name;
        public Texture2D CurrentImg;
        private int CurrentIndex;
        public Texture2D[] IdleImg;
        public Texture2D[] LeftImg;
        public Texture2D[] RightImg;
        public Texture2D FocusImg;
        public Vector2 Position;
        public int Speed;
        public float Angle;
        private string animationState;

        public Character()
        {
            Position = new Vector2(0, 0);
            Angle = 0;
            animationState = "idle";
        }

        public Character(string name, Texture2D[] idleImg, Texture2D[] leftImg, Texture2D[] rightImg, Texture2D focusImg, Vector2 position)
        {
            Name = name;
            IdleImg = idleImg;
            LeftImg = leftImg;
            RightImg = rightImg;
            FocusImg = focusImg;
            CurrentImg = IdleImg[0];
            CurrentIndex = 0;
            Position = position;
            Angle = 0;
            Speed = 6;
            animationState = "idle";
        }

        public Rectangle GetSelfRectangle()
        {
            return new Rectangle(0, 0, IdleImg[0].Width, IdleImg[0].Height);
        }

        public Rectangle GetFocusRectangle()
        {
            return new Rectangle(0, 0, FocusImg.Width, FocusImg.Height);
        }

        public Vector2 GetSelfCenter()
        {
            return new Vector2(CurrentImg.Width / 2, CurrentImg.Height / 2);
        }

        public Vector2 GetFocusCenter()
        {
            return new Vector2(FocusImg.Width / 2, FocusImg.Height / 2);
        }
        public void Move(Keys[] keys, GameWindow window)
        {
            if (keys.Contains(Keys.Right) && !keys.Contains(Keys.Left) || !keys.Contains(Keys.Right) && keys.Contains(Keys.Left))
            {
                if (Position.X < window.ClientBounds.Width - GetSelfCenter().X && Position.X > 0 + GetSelfCenter().X)
                    Position.X += (((keys.Contains(Keys.Right) ? Speed : 0) - (keys.Contains(Keys.Left) ? Speed : 0)) / (keys.Contains(Keys.LeftShift) ? 2 : 1));
                else if (Position.X < 0 + GetSelfCenter().X)
                    Position.X += (((keys.Contains(Keys.Right) ? Speed : 0) - 0) / (keys.Contains(Keys.LeftShift) ? 2 : 1));
                else
                    Position.X += ((0 - (keys.Contains(Keys.Left) ? Speed : 0)) / (keys.Contains(Keys.LeftShift) ? 2 : 1));
            }
            if (keys.Contains(Keys.Up) && !keys.Contains(Keys.Down) || !keys.Contains(Keys.Up) && keys.Contains(Keys.Down))
            {
                if (Position.Y < window.ClientBounds.Height - GetSelfCenter().Y && Position.Y > 0 + GetSelfCenter().Y)
                    Position.Y += (((keys.Contains(Keys.Down) ? Speed : 0) - (keys.Contains(Keys.Up) ? Speed : 0)) / (keys.Contains(Keys.LeftShift) ? 2 : 1));
                else if (Position.Y > window.ClientBounds.Height - GetSelfCenter().Y)
                    Position.Y += ((0 - (keys.Contains(Keys.Up) ? Speed : 0)) / (keys.Contains(Keys.LeftShift) ? 2 : 1));
                else
                    Position.Y += (((keys.Contains(Keys.Down) ? Speed : 0) - 0) / (keys.Contains(Keys.LeftShift) ? 2 : 1));
            }
        }
        public void Focus(Keys[] keys)
        {
            if (keys.Contains(Keys.LeftShift))
            {
                Angle += 0.025f;
                Angle %= 360;
            }
            else
            {
                Angle = 0;
            }
        }
        public void Update(Keys[] keys, GameWindow window)
        {
            Move(keys, window);
            Focus(keys);
        }
        public void UpdateFrame(Keys[] keys)
        {
            if (keys.Contains(Keys.Right) && !keys.Contains(Keys.Left))
            {
                if (animationState != "right")
                {
                    if (animationState != "left")
                        CurrentIndex = 0;
                    animationState = "right";
                }
                CurrentIndex += 1;
                if (CurrentIndex >= 8)
                    CurrentIndex -= 4;
                CurrentImg = RightImg[CurrentIndex];
            }
            else if (!keys.Contains(Keys.Right) && keys.Contains(Keys.Left))
            {
                if (animationState != "left")
                {
                    if (animationState != "right")
                        CurrentIndex = 0;
                    animationState = "left";
                }
                CurrentIndex += 1;

                if (CurrentIndex >= 8)
                    CurrentIndex -= 4;
                CurrentImg = LeftImg[CurrentIndex];
            }
            else
            {
                if (animationState != "idle")
                {
                    if (CurrentIndex > 4)
                        CurrentIndex = 3;
                    CurrentIndex -= 1;
                    CurrentImg = animationState == "right" ? RightImg[CurrentIndex] : LeftImg[CurrentIndex];
                    if (CurrentIndex <= 0)
                        animationState = "idle";
                }
                else
                {
                    CurrentIndex += 1;
                    CurrentIndex %= 8;
                    CurrentImg = IdleImg[CurrentIndex];
                }
            }
        }
    }
}
