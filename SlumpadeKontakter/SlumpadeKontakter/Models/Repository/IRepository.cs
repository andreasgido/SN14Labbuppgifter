using System;
using System.Collections.Generic;
using SlumpadeKontakter.Models;

namespace SlumpadeKontakter.Models.Repository
{
    public interface IRepository
    {
        List<Contact> GetAllContacts();
        Contact GetContact(Guid id);
        void Add(Contact contact);
        void Edit(Contact contact);
        void Delete(Contact contact);          
        void Save();

        List<Contact> GetLastContacts(int count = 20);
    }
}
