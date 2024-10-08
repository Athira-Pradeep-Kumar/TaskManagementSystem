﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TaskManagementSystem.Models;
using TaskManagementSystem.Data;

namespace TaskManagementSystem.DLL
{

    public class AccountRepository
    {
        private readonly ApplicationDbContext dbContext;
        public AccountRepository()
        {
            dbContext = new ApplicationDbContext();
        }

        // New Registration
        public int CreateAccount(Users users)
        {
            dbContext.Users.Add(users);
            return dbContext.SaveChanges();
        }

        // Checking Account is Exist or not
        // If exist it will return true | if not exist then it return false
        public bool VerifyAccount(Users users)
        {
            return dbContext.Users.Any(x => x.Username == users.Username && x.Password == users.Password);
        }

        // Get Users Details
        public Users GetUsers(string username)
        {
            return dbContext.Users.SingleOrDefault(x => x.Username == username);
        }


    }
}