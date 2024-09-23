using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace OnEstPasBenevole.src.Interface
{
    public class Bouton
    {
        private readonly Texte texte;

        private readonly int x, y, width, height;

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

    public class TextBox(Vector2 position, string placeholder, SpriteFont font)
    {
        private Vector2 position = position;
        readonly string placeholder = placeholder;
        private bool isActive;
        readonly SpriteFont font = font;

        private readonly Texte texte = new(font, placeholder, position, Color.Black, 24);

        public string Text { get { return texte.Content; } set { texte.Content = value; } }

        private KeyboardState previousKeyboardState;

        public void Update()
        {
            var currentKeyboardState = Keyboard.GetState();

            if (isActive)
            {
                foreach (var key in currentKeyboardState.GetPressedKeys())
                {
                    if (key == Keys.Back && texte.Content != null && texte.Content.Length > 0 && previousKeyboardState.IsKeyUp(Keys.Back))
                    {
                        texte.Content = texte.Content[..^1];
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

        private static char GetCharFromKey(Keys key)
        {
            Console.WriteLine(key);
            return key switch
            {
                Keys.A => 'A',
                Keys.B => 'B',
                Keys.C => 'C',
                Keys.D => 'D',
                Keys.E => 'E',
                Keys.F => 'F',
                Keys.G => 'G',
                Keys.H => 'H',
                Keys.I => 'I',
                Keys.J => 'J',
                Keys.K => 'K',
                Keys.L => 'L',
                Keys.M => 'M',
                Keys.N => 'N',
                Keys.O => 'O',
                Keys.P => 'P',
                Keys.Q => 'Q',
                Keys.R => 'R',
                Keys.S => 'S',
                Keys.T => 'T',
                Keys.U => 'U',
                Keys.V => 'V',
                Keys.W => 'W',
                Keys.X => 'X',
                Keys.Y => 'Y',
                Keys.Z => 'Z',
                Keys.Space => ' ',
                Keys.D0 or Keys.NumPad0 => '0',
                Keys.D1 or Keys.NumPad1 => '1',
                Keys.D2 or Keys.NumPad2 => '2',
                Keys.D3 or Keys.NumPad3 => '3',
                Keys.D4 or Keys.NumPad4 => '4',
                Keys.D5 or Keys.NumPad5 => '5',
                Keys.D6 or Keys.NumPad6 => '6',
                Keys.D7 or Keys.NumPad7 => '7',
                Keys.D8 or Keys.NumPad8 => '8',
                Keys.D9 or Keys.NumPad9 => '9',
                Keys.Divide => '/',
                Keys.OemComma => ',',
                _ => '\0',
            };
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle rectangle = new((int)position.X, (int)position.Y, 200, 30);
            spriteBatch.DrawRectangle(rectangle, Color.Black);
            texte.Draw(spriteBatch);
        }
    }
}