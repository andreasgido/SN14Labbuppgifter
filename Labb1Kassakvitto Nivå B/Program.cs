//  Laborationsuppgift 1 - Kassakvitto nivå B
//  Labb1KassakvittoNivaB
//  Design: Andreas Gidö
//  Datum:      20150105
//  Reviderad:  20150114

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb1KassakvittoNivaB
{
    class Program
    {
        static void Main(string[] args)
        {
            // Titel i konsolfönstret.
            Console.Title = "Växelpengar - nivå B";

            // do-while loop som låter användaren fortsätta mata in nya löner eller avsluta programmet.
            do
            {
                // Rensa konsolfönstret.
                Console.Clear();

                // Fältvariablar
                string titelTotalsumma = "Ange totalsumman    : ";
                string titelErhalletBelopp = "Ange erhållet belopp: ";
                double braTotalsumma = 0;
                uint braErhalletBelopp = 0;
                uint vaxelbelopp = 0;

                // Metodanrop för att läsa in ett flyttal från användare. 
                // Metoden returnerar ett validerat flyttal.
                braTotalsumma = LasPositivDouble(titelTotalsumma);

                // Öresavrundning
                braTotalsumma = Math.Round(braTotalsumma, 2); // Avrundar till två decimaler för visningen. 
                int summaTotal = (int)Math.Round(braTotalsumma);

                // Rest efter öresavrundning.
                double restAvrundning = summaTotal - braTotalsumma;
                restAvrundning = Math.Round(restAvrundning, 2);

                // Metodanrop för att läsa in ett heltal från användare. 
                // Metoden returnerar ett validerat heltal.
                braErhalletBelopp = LasUInt(titelErhalletBelopp, braTotalsumma);
        
                uint totalsumma = (uint)braTotalsumma;
                vaxelbelopp = braErhalletBelopp - totalsumma;

                // Skriv ut kvittot.
                Console.WriteLine("\nKVITTO");
                Console.WriteLine("-------------------------------");
                Console.WriteLine("{0, -17}: {1, 12:c2}", "Totalt", braTotalsumma);
                Console.WriteLine("{0, -17}: {1, 12:c2}", "Öresavrundning", restAvrundning);
                Console.WriteLine("{0, -17}: {1, 12:c0}", "Att betala", braErhalletBelopp);
                Console.WriteLine("{0, -17}: {1, 12:c0}", "Kontant", braErhalletBelopp);
                Console.WriteLine("{0, -17}: {1, 12:c0}", "Tillbaka", vaxelbelopp);
                Console.WriteLine("-------------------------------\n");

                // Metodanrop för att dela upp växelpengar i olika valörer
                // och sedan skriva ut enbart de valörer som växeln innehåller.
                DelaUppIFaktorer(vaxelbelopp);

                // Utskrift där användaren kan välja mellan att göra en ny beräkning eller avsluta programmet.           
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\nTryck tangent för ny beräkning - ESC avslutar.");
                Console.ResetColor();

            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }

        // Metod för att läsa in och validera inmatning av totalsumman från användaren.
        private static double LasPositivDouble(string titelTotalsumman)
        {
            string inmatning = string.Empty;
            double inmatadSumma = 0;
            while (true)
            {
                try
                {
                    Console.Write(titelTotalsumman);
                    inmatning = Console.ReadLine();
                    inmatadSumma = double.Parse(inmatning);

                    // Öresavrundning
                    inmatadSumma = Math.Round(inmatadSumma, 2); // Avrundar till två decimaler för visningen. 
                    int summaTotal = (int)Math.Round(inmatadSumma);

                    // Rest efter öresavrundning.
                    double restAvrundning = summaTotal - inmatadSumma;
                    restAvrundning = Math.Round(restAvrundning, 2); 

                    // Om inmatad summa är mindre än 1 krona får användaren ett nytt försök.
                    if (inmatadSumma < 1)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Fel! {0} kan inte tolkas som en giltig summa pengar.", inmatadSumma);
                        Console.ResetColor();                     
                    }
                    else
                    {
                        return inmatadSumma;
                    }                   
                }               
                catch (Exception)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Fel! '{0}' kan inte tolkas som en giltig summa pengar.", inmatning);
                    Console.ResetColor();
                }             
            }           
        }
        
        // Metod för att läsa in och validera inmatning av erållet belopp från användaren.
        private static uint LasUInt(string titel, double totalsumman)
        {
            string inmatning = string.Empty;
            uint beloppErhallet = 0;
            while (true)
            {
                try
                {
                    Console.Write(titel);
                    inmatning = Console.ReadLine();
                    beloppErhallet = uint.Parse(inmatning);

                    // Om erhållet belopp är mindre än totalsumman får användaren ett nytt försök.
                    if (beloppErhallet < totalsumman)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Fel! {0:c} är ett för litet belopp.", beloppErhallet);
                        Console.ResetColor();                    
                    }
                    else
                    {
                        return beloppErhallet;
                    }                  
                }
                catch (Exception)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Fel! '{0}' kan inte tolkas som en giltig summa pengar.", inmatning);
                    Console.ResetColor();
                }              
            }                              
        }
        
        // Metod för att dela upp växelsumman i olika valörer.
        private static void DelaUppIFaktorer(uint beloppVaxel)
        {
            uint vaxelBelopp = beloppVaxel;
            uint[] valorer = { 500, 100, 50, 20, 10, 5, 1 };        // En array som innehåller alla valörer.
            string pengar = string.Empty;
            uint vaxel = 0;

            for (int i = 0; i < valorer.Length; i++)
            {
                // Detta villkor avgör när valören är sedlar eller mynt.
                if (valorer[i] > 10)
                {
                    pengar = "-lappar";
                }
                else
                {
                    pengar = "-kronor";
                }

                vaxel = vaxelBelopp / valorer[i];
                vaxelBelopp %= valorer[i];
                
                // Skriver ut växeln tillbaka i ev sedlar och mynt.
                if ( vaxel != 0 )
                {
                    Console.WriteLine("{0, 3}{1, -14}: {2}", valorer[i], pengar, vaxel);                   
                }                                                                           
            }
            Console.WriteLine();
        }               
    }
}
