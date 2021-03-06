﻿namespace ZaklepTo.Core.Domain
{
    public class Employee : User
    {
        public Restaurant Restaurant { get; set; }

        protected Employee() : base()
        {
        }

        public Employee(string login, string firstname, string lastname, string email,
            string phone, string password, string salt, Restaurant restaurant)
            : base(login, firstname, lastname, email, phone, password, salt)
        {
            Restaurant = restaurant;
        }
    }
}
