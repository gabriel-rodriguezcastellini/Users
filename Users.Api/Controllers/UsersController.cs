using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Users.Api.Features.Commands;
using Users.Api.Features.Queries;
using Users.Api.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Users.Api.Controllers;

public class UsersController : BaseApiController
{
    private readonly IMediator mediator;
    private readonly IMapper mapper;
    private readonly ILogger<UsersController> logger;

    public UsersController(IMediator mediator, IMapper mapper, ILogger<UsersController> logger)
    {
        this.mediator = mediator;
        this.mapper = mapper;
        this.logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(QueryResultDto<UserDto>), 200)]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersAsync([FromQuery] QueryDto query)
    {
        logger.LogInformation("Parameter values: {@query}", query);        
        return Ok(await mediator.Send(new GetUsersQuery(query.Page, query.ItemsPerPage)));
    }

    [HttpGet("{id}")]
    [ActionName(nameof(GetUserAsync))]
    [ResponseCache(Duration = 60)]
    [ProducesResponseType(typeof(UserDto), 200)]
    [ProducesResponseType(typeof(NotFoundObjectResult), 404)]
    public async Task<ActionResult<UserDto>> GetUserAsync([Range(1, int.MaxValue)] int id)
    {
        logger.LogInformation("Getting user {Id} at {RunTime}", id, DateTime.Now);
        return Ok(await mediator.Send(new GetUserQuery(id)));
    }

    /// <summary>
    /// Creates a User.
    /// </summary>
    /// <param name="userDto"></param>
    /// <returns>A newly created User</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /Users
    ///     {
    ///        "name": "Gabriel",
    ///        "email": "gabriel.rodriguezcastellini@outlook.com",
    ///        "address": "Alberdi 1555",
    ///        "phone": "+5493413615916",
    ///        "type": 2,
    ///        "money": 100
    ///     }
    ///
    /// </remarks>
    /// <response code="201">Returns the newly created user</response>
    /// <response code="400">If the user is null</response>
    [HttpPost]
    [ProducesResponseType(typeof(UserDto), 201)]
    [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
    public async Task<ActionResult<UserDto>> PostAsync([FromBody] UserDto userDto)
    {
        logger.LogInformation("Parameter values: {@userDto}", userDto);
        var result = await mediator.Send(new AddUserCommand(userDto.Name, userDto.Email, userDto.Address, userDto.Phone, userDto.Type, userDto.Money));
        return CreatedAtAction(nameof(GetUserAsync), new { id = result.Id }, result);
    }

    /// <summary>
    /// Deletes a specific User.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(NotFoundObjectResult), 404)]
    public async Task<IActionResult> DeleteAsync([Range(1, int.MaxValue)] int id)
    {
        logger.LogInformation("Parameter values: {id}", id);
        await mediator.Send(new DeleteUserCommand(id));
        return NoContent();
    }
}
