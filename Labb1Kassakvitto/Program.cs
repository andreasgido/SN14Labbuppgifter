//  Laborationsuppgift 1 - Kassakvitto nivå A
//  Labb1Kassakvitto
//  Design: Andreas Gidö
//  Datum:      20141218
//  Reviderad:  20150104

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Program för att lämna ut ett kvitto och även dela upp eventuell växel i olika valörer.
namespace Labb1Kassakvitto
{
    class Program
    {
        // Start av programmet.
        static void Main(string[] args)
        {
            Console.Title = "Växelpengar - nivå A";
            // Variabler för inmatningen.
            double inmatadSumma = 0d;
            int beloppErhallet = 0;           

            // Läs in inmatning av Totalsumman.
            // Vid ej godkänd inmatning får användaren ett felmeddelande och
            // får sedan ett nytt försök att mata in korrekt värde.
            while (true)
            {
                try
                {
                    Console.Write("Ange totalsumman    : ");
                    inmatadSumma = double.Parse(Console.ReadLine());
                    break;
                }
                catch (Exception)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Fel! Totalsumman är felaktig.");
                    Console.ResetColor();
                }                 
            }

            // Jämför om totalsumman är mindre än 1 kr.
            // Vid för litet inmatat belopp får användaren ett felmeddelande och
            // sedan avslutas programmet.
            if (inmatadSumma < 1)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Fel! Totalsumman är för liten. Köpet kunde inte genomföras.");
                Console.ResetColor();
                return;    //Hoppar ur programmet.
            }
          
            // Läs in inmatning av Erhållet belopp.
            // Vid ej godkänd inmatning får användaren ett felmeddelande och
            // får sedan ett nytt försök att mata in korrekt värde.
            while (true)
            {
                try
                {
                    Console.Write("Ange erhållet belopp: ");                  
                    beloppErhallet = int.Parse(Console.ReadLine());            
                    break;
                }
                catch (Exception)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Fel! Erhållet belopp är felaktig.");
                    Console.ResetColor();              
                }                 
            }

            // Öresavrundning
            inmatadSumma = Math.Round(inmatadSumma, 2); // Avrundar till två decimaler för visningen. 
            int summaTotal = (int)Math.Round(inmatadSumma);

            // Rest efter öresavrundning.
            double restAvrundning = summaTotal - inmatadSumma;
            restAvrundning = Math.Round(restAvrundning, 2);
            
            // Jämför om erhållet belopp är mindre än totalsumman.
            // Vid mindre inmatat belopp än totalsumman får användaren ett felmeddelande och
            // sedan avslutas programmet.
            if (beloppErhallet < summaTotal)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Fel! Totalsumman är för liten. Köpet kunde inte genomföras.");
                Console.ResetColor();
                return;    //Hoppar ur programmet.
            }
            
            // Räkna fram växelbeloppet.
            int vaxelBelopp = beloppErhallet - summaTotal;            
           
            // Skriv ut kvittot.
            string kolon = ":";
            string valuta = "kr";

            Console.WriteLine("\nKVITTO");
            Console.WriteLine("-------------------------------");
            Console.WriteLine("{0} {1, 11} {2, 9} {3}","Totalt", kolon, inmatadSumma, valuta);
            Console.WriteLine("{0} {1, 3} {2, 9:f2} {3}","Öresavrundning", kolon, restAvrundning, valuta);
            Console.WriteLine("{0} {1, 7} {2, 9} {3}","Att betala", kolon, summaTotal, valuta);
            Console.WriteLine("{0} {1, 10} {2, 12:c0}","Kontant", kolon, beloppErhallet);
            Console.WriteLine("{0} {1, 9} {2, 9} {3}","Tillbaka", kolon, vaxelBelopp, valuta);
            Console.WriteLine("-------------------------------\n");

            // Växelpengar fördelat på sedlar och mynt.
            
            int vaxelTillbaka = vaxelBelopp;

            int antalFemhundralappar = vaxelTillbaka / 500;
            vaxelTillbaka %= 500;

            int antalHundralappar = vaxelTillbaka / 100;
            vaxelTillbaka %= 100;

            int antalFemtiolappar = vaxelTillbaka / 50;
            vaxelTillbaka %= 50;

            int antalTjugolappar = vaxelTillbaka / 20;
            vaxelTillbaka %= 20;

            int antalTiokronor = vaxelTillbaka / 10;
            vaxelTillbaka %= 10;

            int antalFemkronor = vaxelTillbaka / 5;
            vaxelTillbaka %= 5;

            int antalEnkronor = vaxelTillbaka;

            // Skriv ut växeln i de valörer som ska vara med. Övriga visas ej.
            if (antalFemhundralappar > 0)
            {
                Console.WriteLine("{0, 11} {1, 6} {2}","500-lappar", ":", antalFemhundralappar);
            }

            if (antalHundralappar > 0)
            {
                Console.WriteLine("{0, 11} {1, 6} {2}","100-lappar", ":", antalHundralappar);
            }

            if (antalFemtiolappar > 0)
            {
                Console.WriteLine("{0, 11} {1, 6} {2}","50-lappar", ":", antalFemtiolappar);
            }

            if (antalTjugolappar > 0)
            {
                Console.WriteLine("{0, 11} {1, 6} {2}","20-lappar", ":", antalTjugolappar);
            }

            if (antalTiokronor > 0)
            {
                Console.WriteLine("{0, 11} {1, 6} {2}","10-kronor", ":", antalTiokronor);
            }

            if (antalFemkronor > 0)
            {
                Console.WriteLine("{0, 11} {1, 6} {2}","5-kronor", ":", antalFemkronor);
            }

            if (antalEnkronor > 0)
            {
                Console.WriteLine("{0, 11} {1, 6} {2}","1-kronor", ":", antalEnkronor);
            }
        }
    }
}
