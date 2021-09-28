using DespPlus.Models;
using FluentValidation;

namespace DespPlus.Validators
{
    public class RegisterValidator : AbstractValidator<CashFlow>
    {
        public RegisterValidator()
        {
            RuleFor(cashflow => cashflow.CategoryDescription).NotEmpty()
                                                             .WithMessage("O Campo 'Categoria' é obrigatório.");

            RuleFor(cashflow => cashflow.PaymentMethodDescription).NotEmpty()
                                                                  .WithMessage("O Campo 'Método de Pagamento' é obrigatório.");
            RuleFor(cashflow => cashflow.Value).LessThan(1)
                                               .WithMessage("O Campo 'Valor' é obrigatório.");
        }
    }
}
