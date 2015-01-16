using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboration4.B
{
    public class SecretNumber
    {
        // Fältvariablar.
        private int[] _guessedNumbers;
        private int _number;

        public const int MaxNumberOfGuesses = 7;

        // Egenskaper (properties).
        private bool CanMakeGuesses
        {
            get { }
            private set { }
        }

        public int Count 
        {
            get { return; }
            private set 
            { 

            }
        }

        public int GuesseLeft 
        {
            get { return MaxNumberOfGuesses; }
            
        }

        // Kontruktor.
        public SecretNumber()
        {
            _guessedNumbers = new int[1];
            Initialize();

        }

        // Metoden Initialize. Initierar fält och egenskaper.
        public void Initialize()
        {
            
            Random randomNumber = new Random();
            _number = randomNumber.Next(1, 101);           

            CanMakeGuesses = true;
            Count = 0;
        }
    

    

        // Metoden MakeGuess.
    }
}
