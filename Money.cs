using System;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OnEstPasBenevole
{
    public class Money
    {
        private Texte texte;

        private Date dateDebut;

        private float salaireParMois;

        private float TotalGagner;

        public Money(Date dateDebut, float salaireParMois, float TotalGagner = 0)
        {
            this.dateDebut = dateDebut;
            this.salaireParMois = salaireParMois;

            this.TotalGagner = TotalGagner;


        }

        public void LoadContent(ContentManager Content, GraphicsDevice GraphicsDevice)
        {
            SpriteFont spriteFont = Content.Load<SpriteFont>("Arial");
            if (spriteFont == null)
            {
                Console.WriteLine("spriteFont is null");
            }
            else
            {
                int x = GraphicsDevice.Viewport.Width / 2;
                int y = GraphicsDevice.Viewport.Height / 2;
                texte = new Texte(spriteFont, "Hello World", new Vector2(x, y), Color.Black, 40);
            }
        }

        public void Update(GraphicsDevice GraphicsDevice)
        {
            texte.Update(GraphicsDevice);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            texte.Draw(spriteBatch);
        }

    }
}