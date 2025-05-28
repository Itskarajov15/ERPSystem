using FluentValidation;

namespace ErpSystem.Application.Customers.Commands.AddCustomer;

public class AddCustomerCommandValidator : AbstractValidator<AddCustomerCommand>
{
    public AddCustomerCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(100)
            .WithMessage("Name must not exceed 100 characters.");
        RuleFor(x => x.Address)
            .NotEmpty()
            .WithMessage("Address is required.")
            .MaximumLength(200)
            .WithMessage("Address must not exceed 200 characters.");
        RuleFor(x => x.Phone)
            .NotEmpty()
            .WithMessage("Phone is required.")
            .MaximumLength(15)
            .WithMessage("Phone must not exceed 15 characters.");
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .MaximumLength(100)
            .WithMessage("Invalid email format.");
        RuleFor(x => x.ContactName)
            .NotEmpty()
            .WithMessage("Contact Name is required.")
            .MaximumLength(100)
            .WithMessage("Contact Name must not exceed 100 characters.");
    }
}
