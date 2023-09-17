using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FcxLabsUserManagement.Application.Common.Models;
using FcxLabsUserManagement.Application.Extensions.Conversions;
using FcxLabsUserManagement.Application.User.Commands;
using FcxLabsUserManagement.Core;
using FcxLabsUserManagement.Core.Contracts.Services;
using FcxLabsUserManagement.Infra.Configs.Options;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FcxLabsUserManagement.Application.User.Handlers;

public class LoginUserHandler : IRequestHandler<LoginUserCommand, ObjectResult>
{
    private readonly UserManager<UserIdentity> _userManager;
    private readonly JwtOptions _jwtConfig;

	public LoginUserHandler(IOptions<JwtOptions> jwtConfig, UserManager<UserIdentity> userManager)
	{
        _userManager = userManager;
        _jwtConfig = jwtConfig.Value;
	}
	public async Task<ObjectResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
	{
		var user = await _userManager.FindByNameAsync(request.Login);
		var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
		
		if(user is not null && isPasswordValid)
		{
			if(user.Status == Core.Enums.Status.BLOCKED || user.Status == Core.Enums.Status.INACTIVATED)
			{
				return new ObjectResult(new Response{ Status="Error", Message="Usuário inativo ou bloqueado."}){ 
					StatusCode = StatusCodes.Status401Unauthorized
				};
			}
			
			var authClaims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.UserName),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
			};
			
			var userRoles = (await _userManager.GetRolesAsync(user)).ToList();
			userRoles.ForEach(ur => authClaims.Add(new Claim(ClaimTypes.Role, ur)));
			
			var jwtToken = GetJwtToken(authClaims);
			var userLoggedVM = user.ToUserLoggedVM(jwtToken);
			
			return new ObjectResult(new Response { Status = "Success", Message = "Logado com sucesso.", Content = userLoggedVM });
		}
		
		return new ObjectResult(new Response{ Status="Error", Message="Credenciais incorretas."}){ 
			StatusCode = StatusCodes.Status401Unauthorized
		};
	}
	
	private string GetJwtToken(List<Claim> authClaims)
	{
		var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Secret));
		var token = new JwtSecurityToken(
			issuer: _jwtConfig.ValidIssuer,
			audience: _jwtConfig.ValidAudience,
			expires: DateTime.Now.AddHours(2),
			claims: authClaims,
			signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
		);
		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}
