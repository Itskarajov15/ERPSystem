using ErpSystem.Application.Authentication.DTOs;
using MediatR;

namespace ErpSystem.Application.Authentication.Commands.Login;

public record LoginCommand(string UserName, string Password) : IRequest<LoginResponseDto>;
