using DespPlus.Models;
using FluentValidation;

namespace DespPlus.Validators
{
    public class RegisterValidator : AbstractValidator<CashFlow>
    {
        public RegisterValidator()
        {
            RuleFor(cashflow => cashflow.Value).InclusiveBetween(0.01, 9999999)
                                               .WithMessage("O Campo 'Valor' é obrigatório.");

            RuleFor(cashflow => cashflow.CategoryId).NotEmpty()
                                                  .WithMessage("O Campo 'Categoria' é obrigatório.");

            RuleFor(cashflow => cashflow.PaymentMethodId).NotEmpty()
                                                       .WithMessage("O Campo 'Método de Pagamento' é obrigatório.");
        }
    }
}
