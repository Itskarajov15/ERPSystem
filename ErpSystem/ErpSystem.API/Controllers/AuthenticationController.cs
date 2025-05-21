using ErpSystem.Application.Authentication.Commands.AssignRole;
using ErpSystem.Application.Authentication.Commands.CreateRole;
using ErpSystem.Application.Authentication.Commands.DeleteRole;
using ErpSystem.Application.Authentication.Commands.EditRole;
using ErpSystem.Application.Authentication.Commands.Login;
using ErpSystem.Application.Authentication.Commands.Register;
using ErpSystem.Application.Authentication.Commands.RemoveRole;
using ErpSystem.Application.Authentication.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ErpSystem.API.Controllers;

[Route("api/authentication")]
public class AuthenticationController : BaseController
{
    public AuthenticationController(IMediator mediator)
        : base(mediator) { }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginCommand command)
    {
        var result = await _mediator.Send(command);

        return Ok(result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterCommand command)
    {
        await _mediator.Send(command);

        return NoContent();
    }

    [HttpPost("roles")]
    public async Task<IActionResult> CreateRole(CreateRoleCommand command)
    {
        var roleId = await _mediator.Send(command);

        return Ok(roleId);
    }

    [HttpPut("roles/{roleId}")]
    public async Task<IActionResult> EditRole(Guid roleId, EditRoleDto dto)
    {
        var command = new EditRoleCommand(roleId, dto);
        await _mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("roles/{id}")]
    public async Task<IActionResult> DeleteRole(Guid id)
    {
        await _mediator.Send(new DeleteRoleCommand(id));

        return NoContent();
    }

    [HttpPost("users/{userId}/roles")]
    public async Task<IActionResult> AssignRole(Guid userId, [FromBody] string roleName)
    {
        await _mediator.Send(new AssignRoleCommand(userId, roleName));

        return NoContent();
    }

    [HttpDelete("users/{userId}/roles/{roleName}")]
    public async Task<IActionResult> RemoveRole(Guid userId, string roleName)
    {
        await _mediator.Send(new RemoveRoleCommand(userId, roleName));

        return NoContent();
    }
}
