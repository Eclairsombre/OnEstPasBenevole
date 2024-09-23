using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OnEstPasBenevole.src.Interface
{
    public enum MenuState
    {
        Main,
        Options,
        Money
    }
    public class Menu
    {
        private readonly Bouton startButton;
        private readonly Bouton optionsButton;

        private Money money;

        private readonly Option option;

        public Option Option { get { return option; } }

        public MenuState State { get; set; }

        readonly SpriteFont spriteFont;

        private Texte titre;





        public Menu(SpriteFont spriteFont, int screenWidth, int screenHeight)
        {
            Vector2 position = new(screenWidth / 2, screenHeight / 2);
            startButton = new Bouton(spriteFont, "Start", (int)position.X - 100, (int)position.Y - 100, 300, 100);
            optionsButton = new Bouton(spriteFont, "Options", (int)position.X - 100, (int)position.Y, 300, 100);

            this.spriteFont = spriteFont;
            State = MenuState.Main;

            money = new Money();

            option = new Option(spriteFont);

            titre = new Texte(spriteFont, "ON EST PAS BENEVOLE", new Vector2(screenWidth / 2 - 60, 200), Color.Black, 24);

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
                    if (startButton.IsClicked())
                    {
                        State = MenuState.Money;
                    }
                    if (optionsButton.IsClicked())
                    {
                        State = MenuState.Options;
                    }
                    break;
                case MenuState.Options:
                    if (keyboardState.IsKeyDown(Keys.Escape))
                    {
                        State = MenuState.Main;
                    }
                    MenuState currentState = State;
                    option.Update(ref money, Content, GraphicsDevice, ref currentState);
                    State = currentState;
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
                    titre.Draw(spriteBatch);
                    startButton.Draw(spriteBatch);
                    optionsButton.Draw(spriteBatch);
                    break;
                case MenuState.Options:
                    option.Draw(spriteBatch);
                    break;
                case MenuState.Money:
                    money.Draw(spriteBatch);
                    break;
            }

        }

    }
}