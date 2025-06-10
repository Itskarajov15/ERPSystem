using MediatR;

namespace ErpSystem.Application.Authentication.Commands.Register;

public record RegisterCommand(
    string UserName,
    string Email,
    string Password,
    string FirstName,
    string LastName,
    string RoleName
) : IRequest;
