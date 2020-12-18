using System;
using System.Collections.Generic;
using System.Text;

namespace ACIC.AMS.DataStore.Interfaces
{
    public interface IContactDataStore
    {
        List<Dto.ContactsTitle> GetContactTitles();

        List<Dto.Contact> GetByAccountId(int accountId);
        Dto.Contact Save(Dto.Contact contact);
    }
}
