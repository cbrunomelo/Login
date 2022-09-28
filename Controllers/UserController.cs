using Data;
using Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using SecureIdentity.Password;
using Services;
using ViewModel;

namespace Controllers;

[ApiController]
public class UserController : ControllerBase
{   
    
    [Authorize]
    [HttpGet("v1/users-names")]
    
    public async Task<IActionResult> GetUsersNames(
        [FromServices] DataContext context)
        {   
            
             var UsersNames = await context
             .Users
             .AsNoTracking()
             .Select(x => 
             
                new {
                        x.Name,
                    }
             )
             .ToListAsync();
             return Ok(new ResultViewModel<object>(UsersNames, null));
            
        }


    [HttpPost("v1/users")]

    public async Task<IActionResult> UserRegister(
        [FromServices] DataContext context,
        [FromBody] UserRegisterViewlModel model,
        [FromServices]EmailService emailService
        )
        {   
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<User>(ModelState.GetErrors()));

            var Password = PasswordGenerator.Generate(10);
            var user = new User{
                Name = model.Name,
                Email = model.Email,
                
                PasswordHash = PasswordHasher.Hash(Password)
                };

            try {
            await context.AddAsync(user);
            await context.SaveChangesAsync();
            emailService.Send(user.Name, user.Email, $"Bem Vindo {user.Name}!", $"A Sua senha é {Password}");
            return Created($"v1/users/{user.Name}",new ResultViewModel<User>(user));

            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<User>("Não foi possível incluir a categoria"));
            }
        }

    
     [HttpPost("v1/users/login")]

     public IActionResult LoginAsync(
        [FromBody]UserLoginViewModel model,
        [FromServices]DataContext dataContext,
        [FromServices]TokenService TokenService

     )   
     {
        if(!ModelState.IsValid)
            return BadRequest(new ResultViewModel<String>(ModelState.GetErrors()));


        var user = dataContext.Users.FirstOrDefault(x => x.Email == model.Email);

        if (user is null)
            return StatusCode(401, new ResultViewModel<string>("Usuário ou senha inválidos"));

        if(!PasswordHasher.Verify(user.PasswordHash, model.Password))
            return StatusCode(401, new ResultViewModel<string>("Usuário ou senha inválidos"));

        var token = TokenService.GenerateToken(user);

     return Ok(new ResultViewModel<string>(token, null));
     }


}   