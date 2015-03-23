using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Labb7A.Models;


namespace Labb7A.Utilities
{
    // Klass för att bryta ut översättningar av utfallet och antalet gissningar till textsträngar 
    // via 2 stycken switch-satser. Detta för att hålla SecretNumber-klassen lite mindre och renare.
    public static class SwitchingResults
    {
               
        // Metod som switchar Count för att kunna visa antalet gissningar
        public static string SwitchCount(this int count)
        {           
            string countString = "";
            
            switch (count)
            {
                case 1:
                    {
                        countString = "Första";
                        break;
                    }
                case 2:
                    {
                        countString = "Andra";
                        break;
                    }
                case 3:
                    {
                        countString = "Tredje";
                        break;
                    }
                case 4:
                    {
                        countString = "Fjärde";
                        break;
                    }
                case 5:
                    {
                        countString = "Femte";
                        break;
                    }
                case 6:
                    {
                        countString = "Sjätte";
                        break;
                    }
                case 7:
                    {
                        countString = "Sjunde";
                        break;
                    }
                default:
                    break; 
            }
            
            if (count > 0)
            {
                countString = countString + " gissningen";
            }
            return countString;
        }
       
        // Metod som översätter Outcome till en sträng som presenteras i vyn
        public static string SwitchOutcome(this SecretNumber secretnumber)
        {
            string outcomeString = "";
            switch (secretnumber.LastGuessedNumber.Outcome)
            {
                case Outcome.Low:
                    {
                        outcomeString = String.Format("{0} är för lågt.", secretnumber.LastGuessedNumber.Number);
                        break;
                    }
                case Outcome.High:
                    {
                        outcomeString = String.Format("{0} är för högt.", secretnumber.LastGuessedNumber.Number);
                        break;
                    }
                case Outcome.OldGuess:
                    {
                        outcomeString = String.Format("Du har redan gissat på {0}, välj ett annat tal.", secretnumber.LastGuessedNumber.Number);
                        break;
                    }
                case Outcome.NoMoreGuesses:
                    {
                        outcomeString = String.Format("Inga fler gissningar, det hemliga talet var: {0}", secretnumber.Number);                                            
                        break;
                    }
                case Outcome.Right:
                    {   
                        string str = secretnumber.Count.SwitchCount().ToLower();
                        outcomeString = String.Format("Du klarade det på {0}", str);
                        break;
                    }
                case Outcome.Undefined:
                    {
                        outcomeString = "";
                        break;
                    }
                default:
                    break;
            }
            
            return outcomeString;
        }
    }   
}