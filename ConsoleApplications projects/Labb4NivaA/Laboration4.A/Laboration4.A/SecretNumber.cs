//  Laborationsuppgift 4 - Gissa Talet nivå A
//  Laboration4.A
//  Design: Andreas Gidö
//  Datum:      20150115

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboration4.A
{
    public class SecretNumber
    {
        // Fältvariabler.
        private int _count;
        private int _number;
        public const int MaxNumberOfGuesses = 7;

        // Konstruktor.
        public SecretNumber()
        {
            Initialize();          
        }

        // Metoden Initialize. Metoden initierar fältvariablarna.
        public void Initialize()
        {
            Random randomNumber = new Random();
            _number = randomNumber.Next(1, 101);

            _count = 0;            
        }

        // Metod för att låta användaren gissa på ett hemligt tal.
        // Metoden kontrollerar även räknaren och det slumpade numret.
        public bool MakeGuess(int number)
        {

            // Kontrollera att det inte gissas fler än tillåtet.
            if (_count >= MaxNumberOfGuesses)
            {
                throw new ApplicationException();
            }

            // Kontrollera att det gissade talet ligger inom rätt intervall.
            if (number < 1 || number > 100)
            {
                throw new ArgumentOutOfRangeException();
            }

            // Räkna upp räknaren med 1.
            _count++;

            // Rätt gissning.
            if (number == _number)
            {
                Console.WriteLine("RÄTT GISSAT! Du klarade det på {0} försök.", _count);
                return true;
            }

            // Om det gissade talet är för lågt. Eller för högt.
            if (number < _number)
            {
                Console.WriteLine("{0} är för lågt. Du har {1} gisnningar kvar.", number, (MaxNumberOfGuesses - _count));
            }           
            else
            {
                Console.WriteLine("{0} är för högt. Du har {1} gisnningar kvar.", number, (MaxNumberOfGuesses - _count));
            }
            

            // Antal gissningar är slut.
            if (_count == MaxNumberOfGuesses)
            {
                Console.WriteLine("Det hemliga talet är {0}", _number);
            }
            return false;
        }       
    }
}
