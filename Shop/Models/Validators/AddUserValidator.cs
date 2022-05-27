using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Shop.Models.Forms;
using Shop.Entities;
namespace Shop.Models.Validators
{
    public class AddUserValidator : AbstractValidator<AddUserForm>
    {
        public AddUserValidator( LocalDbContext db )
        {
            RuleFor(x => x.Email)
                .EmailAddress();
            RuleFor(x => x.Email)
                .Custom((value, message) =>
                {
                    if (db.User.Any(u => u.Email == value))
                    {
                        message.AddFailure("Email addres is already taken");
                    }
                });
            RuleFor(x => x.FristName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(30);
            RuleFor(x => x.LastName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(30);
            RuleFor(x => x.UserRoleId)
                .NotEmpty()
                .NotNull();
            RuleFor(x =>x.PhoneNumber)
                .NotNull();
            
        }
    }
}
