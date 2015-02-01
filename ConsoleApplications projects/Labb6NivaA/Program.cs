using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb6
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Solida Volymer Nivå A";

            // do-while loop som körs tills användaren gjort ett val ur menyn eller väljer att avsluta programmet.
            // Loopen hanterar även fel vid inmatningen för menyvalet.
            do
            {
                Console.Clear();                  
               
                int userChoice;

                // Anropar en metod för visning av en meny till användaren.
                ViewMenu();

                // Kontrollerar om användaren angivet 0, 1 eller 2. Vid annan inmatning för användaren ett felmeddelande.
                if (int.TryParse(Console.ReadLine(), out userChoice) && userChoice >= 0 && userChoice <= 2)
                {
                    switch (userChoice)
                    {
                        // Valet får att avsluta programmet.
                        case 0:
                            {
                                return;
                            }

                        // Valet för att göra beräkningar på en kon.
                        case 1:
                            {
                                Console.Clear();
                                Console.BackgroundColor = ConsoleColor.DarkGreen;
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine(" ╔══════════════════════════════════════════════════╗ ");
                                Console.WriteLine(" ║                      Kon                         ║ ");
                                Console.WriteLine(" ╚══════════════════════════════════════════════════╝ ");
                                Console.ResetColor();

                                ViewSolidDetail(CreateSolid(SolidType.CircularCone));
                                break;
                            }

                        // Valet för att göra beräkningar på en cylinder.
                        case 2:
                            {
                                Console.Clear();
                                Console.BackgroundColor = ConsoleColor.DarkGreen;
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine(" ╔══════════════════════════════════════════════════╗ ");
                                Console.WriteLine(" ║                    Cylinder                      ║ ");
                                Console.WriteLine(" ╚══════════════════════════════════════════════════╝ ");
                                Console.ResetColor();

                                ViewSolidDetail(CreateSolid(SolidType.Cylinder));
                                break;
                            }

                        default:
                            break;
                    }
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(" Fel! Du måste ange ett nummer mellan 0 - 2.");
                    Console.ResetColor();
                }
                  
                // Utskrift där användaren kan välja mellan att göra en ny beräkning eller avsluta programmet.           
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\nTryck valfri tangent för att börja om - ESC avslutar.");
                Console.ResetColor();

            }while (Console.ReadKey(true).Key != ConsoleKey.Escape);
           
        }

        // Metod för att skapa en form beroende på vilket argument som skickats in.
        private static Solid CreateSolid(SolidType solidType)
        {           
            double radius = ReadDoubleGreaterThanZero(" Ange radien (r): ");
            double height = ReadDoubleGreaterThanZero(" Ange höjden (h): ");

            // Mattias lösning.....
            //Solid solid = null;
            //switch(solidType)
            //{
            //    case SolidType.CircularCone:
            //        solid = new CircularCone(radius, height);
            //        break;
            //    case SolidType.Cylinder:
            //        solid = new Cylinder(radius, height);
            //        break;
            //    default:
            //        throw new Exception();
            //}

            //return solid;

            if (solidType == SolidType.CircularCone)
            {
                CircularCone newCone = new CircularCone(radius, height);
                return newCone;
            }
                    
            Cylinder newCylinder = new Cylinder(radius, height);
            return newCylinder;                                       
        }

        // Metod för att visa en meny för användaren.
        // Inga inmatningar gör i denna metod.
        private static void ViewMenu()
        {
           
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" ╔══════════════════════════════════════════════════╗ ");
            Console.WriteLine(" ║                                                  ║ ");
            Console.WriteLine(" ║                  Solida Volymer                  ║ ");
            Console.WriteLine(" ║                                                  ║ ");
            Console.WriteLine(" ╚══════════════════════════════════════════════════╝ ");
            Console.ResetColor();
            Console.WriteLine("\n 0. Avsluta.");
            Console.WriteLine("\n 1. Kon.");
            Console.WriteLine("\n 2. Cylinder.");
            Console.WriteLine("\n ════════════════════════════════════════════════════");
            Console.Write(" Ange ditt menyval [0-2]: ");
        }

        // Metod för att visa resultatet för beräkningarna gjorda på den valda formen.
        private static void ViewSolidDetail(Solid solid)
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" ╔══════════════════════════════════════════════════╗ ");
            Console.WriteLine(" ║                    Detaljer                      ║ ");
            Console.WriteLine(" ╚══════════════════════════════════════════════════╝ ");
            Console.ResetColor();
            Console.WriteLine(solid.ToString());
        }

        // Använd denna metod till inläsning av värden till de olika figurerna.
        private static double ReadDoubleGreaterThanZero(string prompt)
        {
            double goodChoice;
            while (true)
            {                             
                try
                {
                    Console.Write(prompt);
                    goodChoice = double.Parse(Console.ReadLine());
                    if (goodChoice <= 0)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(" Fel! Ange ett flyttal större än noll.");
                        Console.ResetColor();
                        continue;
                    }

                    return goodChoice;
                }
                catch
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(" Fel! Ange ett flyttal större än noll.");
                    Console.ResetColor();
                }              
            }           
        }
    }
}
