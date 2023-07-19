namespace OTPService.Application.Common;

public class Result
{
    public ResultCode ResultCode;

    public Result(ResultCode resultCode)
    {
        ResultCode = resultCode;
    }

    public static Result Ok = new(ResultCode.Ok);
    public static Result Error = new(ResultCode.Error);
}