//  Laborationsuppgift 5 - Kylskåp nivå A
//  Labb5NivaA
//  Design: Andreas Gidö
//  Datum:      20150121
//  Reviderad:  20150122

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb5NivaA
{
    class Program
    {
        // Fält.
        private const string HorisontalLine ="═";

        static void Main(string[] args)
        {
            // Test 1.
            Cooler cooler1 = new Cooler();
            ViewTestHeader("Test1.\nTest av standardkonstruktorn.\n");
            Console.WriteLine(cooler1.ToString());
           
            // Test 2.
            Cooler cooler2 = new Cooler(24.5m , 4);
            ViewTestHeader("Test2.\nTest av konstruktorn med 2 parametrar, (24,5 och 4).\n");
            Console.WriteLine(cooler2.ToString());

            // Test3.
            Cooler cooler3 = new Cooler(19.5m, 4, true, false);
            ViewTestHeader("Test3.\nTest av konstruktorn med 4 parametrar, (19,5 , 4 , True och False).\n");
            Console.WriteLine(cooler3.ToString());

            // Test4.
            Cooler cooler4 = new Cooler(5.3m, 4, true, false);
            ViewTestHeader("Test4.\nTest av kylning med metoden Tick().\n");
            Run(cooler4, 10);

            // Test5.
            Cooler cooler5 = new Cooler(5.3m, 4, false, false);
            ViewTestHeader("Test5.\nTest av kylning med metoden Tick(), vara avslaget och med stängd dörr.\n");
            Run(cooler5, 10);

            // Test6.
            Cooler cooler6 = new Cooler(10.2m, 4, true, true);
            ViewTestHeader("Test6.\nTest av påslaget kylskåp och öppen dörr med metoden Tick().\n");
            Run(cooler6, 10);

            // Test7.
            Cooler cooler7 = new Cooler(19.7m, 4, false, true);
            ViewTestHeader("Test7.\nTest av avslaget kylskåp och öppen dörr med metoden Tick().\n");
            Run(cooler7, 10);

            // Test8. Test av egenskaperna.
            // 2 styck try-catch satser för att fånga 2 olika undantag.
            ViewTestHeader("Test8.\nTest av egenskaperna så att undantag kastas då innertemperatur och måltemperatur tilldelas felaktiga värden\n");
            try
            {               
                Cooler cooler8 = new Cooler();
                cooler8.InsideTemperature = 46.0m;      // Tilldela egenskapen InsideTemperature ett felaktigt värde.
                //Run(cooler8, 10);
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                ViewErrorMessage(ex.Message);              
            }

            try
            {
                Cooler cooler8 = new Cooler();               
                cooler8.TargetTemperature = 21;     // Tilldela egenskapen InsideTemperature ett felaktigt värde.
               // Run(cooler8, 10);
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                ViewErrorMessage(ex.Message);
            }

            // Test9. Test av konstruktorerna.
            // 2 styck try-catch satser för att fånga 2 olika undantag.
            ViewTestHeader("Test9.\nTest av konstruktorer så att undantag kastas då innertemperatur och måltemperatur tilldelas felaktiga värden\n");
            try
            {
                Cooler cooler9 = new Cooler(50.0m, 8, true, true);      // Tilldela konstruktorerna ett felaktigt värde på InsideTemperature
                ViewTestHeader("Test9.\nTest av konstruktorer så att undantag kastas då innertemperatur och måltemperatur tilldelas felaktiga värden");
              //  Run(cooler9, 10);
            }
            catch(Exception ex)
            {
                Console.WriteLine();
                ViewErrorMessage(ex.Message);
            }

            try
            {
                Cooler cooler9 = new Cooler(7.0m, 30, true, true);      // Tilldela konstruktorerna ett felaktigt värde på TargetTemperature.
                ViewTestHeader("Test9.\nTest av konstruktorer så att undantag kastas då innertemperatur och måltemperatur tilldelas felaktiga värden");
              //  Run(cooler9, 10);
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                ViewErrorMessage(ex.Message);
            }
        }

        // Testmetod.
        public static void Run(Cooler cooler, int minutes)
        {
            Console.WriteLine(cooler.ToString());

            for (int countMinutes = 0; countMinutes < 10; countMinutes++)
            {               
                cooler.Tick();
                Console.WriteLine(cooler.ToString());
            }
        }

        // Visar felmeddelande.
        public static void ViewErrorMessage(string message)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        // Visar en Test-Header.
        public static void ViewTestHeader(string header)
        {
            for (int i = 0; i < 50; i++)
            {
                Console.Write(HorisontalLine);
            }
            Console.WriteLine();
            Console.WriteLine(header);
        }
    }
}
