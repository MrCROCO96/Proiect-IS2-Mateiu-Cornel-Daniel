using Microsoft.AspNetCore.Mvc;
using GameTicketing.DataTransferObjects;
using GameTicketing.Services.Abstractions;

namespace GameTicketing.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddUser([FromBody] UsersAddRecord user) // Atributul aici indica faptul ca parametrul este extras din corpul mesajul care este de tip JSON
    {
        await userService.AddUser(user);
        
        
        return NoContent();
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromBody] UsersUpdateRecord user)
    {
        await userService.UpdateUser(user);
        
        return NoContent();
    }
    
    [HttpGet("{userId:int}")]
    public async Task<ActionResult<UsersRecord>> GetUser([FromRoute] int userId) // Atributul aici indica faptul ca parametrul este extras din ruta cererii
    {
        // Raspunsul va fi un raspuns continand datele cerute cu status code 200 Ok
        return Ok(await userService.GetUser(userId));
    }
    
    [HttpGet]
    public async Task<ActionResult<UsersRecord>> GetUsers()
    {
        return Ok(await userService.GetUsers());
    }
    
    [HttpDelete("{userId:int}")]
    public async Task<ActionResult<UsersRecord>> DeleteUser([FromRoute] int userId)
    {
        await userService.DeleteUser(userId);
        
        return NoContent();
    }
}