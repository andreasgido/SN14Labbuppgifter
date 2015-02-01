using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2NivaB
{
    class Program
    {
        // Deklarera fältvariablar.
        const byte MinBas = 1;
        const byte MaxBas = 79;
        const string Titel = "Ange det udda antalet asterisker (max 79) i triangelns bas: ";
        
        // Mainmetoden.
        static void Main(string[] args)
        {  
            do
            {
                // Rensa konsolfönstret.
                Console.Clear();

                // Anropar metoden för inläsning av tal från användaren.
                // Till metoden skickas argumentet med textsträngen till användaren.
                // Metoden ska returnera ett udda heltal som en byte.
                byte basen = ReadOddByte(Titel);

                // Anropar metoden DrawTriangle med argument innehållande det udda talet som användaren matade in.
                DrawTriangel(basen);

                // Skriv ut val till användaren för att fortsätta inmatningen eller avsluta programmet.
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Tryck tangent för att fortsätta - ESC avslutar.");
                Console.ResetColor();
          
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }

        // Metod för att läsa in ett udda heltal från användaren.
        private static byte ReadOddByte(string titel)
        {
            byte number = 0;
            string strTrest = "";
            while (true)
            {
                try
                {
                    Console.Write(titel);
                    strTrest = Console.ReadLine();
                    number = byte.Parse(strTrest);
                  
                    if (number % 2 == 0 || (number < MinBas && number > MaxBas))
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("\nFel! Det inmatade värdet är inte ett udda heltal mellan {0} och {1}", MinBas, MaxBas);
                        Console.ResetColor();
                        continue;
                    }
                    else
                    {
                        return number;
                    }
                }
                catch (Exception)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nFel! {0} kan inte tolkas som ett heltal", strTrest);
                    Console.ResetColor();
                }              
            }                                                 
        }

        // Metod för att rita ut en triangel.
        private static void DrawTriangel(byte antalTecken)
        {
            // for-loop som håller koll på antalet rader i triangeln.
            for (int raknare = 0; raknare < antalTecken; raknare += 2)
            {
                // for-loop som skriver ut mellanrum på varje rad som skrivs ut. 
                for (int raknareMellanrum = antalTecken - raknare; raknareMellanrum >= 0; raknareMellanrum -= 2)
                {
                    Console.Write(" ");     // mellanrum.
                }
                // for-loop som skriver ut tecken(asterisker i detta fall) på varje rad.
                for (int raknareTecken = 0; raknareTecken <= raknare; raknareTecken++)
                {
                    Console.Write("*");     // tecken.
                }
                Console.WriteLine();
            }
        }
    }
}
