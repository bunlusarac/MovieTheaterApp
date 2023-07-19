namespace OTPService.Application.Common;

public interface ICommandHandlerAsync<TCommand> where TCommand : ICommand
{
    Task<Result> Handle(TCommand command);
}