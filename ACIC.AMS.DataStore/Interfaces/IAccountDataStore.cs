using System;
using System.Collections.Generic;
using System.Text;

namespace ACIC.AMS.DataStore.Interfaces
{
    public interface IAccountDataStore
    {
        List<Dto.Account> GetAll();

        List<Dto.Account> GetExpiringAccounts();

        Dto.AccountDetails GetAccountDetails(int accountId);
        Dto.Account Get(int id);

        Dto.AccountDetails Save(Dto.Account account);
    }
}
