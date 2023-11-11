using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using RL.Backend.Commands;
using RL.Backend.Models;
using RL.Data;
using RL.Data.DataModels;

namespace RL.Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly RLContext _context;
    private readonly IMediator _mediator;

    public UsersController(ILogger<UsersController> logger, RLContext context, IMediator mediator)
    {
        _logger = logger;
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    [EnableQuery]
    public IEnumerable<User> Get()
    {
        return _context.Users;
    }
    [HttpPost("AddUsersToProcedure")]
    [EnableQuery]
    public async Task<IActionResult> AddUserMappingToProcedure(AddUsersToProcedureCommand command, CancellationToken token)
    {
        var response = await _mediator.Send(command, token);

        return response.ToActionResult();
    }
    [HttpPost("RemoveUsersFromProcedure")]
    [EnableQuery]
    public async Task<IActionResult> RemoveUserFromProcedure(RemoveUsersFromProcedureCommand command, CancellationToken token)
    {
        var response = await _mediator.Send(command, token);

        return response.ToActionResult();
    }
    [HttpGet("GetUsersMappingList")]
    [EnableQuery]
    public List<UserMapping> GetUsersMappingList(int planId)
    {
        return _context.UserMapping.Where(p => p.PlanId==planId).ToList();
    }
}
