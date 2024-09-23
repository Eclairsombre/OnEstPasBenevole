using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OnEstPasBenevole
{
    public enum MenuState
    {
        Main,
        Options,
        Money
    }
    public class Menu
    {
        private Bouton StartButton;
        private Bouton OptionsButton;

        private Money money;

        private Option option;

        public Option Option { get { return option; } }

        public MenuState State { get; set; }

        private readonly SpriteFont spriteFont;





        public Menu(SpriteFont spriteFont, int screenWidth, int screenHeight)
        {
            Vector2 position = new(screenWidth / 2, screenHeight / 2);
            StartButton = new Bouton(spriteFont, "Start", (int)position.X - 100, (int)position.Y - 100, 300, 100);
            OptionsButton = new Bouton(spriteFont, "Options", (int)position.X - 100, (int)position.Y, 300, 100);

            this.spriteFont = spriteFont;
            State = MenuState.Main;

            money = new Money();

            option = new Option(spriteFont);

        }

        public void LoadContent(ContentManager Content, GraphicsDevice GraphicsDevice)
        {
            money.LoadContent(Content, GraphicsDevice);
            option.LoadData(ref money, Content, GraphicsDevice);
        }

        public void Update(GameTime gameTime, GraphicsDevice GraphicsDevice, ContentManager Content)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            switch (State)
            {
                case MenuState.Main:
                    if (StartButton.IsClicked())
                    {
                        State = MenuState.Money;
                    }
                    if (OptionsButton.IsClicked())
                    {
                        State = MenuState.Options;
                    }
                    break;
                case MenuState.Options:
                    if (keyboardState.IsKeyDown(Keys.Escape))
                    {
                        State = MenuState.Main;
                    }
                    option.Update(gameTime, ref money, Content, GraphicsDevice);
                    break;
                case MenuState.Money:
                    if (keyboardState.IsKeyDown(Keys.Escape))
                    {
                        State = MenuState.Main;
                    }
                    money.Update(gameTime, GraphicsDevice);
                    break;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            switch (State)
            {
                case MenuState.Main:
                    StartButton.Draw(spriteBatch);
                    OptionsButton.Draw(spriteBatch);
                    break;
                case MenuState.Options:
                    option.Draw(spriteBatch, spriteFont);
                    break;
                case MenuState.Money:
                    money.Draw(spriteBatch);
                    break;
            }
        }

    }
}