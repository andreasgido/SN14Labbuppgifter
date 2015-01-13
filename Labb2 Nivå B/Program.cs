using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2NivaB
{
    class Program
    {
        // Deklarera fältvariabler
        const byte minBas = 1;
        const byte maxBas = 79;
        const string titel = "Ange det udda antalet asterisker (max 79) i triangelns bas: ";
        
        // Mainmetoden
        static void Main(string[] args)
        {

            do
            {

                // Anropar metoden för inläsning av tal från användaren
                // Till metoden skickas argumentet med textsträngen till användaren
                // Metoden ska returnera ett udda heltal som en byte
                byte basen = ReadOddByte(titel);

                // Anropar metoden för att rita ut triangeln
                // Till metoden skickas argumentet med det udda talet som
                // användaren matade in
                DrawTriangel(basen);

                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Tryck tangent för att fortsätta -ESC avslutar.");
                Console.ResetColor();

           

            } while (Console.ReadKey().Key != ConsoleKey.Escape);
            Console.Clear();
        }

        // Metod för att läsa in ett udda heltal från användaren
        private static byte ReadOddByte(string titel)
        {

            byte number = 0;

            while (true)
            {
                try
                {

                    Console.Write(titel);
                    number = byte.Parse(Console.ReadLine());

                    if (number % 2 == 1)
                    {
                        break;
                    }

                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("\nFel! Det inmatade värdet är inte ett udda heltal mellan {0} och {1}", minBas, maxBas);
                        Console.ResetColor();
                    }
                    
                }

                catch (Exception)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nFel! {0} kan inte tolkas som ett heltal", number);
                    Console.ResetColor();
                }  
             
                
            }
                     
            return number;
                  
        }

        // Metod för att rita ut en triangel
        private static void DrawTriangel(byte num)
        {

            for (int i = 0; i < num; i += 2)
            {

                for (int space = num - i; space >= 0; space -= 2)
                {
                    Console.Write(" ");
                }

                for (int k = 0; k <= i; k++)
                {
                    Console.Write("*");
                }

                Console.WriteLine();
            }

        }
    }
}
