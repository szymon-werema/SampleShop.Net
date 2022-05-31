using FluentValidation;
using Shop.Entities;
using Shop.Models.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Models.Validators
{
    public class CategoryValidator : AbstractValidator<CategoryForm>
    {
        public CategoryValidator(LocalDbContext db)
        {
            RuleFor(x => x.Name)
               .Custom((value, message) =>
               {
                   if (db.Categories.Any(c => c.Name == value))
                   {
                       message.AddFailure("Category exist");
                   }
               });
        }
    }
}
