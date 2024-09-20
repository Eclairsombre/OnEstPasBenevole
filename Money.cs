using System;
using System.IO;
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

        private float salaireParSeconde;

        private float TotalGagner;

        private TimeSpan elapsedTime = TimeSpan.Zero;
        private string fileContent;

        public float totalGagner
        {
            get { return TotalGagner; }
            set { TotalGagner = value; }
        }

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
                texte = new Texte(spriteFont, "0 €", new Vector2(x, y), Color.Black, 24);
            }
            Init(Content);
        }

        public void Init(ContentManager Content)
        {
            using (var stream = TitleContainer.OpenStream("Content/data.txt"))
            using (var reader = new StreamReader(stream))
            {
                fileContent = reader.ReadToEnd();
            }

            string[] lines = fileContent.Split('\n');

            if (lines.Length > 0 && !string.IsNullOrWhiteSpace(lines[0]))
            {
                if (float.TryParse(lines[0], out float totalGagner))
                {
                    TotalGagner = totalGagner;
                }


                if (lines.Length > 1 && !string.IsNullOrWhiteSpace(lines[1]))
                {
                    string[] date = lines[1].Split(' ');
                    if (date.Length == 3 &&
                        int.TryParse(date[0], out int jour) &&
                        int.TryParse(date[1], out int mois) &&
                        int.TryParse(date[2], out int annee))
                    {
                        dateDebut.jour = jour;
                        dateDebut.mois = mois;
                        dateDebut.annee = annee;
                    }

                }

                if (lines.Length > 2 && !string.IsNullOrWhiteSpace(lines[2]))
                {
                    if (float.TryParse(lines[2], out float salaire))
                    {
                        salaireParMois = salaire;
                    }

                }
            }

            Calcul();


        }

        public void Calcul()
        {
            DateTime now = DateTime.Now;

            if (!IsWorkingHours(now))
            {
                float dailySalary = salaireParMois / 30;
                int workingSecondsPerDay = 7 * 60 * 60;
                salaireParSeconde = dailySalary / workingSecondsPerDay;

                DateTime startDateTime = new DateTime(dateDebut.annee, dateDebut.mois, dateDebut.jour, 9, 0, 0);
                DateTime endDateTime = now;

                int totalWorkingSeconds = 0;

                while (startDateTime < endDateTime)
                {
                    if (IsWorkingHours(startDateTime))
                    {
                        totalWorkingSeconds++;
                    }
                    startDateTime = startDateTime.AddSeconds(1);
                }

                DateTime startOfDay = new DateTime(now.Year, now.Month, now.Day, 9, 0, 0);
                int workingSecondsToday = 0;
                while (startOfDay < now)
                {
                    if (IsWorkingHours(startOfDay))
                    {
                        workingSecondsToday++;
                    }
                    startOfDay = startOfDay.AddSeconds(1);
                }

                int secondSinceStart = totalWorkingSeconds + workingSecondsToday;

                TotalGagner = salaireParSeconde * (float)secondSinceStart;
            }

            elapsedTime = TimeSpan.Zero;
        }

        public void Save()
        {
            try
            {
                // Utiliser le répertoire de base de l'application
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string contentDirectory = Path.Combine(baseDirectory, "Content");

                if (!Directory.Exists(contentDirectory))
                {
                    Directory.CreateDirectory(contentDirectory);
                }

                string dataFilePath = Path.Combine(contentDirectory, "data.txt");
                string text = TotalGagner.ToString("0.0000");
                File.WriteAllText(dataFilePath, text);
                Console.WriteLine($"TotalGagner écrit dans {dataFilePath}");

                string dateDebutFilePath = Path.Combine(contentDirectory, "dateDebut.txt");
                string dateDebutText = $"{dateDebut.jour} {dateDebut.mois} {dateDebut.annee}";
                File.WriteAllText(dateDebutFilePath, dateDebutText);
                Console.WriteLine($"dateDebut écrit dans {dateDebutFilePath}");

                string salaireParMoisFilePath = Path.Combine(contentDirectory, "salaireParMois.txt");
                string salaireParMoisText = salaireParMois.ToString("0.0000");
                File.WriteAllText(salaireParMoisFilePath, salaireParMoisText);
                Console.WriteLine($"salaireParMois écrit dans {salaireParMoisFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la sauvegarde des fichiers : {ex.Message}");
            }
        }

        private bool IsWorkingHours(DateTime now)
        {
            if (now.DayOfWeek < DayOfWeek.Monday || now.DayOfWeek > DayOfWeek.Friday)
            {
                return false;
            }

            TimeSpan startTime = new TimeSpan(9, 0, 0);
            TimeSpan endTime = new TimeSpan(17, 0, 0);
            TimeSpan lunchStart = new TimeSpan(13, 0, 0);
            TimeSpan lunchEnd = new TimeSpan(14, 0, 0);

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
            elapsedTime += gameTime.ElapsedGameTime;

            if (elapsedTime.TotalSeconds >= 1)
            {
                TotalGagner = (float)Math.Round(TotalGagner + salaireParSeconde, 5);
                string text = TotalGagner.ToString("0.0000") + " €";
                texte.Content = text;
                texte.Update(GraphicsDevice);

                elapsedTime = TimeSpan.Zero;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            texte.Draw(spriteBatch);
        }

    }
}