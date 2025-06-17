using ErpSystem.Application.Common.Constants;
using FluentValidation;

namespace ErpSystem.Application.Customers.Commands.AddCustomer;

public class AddCustomerCommandValidator : AbstractValidator<AddCustomerCommand>
{
    public AddCustomerCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(CustomerErrorKeys.NameRequired)
            .MaximumLength(100)
            .WithMessage(CustomerErrorKeys.NameTooLong);
        RuleFor(x => x.Address)
            .NotEmpty()
            .WithMessage(CustomerErrorKeys.AddressRequired)
            .MaximumLength(200)
            .WithMessage(CustomerErrorKeys.AddressTooLong);
        RuleFor(x => x.Phone)
            .NotEmpty()
            .WithMessage(CustomerErrorKeys.PhoneRequired)
            .MaximumLength(15)
            .WithMessage(CustomerErrorKeys.PhoneTooLong);
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(CustomerErrorKeys.EmailRequired)
            .EmailAddress()
            .WithMessage(CustomerErrorKeys.EmailInvalid);
        RuleFor(x => x.ContactName)
            .NotEmpty()
            .WithMessage(CustomerErrorKeys.ContactNameRequired)
            .MaximumLength(100)
            .WithMessage(CustomerErrorKeys.ContactNameTooLong);
    }
}
