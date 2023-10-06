using API.Dtos;
using API.Services;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class UserController : BaseApiController
{
    private readonly ILogger<UserController> _Logger;
    private readonly IAuthService _Auth;
    private readonly IUserService _userService;
    private readonly IUnitOfWork unitofwork;
    private readonly  IMapper mapper;
        private readonly IPasswordHasher<User> _passwordHasher;


    public UserController(IUserService userService, IUnitOfWork unitofwork, IMapper mapper, ILogger<UserController> logger, IAuthService auth,IPasswordHasher<User> passwordHasher)
    {
        
        this.unitofwork = unitofwork;
        this.mapper = mapper;
        _userService = userService;
        _Logger = logger;
        _Auth = auth;
        _passwordHasher = passwordHasher;
    }

    [HttpPost("QR")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]    
    public async Task<ActionResult> QR([FromBody] LoginDto data)
    {        
        try
        {
            User u = await unitofwork.Users.GetByUsernameAsync(data.Usuario);
            var result = _passwordHasher.VerifyHashedPassword(u, u.Password, data.Password);
            if (result != PasswordVerificationResult.Success)
            {
                return Unauthorized("La contraseña proporcionada no es válida.");
            }
            await _Auth.SendQRCodeToEmail(u,data); 
            return Ok("Código QR enviado por correo electrónico.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }  
    }


    [HttpPost("Verify")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]    
    public async Task<ActionResult> Verify([FromBody] VerifyDto data){ 
        try{

            User u = await unitofwork.Users.GetByUsernameAsync(data.Usuario);
            if(u.TwoFactorSecret == null){
                throw new ArgumentNullException(u.TwoFactorSecret);
            }
            var isVerified = _Auth.VerifyCode(u.TwoFactorSecret, data.Code);            
            if(isVerified == true){
                return Ok("authenticated!!");
            }
            return Unauthorized();
        }
        catch (Exception ex){
            _Logger.LogError(ex.Message);
            return BadRequest("some wrong");
        }  
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<UserDto>>> Get()
    {
        var entidad = await unitofwork.Users.GetAllAsync();
        return mapper.Map<List<UserDto>>(entidad);
    }



    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<UserDto>> Get(int id)
    {
        var entidad = await unitofwork.Users.GetByIdAsync(id);
        if (entidad == null){
            return NotFound();
        }
        return this.mapper.Map<UserDto>(entidad);
    }

    [HttpPost("register")]
    public async Task<ActionResult> RegisterAsync(RegisterDto model)
    {
        var result = await _userService.RegisterAsync(model);
        return Ok(result);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<UserDto>> Put(int id, [FromBody]UserDto entidadDto){
        if(entidadDto == null)
        {
            return NotFound();
        }
        var entidad = this.mapper.Map<User>(entidadDto);
        unitofwork.Users.Update(entidad);
        await unitofwork.SaveAsync();
        return entidadDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var entidad = await unitofwork.Users.GetByIdAsync(id);
        if(entidad == null)
        {
            return NotFound();
        }
        unitofwork.Users.Remove(entidad);
        await unitofwork.SaveAsync();
        return NoContent();
    }
}
