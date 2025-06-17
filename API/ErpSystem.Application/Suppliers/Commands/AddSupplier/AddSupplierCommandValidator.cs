using ErpSystem.Application.Common.Constants;
using FluentValidation;

namespace ErpSystem.Application.Suppliers.Commands.AddSupplier;

public class AddSupplierCommandValidator : AbstractValidator<AddSupplierCommand>
{
    public AddSupplierCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(SupplierErrorKeys.NameRequired)
            .MaximumLength(100)
            .WithMessage(SupplierErrorKeys.NameTooLong);
        RuleFor(x => x.Address)
            .NotEmpty()
            .WithMessage(SupplierErrorKeys.AddressRequired)
            .MaximumLength(200)
            .WithMessage(SupplierErrorKeys.AddressTooLong);
        RuleFor(x => x.Phone)
            .NotEmpty()
            .WithMessage(SupplierErrorKeys.PhoneRequired)
            .MaximumLength(15)
            .WithMessage(SupplierErrorKeys.PhoneTooLong);
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(SupplierErrorKeys.EmailRequired)
            .EmailAddress()
            .WithMessage(SupplierErrorKeys.EmailInvalid);
        RuleFor(x => x.ContactPerson)
            .NotEmpty()
            .WithMessage(SupplierErrorKeys.ContactPersonRequired)
            .MaximumLength(100)
            .WithMessage(SupplierErrorKeys.ContactPersonTooLong);
    }
}
