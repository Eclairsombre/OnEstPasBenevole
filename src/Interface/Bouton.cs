using System;
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
            Vector2 position = new(x + width / 2 - 25, y + height / 2);
            this.texte = new Texte(spriteFont, texte, position, Color.Black);
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle rectangle = new(x, y, width, height);
            spriteBatch.DrawRectangle(rectangle, Color.Black);
            texte.Draw(spriteBatch);
        }

        public bool IsClicked()
        {
            Rectangle rectangle = new(x, y, width, height);
            if (rectangle.Contains(Mouse.GetState().Position) && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                return true;
            }
            return false;
        }


    }

    public class TextBox
    {
        private Vector2 position;
        private string placeholder;
        private bool isActive;
        private SpriteFont font;

        private Texte texte;

        public string Text { get { return texte.Content; } set { texte.Content = value; } }

        public TextBox(Vector2 position, string placeholder, SpriteFont font)
        {
            texte = new Texte(font, placeholder, position, Color.Black, 24);
            this.position = position;
            this.placeholder = placeholder;
            this.font = font;
        }

        private KeyboardState previousKeyboardState;

        public void Update(GameTime gameTime)
        {
            var currentKeyboardState = Keyboard.GetState();

            if (isActive)
            {
                foreach (var key in currentKeyboardState.GetPressedKeys())
                {
                    if (key == Keys.Back && texte.Content != null && texte.Content.Length > 0 && previousKeyboardState.IsKeyUp(Keys.Back))
                    {
                        texte.Content = texte.Content.Substring(0, texte.Content.Length - 1);
                    }
                    else if (key != Keys.Back && previousKeyboardState.IsKeyUp(key))
                    {
                        char character = GetCharFromKey(key);
                        if (character != '\0')
                        {
                            texte.Content += character;
                        }
                    }

                }
            }

            // Gérer l'activation du champ de saisie
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                Rectangle textBoxRect = new((int)position.X, (int)position.Y, 200, 30);
                if (textBoxRect.Contains(Mouse.GetState().Position))
                {
                    isActive = true;
                    texte.Content = "";
                }
                else
                {
                    isActive = false;
                }
            }

            previousKeyboardState = currentKeyboardState;
        }

        private char GetCharFromKey(Keys key)
        {
            Console.WriteLine(key);
            switch (key)
            {
                case Keys.A: return 'A';
                case Keys.B: return 'B';
                case Keys.C: return 'C';
                case Keys.D: return 'D';
                case Keys.E: return 'E';
                case Keys.F: return 'F';
                case Keys.G: return 'G';
                case Keys.H: return 'H';
                case Keys.I: return 'I';
                case Keys.J: return 'J';
                case Keys.K: return 'K';
                case Keys.L: return 'L';
                case Keys.M: return 'M';
                case Keys.N: return 'N';
                case Keys.O: return 'O';
                case Keys.P: return 'P';
                case Keys.Q: return 'Q';
                case Keys.R: return 'R';
                case Keys.S: return 'S';
                case Keys.T: return 'T';
                case Keys.U: return 'U';
                case Keys.V: return 'V';
                case Keys.W: return 'W';
                case Keys.X: return 'X';
                case Keys.Y: return 'Y';
                case Keys.Z: return 'Z';
                case Keys.Space: return ' ';
                case Keys.D0:
                case Keys.NumPad0: return '0';
                case Keys.D1:
                case Keys.NumPad1: return '1';
                case Keys.D2:
                case Keys.NumPad2: return '2';
                case Keys.D3:
                case Keys.NumPad3: return '3';
                case Keys.D4:
                case Keys.NumPad4: return '4';
                case Keys.D5:
                case Keys.NumPad5: return '5';
                case Keys.D6:
                case Keys.NumPad6: return '6';
                case Keys.D7:
                case Keys.NumPad7: return '7';
                case Keys.D8:
                case Keys.NumPad8: return '8';
                case Keys.D9:
                case Keys.NumPad9: return '9';
                case Keys.Divide: return '/';
                case Keys.OemComma: return ',';
                default: return '\0';
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle rectangle = new((int)position.X, (int)position.Y, 200, 30);
            spriteBatch.DrawRectangle(rectangle, Color.Black);
            texte.Draw(spriteBatch);
        }
    }
}