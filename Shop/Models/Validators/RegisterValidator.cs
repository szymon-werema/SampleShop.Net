using FluentValidation;
using Shop.Entities;

using Shop.Models.Forms;
namespace Shop.Models.Validators
{
    public class RegisterValidator : AbstractValidator<UserRegisterForm>
    {
        public RegisterValidator( LocalDbContext db)
        {
           
            RuleFor(x => x.Email)
                 .EmailAddress();
            //RuleFor(x => x.Email)
            //    .Custom((value, message) =>
            //    {
            //        if(db.User.Any( u => u.Email == value))
            //        {
            //            message.AddFailure("Email addres is already taken");
            //        }
            //    });
            RuleFor(x => x.Password)
                .MinimumLength(6)
                .MaximumLength(20);
            RuleFor(x => x.ComfirmPassword)
                .Equal(p => p.Password);
            RuleFor(x => x.FristName )
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(30);
            RuleFor(x => x.LastName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(30);

        }

       
    }
}
