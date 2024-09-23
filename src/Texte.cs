using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OnEstPasBenevole.src
{
    public class Texte(SpriteFont font, string text, Vector2 position, Color color, int size = 12)
    {
        private readonly SpriteFont _font = font;
        private string _text = text;
        private Vector2 _position = position;
        private Color _color = color;

        private int _size = size;

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 textSize = _font.MeasureString(_text);
            Vector2 adjustedPosition = new(_position.X - textSize.X / 3, _position.Y - textSize.Y / 2);
            spriteBatch.DrawString(_font, _text, adjustedPosition, _color, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
        }
        public void Update(GraphicsDevice GraphicsDevice)
        {
            int screenWidth = GraphicsDevice.Viewport.Width;
            int screenHeight = GraphicsDevice.Viewport.Height;
            int textLength = Content.Length;

            float newSize = Math.Min(screenWidth / (textLength * 10), screenHeight / 10);
            Size = (int)newSize;
            Position = new Vector2(screenWidth / 2, screenHeight / 2);
        }

        public string Content
        {
            get { return _text; }
            set { _text = value; }
        }

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }


        public int Size
        {
            get { return _size; }
            set { _size = value; }
        }
    }
}