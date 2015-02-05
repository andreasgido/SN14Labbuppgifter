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
        private bool _canMakeGuess;
        public const int MaxNumberOfGuesses = 7;

        // Egenskaper (properties).
        public bool CanMakeGuess
        {
            get
            {
                return (Count >= MaxNumberOfGuesses || _canMakeGuess == false) ? false : true;
            }
            private set
            {
                _canMakeGuess = value;
            }           
        }

        public int Count { get; private set; }

        public int GuessesLeft 
        {
            get { return MaxNumberOfGuesses - Count ; }           
        }

        // Kontruktor.
        public SecretNumber()
        {
            _guessedNumbers = new int[MaxNumberOfGuesses];
            Initialize();
        }

        // Metoden Initialize. Initierar fält och egenskaper.
        public void Initialize()
        {
            Array.Clear(_guessedNumbers, 0, _guessedNumbers.Length);
            Random randomNumber = new Random();
            _number = randomNumber.Next(1, 101);           
            CanMakeGuess = true;
            Count = 0;
        }
       
        // Metoden MakeGuess.
        public bool MakeGuess(int presentNumber)
        {
            if (Count >= MaxNumberOfGuesses)
            {
                throw new ApplicationException();
            }

            if (presentNumber < 1 || presentNumber >100)
            {
                throw new ArgumentOutOfRangeException();
            }

            // Jämför alla värdena i arrayen(_guessedNumbers) mot det gissade numret(presentNumber). Om inget matchar 
            // returnerar Array.IndexOf -1 till variabeln arrayIndex och man får göra om sin gissning.
            int arrayIndex = Array.IndexOf(_guessedNumbers, presentNumber);     
            if (arrayIndex > -1)
            {
                Console.WriteLine("Gissning {0}: {1}", (Count + 1), presentNumber);
                Console.WriteLine("Du har redan gissat på {0}. Gör om gissningen!", presentNumber);
                return false;
            }
            // Annars lagra det gissade numret, räkna upp gissningarna och fortsätt.
            else
            {
                _guessedNumbers[Count] = presentNumber;
                Count++;
            }
          
            if (presentNumber < _number)
            {
                Console.WriteLine("Gissning {0}: {1}\n", Count, presentNumber);
                Console.WriteLine("{0} är för lågt. Du har {1} gissningar kvar.", presentNumber, GuessesLeft);
            }
            else
            {
                Console.WriteLine("Gissning {0}: {1}\n", Count, presentNumber);
                Console.WriteLine("{0} är för högt. Du har {1} gissningar kvar.", presentNumber, GuessesLeft);
            }
                   
            if (presentNumber == _number)
            {
                Console.WriteLine("Gissning {0}: {1}\n", Count, presentNumber);
                Console.WriteLine("RÄTT GISSAT. Du klarade det på {0} antal försök.", Count);
                CanMakeGuess = false;
                return true;
            }

            if (Count == MaxNumberOfGuesses)
            {               
                Console.WriteLine("Det hemliga talet är {0}.", _number);
            }
            return false;
        }
    }
}
