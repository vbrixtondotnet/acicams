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
    public class AccountDataStore : BaseDataStore, IAccountDataStore
    {
        public AccountDataStore(ACICDBContext context, IMapper mapper) : base(context, mapper){}

        public Account Get(int id)
        {
            var dbAccount = _context.Account.Where(a => a.AccountId == id).FirstOrDefault();
            return _mapper.Map<Dto.Account>(dbAccount);
        }

        public AccountDetails GetAccountDetails(int accountId)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("@AccountId", accountId);

            return this.ExecuteQuery<AccountDetails>("[dbo].[GetAccountDetails]", parameters).FirstOrDefault();
        }

        public List<Account> GetAll()
        {
            List<Account> retval = new List<Account>();
            var dbAccounts = _context.Account.OrderBy(a => a.LegalName).ToList();
            dbAccounts.ForEach(a => {
                retval.Add(_mapper.Map<Dto.Account>(a));
            });

            return retval;
        }


        public List<Account> GetExpiringAccounts()
        {
            return this.ExecuteQuery<Account>("[dbo].[GetExpiringAccounts]", null);
        }

        public AccountDetails Save(Account account)
        {
            Domain.Models.Account dbAccount;
            if (account.AccountId != 0)
            {
                dbAccount = _context.Account.Where(a => a.AccountId == account.AccountId).AsNoTracking().FirstOrDefault();

                var updatedAccount = _mapper.Map<Domain.Models.Account>(account);
                updatedAccount.DateModified = DateTime.Now;
                updatedAccount.DateCreated = dbAccount.DateCreated;
                _context.Update(updatedAccount);
                _context.SaveChanges();
            }
            else
            {
                var newAccount = _mapper.Map<Domain.Models.Account>(account);
                newAccount.DateCreated = DateTime.Now;
                _context.Account.Add(newAccount);
                _context.SaveChanges();
                account.AccountId = newAccount.AccountId;
            }

            return GetAccountDetails(account.AccountId);


        }
    }
}
