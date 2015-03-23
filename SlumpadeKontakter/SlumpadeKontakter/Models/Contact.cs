using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SlumpadeKontakter.Models
{
    public class Contact
    {
        // Egenskaper
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        // Konstruktor för att generera ett GUID från klassen
        public Contact()
        {
            Id = Guid.NewGuid();
        }
    }
}