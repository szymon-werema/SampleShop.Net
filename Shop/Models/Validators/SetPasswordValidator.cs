using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Shop.Models.Forms;
namespace Shop.Models.Validators
{
    public class SetPasswordValidator : AbstractValidator<SetPasswordForm>
    {
        public SetPasswordValidator()
        {
            RuleFor(x => x.Password)
                .MinimumLength(6)
                .MaximumLength(20);
            RuleFor(x => x.ComfirmPassword)
                .Equal(p => p.Password);

        }
    }
}
