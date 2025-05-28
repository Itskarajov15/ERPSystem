using ErpSystem.API.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ErpSystem.API.Controllers;

[SecureRoute]
[ApiController]
public class BaseController : ControllerBase
{
    protected readonly IMediator _mediator;

    public BaseController(IMediator mediator)
    {
        _mediator = mediator;
    }
}
