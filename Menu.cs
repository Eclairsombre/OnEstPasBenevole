using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

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

        public MenuState State { get; set; }

        private Date dateDebut;

        double salaireAnnee1;
        double salaireAnnee2;
        double salaireAnnee3;


        public Menu(SpriteFont spriteFont, int screenWidth, int screenHeight)
        {
            Vector2 position = new Vector2(screenWidth / 2, screenHeight / 2);
            StartButton = new Bouton(spriteFont, "Start", (int)position.X - 100, (int)position.Y - 100, 300, 100);
            OptionsButton = new Bouton(spriteFont, "Options", (int)position.X - 100, (int)position.Y, 300, 100);

            State = MenuState.Main;

            money = new Money(new Date(1, 9, 2024), 850.5f, 0, 4);
            option = new Option(spriteFont);

        }

        public void LoadContent(ContentManager Content, GraphicsDevice GraphicsDevice)
        {
            money.LoadContent(Content, GraphicsDevice);


        }

        public void Update(GameTime gameTime, GraphicsDevice GraphicsDevice)
        {
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
                    option.Update(gameTime);
                    break;
                case MenuState.Money:
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
                    option.Draw(spriteBatch);
                    break;
                case MenuState.Money:
                    money.Draw(spriteBatch);
                    break;
            }
        }

    }
}