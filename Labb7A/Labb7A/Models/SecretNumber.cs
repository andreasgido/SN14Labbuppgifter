using Labb7A.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Labb7A.Models
{
    public class SecretNumber
    {
        // Fält
        private List<GuessedNumber> _guessedNumbers;        // Lista som ska innehålla tidigare gissningar och utfall
        private GuessedNumber _lastGuessedNumber;           // Ska innehålla senaste gissning och utfall
        private int? _number;                               // Innehåller det slumpade hemliga numret
        public const int MaxNumberOfGuesses = 7;            // Konstant för max antal gissningar

        // Egenskaper
        public bool CanMakeGuess                            // Så länge antalet gissningar är mindre än 7 eller gissningen inte är rätt så är returneras true
        {
            get { return ((Count >= MaxNumberOfGuesses || LastGuessedNumber.Outcome == Outcome.Right) ? false : true); }      
        }                                                                                                                    
        public int Count { get { return _guessedNumbers.Count; } }      // Håller reda på hur många gissningar som gjorts

        public IList<GuessedNumber> GuessedNumbers                      
        {
            get { return _guessedNumbers.AsReadOnly(); }
        }

        public GuessedNumber LastGuessedNumber { get { return _lastGuessedNumber; } }   // Läser _lastGuessedNumber med senaste gissning och utfall 

        public int? Number                                              // Returnerar null så länge som det går att gissa. (CanMakeGuess = true)
        {
            get
            {
                if (CanMakeGuess)
                {
                    return null;
                }
                return _number;
            }
            private set
            {
                _number = value;
            }
        }

        // Konstruktor
        public SecretNumber()
        {
            _guessedNumbers = new List<GuessedNumber>();
            Initialize();
        }

        //Metoder
        // Metoden Initialize. Initierar klassens fält och egenskaper efter anrop från konstruktorn
        public void Initialize()
        {
            _guessedNumbers.Clear();
            _lastGuessedNumber.Outcome = Outcome.Undefined;
            Random randomNumber = new Random();
            _number = randomNumber.Next(1, 101);        
        }

        // Metod som hanterar en gissning och utvärderar den
        public Outcome MakeGuess(int newGuess)
        {            
            if (newGuess < 1 || newGuess > 100)
            {
                throw new ArgumentOutOfRangeException();
            }

            _lastGuessedNumber = new GuessedNumber() { Number = newGuess };

            if (_guessedNumbers.Any(x => x.Number == newGuess))
            {
                // Outcome.OldGuess
                _lastGuessedNumber.Outcome = Outcome.OldGuess;
            }
            else if (CanMakeGuess)
            {               
                if (newGuess < _number)
                {
                    // Outcome.Low
                    _lastGuessedNumber.Outcome = Outcome.Low;
                }
                else if (newGuess > _number)
                {
                    // Outcome.High
                    _lastGuessedNumber.Outcome = Outcome.High;
                }
                else if (newGuess == _number)
                {
                    // Outcome.Right
                    _lastGuessedNumber.Outcome = Outcome.Right;
                }

                _guessedNumbers.Add(_lastGuessedNumber);
            }

            if (Count == MaxNumberOfGuesses && (_lastGuessedNumber.Outcome != Outcome.Right))
            {
                // Outcome.NoMoreGuesses
                _lastGuessedNumber.Outcome = Outcome.NoMoreGuesses;
            }
        
            return _lastGuessedNumber.Outcome; 
        }
    }

    public struct GuessedNumber
    {
        // Fält
        public int? Number;
        public Outcome Outcome;
    }

    public enum Outcome
    {
        Undefined, Low, High, Right, NoMoreGuesses, OldGuess
    }
}
