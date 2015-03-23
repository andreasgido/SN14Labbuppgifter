using SlumpadeKontakter.Models;
using SlumpadeKontakter.Models.Repository;
using SlumpadeKontakter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace SlumpadeKontakter.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository _repository;

        // Defaultkonstruktor
        public HomeController()
            : this(new XmlRepository())
        {
        }

        public HomeController (IRepository repository)
        {
            _repository = repository;
        }

        // GET: Contacts
        [HttpGet]
        public ActionResult Index()
        {     
            // Skapa koppling mellan modell och vymodell för att kunna skriva ut listan från vymodellen

            //var contacts = _repository.GetAllContacts();      // Använd detta metodanrop för att visa alla kontakterna

            var contacts = _repository.GetLastContacts();       // Använd detta metodanrop för att visa x antal kontakterna

            var contactViewModel = new List<ContactViewModel>();

            foreach (var contact in contacts)
            {
                var viewModelOfContacts = new ContactViewModel
                {
                    Id = contact.Id,
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    Email = contact.Email
                };

                // Fyll på listan
                contactViewModel.Add(viewModelOfContacts);
            }

            // Returnera vymodellen
            return View(contactViewModel);
        }

        // GET: AddNewContact
        [HttpGet]
        public ActionResult AddContact()
        {
            // Hämta AddContact för att visa vyn och sedan skapa en ny kontakt           
            return View();
        }

        // POST: AddNewContact
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddContact([Bind(Include = "FirstName, LastName, Email")]ContactViewModel contactViewModel)      // Här ska 'Bind' användas för binda upp vilka fält i formuläret vi använda för att skapa en ny kontakt
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Gör om vymodellen till ett modell-objekt som ska sparas i xml-dokumentet
                    var contact = new Contact
                    {
                        FirstName = contactViewModel.FirstName,
                        LastName = contactViewModel.LastName,
                        Email = contactViewModel.Email
                    };

                    // Lägg till och spara kontakten
                    _repository.Add(contact);
                    _repository.Save();

                    // Visa för användaren att det gick att lägga till kontakten
                    TempData["success"] = String.Format("Kontakten {0} {1} har lagts till i listan", contact.FirstName, contact.LastName);

                    // Återgå till Index-vyn
                    return RedirectToAction("Index");
                }
                catch
                {
                    // Om något gått fel
                    TempData["error"] = "Misslyckades med att spara, försök igen";
                }
            }

            // Returnera vymodellen
            return View(contactViewModel);
        }

        // GET: EditContact
        [HttpGet]
        public ActionResult EditContact(Guid? id)
        {
            // Kolla om det finns en kontakt med angivet id
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Läs in kontakten med angivet id från modellen      
            var contact = _repository.GetContact(id.Value);

            // Kolla om kontakten finns
            if (contact == null)
            {
                return HttpNotFound();
            }

            // Gör om modellen till ett vymodell-objekt som ska visas i vyn
            var contactViewModel = new ContactViewModel
            {
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Email = contact.Email
            };

            // Returnera vymodellen
            return View(contactViewModel);
        }

        // POST: EditContact
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditContact(Guid id)
        {
            // Läs in kontakten som ska redigeras med angivet id
            var contactToEdit = _repository.GetContact(id);

            // Kolla om kontakten finns
            if (contactToEdit == null)
            {
                return HttpNotFound();
            }

            // Nytt vymodells-objekt som ska lagra dom redigerade egenskaperna
            var vm = new ContactViewModel();

            // Om det gick att redigera egenskaperna till kontakten
            if (TryUpdateModel(vm, string.Empty, new string[] { "FirstName", "LastName", "Email" }))
            {
                try
                {
                    // Gör om vymodellen till ett modell-objekt som ska sparas i xml-dokumentet
                    var contact = new Contact
                    {
                        FirstName = vm.FirstName,
                        LastName = vm.LastName,
                        Email = vm.Email
                    };

                    // Redigera och spara
                    _repository.Edit(contactToEdit);
                    _repository.Save();

                    // Visa användaren att ändringen blivit genomförd
                    TempData["success"] = String.Format("Kontakten {0} {1} har redigerats", contactToEdit.FirstName, contactToEdit.LastName);

                    // Återvänd till Index(listan)
                    return RedirectToAction("Index");
                }
                catch
                {
                    // Hantera felet med TempData
                    TempData["error"] = "Misslyckades med att redigera, försök igen";
                }
            }
            // Returnera modellen
            return View(contactToEdit);
        }

        // GET: DeleteContact
        [HttpGet]
        public ActionResult DeleteContact(Guid? id)
        {
            // Kolla om det finns en kontakt med angivet id
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Läs in kontakten med angivet id
            var contactToDelete = _repository.GetContact(id.Value);

            // Kolla så att kontakten finns
            if (contactToDelete == null)
            {
                return HttpNotFound();
            }

            // Gör om modellen till ett vymodell-objekt som ska visas i vyn
            var contactViewModel = new ContactViewModel
            {
                FirstName = contactToDelete.FirstName,
                LastName = contactToDelete.LastName,
                Email = contactToDelete.Email
            };
           
            // Returnera vymodellen
            return View(contactViewModel);
        }

        // POST: DeleteContact
        [HttpPost]
        [ActionName("DeleteContact")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            // Kolla om det gick att radera kontakten
            try
            {
                var contactToDelete = new Contact { Id = id };
                _repository.Delete(contactToDelete);
                _repository.Save();

                // Visa användaren att borttagningen blivit genomförd
                TempData["success"] = String.Format("Kontakten {0} {1} har tagits bort från listan", contactToDelete.FirstName, contactToDelete.LastName);
            }
            catch
            {
                // Hantera felet med TempData
                TempData["error"] = "Misslyckades med att ta bort kontakten, försök igen";
                return RedirectToAction("Delete", new { id = id });
            }

            // Återgå till Index-vyn
            return RedirectToAction("Index");
        }
    }
}