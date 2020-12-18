using ACIC.AMS.DataStore.Interfaces;
using ACIC.AMS.Dto;
using ACIC.AMS.Repository;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace ACIC.AMS.DataStore
{
    public class ContactDataStore : BaseDataStore, IContactDataStore
    {
        public ContactDataStore(ACICDBContext context, IMapper mapper) : base(context, mapper){}

        public Account Get(int id)
        {
            var dbAccount = _context.Account.Where(a => a.AccountId == id).FirstOrDefault();
            return _mapper.Map<Dto.Account>(dbAccount);
        }

        public List<ContactsTitle> GetContactTitles()
        {
            List<ContactsTitle> retval = new List<ContactsTitle>();
            var dbContactTitles = _context.DdContactsTitle.ToList();

            dbContactTitles.ForEach(ct => {
                retval.Add(_mapper.Map<ContactsTitle>(ct));
            });

            return retval; 
        }

        public List<Contact> GetByAccountId(int accountId)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("@AccountId", accountId);

            return this.ExecuteQuery<Contact>("[dbo].[GetContacts]", parameters);
        }

        public Contact Save(Contact contact)
        {
            Domain.Models.Contact dbContact;
            if (contact.ContactId != 0)
            {
                dbContact = _context.Contact.Where(a => a.ContactId == contact.ContactId).AsNoTracking().FirstOrDefault();

                var updatedContact = _mapper.Map<Domain.Models.Contact>(contact);
                updatedContact.DateModified = DateTime.Now;
                updatedContact.DateCreated = dbContact.DateCreated;
                _context.Update(updatedContact);
                _context.SaveChanges();
                return contact;
            }
            else
            {
                var newContact = _mapper.Map<Domain.Models.Contact>(contact);
                newContact.DateCreated = DateTime.Now;
                _context.Contact.Add(newContact);
                _context.SaveChanges();
                contact.ContactId = newContact.ContactId;
                return contact;
            }
        }
    }
}
