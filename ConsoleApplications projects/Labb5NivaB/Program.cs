using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb5NivaB
{
    public class Program
    {
        private const string HorisontalLine = "═";

        static void Main(string[] args)
        {
            // Test 1
            Cooler cooler1 = new Cooler();
            ViewTestHeader("Test 1.\nTest av standardkonstruktorn.\n");
            Console.WriteLine(cooler1.ToString());

            // Test 2.
            Cooler cooler2 = new Cooler(24.5m, 4);
            ViewTestHeader("Test 2.\nTest av konstruktorn med två parametrar, (24,5 och 4,0)\n");
            Console.WriteLine(cooler2.ToString());  
   
            // Test 3.
            Cooler cooler3 = new Cooler(19.5m, 4, true, false);
            ViewTestHeader("Test 3.\nTest av konstruktorn med fyra parametrar, (19,5, 4,0, true och false)\n");
            Console.WriteLine(cooler3.ToString());

            // Test 4.
            Cooler cooler4 = new Cooler(5.3m, 4, true, false);
            ViewTestHeader("Test 4.\nTest av kylning med metoden Tick\n");
            Run(cooler4, 10);

            // Test 5.
            Cooler cooler5 = new Cooler(5.3m, 4, false, false);
            ViewTestHeader("Test 4.\nTest av avstängt kylskåp med metoden Tick, med stängd dörr.\n");
            Run(cooler5, 10);

            // Test 6.
            Cooler cooler6 = new Cooler(10.2m, 4, true, true);
            ViewTestHeader("Test 6.\nTest av påslaget kylskåp med metoden Tick, med öppen dörr.\n");
            Run(cooler6, 10);

            // Test 7.
            Cooler cooler7 = new Cooler(19.7m, 4, false, true);
            ViewTestHeader("Test 7.\nTest av avslaget kylskåp med metoden Tick, med öppen dörr.\n");
            Run(cooler7, 10);

            // Test8. Test av egenskaperna.
            // 2 styck try-catch satser för att fånga 2 olika undantag.
            ViewTestHeader("Test8.\nTest av egenskaperna så att undantag kastas då innertemperatur och måltemperatur tilldelas felaktiga värden");
            TemperatureSensor insideTemperatureTest8 = new TemperatureSensor(10.0m);
            try
            {               
                insideTemperatureTest8.Temperature = 50.0m;      
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                ViewErrorMessage(ex.Message);
            }

            TemperatureDisplay tempSensorTarget = new TemperatureDisplay(10.0m, 4, true, true);
            try
            {
                tempSensorTarget.TargetTemperature = 25.0m;
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                ViewErrorMessage(ex.Message);
            }

            // Test9. Test av konstruktorerna.
            // 2 styck try-catch satser för att fånga 2 olika undantag.
            ViewTestHeader("Test9.\nTest av konstruktorer så att undantag kastas då innertemperatur och måltemperatur tilldelas felaktiga värden");
            TemperatureSensor insideTemperatureTest9 = new TemperatureSensor(10.0m); 
            try
            {
                insideTemperatureTest9.Temperature = 46.0m;
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                ViewErrorMessage(ex.Message);
            }

            TemperatureDisplay temperatureDisplay = new TemperatureDisplay(10.0m, 4, true, true);
            try
            {
                temperatureDisplay.TargetTemperature = 25;
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                ViewErrorMessage(ex.Message);
            }
        }

        private static void Run(Cooler cooler, int minutes)
        {
            Console.WriteLine(cooler.ToString());
            for (int i = 0; i < minutes; i++)
            {
                cooler.Tick();
                Console.WriteLine(cooler.ToString());
            }           
        }

        private static void ViewErrorMessages(string message)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        // Visar en testheader.
        private static void ViewTestHeader(string header)
        {
            for (int i = 0; i < 50; i++)
            {
                Console.Write(HorisontalLine);
            }
            Console.WriteLine();
            Console.WriteLine(header);
        }

        // Visar felmeddelande.
        public static void ViewErrorMessage(string message)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
