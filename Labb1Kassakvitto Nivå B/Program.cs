//  Laborationsuppgift 1 - Kassakvitto nivå B
//  Labb1KassakvittoNivaB
//  Design: Andreas Gidö
//  Datum:      20150105
//  Reviderad:  20150114
//  Reviderad:  20150116

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

            // do-while loop som låter användaren fortsätta med ny inmatning eller avsluta programmet.
            do
            {
                // Rensa konsolfönstret.
                Console.Clear();

                // Fältvariablar
                string titelTotalsumma = "Ange totalsumman    : ";
                string titelErhalletBelopp = "Ange erhållet belopp: ";
                double inmatadDouble = 0;
                int erhalletVarde = 0;
                int vaxelbelopp = 0;
                //int braVarde = 0;

                // Metodanrop för att läsa in ett flyttal från användare. 
                // Metoden returnerar ett flyttal.
                do
                {
                    inmatadDouble = LasPositivDouble(titelTotalsumma);
                    if (inmatadDouble < 1 )
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Fel! {0} kan inte tolkas som en giltig summa pengar.", inmatadDouble);
                        Console.ResetColor();
                    }
                } while (inmatadDouble < 1 );
               
                // Öresavrundning
                inmatadDouble = Math.Round(inmatadDouble, 2); // Avrundar till två decimaler för visningen. 
                int avrundadDouble = (int)Math.Round(inmatadDouble);

                // Rest efter öresavrundning.
                double restAvrundning = inmatadDouble - avrundadDouble;
                restAvrundning = Math.Round(restAvrundning, 2);
              
                erhalletVarde = LasInt(titelErhalletBelopp, avrundadDouble);
                                           
                //int total = (int)braDouble;
                vaxelbelopp = erhalletVarde - avrundadDouble;

                // Skriv ut kvittot.
                Console.WriteLine("\nKVITTO");
                Console.WriteLine("-------------------------------");
                Console.WriteLine("{0, -17}: {1, 12:c2}", "Totalt", inmatadDouble);
                Console.WriteLine("{0, -17}: {1, 12:c2}", "Öresavrundning", restAvrundning);
                Console.WriteLine("{0, -17}: {1, 12:c0}", "Att betala", avrundadDouble);
                Console.WriteLine("{0, -17}: {1, 12:c0}", "Kontant", erhalletVarde);
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

        // Metod för att läsa in inmatning av totalsumman från användaren.
        private static double LasPositivDouble(string titel)
        {
            string inmatning = string.Empty;
            double inmatatVarde = 0;
            while (true)
            {
                try
                {
                    Console.Write(titel);
                    inmatning = Console.ReadLine();
                    inmatatVarde = double.Parse(inmatning);
                    if (inmatatVarde <= 0)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Fel! '{0}' kan inte tolkas som en giltig summa pengar.", inmatning);
                        Console.ResetColor();
                        continue;
                    }
                 
                    return inmatatVarde;
                    

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
             
        // Metod för att läsa in inmatning av erhållet belopp från användaren.
        private static int LasInt(string titel, int belopp)
        {
            string inmatning = string.Empty;
            int beloppErhallet = 0;
            while (true)
            {
                try
                {
                    Console.Write(titel);
                    inmatning = Console.ReadLine();
                    beloppErhallet = int.Parse(inmatning);

                    // Om erhållet belopp är mindre än totalsumman får användaren ett nytt försök.
                    if (beloppErhallet < belopp)
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
        private static void DelaUppIFaktorer(int beloppVaxel)
        {
            int vaxelBelopp = beloppVaxel;
            int[] valorer = { 500, 100, 50, 20, 10, 5, 1 };        // En array som innehåller alla valörer.
            string pengar = string.Empty;
            int vaxel = 0;

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
