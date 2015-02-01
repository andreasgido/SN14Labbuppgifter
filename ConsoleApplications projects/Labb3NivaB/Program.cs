//  Laborationsuppgift 3 - Lönerevision nivå B
//  Labb3NivaB
//  Design: Andreas Gidö
//  Datum:      20150116
//  Reviderad:  20150118

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb3NivaB
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Lönerevision nivå B";

            do
            {
                // Rensa konsolfönstret före inmatning.
                Console.Clear();

                // Fältvariablar
                string titleNumbersOfSalaries = "Ange antal löner att mata in: ";
                int goodNumberOfSalaries = 0;
                
                // Metodanrop till metoden ReadInt() för att läsa in antal löner. 
                // Metoden ska returnera ett värde av typen int.
                goodNumberOfSalaries = ReadInt(titleNumbersOfSalaries);

                // Ny array för att lagra den returnerade array från metoden ReadSalaries().
                int[] goodArrayOfSalaries = new int[goodNumberOfSalaries];

                Console.WriteLine();

                // Kollar så att det minst är två löner att behandla.
                if (goodNumberOfSalaries >= 2)
                {
                    // Metodanrop till ReadSalaries() för att läsa in det antal löner som användaren angav.
                    goodArrayOfSalaries = ReadSalaries(goodNumberOfSalaries);

                    // Metodanrop till metoden ViewResult() där uträkningarna presenteras.
                    ViewResult(goodArrayOfSalaries);
                }           
                else
                {
                    string messageMoreSalaries = "Du måste mata in minst två löner för att göra en beräkning";
                    ViewMessage(messageMoreSalaries, true);
                }
                              		               
            } while (IsContinuing());
        }

        // Metod för validering av inmatat värde från användaren.
        // Giltigt värde som returneras är ett heltal av typen int.
        private static int ReadInt(string prompt)
        {
            int result = 0;
            string strTest = string.Empty;

            while (true)
                try
                {
                    Console.Write(prompt);
                    strTest = Console.ReadLine();
                    result = int.Parse(strTest);

                    if (result <= 0)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Fel! '{0}' kan inte tolkas som ett giltigt heltal.", result);
                        Console.ResetColor();
                        continue;
                    }
                    return result;
                }
                catch (Exception)
                {
                    string messageError = String.Format("Fel! '{0}' kan inte tolkas som ett heltal", strTest);                   
                    ViewMessage(messageError, true);                   
                }           
        }

        // Metod för att läsa in angivet antal löner till en array som seadn returneras till metodanropet.
        private static int[] ReadSalaries(int count)
        {
            // Initiera en array som ska innehålla samma antal löner som användaren angav.
            int[] salaries = new int[count];

            // Läser in lönerna och lägger dom i arrayen.
            for (int i = 0; i < count; i++)
            {
                string text = String.Format("{0} {1} :", "Ange lön nummer ", (i + 1));              
                salaries[i] = ReadInt(text);                       // ReadInt är samma metod för inläsning av heltal                                       
            }                                                      // som användes för att ange antal löner.
            
            return salaries;
        }

        // Metod för att visa resultaten av beräkningar som gjorts på inmatade löner.
        private static void ViewResult(int[] salaries)
        {

            // Utskrift av olika uträkningar från inmatade löner.
            Console.Write("\n-------------------------------\n");
            Console.WriteLine("{0}: {1, 13:c}", "Medianlön", GetMedian(salaries));
            Console.WriteLine("{0}: {1, 14:c}", "Medellön", salaries.Average());
            Console.WriteLine("{0}: {1, 9:c}", "Lönespridning", GetDispertion(salaries));
            Console.Write("-------------------------------");

            // Utskrift av orginal-arrayen i den ordning som användaren skrev.
            for (int i = 0; i < salaries.Length; i++)
            {
                if (i % 3 != 0)
                {
                    Console.Write("{0, 8}", salaries[i]);
                }
                else
                {
                    Console.WriteLine();
                    Console.Write("{0, 8}", salaries[i]);
                }
            }
            Console.WriteLine();
        }

        // Metod för att räkna ut lönespridningen.
        private static int GetDispertion(int[] source)
        {
            int maxSalary = source.Max();
            int minSalary = source.Min();
            int salaryDispertion = maxSalary - minSalary;

            return salaryDispertion;
        }

        // Metod för att räkna ut medianlönen.
        private static int GetMedian(int[] source)
        {
            double median;
            double median1;
            double median2;

            int indexOfArray = source.Length;       // Variabel för att få ut antal index i orginal-arrayen.
            int[] medianSalary = new int[source.Length];

            // Gör en kopia på orginal-arrayen.
            Array.Copy(source, medianSalary, indexOfArray);

            // Sortera kopian.
            Array.Sort(medianSalary);
            // Typomvandlar kopian från int till double.
            double[] doubleMedianSalary = Array.ConvertAll(medianSalary, Convert.ToDouble);

            if (doubleMedianSalary.Length% 2 == 1)
            {
                median = doubleMedianSalary[(indexOfArray / 2)];
            }
            else
            {
                median1 = doubleMedianSalary[(indexOfArray / 2)];
                median2 = doubleMedianSalary[(indexOfArray / 2 -1)];
                median = (median1 + median2) / 2;
            }
            int medianAnswer = (int)median;     // Typomvandlar svaret till ett heltal då metoden ska returnera ett heltal.
            return medianAnswer;
        }

        // Metod för att visa två olika meddelanden.
        private static void ViewMessage(string message, bool isError)
        {
            if (isError == false)
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(message);
                Console.ResetColor();
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(message);
                Console.ResetColor();
            }
        }

        // Metod för att kolla om använadren vill göra en ny inmatning eller avsluta programmet.
        private static bool IsContinuing()
        {
            string messageChoice = "\nTryck en tangent för ny beräkning - Esc avslutar.";
            // Här läggs metodanrop till ViewMessage istället. Ev ska meddelandet skickas med.....
            ViewMessage(messageChoice, false);

            if (Console.ReadKey(true).Key == ConsoleKey.Escape)
            {
                return false;
            }           
            return true;
        }
    }
}
