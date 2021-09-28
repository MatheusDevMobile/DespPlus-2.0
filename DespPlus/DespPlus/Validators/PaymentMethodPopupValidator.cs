using DespPlus.Models;
using FluentValidation;

namespace DespPlus.Validators
{
    public class PaymentMethodPopupValidator : AbstractValidator<PaymentMethod>
    {
        public PaymentMethodPopupValidator()
        {
            RuleFor(category => category.Name).NotEmpty()
                                              .WithMessage("O método de pagamento precisa ter um nome");
        }
    }
}
