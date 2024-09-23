using System;
using System.IO;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OnEstPasBenevole
{
    public class Option
    {
        private TextBox dateDebutTextBox;
        private TextBox salaireAnnee1TextBox;
        private TextBox salaireAnnee2TextBox;
        private TextBox salaireAnnee3TextBox;
        private Bouton validerButton;

        private const string SaveFilePath = "data.txt";


        public Option(SpriteFont spriteFont)
        {
            // Initialiser les champs de saisie et les boutons
            dateDebutTextBox = new TextBox(new Vector2(500, 200), "JJ/MM/AAAA", spriteFont);
            salaireAnnee1TextBox = new TextBox(new Vector2(500, 300), "Salaire Annee 1", spriteFont);
            salaireAnnee2TextBox = new TextBox(new Vector2(500, 400), "Salaire Annee 2", spriteFont);
            salaireAnnee3TextBox = new TextBox(new Vector2(500, 500), "Salaire Annee 3", spriteFont);
            validerButton = new Bouton(spriteFont, "Valider", 500, 600, 300, 50);
        }

        public void LoadData(ref Money money, ContentManager Content, GraphicsDevice GraphicsDevice)
        {
            if (File.Exists(SaveFilePath))
            {
                string[] lines = File.ReadAllLines(SaveFilePath);
                if (lines.Length >= 4)
                {
                    dateDebutTextBox.Text = lines[0];
                    salaireAnnee1TextBox.Text = lines[1];
                    salaireAnnee2TextBox.Text = lines[2];
                    salaireAnnee3TextBox.Text = lines[3];

                    string dateDebutStr = dateDebutTextBox.Text;
                    string salaireAnnee1Str = salaireAnnee1TextBox.Text;
                    string salaireAnnee2Str = salaireAnnee2TextBox.Text;
                    string salaireAnnee3Str = salaireAnnee3TextBox.Text;

                    DateTime dateDebut = DateTime.ParseExact(dateDebutStr, "dd/MM/yyyy", null);
                    float salaireAnnee1 = float.Parse(salaireAnnee1Str);
                    if (salaireAnnee2Str == "" || salaireAnnee2Str == "Salaire Annee 2")
                    {
                        salaireAnnee2Str = "0";
                    }
                    if (salaireAnnee3Str == "" || salaireAnnee3Str == "Salaire Annee 3")
                    {
                        salaireAnnee3Str = "0";
                    }
                    float salaireAnnee2 = float.Parse(salaireAnnee2Str);
                    float salaireAnnee3 = float.Parse(salaireAnnee3Str);

                    Date dateDebutObj = new(dateDebut.Day, dateDebut.Month, dateDebut.Year);
                    money = new Money(dateDebutObj, salaireAnnee1, salaireAnnee2, salaireAnnee3);
                    money.Init(Content, GraphicsDevice);


                }
            }
        }

        public void SaveData()
        {
            try
            {
                using (StreamWriter writer = new(SaveFilePath))
                {
                    writer.WriteLine(dateDebutTextBox.Text);
                    writer.WriteLine(salaireAnnee1TextBox.Text);
                    writer.WriteLine(salaireAnnee2TextBox.Text);
                    writer.WriteLine(salaireAnnee3TextBox.Text);
                }
                Console.WriteLine("Data saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save data: {ex.Message}");
            }
        }

        public void Update(GameTime gameTime, ref Money money, ContentManager Content, GraphicsDevice GraphicsDevice)
        {
            dateDebutTextBox.Update(gameTime);
            salaireAnnee1TextBox.Update(gameTime);
            salaireAnnee2TextBox.Update(gameTime);
            salaireAnnee3TextBox.Update(gameTime);

            if (validerButton.IsClicked())
            {

                if (dateDebutTextBox.Text == "" || salaireAnnee1TextBox.Text == "" || salaireAnnee2TextBox.Text == "" || salaireAnnee3TextBox.Text == "")
                {
                    return;
                }
                SaveData();
                string dateDebutStr = dateDebutTextBox.Text;
                string salaireAnnee1Str = salaireAnnee1TextBox.Text;
                string salaireAnnee2Str = salaireAnnee2TextBox.Text;
                string salaireAnnee3Str = salaireAnnee3TextBox.Text;

                DateTime dateDebut = DateTime.ParseExact(dateDebutStr, "dd/MM/yyyy", null);
                float salaireAnnee1 = float.Parse(salaireAnnee1Str);
                if (salaireAnnee2Str == "" || salaireAnnee2Str == "Salaire Annee 2")
                {
                    salaireAnnee2Str = "0";
                }
                if (salaireAnnee3Str == "" || salaireAnnee3Str == "Salaire Annee 3")
                {
                    salaireAnnee3Str = "0";
                }
                float salaireAnnee2 = float.Parse(salaireAnnee2Str);
                float salaireAnnee3 = float.Parse(salaireAnnee3Str);

                Date dateDebutObj = new(dateDebut.Day, dateDebut.Month, dateDebut.Year);
                money = new Money(dateDebutObj, salaireAnnee1, salaireAnnee2, salaireAnnee3);
                money.Init(Content, GraphicsDevice);

                Console.WriteLine("Date de debut: " + dateDebutObj.jour + "/" + dateDebutObj.mois + "/" + dateDebutObj.annee);
                Console.WriteLine("Salaire annee 1: " + salaireAnnee1);
                Console.WriteLine("Salaire annee 2: " + salaireAnnee2);
                Console.WriteLine("Salaire annee 3: " + salaireAnnee3);
                Console.WriteLine("Total gagner: " + money.TotalGagner);
            }
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont)
        {
            dateDebutTextBox.Draw(spriteBatch);
            salaireAnnee1TextBox.Draw(spriteBatch);
            salaireAnnee2TextBox.Draw(spriteBatch);
            salaireAnnee3TextBox.Draw(spriteBatch);
            validerButton.Draw(spriteBatch);
        }
    }
}
