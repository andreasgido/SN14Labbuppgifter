//  Laborationsuppgift 3 - Lönerevision nivå A
//  Labb3NivaA
//  Design: Andreas Gidö
//  Datum:      20150109
//  Reviderad:  20150112

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb3NivaA
{
    class Program
    {
        static void Main(string[] args)
        {
            // Titel på konsolfönstret.
            Console.Title = "Lönerevision Nivå A";
            
            // do-while loop som låter användaren göra ny inmatning eller avsluta programmet.
            do
            {
                // Rensa konsolfönstret för inmatning.
                Console.Clear();

                // Fältvariablar
                string titelAntalLoner = "Ange antal löner att mata in: ";            
                int braAntalLoner = 0;

                // Metodanrop till metoden ReadInt för att läsa in antal löner. 
                // Metoden ska returnera ett värde av typen int.
                braAntalLoner = ReadInt(titelAntalLoner);

                // Kontroll så att antal löner som ska beräknas är två eller flera.
                // Vid mindre än två får användaren ett felmeddelande annars fortsätter programmet.
                if (braAntalLoner >= 2)
                {
                    // Metodanrop till metoden ProcessSalaries.
                    ProcessSalaries(braAntalLoner);                   
                }

                else
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Du måste mata in minst två löner för att göra en beräkning");
                    Console.ResetColor();                 
                }
                
                // Utskrift där användaren kan välja mellan att göra en ny beräkning eller avsluta programmet.           
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\nTryck tangent för att fortsätta - ESC avslutar.");
                Console.ResetColor();

            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);          
        }

        // Metod för validering av inmatat värde från användaren.
        // Giltigt värde som returneras är ett heltal av typen int.
        private static int ReadInt(string prompt)
        {
            int resultat = 0;
            string strTest = string.Empty;

            while (true)
                try
                {                    
                    Console.Write(prompt);
                    strTest = Console.ReadLine();                   
                    resultat = int.Parse(strTest);
                    break;                                                         
                }

                catch (Exception)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Fel! '{0}' kan inte tolkas som ett heltal", strTest);
                    Console.ResetColor();
                }

            return resultat;    // Returnerar validerad inmatning till metodanropet.
        }

        // Metod för att göra beräkningar/utskrifter på inmatade löner.
        private static void ProcessSalaries(int antal)
        {
            string prompt  = "Ange lön nummer ";
            // Initiera en array som ska innehålla samma antal löner som användaren angav.
            int[] loner = new int[antal];
            
            // Läser in lönerna och lägger dom i arrayen.
            for (int i = 0; i < antal; i++)
            
            {    
                loner[i] = ReadInt(prompt + (i + 1) + ": ");    // ReadInt är samma metod för inläsning av heltal                                       
            }                                                   // som användes för att ange antal löner.

            Console.WriteLine("\n------------------------------");

            // Uträkning av medellönen.
            double lonMedel = loner.Average();           
            int medellon = (int)Math.Round(lonMedel);

            //Console.WriteLine(medellon);

            // Uträkning av spridningen.
            int maxlon = loner.Max();
            int minlon = loner.Min();
            int spridning = maxlon - minlon;
            //Console.WriteLine(spridning);

            // Gör en kopia av arrayen för att sortera den och sedan
            // räkna ut medianlönen.
            int[] lonerSorted = new int[antal];     // ny array.
            Array.Copy(loner, lonerSorted, antal);  // gör en kopia av orginal-arrayen.

            Array.Sort(lonerSorted);        // Sortera kopian.

            int medianlon;
            // Om antal löner är udda.
            if (lonerSorted.Length % 2 == 1)
            {
                medianlon = lonerSorted[(antal / 2)];
            }

            // Om antal löner är jämna.
            else
            {
                int tal1 = lonerSorted[(antal / 2)];
                int tal2 = lonerSorted[(antal / 2 - 1)];
                medianlon = (tal1 + tal2) / 2;
            }

            // Utskrift av olika uträkningar från inmatade löner.
            Console.WriteLine("{0}: {1, 13:C0}", "Medianlön", medianlon);
            Console.WriteLine("{0}: {1, 14:C0}", "Medellön", medellon);
            Console.WriteLine("{0}: {1, 9:c0}", "Lönespridning", spridning);
            Console.Write("------------------------------");

            // Skriv ut orginal-arrayen som inte är sorterad. Radbrytning efter var treje värde.
            for (int i = 0; i < loner.Length; i++)
            {
                if ( i % 3 != 0)
                {
                    Console.Write("{0, 8}", loner[i]);
                }

                else
                {
                    Console.WriteLine();
                    Console.Write("{0, 8}", loner[i]);
                }
            }
            Console.WriteLine();
        }
    }
}
