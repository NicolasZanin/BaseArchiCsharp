using BaseArchiCsharp.Application.Common.Exception;
using BaseArchiCsharp.Application.Interface.InterfaceService;
using BaseArchiCsharp.Domain.Entities;
using BaseArchiCsharp.Domain.Dto;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BaseArchiCsharp.WebAPI.Controllers;

// <summary>
// Controller for user management
// </summary>
[ApiController]
[Route("api/[controller]")]
public class UserController(IUserRegister userRegister, IUserFinder userFinder) : ControllerBase
{
    // <summary>
    // Register a new user depending on username, password and email
    // </summary>
    // <param name="userDto">User information</param>
    // <returns>Return the user created</returns>
    // <exception cref="AlreadyCreateException">If the user already exists</exception>
    // <exception cref="BadRequest">If the user informations are not valid</exception>
    // <exception cref="ObjectResult">If an error with database occurs</exception>
    [HttpPost]
    [ProducesResponseType(typeof(UserDto), 201)]
    /*[ProducesResponseType(typeof(BadRequest), 400)]
    [ProducesResponseType(typeof(AlreadyCreateException), 409)]
    [ProducesResponseType(typeof(ObjectResult), 500)]*/
    public async Task<IActionResult> RegisterUser([FromBody] UserDto userDto)
    {
        User userCreate = await userRegister.RegisterUser(userDto.Username, userDto.Password, userDto.Email);
        return Created($"/api/user/{userCreate.Id}", ConvertToDto(userCreate));
    }
    
    // <summary>
    // Modify a user depending on id, username, password and email
    // </summary>
    // <param name="id">User id</param>
    // <param name="userDto">User information</param>
    // <returns>Return the user modified</returns>
    // <exception cref="NotFoundException">If the user doesn't exist</exception>
    // <exception cref="BadRequest">If the user informations are not valid</exception>
    // <exception cref="ObjectResult">If an error with database occurs</exception>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(UserDto), 201)]
    /*[ProducesResponseType(typeof(BadRequest), 400)]
    [ProducesResponseType(typeof(NotFound), 404)]
    [ProducesResponseType(typeof(ObjectResult), 500)]*/
    public async Task<IActionResult> ModifyUser(int id, [FromBody] UserDto userDto)
    {
        User userModify = await userRegister.ModifyUser(id, userDto.Username, userDto.Password, userDto.Email);
        return Created($"/api/user/{userModify.Id}", ConvertToDto(userModify));
    }
    
    // <summary>
    // Delete a user depending on id
    // </summary>
    // <param name="id">User id</param>
    // <returns>Return nothing</returns>
    // <exception cref="NotFoundException">If the user doesn't exist</exception>
    // <exception cref="ObjectResult">If an error with database occurs</exception>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(void), 204)]
    /*[ProducesResponseType(typeof(NotFound), 404)]
    [ProducesResponseType(typeof(ObjectResult), 500)]*/
    public async Task<IActionResult> DeleteUser(int id)
    {
        await userRegister.DeleteUser(id);
        return NoContent();
    }
    
    // <summary>
    // Get a user depending on his id
    // </summary>
    // <param name="id">User id</param>
    // <returns>Return the user</returns>
    // <exception cref="NotFoundException">If the user doesn't exist</exception>
    // <exception cref="ObjectResult">If an error with database occurs</exception>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(void), 200)]
    /*[ProducesResponseType(typeof(NotFound), 404)]
    [ProducesResponseType(typeof(ObjectResult), 500)]*/
    public async Task<IActionResult> GetUserById(int id)
    {
        User user = await userFinder.GetUserById(id);
        return Ok(ConvertToDto(user));
    }
    
    // <summary>
    // Get all users
    // </summary>
    // <returns>Return all users</returns>
    // <exception cref="ObjectResult">If an error with database occurs</exception>
    [HttpGet]
    [ProducesResponseType(typeof(void), 200)]
    // [ProducesResponseType(typeof(ObjectResult), 500)]
    public async Task<IActionResult> GetAllUsers()
    {
        List<User> users = await userFinder.GetAllUsers();
        return Ok(users.Select(ConvertToDto));
    }

    private static UserDto ConvertToDto(User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            Password = user.Password,
            Username = user.Username
        };
    }
}