using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2NivaA
{
    class Program
    {
        static void Main(string[] args)
        {
            // Yttre loopen för raderna
            for (int rader = 0; rader < 25; rader++)
            {
                // Resten av beräkningen (rader / 2) blir antingen 0 eller 1. 
                // if-satsen kollar om det är lika med 1.
                // Detta ger en förskjutning av varannan rad med ett blanksteg.
                if (rader % 2 == 1)
                {
                    Console.Write(" ");
                }

                // Resten av beräkningen (rader / 3) blir antingen 0, 1 eller 2.
                // Switch-satsen byter textfärger.
                switch (rader % 3)
                {
                    case 0:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;

                    case 1:
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        break;

                    case 2:
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;

                    default:
                        break;
                }

                // Inre loopen för kolumnerna.
                for (int kolumn = 0; kolumn < 39; kolumn++)
                {
                    Console.Write("* ");
                }

                // Radbrytning.
                Console.WriteLine();
                
            }
            // Ställer tillbaka textfärgen till default.
            Console.ResetColor();
        }
    }
}
