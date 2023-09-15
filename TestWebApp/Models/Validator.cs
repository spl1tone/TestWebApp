using FluentValidation;
namespace TestWebApp.Models;

public class Validator : AbstractValidator<Product>
{
    public Validator ()
    {
        RuleFor(product => product.Name)
            .NotEmpty()
            .WithMessage("Product name is required")
            .Length(2, 50)
            .WithMessage("Product name must be > 2 and <50 characters");

        RuleFor(product => product.Price)
            .GreaterThan(0)
            .WithMessage("Price must be > than 0");

        RuleFor(product => product.Count)
            .GreaterThan(0)
            .WithMessage("Count must be > than 0");
    }
}
