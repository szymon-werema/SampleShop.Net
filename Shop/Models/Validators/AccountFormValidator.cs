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
    public class AccountFormValidator : AbstractValidator<AccountForm>
    {
        private readonly LocalDbContext db;

        public AccountFormValidator(LocalDbContext db)
        {
            this.db = db;
        }
        public AccountFormValidator()
        {
            RuleFor(x => x.User.FristName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(30);
            RuleFor(x => x.User.LastName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(30);
            RuleFor(x => x.User.PhoneNumber)
               .NotNull()
               .Length(5);
            RuleFor(x => x.Address.City)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.Address.ZipCode)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.Address.Street)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.Address.HouseNumber)
                .NotEmpty()
                .NotNull();
            
        }
    }
}
