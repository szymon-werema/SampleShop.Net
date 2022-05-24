﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Shop.Entities;
namespace Shop.Models.AccountMenager
{
    public class AccountMenager
    {
        private readonly LocalDbContext db;
        private readonly IPasswordHasher<User> passwordHasher;

        public AccountMenager(LocalDbContext db, IPasswordHasher<User> passwordHasher)
        {
            this.db = db;
            this.passwordHasher = passwordHasher;
        }
        public User findUser(string email)
        {
            return db.User.Where(u => u.Email == email).FirstOrDefault();
        }
        public async Task ActiveAccountAsync(string email)
        {
            User u = findUser(email);
            u.isActive = true;
            await db.SaveChangesAsync();
        }
        public async Task ChangePasswordAsync(string email, string password)
        {
            User u = findUser(email);
            u.Password = passwordHasher.HashPassword(u,password);
            await db.SaveChangesAsync();
        }
        public  bool CheackActivation(string email)
        {
            User u = findUser(email);
            return u.isActive;
        }


    }
}
