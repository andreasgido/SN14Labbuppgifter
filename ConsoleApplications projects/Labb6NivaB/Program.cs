using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb6NivaB
{
    public class Program
    {
        // Klassmedlem

        static Random randomValue = new Random();
        
        static void Main(string[] args)
        {
            do
            {
                Console.Clear();

                Solid[] solidArray = RandomizeSolids();

                Array.Sort(solidArray);

                ViewSolids(solidArray);

                // Utskrift där användaren kan välja mellan att göra en ny beräkning eller avsluta programmet.           
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\nTryck valfri tangent för att börja om - ESC avslutar.");
                Console.ResetColor();
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }

        private static Solid[] RandomizeSolids()
        {
            // Slumpa antalet solider som ska beräknas
            Random random = new Random();
            Solid[] arrayOfSolids = new Solid[random.Next(5, 21)];

            // Fyller på arrayen med det slumpade antalet Solider
            for (int i = 0; i < arrayOfSolids.Length; i++)
            {                
                switch ((SolidType)random.Next(0, 2))   // Slumpar fram vilken typ av solid som ska fylla på arrayen
                {                                       // Antingen 0 för CircularCone eller 1 för Cylinder
                    case SolidType.CircularCone:                       
                        arrayOfSolids[i] = new CircularCone(GenerateRandomDouble(), GenerateRandomDouble());
                        break;

                    case SolidType.Cylinder:
                        arrayOfSolids[i] = new Cylinder(GenerateRandomDouble(), GenerateRandomDouble());
                        break;
                }
            }
            return arrayOfSolids;
        }

        private static void ViewSolids(Solid[] solids)
        {
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("╔═════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                                                                 ║");
            Console.WriteLine("║                         Solida Volymer                          ║");
            Console.WriteLine("║                                                                 ║");
            Console.WriteLine("╚═════════════════════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine(" {0, -10} {1, 8} {2, 8} {3, 12} {4, 11} {5, 11}", "Solid", "Radie", "Höjd", "Volym", "Basarea", "Ytarea");
            Console.WriteLine(" ═════════════════════════════════════════════════════════════════");

            foreach (var solid in solids)
            {
                Console.WriteLine(solid.ToString());
            }
        }
        
        // Metod för att generera slumpade flyttal.
        private static double GenerateRandomDouble()
        {
            const double minValue = 5;
            const double maxValue = 101;            
            double randomResult = minValue + randomValue.NextDouble()  * (maxValue - minValue);
            return randomResult;
        }
    }
}
