using ErpSystem.Application.Authentication.Commands.AssignRole;
using ErpSystem.Application.Authentication.Commands.CreateRole;
using ErpSystem.Application.Authentication.Commands.DeleteRole;
using ErpSystem.Application.Authentication.Commands.EditRole;
using ErpSystem.Application.Authentication.Commands.Login;
using ErpSystem.Application.Authentication.Commands.Register;
using ErpSystem.Application.Authentication.Commands.RemoveRole;
using ErpSystem.Application.Authentication.DTOs;
using ErpSystem.Application.Authentication.Queries.GetEndpoints;
using ErpSystem.Application.Authentication.Queries.GetRoleById;
using ErpSystem.Application.Authentication.Queries.GetRoles;
using ErpSystem.Application.Authentication.Queries.GetUsersWithRoles;
using ErpSystem.Domain.Common.Filters;
using ErpSystem.Domain.Common.Pagination;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ErpSystem.API.Controllers;

[Route("api/authentication")]
public class AuthenticationController : BaseController
{
    public AuthenticationController(IMediator mediator)
        : base(mediator) { }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginCommand command)
    {
        var result = await _mediator.Send(command);

        return Ok(result);
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterCommand command)
    {
        await _mediator.Send(command);

        return NoContent();
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetUsers(
        [FromQuery] PaginationParams paginationParams,
        [FromQuery] string? searchTerm = null
    )
    {
        var userFilters = string.IsNullOrEmpty(searchTerm)
            ? null
            : new UserFilters { SearchTerm = searchTerm };
        var query = new GetUsersWithRolesQuery(paginationParams, userFilters);
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpGet("roles")]
    public async Task<IActionResult> GetRoles(
        [FromQuery] PaginationParams paginationParams,
        [FromQuery] string? searchTerm = null
    )
    {
        var roleFilters = string.IsNullOrEmpty(searchTerm)
            ? null
            : new RoleFilters { SearchTerm = searchTerm };
        var query = new GetRolesQuery(paginationParams, roleFilters);
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpGet("roles/{id}")]
    public async Task<IActionResult> GetRoleById(Guid id)
    {
        var query = new GetRoleByIdQuery(id);
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpGet("endpoints")]
    public async Task<IActionResult> GetEndpoints()
    {
        var query = new GetEndpointsQuery();
        var result = await _mediator.Send(query);

        return Ok(result);
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
