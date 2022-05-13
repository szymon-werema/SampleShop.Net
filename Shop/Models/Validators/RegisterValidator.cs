﻿using FluentValidation;
using Shop.Models.Users;
using Shop.Entities;
using ServiceStack;

namespace Shop.Models.Validators
{
    public class RegisterValidator : AbstractValidator<UserClient>
    {
        public RegisterValidator( LocalDbContext db)
        {
            RuleFor(x => x.UserRoleId)

                .Custom((vaule, message) =>
                {
                    if (vaule != db.UserRole.Where(r => r.Name == "User").Select(r => r.Id).FirstOrDefault())
                    {

                        message.AddFailure("Incorrect id for this user");
                    }
                    
                });
            RuleFor(x => x.Email)
                 .EmailAddress();
            RuleFor(x => x.Email)
                .Custom((value, message) =>
                {
                    if(db.User.Any( u => u.Email == value))
                    {
                        message.AddFailure("Email addres is already taken");
                    }
                });
            RuleFor(x => x.Password)
                .MinimumLength(6)
                .MaximumLength(20);
            RuleFor(x => x.ComfirmPassword)
                .Equal(p => p.Password);
            RuleFor(x => x.FristName )
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(10);
            RuleFor(x => x.LastName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(10);

        }

       
    }
}