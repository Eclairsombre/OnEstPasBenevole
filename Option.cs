using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OnEstPasBenevole
{
    class Option
    {
        private StringBuilder _inputText;

        private SpriteFont spriteFont;
        private KeyboardState _currentKeyboardState;
        private KeyboardState _previousKeyboardState;

        public Option(SpriteFont font)
        {
            _inputText = new StringBuilder();

            spriteFont = font;
        }



        protected void LoadContent()
        {
        }

        public void Update(GameTime gameTime)
        {
            _currentKeyboardState = Keyboard.GetState();

            foreach (var key in _currentKeyboardState.GetPressedKeys())
            {
                if (_previousKeyboardState.IsKeyUp(key))
                {
                    if (key == Keys.Back && _inputText.Length > 0)
                    {
                        _inputText.Remove(_inputText.Length - 1, 1);
                    }
                    else if (key == Keys.Enter)
                    {
                        // Handle enter key if needed
                    }
                    else if (key == Keys.Space)
                    {
                        _inputText.Append(' ');
                    }
                    else
                    {
                        var keyString = key.ToString();
                        if (key >= Keys.D0 && key <= Keys.D9 && keyString.Length == 1)
                        {
                            keyString = ((char)(key - Keys.D0 + '0')).ToString();
                            _inputText.Append(keyString);

                        }

                    }
                }
            }

            _previousKeyboardState = _currentKeyboardState;
        }

        public void Draw(SpriteBatch _spriteBatch)
        {

            _spriteBatch.DrawString(spriteFont, "Enter text: " + _inputText.ToString(), new Vector2(10, 10), Color.Black);

        }
    }
}