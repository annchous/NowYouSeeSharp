using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using NYSS;

namespace Lab1
{
    class AccountManager
    {
        public Dictionary<string, Account> Accounts { get; }

        public AccountManager()
            :this(new Dictionary<string, Account>())
        {}
        public AccountManager(Dictionary<string, Account> accounts)
        {
            Accounts = accounts;
        }

        public void CreateAccount(Account account) => Accounts.Add(account.PhoneNumber, account);
        public void EditAccount(string phoneNumber, Account editedAccount) => Accounts[phoneNumber] = editedAccount;
        public void DeleteAccount(string phoneNumber) => Accounts.Remove(phoneNumber);
        public void ShowAccount(string phoneNumber) => Console.WriteLine(Accounts[phoneNumber].ToString());
    }
}
