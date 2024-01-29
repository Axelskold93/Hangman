using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Security;
using System.Threading;

namespace Större_övning
{
    public class Program
    {
        public static void Main()
        {
            bool endGame = false;
            while (!endGame)
            {

                CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
                Console.Clear();
                int startMenuOption = ShowMenu("Hej och välkommen till \"Hänga Gubben\"!", new[]
                {

                "Singleplayer",
                "Multiplayer",
                "Avsluta"
                });
                if (startMenuOption == 0)
                {
                    Program.SinglePlayer();
                }
                else if (startMenuOption == 1)
                {
                    Program.MultiPlayer();
                }
                else if (startMenuOption == 2)
                {
                    Console.WriteLine("Tack för att du spelade!");
                    Thread.Sleep(1000);
                    Environment.Exit(0);
                }

            }
        }
        public static void SinglePlayer()
        {

            Random rnd = new Random();
            List<string> randomWords = new List<string> {"ELEFANT", "CLOWN", "YXA", "LOKOMOTIV", "TEKNIKHÖGSKOLAN", "GÖTEBORG", "HÄST", "NATIONALENCYKLOPEDI",
            "BLÅVITT", "LASERSVÄRD", "TAVLA", "BOKHYLLA", "SJUMANNATÄLT", "VEDTRÄ", "GÅS", "XYLOFON", "BLÅMES", "FUNKTIONÄR", "BLÅVAL", "LAMPA"};
            Console.Clear();
            Console.Write("Singleplayer!");
            Thread.Sleep(1000);
            Console.Clear();


            Console.WriteLine("Ett hemligt ord slumpas nu fram.");
            int rndIndex = rnd.Next(0, randomWords.Count);
            string randomWord = randomWords[rndIndex];


            Thread.Sleep(800);

            int maxAttempts = 10;//lägg till svårighetsgrad

            string guessedWord = new string('_', randomWord.Length);

            while (maxAttempts > 0)
            {

                Console.WriteLine("Gissningar:" + maxAttempts);
                Console.WriteLine("");
                Console.WriteLine("Gissa på en bokstav:");
                char yourGuess = Char.ToUpper(Console.ReadLine()[0]);


                bool found = false;

                for (int i = 0; i < randomWord.Length; i++)
                {
                    if (randomWord[i] == yourGuess)
                    {
                        guessedWord = guessedWord.Remove(i, 1).Insert(i, yourGuess.ToString());
                        found = true;
                    }
                }

                if (found)
                {
                    Console.Clear();
                    Console.WriteLine("Bra gissat! Bokstaven finns i ordet: " + guessedWord);

                }
                else
                {

                    Console.WriteLine("Tyvärr, bokstaven finns inte i ordet.");
                    maxAttempts--;

                }

                if (guessedWord.Equals(randomWord))
                {
                    Console.WriteLine("Grattis, du gissade rätt ord: " + randomWord);
                    break;
                }

                if (maxAttempts == 0)
                {
                    Console.WriteLine("Tyvärr, du har inga gissningar kvar. Rätt ord var: " + randomWord);
                }
            }
        }
        public static void MultiPlayer()
        {
            
            Console.WriteLine("Var god skriv in ditt ord:");
            string yourWord = Console.ReadLine().ToUpper();
            Console.Clear();
            Console.WriteLine("Tack! Då är det dags för spelare 2 att gissa!");

            int maxAttempts = 10;

            string guessedWord = new string('_', yourWord.Length);

            while (maxAttempts > 0)
            {
                Console.WriteLine("Gissningar:" +  maxAttempts);
                Console.WriteLine("Gissa på en bokstav:");
                char yourGuess = Char.ToUpper(Console.ReadLine()[0]);

                bool found = false;

                for (int i = 0; i < yourWord.Length; i++)
                {
                    if (yourWord[i] == yourGuess)
                    {
                        guessedWord = guessedWord.Remove(i, 1).Insert(i, yourGuess.ToString());
                        found = true;
                    }
                }

                if (found)
                {
                    Console.Clear();
                    Console.WriteLine("Bra gissat! Bokstaven finns i ordet: " + guessedWord);
                }
                else
                {
                    Console.WriteLine("Tyvärr, bokstaven finns inte i ordet.");
                    maxAttempts--;
                }

                if (guessedWord.Equals(yourWord))
                {
                    Console.WriteLine("Grattis, du gissade rätt ord: " + yourWord);
                    break;
                }

                if (maxAttempts == 0)
                {
                    Console.WriteLine("Tyvärr, du har inga gissningar kvar. Rätt ord var: " + yourWord);
                    int PlayAgain = ShowMenu("Do you want to play again?", new[]
                    {
                      "Ja",
                      "Nej"
                    });
                    if (PlayAgain == 0)
                    {
                        continue;
                    }
                    else if (PlayAgain == 1)
                    {
                        Environment.Exit(0);
                    }
                }
            }
        }
        
        
        public static int ShowMenu(string prompt, IEnumerable<string> options)
        {
            if (options == null || options.Count() == 0)
            {
                throw new ArgumentException("Cannot show a menu for an empty list of options.");
            }

            Console.WriteLine(prompt);

            // Hide the cursor that will blink after calling ReadKey.
            Console.CursorVisible = false;

            // Calculate the width of the widest option so we can make them all the same width later.
            int width = options.MaxBy(option => option.Length).Length;

            int selected = 0;
            int top = Console.CursorTop;
            for (int i = 0; i < options.Count(); i++)
            {
                // Start by highlighting the first option.
                if (i == 0)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;
                }

                var option = options.ElementAt(i);
                // Pad every option to make them the same width, so the highlight is equally wide everywhere.
                Console.WriteLine("- " + option.PadRight(width));

                Console.ResetColor();
            }
            Console.CursorLeft = 0;
            Console.CursorTop = top - 1;

            ConsoleKey? key = null;
            while (key != ConsoleKey.Enter)
            {
                key = Console.ReadKey(intercept: true).Key;

                // First restore the previously selected option so it's not highlighted anymore.
                Console.CursorTop = top + selected;
                string oldOption = options.ElementAt(selected);
                Console.Write("- " + oldOption.PadRight(width));
                Console.CursorLeft = 0;
                Console.ResetColor();

                // Then find the new selected option.
                if (key == ConsoleKey.DownArrow)
                {
                    selected = Math.Min(selected + 1, options.Count() - 1);
                }
                else if (key == ConsoleKey.UpArrow)
                {
                    selected = Math.Max(selected - 1, 0);
                }

                // Finally highlight the new selected option.
                Console.CursorTop = top + selected;
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.White;
                string newOption = options.ElementAt(selected);
                Console.Write("- " + newOption.PadRight(width));
                Console.CursorLeft = 0;
                // Place the cursor one step above the new selected option so that we can scroll and also see the option above.
                Console.CursorTop = top + selected - 1;
                Console.ResetColor();
            }

            // Afterwards, place the cursor below the menu so we can see whatever comes next.
            Console.CursorTop = top + options.Count();

            // Show the cursor again and return the selected option.
            Console.CursorVisible = true;
            return selected;
        }
        public static void Delay(int milliSeconds)
        {
            Thread.Sleep(milliSeconds);
        }


        [TestClass]
        public class ProgramTests
        {
            [TestMethod]
            public void ExampleTest()
            {
                using FakeConsole console = new FakeConsole("First input", "Second input");
                Program.Main();
                Assert.AreEqual("Hello!", console.Output);
            }
        }
    }
}
