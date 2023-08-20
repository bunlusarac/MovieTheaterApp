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

        try
        {
            var cmd = new ValidateOtpCommand(dto.PrimaryOtp, dto.SecondaryOtp, dto.UserId);
            return Ok(await _mediator.Send(cmd));
        }
        catch (Exception e)
        {
            return Problem("Given OTP(s) are incorrect.",
                "/otp/validate",
                (int)OtpStatusCode.InvalidOtp, "Invalid OTP(s)");
        }
    }
    
    [HttpPost]
    [Route("request")]
    public async Task<IActionResult> RequestOtp([FromBody] RequestOtpDto dto)
    {
        var result = (Result)(await _mediator.Send(new IssueOtpCommand(dto.UserId, dto.PrimaryOtpClaim, dto.EmailAddress, dto.PhoneNumber)) ?? Result.Error);

        if (result.ResultCode == ResultCode.Error)
            return Problem("An error occured during processing the OTP request.",
                "/otp/request",
                (int)OtpStatusCode.OtpRequestFailed, "OTP Request Failed");
        
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
                (int)OtpStatusCode.RegistrationFailed, "Registration Failed");

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
                (int)OtpStatusCode.MfaStatusUpdateFailed, "MFA Status Update Failed");

        return Ok();
    }
}