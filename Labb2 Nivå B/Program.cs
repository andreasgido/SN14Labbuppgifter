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

                // Kontrollera så det returnerade värdet är ett udda värde.
                if (basen % 2 == 1)
                {
                    // Anropar metoden för att rita ut triangeln.
                    // Till metoden skickas argumentet med det udda talet som
                    // användaren matade in.
                    DrawTriangel(basen);
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nFel! Det inmatade värdet är inte ett udda heltal mellan {0} och {1}", MinBas, MaxBas);
                    Console.ResetColor();
                } 

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
            while (true)
            {
                try
                {
                    Console.Write(titel);
                    number = byte.Parse(Console.ReadLine());                  
                }
                catch (Exception)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nFel! {0} kan inte tolkas som ett heltal", number);
                    Console.ResetColor();
                }
                return number;
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
