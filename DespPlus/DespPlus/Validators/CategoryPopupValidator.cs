using DespPlus.Models;
using FluentValidation;

namespace DespPlus.Validators
{
    public class CategoryPopupValidator : AbstractValidator<Category>
    {
        public CategoryPopupValidator()
        {
            RuleFor(category => category.Name).NotEmpty()
                                              .WithMessage("A categoria precisa ter um nome");
        }
    }
}
