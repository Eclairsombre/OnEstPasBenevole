using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace OnEstPasBenevole
{
    public class Bouton
    {
        private Texte texte;

        private int x, y, width, height;

        public Bouton(SpriteFont spriteFont, string texte, int x, int y, int width, int height)
        {
            Vector2 position = new Vector2(x + width / 2 - 25, y + height / 2);
            this.texte = new Texte(spriteFont, texte, position, Color.Black);
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle rectangle = new Rectangle(x, y, width, height);
            spriteBatch.DrawRectangle(rectangle, Color.Black);
            texte.Draw(spriteBatch);
        }

        public bool IsClicked()
        {
            Rectangle rectangle = new Rectangle(x, y, width, height);
            if (rectangle.Contains(Mouse.GetState().Position) && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                return true;
            }
            return false;
        }


    }
}