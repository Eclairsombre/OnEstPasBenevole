using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace OnEstPasBenevole.src
{
    public class Money
    {
        private Texte texte;

        private Date dateDebut;


        private double salaireParSeconde;

        private double salaireParDixiemeSeconde;

        private double totalGagner;

        private TimeSpan elapsedTime = TimeSpan.Zero;

        private readonly int nbApresVirgule;

        private double salaireAnnee1;
        private double salaireAnnee2;
        private double salaireAnnee3;

        private double currentSalaireAnnee;

        private Texte stateTexte;

        public double SalaireAnnee1 { get { return salaireAnnee1; } set { salaireAnnee1 = value; } }
        public double SalaireAnnee2 { get { return salaireAnnee2; } set { salaireAnnee2 = value; } }
        public double SalaireAnnee3 { get { return salaireAnnee3; } set { salaireAnnee3 = value; } }

        public Date DateDebut { get { return dateDebut; } set { dateDebut = value; } }
        public double TotalGagner
        {
            get { return totalGagner; }
            set { totalGagner = value; }
        }

        public Money(Date dateDebut, float salaireAnnee1, float salaireAnnee2 = 0, float salaireAnnee3 = 0, int nbApresVirgule = 4)
        {
            this.dateDebut = dateDebut;
            this.salaireAnnee1 = salaireAnnee1;
            this.salaireAnnee2 = salaireAnnee2;
            this.salaireAnnee3 = salaireAnnee3;
            this.nbApresVirgule = nbApresVirgule;

        }

        public Money()
        {
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
                texte = new Texte(spriteFont, "0 €", new Vector2(x, y), Color.Black, 24);
                stateTexte = new Texte(spriteFont, "", new Vector2(x - 20, 200), Color.Black, 24);

            }

        }

        public void Init(ContentManager Content, GraphicsDevice GraphicsDevice)
        {
            LoadContent(Content, GraphicsDevice);
            Calcul(GraphicsDevice);
        }

        public void Calcul(GraphicsDevice GraphicsDevice)
        {


            DateTime now = DateTime.Now;

            if (now.Year == dateDebut.annee)
            {
                currentSalaireAnnee = salaireAnnee1;
            }
            else if (now.Year == dateDebut.annee + 1)
            {
                currentSalaireAnnee = salaireAnnee2;
            }
            else if (now.Year == dateDebut.annee + 2)
            {
                currentSalaireAnnee = salaireAnnee3;
            }
            else
            {
                currentSalaireAnnee = 0;
            }
            int totalWorkingday = 0;


            DateTime startDateTime = new(dateDebut.annee, dateDebut.mois, dateDebut.jour, 0, 0, 0);
            DateTime endDateTime = now;

            while (startDateTime < endDateTime)
            {
                if (startDateTime.DayOfWeek >= DayOfWeek.Monday && startDateTime.DayOfWeek <= DayOfWeek.Friday)
                {
                    totalWorkingday++;
                }
                startDateTime = startDateTime.AddDays(1);
            }

            int wokingDayInCurrentMonth = 0;
            startDateTime = new(now.Year, now.Month, 1, 0, 0, 0);
            endDateTime = new(now.Year, now.Month + 1, 1, 0, 0, 0);
            while (startDateTime < endDateTime)
            {
                if (startDateTime.DayOfWeek >= DayOfWeek.Monday && startDateTime.DayOfWeek <= DayOfWeek.Friday)
                {
                    wokingDayInCurrentMonth++;
                }
                startDateTime = startDateTime.AddDays(1);
            }


            double dailySalary = currentSalaireAnnee / wokingDayInCurrentMonth;
            int workingSecondsPerDay = 7 * 60 * 60;
            salaireParSeconde = dailySalary / workingSecondsPerDay;
            DateTime startOfDay = new(now.Year, now.Month, now.Day, 9, 0, 0);
            int workingSecondsToday = 0;
            while (startOfDay < now)
            {
                if (IsWorkingHours(startOfDay))
                {
                    workingSecondsToday++;
                }
                startOfDay = startOfDay.AddSeconds(1);
            }

            int secondSinceStart = (int)(totalWorkingday * workingSecondsPerDay + workingSecondsToday);

            totalGagner = salaireParSeconde * secondSinceStart;

            salaireParDixiemeSeconde = salaireParSeconde / 10;


            elapsedTime = TimeSpan.Zero;

            totalGagner = Math.Round(totalGagner + salaireParSeconde, nbApresVirgule);
            string nbApresVirguleString = "0." + new string('0', nbApresVirgule);
            string text = totalGagner.ToString(nbApresVirguleString) + " €";
            texte.Content = text;

            texte.Update(GraphicsDevice);
            elapsedTime = TimeSpan.Zero;

        }


        private static bool IsWorkingHours(DateTime now)
        {
            if (now.DayOfWeek < DayOfWeek.Monday || now.DayOfWeek > DayOfWeek.Friday)
            {
                return false;
            }

            TimeSpan startTime = new(9, 0, 0);
            TimeSpan endTime = new(17, 0, 0);
            TimeSpan lunchStart = new(13, 0, 0);
            TimeSpan lunchEnd = new(14, 0, 0);

            if (now.TimeOfDay >= startTime && now.TimeOfDay < lunchStart)
            {
                return true;
            }
            else if (now.TimeOfDay >= lunchEnd && now.TimeOfDay < endTime)
            {
                return true;
            }

            return false;
        }


        public void Update(GameTime gameTime, GraphicsDevice GraphicsDevice)
        {

            if (IsWorkingHours(DateTime.Now))
            {
                elapsedTime += gameTime.ElapsedGameTime;

                if (elapsedTime.TotalSeconds >= 0.1)
                {
                    totalGagner = Math.Round(totalGagner + salaireParDixiemeSeconde, nbApresVirgule);
                    string nbApresVirguleString = "0." + new string('0', nbApresVirgule);
                    string text = totalGagner.ToString(nbApresVirguleString) + " €";
                    texte.Content = text;
                    texte.Update(GraphicsDevice);
                    elapsedTime = TimeSpan.Zero;

                    stateTexte.Content = "MONEYYYYYYYYYY";
                }
            }
            else
            {
                stateTexte.Content = "On va se reposer chef";
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            stateTexte.Draw(spriteBatch);

            texte.Draw(spriteBatch);
        }

    }
}