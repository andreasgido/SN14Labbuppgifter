using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Labb7A.Models;

namespace Labb7A.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var sessionLive = GetListOfNumberAndOutcome();       // Hämtar listan för att kunna visa den i vyn
            return View(sessionLive);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(int? newguess)  
        {            
            // Kolla om Session objektet finns kvar.
            if (Session.IsNewSession)
            {
                // Timeout!! Väntat för länge, hantera felet!
                return RedirectToAction("SessionExpired");
            }

            // Hämtar listan från metoden GetListOfNumberAndOutcome
            var sessionLive = GetListOfNumberAndOutcome();

            // Om det inte finns ett värde medskickat från gissningsfältet
            if (!newguess.HasValue)     
            {
                ModelState.AddModelError("", "Du måste ange ett tal."); 
                return View(sessionLive);                     
            }
            else
            {
                // Validera om värdet är mellan 1 och 100
                if (newguess < 1 || newguess > 100)
                {
                    ModelState.AddModelError("", "Talet måste vara mellan 1 och 100.");
                    return View(sessionLive);
                }
                else
                {
                    // Kalla på metoden MakeGuess              
                    sessionLive.MakeGuess(newguess.Value);

                    if (sessionLive.LastGuessedNumber.Outcome == Outcome.Right)
                    {
                        return View("RightNumber", sessionLive); 
                    }
                    return View(sessionLive);
                }                             
            }        
        }

        // GET: NewRandomNumber
        public ActionResult NewRandomNumber()
        {
            GetListOfNumberAndOutcome().Initialize();
            return RedirectToAction("Index");
        }

        // GET: SessionExpired
        public ActionResult SessionExpired()
        {
            return View();
        }

        // GET: RightNumber
        public ActionResult RightNumber()
        {
            return View();
        }

        // Lägg senaste listan med gissning/utfall i ett Seesion-objekt för att kunna presentera resultatet
        // Om ingen lista finns (innan första gissningen) så skapas en ny lista till Seesion-objektet
        // Detta behöver göras för att listan försvinner när man uppdaterar vyn annars. 
        private SecretNumber GetListOfNumberAndOutcome()
        {
            var sessionLive = Session["secretnumber"] as SecretNumber;      // "secretnumber" kan heta vad som helst, bara ett namn
            if (sessionLive == null)        // Om listan inte finns.
            {
                sessionLive = new SecretNumber();
                Session["secretnumber"] = sessionLive;
            }
            return sessionLive;
        }
    }
}