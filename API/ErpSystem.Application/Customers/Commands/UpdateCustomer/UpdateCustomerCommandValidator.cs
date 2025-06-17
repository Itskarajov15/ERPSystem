using ErpSystem.Application.Common.Constants;
using FluentValidation;

namespace ErpSystem.Application.Customers.Commands.UpdateCustomer;

public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator()
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
            .EmailAddress()
            .WithMessage(CustomerErrorKeys.EmailInvalid)
            .MaximumLength(100)
            .WithMessage(CustomerErrorKeys.EmailRequired);
        RuleFor(x => x.ContactName)
            .NotEmpty()
            .WithMessage(CustomerErrorKeys.ContactNameRequired)
            .MaximumLength(100)
            .WithMessage(CustomerErrorKeys.ContactNameTooLong);
    }
}
