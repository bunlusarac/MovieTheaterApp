using MediatR;
using Microsoft.AspNetCore.Mvc;
using OTPService.API.DTOs;
using OTPService.API.Utils;
using OTPService.Application.Commands;
using OTPService.Application.Common;

namespace OTPService.API.Controllers;

[ApiController]
[Route("otp")]
public class OtpController: ControllerBase
{
    private readonly IMediator _mediator;

    public OtpController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("validate")]
    public async Task<IActionResult> ValidateOtp([FromBody] ValidateOtpDto dto)
    {
        /*
        var result = (Result) (await _mediator.Send(new ValidateOtpCommand(
            dto.PrimaryOtp, dto.SecondaryOtp, dto.UserId)) ?? Result.Error);
        */
        var bearer = HttpContext.Request.Headers.Authorization.ToString();
        
        try
        {
            var cmd = new ValidateOtpCommand(dto.PrimaryOtp, dto.SecondaryOtp, bearer);
            return Ok(await _mediator.Send(cmd));
        }
        catch (Exception e)
        {
            return Problem("Given OTP(s) are incorrect.",
                "/otp/validate",
                StatusCodes.Status401Unauthorized, "Invalid OTP(s)");
        }
    }
    
    [HttpPost]
    [Route("request")]
    public async Task<IActionResult> RequestOtp()
    {
        var bearer = HttpContext.Request.Headers.Authorization.ToString();
        
        var result = (Result)(await _mediator.Send(new IssueOtpCommand(bearer)) ?? Result.Error);

        if (result.ResultCode == ResultCode.Error)
            return Problem("An error occured during processing the OTP request.",
                "/otp/request",
                StatusCodes.Status500InternalServerError, "OTP Request Failed");
        
        return Ok();
    }
    
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> RegisterOtpUser([FromBody] RegisterOtpUserDto dto)
    {
        var result = await _mediator.Send(new RegisterOtpUserCommand(dto.UserId));

        if (result == null || result.ResultCode == ResultCode.Error)
            return Problem("User already exists or an error occured during saving new user.",
                "/otp/register",
                StatusCodes.Status409Conflict, "Registration Failed");

        return Ok();
    }
    
    [HttpPost]
    [Route("set-mfa")]
    public async Task<IActionResult> SwitchMfaStatus([FromBody] UpdateOtpUserMfaStatusDto dto)
    {
        var result = (Result)(await _mediator.Send(new UpdateOtpUserMfaStatusCommand(dto.UserId, dto.MfaEnabled)) ?? Result.Error);
            
        if (result.ResultCode == ResultCode.Error)
            return Problem("User not found or an error occured during saving new MFA state.",
                "/otp/register",
                StatusCodes.Status404NotFound, "MFA Status Update Failed");

        return Ok();
    }
}