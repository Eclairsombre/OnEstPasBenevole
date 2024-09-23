using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace OnEstPasBenevole.src.Interface
{
    public class Option(SpriteFont spriteFont)
    {

        private readonly TextBox dateDebutTextBox = new(new Vector2(900, 200), "JJ/MM/AAAA", spriteFont);
        private readonly TextBox salaireAnnee1TextBox = new(new Vector2(900, 300), "Salaire Annee 1", spriteFont);
        private readonly TextBox salaireAnnee2TextBox = new(new Vector2(900, 400), "Salaire Annee 2", spriteFont);
        private readonly TextBox salaireAnnee3TextBox = new(new Vector2(900, 500), "Salaire Annee 3", spriteFont);
        private readonly Bouton validerButton = new(spriteFont, "Valider", 850, 700, 300, 100);

        private const string SaveFilePath = "data.txt";

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

                    DateTime dateDebut;
                    float salaireAnnee1 = 0;
                    float salaireAnnee2 = 0;
                    float salaireAnnee3 = 0;
                    if (dateDebutTextBox.Text != "" && dateDebutTextBox.Text != "JJ/MM/AAAA")
                    {
                        dateDebut = DateTime.ParseExact(dateDebutTextBox.Text, "dd/MM/yyyy", null);
                        if (salaireAnnee1Str != "" && salaireAnnee1Str != "Salaire Annee 1")
                        {
                            salaireAnnee1 = float.Parse(salaireAnnee1Str);
                        }
                        if (salaireAnnee2Str != "" && salaireAnnee2Str != "Salaire Annee 2")
                        {
                            salaireAnnee2 = float.Parse(salaireAnnee2Str);
                        }
                        if (salaireAnnee3Str != "" && salaireAnnee3Str != "Salaire Annee 3")
                        {
                            salaireAnnee3 = float.Parse(salaireAnnee3Str);
                        }
                        Date dateDebutObj = new(dateDebut.Day, dateDebut.Month, dateDebut.Year);
                        money = new Money(dateDebutObj, salaireAnnee1, salaireAnnee2, salaireAnnee3);
                        money.Init(Content, GraphicsDevice);
                    }






                }
            }
        }

        public void SaveData()
        {
            try
            {
                using (StreamWriter writer = new(SaveFilePath))
                {
                    if (dateDebutTextBox.Text != "" && dateDebutTextBox.Text != "JJ/MM/AAAA")
                    {
                        writer.WriteLine(dateDebutTextBox.Text);
                    }
                    else
                    {
                        writer.WriteLine("JJ/MM/AAAA");
                    }
                    if (salaireAnnee1TextBox.Text != "" && salaireAnnee1TextBox.Text != "Salaire Annee 1")
                    {
                        writer.WriteLine(salaireAnnee1TextBox.Text);
                    }
                    else
                    {
                        writer.WriteLine("Salaire Annee 1");
                    }
                    if (salaireAnnee2TextBox.Text != "" && salaireAnnee2TextBox.Text != "Salaire Annee 2")
                    {
                        writer.WriteLine(salaireAnnee2TextBox.Text);
                    }
                    else
                    {
                        writer.WriteLine("Salaire Annee 2");
                    }
                    if (salaireAnnee3TextBox.Text != "" && salaireAnnee3TextBox.Text != "Salaire Annee 3")
                    {
                        writer.WriteLine(salaireAnnee3TextBox.Text);
                    }
                    else
                    {
                        writer.WriteLine("Salaire Annee 3");
                    }

                }
                Console.WriteLine("Data saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save data: {ex.Message}");
            }
        }

        public void Update(ref Money money, ContentManager Content, GraphicsDevice GraphicsDevice, ref MenuState Menu)
        {
            dateDebutTextBox.Update();
            salaireAnnee1TextBox.Update();
            salaireAnnee2TextBox.Update();
            salaireAnnee3TextBox.Update();

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

                Menu = MenuState.Main;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            dateDebutTextBox.Draw(spriteBatch);
            salaireAnnee1TextBox.Draw(spriteBatch);
            salaireAnnee2TextBox.Draw(spriteBatch);
            salaireAnnee3TextBox.Draw(spriteBatch);
            validerButton.Draw(spriteBatch);
        }
    }
}
