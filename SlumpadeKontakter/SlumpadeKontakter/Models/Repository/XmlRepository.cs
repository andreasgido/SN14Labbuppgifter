using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace SlumpadeKontakter.Models.Repository
{
    public class XmlRepository : IRepository
    {
        // Fält
        private static readonly string PhysicalPath;
        private XDocument _document;

        // Egenskap
        // Kollar om det finns något i _document. Om det finns returnera det annars läs in från xml-dokumentet och returnera sedan.
        private XDocument Document 
        {
            get { return _document ?? (_document = XDocument.Load(PhysicalPath)); } 
        }

        static XmlRepository()
        {
           // Skapar en koppling(sökväg) till xml-dokumentet
            PhysicalPath = Path.Combine(
                AppDomain.CurrentDomain.GetData("DataDirectory").ToString(),
                "contacts.xml");
        }

        // Metod för att visa de sista xx antal poster från xml-dokumentet
        public List<Contact> GetLastContacts(int count = 20)
        {
            // Refactoring p.g.a D.R.Y!!
            //var contacts = Document.Descendants("Contact")
            //    .Select(element => new Contact
            //    {
            //        Id = Guid.Parse(element.Element("Id").Value),
            //        FirstName = element.Element("FirstName").Value,
            //        LastName = element.Element("LastName").Value,
            //        Email = element.Element("Email").Value
            //    })
            //    .Reverse().Take(count)
            //    .ToList();

            //var contactList = GetAllContacts().ToList();
            //var contacts = contactList.OrderByDescending(x => x.FirstName).Take(count).ToList();

            var contacts = GetAllContacts().OrderByDescending(x => x.LastName).Take(count).ToList();

            return contacts;
        }

        // Metod för att hämta listan med all kontakter från xml-dokumentet
        public List<Contact> GetAllContacts()
        {
            // Hämtar listan med nedanstående ingående element från xml-dokumentet via egenskapen Document
            var contacts = Document.Descendants("Contact")
                .Select(element => new Contact
                {
                    Id = Guid.Parse(element.Element("Id").Value),                   
                    FirstName = element.Element("FirstName").Value,
                    LastName = element.Element("LastName").Value,
                    Email = element.Element("Email").Value
                })
                .OrderBy(x => x.FirstName)      // Sorterar på FirstName(Förnamn)
                .ToList();
                  
            // Returnera listan med kontakterna
            return contacts;
        }

        // Metod för att hämta en kontakt från listan i xml-dokumentet baserat på kontaktens Id(Guid)
        public Contact GetContact(Guid id)
        {
            // Matcha id:t på kontakten som ska hämtas mot id:t i xml-dokumentet och skapa 
            // ett nytt Contact-objekt med nedanstående attribut (Id, Förnamn, Efternamn och Epost)
            var contact = Document.Descendants("Contact")
                .Where(element => Guid.Parse(element.Element("Id").Value) == id)
                .Select(element => new Contact
                {
                    Id = Guid.Parse(element.Element("Id").Value),
                    FirstName = element.Element("FirstName").Value,
                    LastName = element.Element("LastName").Value,
                    Email = element.Element("Email").Value
                })
                .FirstOrDefault();

            // returnera kontakten
            return contact;
        }

        // Metod för att lägga till en kontakt i xml-dokumentet
        public void Add(Contact contact)
        {
            // Skapa en ny kontakt som sedan ska sparas i xml-dokumentet
            var element = new XElement("Contact",
                    new XElement("Id", contact.Id.ToString()),
                    new XElement("FirstName", contact.FirstName),
                    new XElement("LastName", contact.LastName),
                    new XElement("Email", contact.Email));
            
            // Sparar kontakten i xml-dokumentet
            Document.Root.Add(element);
        }

        // Metod för att uppdatera en befintlig kontakt från xml-dokumentet baserat på kontaktens Id(Guid)
        public void Edit(Contact contact)
        {
            // Kolla om objektet som skickas in i metoden är tomt
            if (contact == null)
            {
                throw new ArgumentNullException("contact");
            }

            // Matcha id:t på kontakten som ska uppdaters mot id:t i xml-dokumentet
            var elementToEdit = Document.Descendants("Contact")
                .Where(element => Guid.Parse(element.Element("Id").Value) == contact.Id)
                .FirstOrDefault();

            // Om id:t finns uppdatera kontakten
            if (elementToEdit != null)
            {
                elementToEdit.Element("FirstName").Value = contact.FirstName;
                elementToEdit.Element("LastName").Value = contact.LastName;
                elementToEdit.Element("Email").Value = contact.Email;
            }
        }

        // Metod för att radera en befintlig kontakt från xml-dokumentet baserat på kontaktens Id(Guid)
        public void Delete(Contact contact)
        {
            // Matcha id:t på kontakten som ska uppdaters mot id:t i xml-dokumentet
            var elementToDelete = (from element in Document.Descendants("Contact")
                                   where Guid.Parse(element.Element("Id").Value).Equals(contact.Id)
                                   select element)
                                   .FirstOrDefault();
      
            // Om id:t finns radera kontakten
            if (elementToDelete != null)
            {
                elementToDelete.Remove();
            }
        }

        // Metod för att spara Skapande, Redigering eller Borttagning av en kontakt i xml-dokumentet
        public void Save()
        {
            Document.Save(PhysicalPath);
        }
    }    
}