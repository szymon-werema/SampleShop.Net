using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Shop.Entities;
namespace Shop.Models.Services
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

        public bool CheackPassword(string password, string email)
        {
            var user = findUser(email);
            if(passwordHasher.VerifyHashedPassword(user,user.Password,password)== PasswordVerificationResult.Failed) return false;
            return true;
        }

        public async Task changeFirstName(string email, string firstname)
        {
            var user = findUser(email);
            user.FristName = firstname;
            await db.SaveChangesAsync();
        }
        public async Task changeLastName(string email, string lastname)
        {
            var user = findUser(email);
            user.LastName = lastname;
            await db.SaveChangesAsync();
        }
        public async Task changePhonenumber(string email, string phonenumber)
        {
            var user = findUser(email);
            user.PhoneNumber = phonenumber;
            await db.SaveChangesAsync();
        }
        public async Task setAddress(string email, Address addres)
        {
            var user = findUser(email);
            addres.UserId = user.Id;
            await db.Address.AddAsync(addres);
            user.Address=addres;
            await db.SaveChangesAsync();
        }
        public async Task updateAddress(string email, Address addres)
        {
            var user = findUser(email);
            Address a = db.Address.Where(x => x.UserId == user.Id).FirstOrDefault();
            a.Street = addres.Street;
            a.City = addres.City;
            a.HouseNumber = addres.HouseNumber;
            a.ApartamentNumber = addres.ApartamentNumber;
            await db.SaveChangesAsync();
        }
        public int getIdUser(string email)
        {
            return findUser(email).Id;
        }
        public async Task<Bucket> getBucket(int idbucket)
        {
            Bucket bucket = db.Buckets.FirstOrDefault(x => x.Id == idbucket);
            
            List<int> items = db.BucketItems.Where(x => x.BucketId == idbucket).Select( bi => bi.ItemId).ToList();
            bucket.Items = db.Items.Where(x => items.Contains(x.Id) == true ).ToList();
            foreach(Item i in bucket.Items)
            {
                Image miniature = db.Images.Where(img => img.ItemId == i.Id).ToList().ElementAt(i.Miniature);
                i.Images.Add(miniature);
                i.User = db.User.Where(u => i.UserId == u.Id).FirstOrDefault();
                i.Ammount = db.BucketItems.Where(bi => bi.BucketId == bucket.Id)
                    .Where(bi => bi.ItemId == i.Id).Select(bi => bi.Ammount).First();
            }
           
            bucket.User = db.User.Where(u => u.Id == bucket.UserId).First();
            return bucket;

        }
        public async Task clearBucket(int idBucket)
        {
            Bucket b = db.Buckets.Where(x => x.Id == idBucket).First();
            db.BucketItems.RemoveRange(db.BucketItems.Where(bi => bi.BucketId == idBucket).ToList());
            db.SaveChangesAsync();
        }
    }
}
