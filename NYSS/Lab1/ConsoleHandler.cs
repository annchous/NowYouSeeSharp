using System;
using System.Collections.Generic;
using System.Text;
using NYSS;

namespace Lab1
{
    class ConsoleHandler
    {
        private bool _run;
        private string[] _line;
        private AccountManager AccountManager { get; }

        public ConsoleHandler()
        {
            _run = true;
            AccountManager = new AccountManager();
        }

        public void Run()
        {
            while (_run)
            {
                try
                {
                    ParseCommand();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private void ParseCommand()
        {
            _line = Console.ReadLine()?.Split(" ");
            switch (_line?[0])
            {
                case "/help":
                    Help();
                    break;
                case "/create":
                    Create(_line);
                    break;
                case "/edit":
                    Edit(_line[1]);
                    break;
                case "/delete":
                    Delete(_line[1]);
                    break;
                case "/account":
                    ShowAccount(_line[1]);
                    break;
                case "/accounts":
                    ShowAccounts();
                    break;
                case "/exit":
                    _run = false;
                    break;
                default:
                    Console.WriteLine("Wrong command!");
                    break;
            }
        }

        private void Create(string[] args)
        {
            Console.Write("Surname*: ");
            if (!ArgumentReader.ReadRequired(out string surname)) return;
            Console.Write("Name*: ");
            if (!ArgumentReader.ReadRequired(out string name)) return;
            Console.Write("Patronymic: ");
            ArgumentReader.ReadOptional(out string patronymic);
            Console.Write("Phone number*: ");
            if (!ArgumentReader.ReadPhoneNumber(out string phoneNumber)) return;
            if (AccountManager.Accounts.ContainsKey(phoneNumber))
            {
                Console.WriteLine("Account with this phone number already exists!");
                return;
            }
            Console.Write("Country*: ");
            if (!ArgumentReader.ReadRequired(out string country)) return;
            Console.Write("Date of birth: ");
            ArgumentReader.ReadOptional(out string dateOfBirth);
            Console.Write("Organization: ");
            ArgumentReader.ReadOptional(out string organization);
            Console.Write("Position: ");
            ArgumentReader.ReadOptional(out string position);
            Console.Write("Note: ");
            ArgumentReader.ReadOptional(out string note);
            Console.WriteLine();

            AccountManager.Accounts.Add(phoneNumber, new Account(surname, name, patronymic, phoneNumber, country, string.IsNullOrEmpty(dateOfBirth) ? DateTime.MinValue : DateTime.Parse(dateOfBirth), organization, position, note));
        }

        private void Edit(string phoneNumber)
        {
            if (!AccountManager.Accounts.ContainsKey(phoneNumber))
            {
                Console.WriteLine($"Account with number {phoneNumber} does not exist!");
                return;
            }
            Console.Write("Enter the parameter you would like to change: ");
            var parameter = Console.ReadLine();
            Console.Write("Enter new parameter: ");
            switch (parameter?.ToLower())
            {
                case "surname":
                    if (!ArgumentReader.ReadRequired(out string newSurname)) return;
                    else AccountManager.Accounts[phoneNumber].Surname = newSurname;
                    break;
                case "name":
                    if (!ArgumentReader.ReadRequired(out string newName)) return;
                    else AccountManager.Accounts[phoneNumber].Name = newName;
                    break;
                case "patronymic":
                    ArgumentReader.ReadOptional(out string newPatronymic);
                    AccountManager.Accounts[phoneNumber].Patronymic = newPatronymic;
                    break;
                case "phone number":
                    if (!ArgumentReader.ReadPhoneNumber(out string newPhone)) return;
                    if (AccountManager.Accounts.ContainsKey(newPhone))
                    {
                        Console.WriteLine("Account with this phone number already exists!");
                    }
                    else
                    {
                        var account = AccountManager.Accounts[phoneNumber];
                        AccountManager.DeleteAccount(account.PhoneNumber);
                        AccountManager.Accounts.Add(newPhone, new Account(
                            account.Surname, 
                            account.Name, 
                            account.Patronymic, 
                            newPhone, 
                            account.Country, 
                            account.DateOfBirth, 
                            account.Organization, 
                            account.Position, 
                            account.Note));
                    }
                    break;
                case "country":
                    if (!ArgumentReader.ReadRequired(out string newCountry)) return;
                    else AccountManager.Accounts[phoneNumber].Country = newCountry;
                    break;
                case "date of birth":
                    ArgumentReader.ReadOptional(out string newDateOfBirth);
                    AccountManager.Accounts[phoneNumber].DateOfBirth = string.IsNullOrEmpty(newDateOfBirth) 
                        ? DateTime.MinValue 
                        : DateTime.Parse(newDateOfBirth);
                    break;
                case "organization":
                    ArgumentReader.ReadOptional(out string newOrganization);
                    AccountManager.Accounts[phoneNumber].Organization = newOrganization;
                    break;
                case "position":
                    ArgumentReader.ReadOptional(out string newPosition);
                    AccountManager.Accounts[phoneNumber].Position = newPosition;
                    break;
                case "note":
                    ArgumentReader.ReadOptional(out string newNote);
                    AccountManager.Accounts[phoneNumber].Note = newNote;
                    break;
                default:
                    Console.WriteLine("No such parameter!");
                    break;
            }
        }

        private void Delete(string phoneNumber) => AccountManager.Accounts.Remove(phoneNumber);

        private void ShowAccount(string phoneNumber) => AccountManager.ShowAccount(phoneNumber);

        private void ShowAccounts()
        {
            foreach (var account in AccountManager.Accounts)
            {
                Console.WriteLine($"Surname: {account.Value.Surname}    Name: {account.Value.Name}  Phone number: {account.Key}");
            }
        }

        private void Help()
        {
            Console.WriteLine("Available actions:");
            Console.WriteLine("1.   /create - Create new account");
            Console.WriteLine("2.   /edit - Edit an existing account");
            Console.WriteLine("         enter the phone number linked to account");
            Console.WriteLine("3.   /delete - Delete an existing account");
            Console.WriteLine("         enter the phone number linked to account");
            Console.WriteLine("4.   /account - Show full information about an existing account");
            Console.WriteLine("         enter the phone number linked to account");
            Console.WriteLine("5.   /accounts - Show all existing accounts");
            Console.WriteLine("6.   /help - Show help list");
            Console.WriteLine("7.   /exit - Stop program");
            Console.WriteLine();
        }
    }
}
