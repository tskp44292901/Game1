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
        public Texture2D SelfImg;
        public Texture2D FocusImg;
        public Vector2 Position;
        public float Angle;

        public Character()
        {
            Position = new Vector2(0, 0);
            Angle = 0;
        }
        public Character(string name, Texture2D image, Texture2D image2, Vector2 position)
        {
            Name = name;
            SelfImg = image;
            FocusImg = image2;
            Position = position;
            Angle = 0;
        }
        public Rectangle GetSelfRectangle()
        {
            return new Rectangle(0, 0, SelfImg.Width, SelfImg.Height);
        }
        public Rectangle GetFocusRectangle()
        {
            return new Rectangle(0, 0, FocusImg.Width, FocusImg.Height);
        }
        public Vector2 GetSelfCenter()
        {
            return new Vector2(SelfImg.Width / 2, SelfImg.Height / 2);
        }

        public Vector2 GetFocusCenter()
        {
            return new Vector2(FocusImg.Width / 2, FocusImg.Height / 2);
        }
        public void Move(Keys[] keys)
        {

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
        public void Update(Keys[] keys)
        {
            Move(keys);
            Focus(keys);
        }
    }
}
