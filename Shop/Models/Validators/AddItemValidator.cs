using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Shop.Models.Forms;
namespace Shop.Models.Validators
{
    public class AddItemValidator : AbstractValidator<ItemForm>
    {
        public AddItemValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Ammount).GreaterThan(0);
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Price).GreaterThan(-1);
            RuleFor(x => x.Images).NotEmpty();
            RuleFor(x => x.CategoryId).NotEmpty();
            RuleFor(x => x.Miniature)
                .GreaterThan(-1)
                .LessThan(x => x.Images.Count);
            
        }
    }
}
