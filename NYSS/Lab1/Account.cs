using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace NYSS
{
    class Account
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string PhoneNumber { get; }
        public string Country { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Organization { get; set; }
        public string Position { get; set; }
        public string Note { get; set; }
        
        public Account(string surname, string name, string patronymic, string phoneNumber, string country, DateTime dateOfBirth, string organization, string position, string note)
        {
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
            PhoneNumber = phoneNumber;
            Country = country;
            DateOfBirth = dateOfBirth;
            Organization = organization;
            Position = position;
            Note = note;
        }

        public override string ToString()
        {
            return $"Surname: {Surname}\n"
                   + $"Name: {Name}\n"
                   + $"Patronymic: {Patronymic}\n"
                   + $"Phone number: {PhoneNumber}\n"
                   + $"Country: {Country}\n"
                   + $"Date of birth: {(DateOfBirth == DateTime.MinValue ? string.Empty : DateOfBirth.ToShortDateString())}\n"
                   + $"Organization: {Organization}\n"
                   + $"Position: {Position}\n"
                   + $"Note: {Note}\n";
        }
    }
}
